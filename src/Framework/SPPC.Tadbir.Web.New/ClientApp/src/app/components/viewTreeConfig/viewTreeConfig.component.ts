import { Component, OnInit, Input, Renderer2 } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { Response } from '@angular/http';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { SettingService } from '../../service/index';
import { ViewName } from '../../security/viewName';
import { ViewTreeConfig, ViewTreeLevelConfig } from '../../model/index';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { SettingsApi, LookupApi } from '../../service/api/index';
import { MetaDataService } from '../../service/metadata/metadata.service';

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
#level-list > li:last-child{ border-bottom: solid 1px #337ab7; } .view-body { min-height: 600px; }
#level-list > li.enable{ cursor: pointer; } #level-list > li.enable:hover{ background-color: #8ab8e0;}
#level-list > li.disable{ background-color: #fdfdfd; } #level-list > li.selected{ background-color: #337ab7; }
`],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ViewTreeConfigComponent extends DefaultComponent implements OnInit {


  ddlEntites: Array<Item>;
  entityArray: Array<Item>;
  ddlEntitySelected: number = 0;
  levelSelected: number = 0;
  maxDepthValue: number;
  disabledNumTxt: boolean = true;

  viewTreeConfig: ViewTreeConfig;
  viewTreeDefaultConfig: ViewTreeConfig;
  finalViewTreeConfig: Array<ViewTreeConfig> = [];

  public errorMessage = String.Empty;


  public levelForm = new FormGroup({
    no: new FormControl("", Validators.required),
    name: new FormControl("", Validators.required),
    codeLength: new FormControl("", Validators.required),
    isEnabled: new FormControl()
  });

  public ngOnInit(): void {
    this.loadEntity();

    this.changeFormValue();
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Settings, '');

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

    this.levelForm.reset();
    this.levelSelected = 0;

    this.settingService.getViewTreeSettings(item).subscribe(res => {

      let result: any = res;

      var config = result.current;
      if (this.finalViewTreeConfig && this.finalViewTreeConfig.find(f => f.viewId == config.viewId)) {
        this.viewTreeConfig = this.finalViewTreeConfig.find(f => f.viewId == config.viewId);
      }
      else {
        this.viewTreeConfig = config;
      }

      this.viewTreeDefaultConfig = result.default;

      this.maxDepthValue = this.viewTreeConfig.maxDepth;
      this.disabledNumTxt = false;
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
   * هنگام انتخاب سطح اجرا میشود و اطلاعات مربوط به سطر را در فرم نمایش میدهد
   * @param level
   */
  onChangeLevel(level: ViewTreeLevelConfig) {

    if (level.no <= this.maxDepthValue) {

      this.levelSelected = level.no;
      if (level.isEnabled) {
        this.levelForm.reset(level);
      }
      else {
        this.levelForm.reset();
        this.levelForm.patchValue({ 'no': this.levelSelected, 'isEnabled': true })
      }
    }
  }

  /**
   * ذخیره موقت سطح تغییر داده شده 
   */
  saveLocalChangesLevel() {
    if (this.levelSelected > 0 && this.levelForm.valid) {

      var item = this.viewTreeConfig.levels.find(f => f.isEnabled && f.no == this.levelSelected);

      if (item) {
        let index = this.viewTreeConfig.levels.indexOf(item);
        this.viewTreeConfig.levels[index] = this.levelForm.value;
      }
      else {
        this.viewTreeConfig.maxDepth = this.maxDepthValue;
        this.viewTreeConfig.levels[this.levelSelected - 1] = this.levelForm.value;
      }


      this.saveLocalChengesView();
    }
  }


  /**
   * ذخیره موقت تنظیمات یک موجودیت
   */
  saveLocalChengesView() {

    if (this.viewTreeConfig && this.levelSelected > 0) {
      var config = this.finalViewTreeConfig.find(f => f.viewId == this.viewTreeConfig.viewId);
      if (config) {
        let index = this.finalViewTreeConfig.indexOf(config);

        this.finalViewTreeConfig[index] = this.viewTreeConfig;
      }
      else {
        this.finalViewTreeConfig.push(this.viewTreeConfig);
      }
    }
  }

  /**
   * ذخیره تمام تنظیمات
   */
  saveAllConfig() {

    this.saveLocalChangesLevel();

    if (this.finalViewTreeConfig.length > 0) {

      let myList: Array<{ current: ViewTreeConfig, default: ViewTreeConfig }> = [];

      for (let item of this.finalViewTreeConfig) {
        myList.push({ current: item, default: item });
      }

      this.settingService.putViewTreeConfig(SettingsApi.ViewTreeSettings, myList).subscribe(res => {
        this.ddlEntitySelected = 0;
        this.levelSelected = 0;
        this.maxDepthValue = undefined;

        this.viewTreeConfig = undefined;
        this.finalViewTreeConfig = [];
        this.levelForm.reset();

        sessionStorage.removeItem("viewTreeConfig");

        this.showMessage(this.updateMsg, MessageType.Succes);

      }, (error => {
        this.errorMessage = error;
      }));

    }    
  }

  /**
   * اعمال تنظیمات پیش فرض
   */
  setDefaultConfig() {

    var defaultItem = this.viewTreeDefaultConfig.levels.find(f => f.no == this.levelSelected);
    defaultItem.isEnabled = true;
    this.levelForm.reset(defaultItem);
  }

  /**
   * وقتی تعداد سطوح کم میشود عملیات حذف آخرین سطح را از لیست سطوح انجام میدهد و در حافظه موقت ذخیره میکند
   */
  onChangeDepth() {

    var levelsCount = this.viewTreeConfig.levels.filter(f => f.isEnabled).length;

    if (this.maxDepthValue < levelsCount) {

      this.levelSelected = 0;
      this.levelForm.reset();

      var item = this.viewTreeConfig.levels.find(f => f.no == levelsCount);

      var defaultItem = this.viewTreeDefaultConfig.levels.find(f => f.no == levelsCount);

      item.isEnabled = false;
      item.name = defaultItem.name;

      this.viewTreeConfig.levels[levelsCount - 1] = item;
      this.viewTreeConfig.maxDepth = this.maxDepthValue;


      var config = this.finalViewTreeConfig.find(f => f.viewId == this.viewTreeConfig.viewId);
      if (config) {
        let index = this.finalViewTreeConfig.indexOf(config);

        this.finalViewTreeConfig[index] = this.viewTreeConfig;
      }
      else {
        this.finalViewTreeConfig.push(this.viewTreeConfig);
      }
      
    }

  }

  /**
   * اگر اطلاعات داخل فرم تغییر کند این متد اجرا میشود
   */
  changeFormValue() {
    this.levelForm.valueChanges.subscribe(val => {
      this.saveLocalChangesLevel();
    })
  }
}
