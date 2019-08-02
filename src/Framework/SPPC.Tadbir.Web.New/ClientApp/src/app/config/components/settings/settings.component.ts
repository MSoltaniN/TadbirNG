import { Component, OnInit, Input, Renderer2, ViewChild, forwardRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { DefaultComponent } from "../../class/default.component";
import { MessageType, Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { SettingService, SettingTreeNodeInfo, SettingBriefInfo } from '../../service/index';
import { SettingsApi } from '../../service/api/settingsApi';
import { TreeItem } from '@progress/kendo-angular-treeview';
import { SettingsFormComponent } from './settings-form.component';
import { SettingKey } from '../../enum/settingsKey';
import { BrowserStorageService } from '../../service/browserStorage.service';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class SettingsComponent extends DefaultComponent implements OnInit {

  @ViewChild(forwardRef(() => SettingsFormComponent)) private settingForm: SettingsFormComponent;

  public errorMessage = String.Empty;
  public expandedKeys: any[] = [-1];
  public lastSelectedType: string;
  public settingsCategories: any[];
  public settingModel: any[];
  public itemSelectedModel: SettingBriefInfo;
  public itemUpdatedModel: SettingBriefInfo;

  ngOnInit(): void {

    this.settingsService.getSettingsCategories(SettingsApi.AllSettings).subscribe(res => {
      this.settingsCategories = res;
      var treeData = new Array<SettingTreeNodeInfo>();
      if (this.settingsCategories != undefined) {

        if (this.CurrentLanguage == "fa")
          treeData.push(new SettingTreeNodeInfo(-1, undefined, "تنظیمات", undefined, undefined));
        else
          treeData.push(new SettingTreeNodeInfo(-1, undefined, "Settings", undefined, undefined));

        for (let setting of this.settingsCategories) {
          treeData.push(new SettingTreeNodeInfo(setting.id, -1, setting.title, setting.description, setting.modelType))
        }
      }
      this.settingModel = JSON.parse(JSON.stringify(treeData));
    });

  }

  constructor(public toastrService: ToastrService, public translate: TranslateService, public sppcLoading: SppcLoadingService, public bStorageService: BrowserStorageService,
    private settingsService: SettingService, public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingsService, Entities.Setting, undefined);
  }

  public handleSelection(item: TreeItem): void {
    this.itemSelectedModel = this.settingsCategories.find(f => f.id == item.dataItem.id);

    if (this.lastSelectedType && this.lastSelectedType != 'ViewTreeConfig') {
      this.settingForm.updateListHandler();
      this.updateList(this.lastSelectedType);
    }
    this.lastSelectedType = item.dataItem.modelType;
  }

  updateListHandler(model: SettingBriefInfo) {
    this.itemUpdatedModel = model;
  }


  updateList(type: string) {
    let item: SettingBriefInfo = this.settingsCategories.find(f => f.modelType == type);
    if (item) {
      item.values = this.itemUpdatedModel.values;
    }
  }

  onSaveSettingsList() {

    if (this.lastSelectedType) {
      this.settingForm.updateListHandler();
      this.updateList(this.lastSelectedType);
    }


    //#region بروزرسانی تنظیمات ذخیره شده 
    this.bStorageService.removeNumberConfig();

    var numConfig = this.settingsCategories.find(f => f.id == SettingKey.NumberDisplayConfig);
    if (numConfig) {
      this.bStorageService.setNumberConfig(numConfig.values)
    }

    this.bStorageService.removeDateRangeConfig();

    var dateConfig = this.settingsCategories.find(f => f.id == SettingKey.DateRangeConfig);
    if (dateConfig) {
      this.bStorageService.setDateRangeConfig(dateConfig.values);
    }

    this.bStorageService.removeSelectedDateRange();
    //#endregion


    this.settingsService.putSettingsCategories(SettingsApi.AllSettings, this.settingsCategories).subscribe(res => {
      this.showMessage(this.updateMsg, MessageType.Succes);
    }, (error => {
      this.errorMessage = error;
    }));

  }

  onDefaultSettings() {
    this.settingForm.defaultSettingsHandler();
  }
}

