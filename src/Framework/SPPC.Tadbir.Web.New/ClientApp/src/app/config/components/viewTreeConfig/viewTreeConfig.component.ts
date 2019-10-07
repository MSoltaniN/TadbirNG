import { Component, OnInit, Renderer2, Output, EventEmitter, Input } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { RowClassArgs } from '@progress/kendo-angular-grid';
import { String, DefaultComponent } from '@sppc/shared/class';
import { Layout, Entities, MessageType } from '@sppc/env/environment';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { ViewTreeLevelConfig, ViewTreeConfig } from '@sppc/config/models';
import { SettingService } from '@sppc/config/service';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';



export interface Item {
  key: number,
  value: string
}

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'view-tree-config',
  templateUrl: './viewTreeConfig.component.html',
  styles: [`
.input-frm{ width:100%; } #level-list{ list-style-type: none; padding: 0; width: 150px;}
#level-list > li {
    border: solid 1px #337ab7;
    border-bottom: none;
    background-color: #e2e8ec;
    text-align: center;
    padding: 7px 0;
    -webkit-user-select: none; /* Safari */        
    -moz-user-select: none; /* Firefox */
    -ms-user-select: none; /* IE10+/Edge */
    user-select: none; /* Standard */
}
#level-list > li:last-child{ border-bottom: solid 1px #337ab7; }
#level-list > li.enable{ cursor: pointer; } #level-list > li.enable:hover{ background-color: #8ab8e0;}
#level-list > li.disable{ background-color: #fdfdfd; } #level-list > li.selected{ background-color: #337ab7; }
/deep/.k-grid tr.notEnabled { color: #cac4c4; }

.btn-setting {
    position: absolute;
    bottom: 15px;
    text-align: justify;
}
.btn-setting-rtl {left: 28px;}
.btn-setting-ltr {right: 30px;}
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ViewTreeConfigComponent extends DefaultComponent implements OnInit {

  //@ViewChild(GridComponent) private grid: GridComponent;
  //private editedRowIndex: number;
  //private docClickSubscription: any;

  public formGroup: FormGroup;

  ddlEntites: Array<Item>;
  entityArray: Array<Item>;
  viewTreeLevels: Array<ViewTreeLevelConfig> = [];

  ddlEntitySelected: number = 0;
  maxDepthValue: number;
  maxDepth: number = 8;
  minDepth: number = 1;

  viewTreeConfig: ViewTreeConfig;
  viewTreeDefaultConfig: ViewTreeConfig;
  finalViewTreeConfig: Array<ViewTreeConfig> = [];

  public errorMessage = String.Empty;

  @Input() public set reloadComponent(isReload: boolean) {
    if (isReload) {
      this.maxDepthValue = undefined;
      this.ddlEntitySelected = undefined;
      this.viewTreeConfig = undefined;
      this.viewTreeLevels = [];
      this.finalViewTreeConfig = [];
    }
  }

  @Output() viewTreeValue: EventEmitter<Array<any>> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, private formBuilder: FormBuilder,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.Setting, undefined);

  }

  public ngOnInit(): void {
    this.loadEntity();
  }


  /**
   * عملیات مربوط به وقتی که بر روی سلولی کلیک میشود
   * @param param0
   */
  public cellClickHandler({ sender, rowIndex, column, columnIndex, dataItem, isEdited }) {
    //debugger;
    if (!isEdited && !this.isReadOnly(column.field) && dataItem.isEnabled) {
      if ((this.viewTreeConfig.viewId == ViewName.Account && dataItem.no <= 3 && column.field == 'name') || (column.field == 'codeLength' && dataItem.isUsed))
        return
      sender.editCell(rowIndex, columnIndex, this.createFormGroup(dataItem.no));
    }
  }


  /**
   * عملیات بستن فرم در سطرها و ذخیره تغییرات داده شده در حافظه موقت
   * @param args
   */
  public cellCloseHandler(args: any) {

    const { formGroup, dataItem } = args;

    if (!formGroup.valid) {
      args.preventDefault();
    } else if (formGroup.dirty) {
      var level = this.viewTreeLevels.find(f => f.no == formGroup.value.no);
      if (level) {

        var formValue = formGroup.value;

        if (formValue.codeLength > this.maxDepth)
          formValue.codeLength = this.maxDepth;
        if (formValue.codeLength < 1)
          formValue.codeLength = 1;

        var index = this.viewTreeLevels.indexOf(level);
        this.viewTreeLevels[index] = formValue;
      }

      this.saveLocalChengesView();
    }
  }

  /**
   * ساخت فرم برای سطری که کلیک میشود
   * @param dataItem
   */
  public createFormGroup(no: any): FormGroup {
    var dataItem = this.viewTreeLevels.find(f => f.no == no)
    return this.formBuilder.group({
      'no': dataItem.no,
      'name': [dataItem.name, Validators.required],
      'codeLength': [dataItem.codeLength, Validators.required],
      'isEnabled': dataItem.isEnabled,
      'isUsed': dataItem.isUsed
    });
  }

  /**
   * ستون هایی که غیر قابل تغییر هستند را مشخص میکند
   * @param field
   */
  private isReadOnly(field: string): boolean {
    const readOnlyColumns = ['no', 'isEnabled'];
    return readOnlyColumns.indexOf(field) > -1;
  }

  /**
   * غیر فعال کردن سطرهایی که غیر قابل ویرایش هستند
   * @param context
   */
  public rowCallback(context: RowClassArgs) {
    const isEven = context.dataItem.isEnabled;
    return {
      notEnabled: !isEven,
    };
  }

  /**
   * لیست موجودیت ها را از سرویس میگیرد
   */
  loadEntity() {
    this.settingService.getAll(LookupApi.TreeViews).subscribe(res => {
      this.ddlEntites = res.body;
      this.entityArray = this.ddlEntites;
    })
  }


  /**
   * هنگام انتخاب یک موجودیت اجرا میشود و تنظیمات موجودیت انتخاب شده را از سرویس میخواند
   * @param item آیدی موجودیت انتخاب شده
   */
  handleEntityChange(item: any) {

    this.settingService.getViewTreeSettings(item).subscribe(res => {

      let result: any = res;
      var config = result.current;

      if (this.finalViewTreeConfig && this.finalViewTreeConfig.find(f => f.viewId == config.viewId)) {
        this.viewTreeConfig = this.finalViewTreeConfig.find(f => f.viewId == config.viewId);
      }
      else {
        this.viewTreeConfig = config;
      }

      this.viewTreeLevels = this.viewTreeConfig.levels;

      this.viewTreeDefaultConfig = result.default;
      this.maxDepth = this.viewTreeDefaultConfig.levels.length;
      var minDepth = result.current.levels.filter(f => f.isUsed).length;

      this.maxDepthValue = this.viewTreeConfig.maxDepth;

      if (this.viewTreeConfig.viewId == 1 && minDepth < 3)
        this.minDepth = 3;
      else
        this.minDepth = minDepth > 0 ? minDepth : 1;
    })

  }


  /**
   * فیلتر در لیست موجودیت ها
   * @param value
   */
  handleFilter(value: any) {
    this.ddlEntites = this.entityArray.filter((s) => s.value.toLowerCase().indexOf(value.toLowerCase()) !== -1);
  }

  /**
   * عملیات مربوط به تغییر حداکثر عمق
   */
  onChangeDepth() {

    if (this.maxDepthValue >= 1 && this.maxDepthValue <= this.maxDepth) {
      var levelsCount = this.viewTreeConfig.levels.filter(f => f.isEnabled).length;
      if (this.maxDepthValue < levelsCount) {
        while (this.maxDepthValue < levelsCount) {
          var level = this.viewTreeLevels.find(f => f.no == levelsCount);

          if (!level.isUsed) {
            level.isEnabled = false;
            this.viewTreeLevels[levelsCount - 1] = level;

            this.viewTreeConfig.levels = this.viewTreeLevels;
            this.viewTreeConfig.maxDepth = this.maxDepthValue;
            this.saveLocalChengesView();
          }

          levelsCount--;
        }
      }
      else {
        while (this.maxDepthValue > levelsCount) {
          var level = this.viewTreeLevels.find(f => f.no == levelsCount + 1);
          level.isEnabled = true;
          level.isUsed = false;
          this.viewTreeLevels[levelsCount] = level;
          levelsCount++;
        }

        this.viewTreeConfig.levels = this.viewTreeLevels;
        this.viewTreeConfig.maxDepth = this.maxDepthValue;
        this.saveLocalChengesView();
      }

    }
  }

  /**
   * ذخیره موقت تنظیمات یک موجودیت
   */
  saveLocalChengesView() {
    if (this.viewTreeConfig) {
      var config = this.finalViewTreeConfig.find(f => f.viewId == this.viewTreeConfig.viewId);
      if (config) {
        let index = this.finalViewTreeConfig.indexOf(config);

        this.finalViewTreeConfig[index] = this.viewTreeConfig;
      }
      else {
        this.finalViewTreeConfig.push(this.viewTreeConfig);
      }

      this.emitViewTreeValue();
    }
  }


  emitViewTreeValue() {
    let myList: Array<{ current: ViewTreeConfig, default: ViewTreeConfig }> = [];

    for (let item of this.finalViewTreeConfig) {
      myList.push({ current: item, default: item });
    }

    this.viewTreeValue.emit(myList);
  }

  ///**
  // * ذخیره تمام تنظیمات
  // */
  //saveAllConfig() {

  //  let myList: Array<{ current: ViewTreeConfig, default: ViewTreeConfig }> = [];

  //  for (let item of this.finalViewTreeConfig) {
  //    myList.push({ current: item, default: item });
  //  }

  //  this.settingService.putViewTreeConfig(SettingsApi.ViewTreeSettings, myList).subscribe(res => {
  //    this.maxDepthValue = undefined;
  //    this.ddlEntitySelected = undefined;
  //    this.viewTreeConfig = undefined;
  //    this.viewTreeLevels = [];
  //    this.finalViewTreeConfig = [];

  //    localStorage.removeItem("viewTreeConfig");

  //    this.showMessage(this.updateMsg, MessageType.Succes);

  //  }, (error => {
  //    this.errorMessage = error;
  //  }));

  //}

  /**
   * اعمال تنظیمات پیش فرض
   */
  setDefaultConfig(item: ViewTreeLevelConfig) {
    if (item.isUsed) {
      this.showMessage(this.getText("ViewTreeConfig.LevelIsUsedMsg"), MessageType.Warning);
    }
    else {
      var defaultItem = this.viewTreeDefaultConfig.levels.find(f => f.no == item.no);
      var level = this.viewTreeLevels.find(f => f.no == item.no);
      var index = this.viewTreeLevels.indexOf(level);

      defaultItem.isEnabled = true;
      this.viewTreeLevels[index] = defaultItem;
      this.viewTreeConfig.levels = this.viewTreeLevels;

      this.saveLocalChengesView();
    }
  }


}
