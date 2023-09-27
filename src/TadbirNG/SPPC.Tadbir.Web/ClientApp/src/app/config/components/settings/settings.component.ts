import {
  Component,
  forwardRef,
  OnInit,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { RTL } from "@progress/kendo-angular-l10n";
import { TreeItem } from "@progress/kendo-angular-treeview";
import {
  SettingBriefInfo,
  SettingService,
  SettingTreeNodeInfo,
} from "@sppc/config/service";
import { SettingsApi } from "@sppc/config/service/api";
import { DefaultComponent } from "@sppc/shared/class";
import { SettingKey } from "@sppc/shared/enum";
import { Entities, Layout, MessageType } from "@sppc/shared/enum/metadata";
import { SettingPermissions } from "@sppc/shared/security";
import {
  BrowserStorageService,
  ErrorHandlingService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
// import "rxjs/Rx";
import { SettingsFormComponent } from "./settings-form.component";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "settings",
  templateUrl: "./settings.component.html",
  styleUrls: ["./settings.component.css"],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class SettingsComponent extends DefaultComponent implements OnInit {
  @ViewChild(forwardRef(() => SettingsFormComponent), {static: true})
  private settingForm: SettingsFormComponent;

  //public errorMessage = String.Empty;
  public expandedKeys: any[] = [-1];
  public lastSelectedType: string;
  public settingsCategories: any[];
  public settingModel: any[];
  public itemSelectedModel: SettingBriefInfo;
  public itemUpdatedModel: SettingBriefInfo;

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    private settingsService: SettingService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public errorHandlingService: ErrorHandlingService
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      settingsService,
      Entities.Setting,
      undefined
    );
  }

  disableDefaultButtons = false;

  ngOnInit(): void {
    if (!this.isAccess(Entities.Setting, SettingPermissions.ViewSettings)) {
      this.showMessage(
        this.getText("App.AccessDenied"),
        MessageType.Warning
      );
    } else {
      this.getSettings();
    }
  }

  getSettings() {
    this.settingsService
      .getSettingsCategories(SettingsApi.AllSettings)
      .subscribe((res) => {
        this.settingsCategories = res;
        var treeData = new Array<SettingTreeNodeInfo>();
        if (this.settingsCategories != undefined) {
          if (this.CurrentLanguage == "fa")
            treeData.push(
              new SettingTreeNodeInfo(
                -1,
                undefined,
                "تنظیمات",
                undefined,
                undefined
              )
            );
          else
            treeData.push(
              new SettingTreeNodeInfo(
                -1,
                undefined,
                "Settings",
                undefined,
                undefined
              )
            );

          for (let setting of this.settingsCategories) {
            treeData.push(
              new SettingTreeNodeInfo(
                setting.id,
                -1,
                setting.title,
                setting.description,
                setting.modelType
              )
            );
          }

          if (this.CurrentLanguage == "fa") {
            let userProfileConfig = new SettingTreeNodeInfo(
              11,
              -1,
              "تنظیمات داشبورد",
              'تنظیمات مربوط به نمایش داشبورد هنگام ورود.',
              "UserProfileConfig"
            )
            treeData.push(userProfileConfig);
            this.settingsCategories.push(userProfileConfig);
          } else {
            let userProfileConfig = new SettingTreeNodeInfo(
              11,
              -1,
              "Dashboard Setting",
              "showDashboardAtStartup",
              "UserProfileConfig"
            )
            treeData.push(userProfileConfig);
            this.settingsCategories.push(userProfileConfig);
          }
        }

        this.settingsService
        .getSettingsCategories(SettingsApi.UserProfileConfig)
        .subscribe(res => {
          let userProfileConfig = this.settingsCategories.find(i => i.modelType == "UserProfileConfig");
          userProfileConfig.values = res;
        })

        this.settingModel = JSON.parse(JSON.stringify(treeData));
      });
  }

  public handleSelection(item: TreeItem): void {
    this.itemSelectedModel = this.settingsCategories.find(
      (f) => f.id == item.dataItem.id
    );

    if (this.lastSelectedType && this.lastSelectedType != "SystemConfig") {
      this.settingForm.updateListHandler();
      this.updateList(this.lastSelectedType);
    }
    this.lastSelectedType = item.dataItem.modelType;
    this.disableDefaultButtons = ![1,2,3,9,11].includes(item.dataItem.id);
  }

  updateListHandler(model: SettingBriefInfo) {
    this.itemUpdatedModel = model;
  }

  updateList(type: string) {
    let item: SettingBriefInfo = this.settingsCategories.find(
      (f) => f.modelType == type
    );

    if (item?.values) {
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

    var numConfig = this.settingsCategories.find(
      (f) => f.id == SettingKey.NumberDisplayConfig
    );
    if (numConfig) {
      this.bStorageService.setNumberConfig(numConfig.values);
    }

    this.bStorageService.removeDateRangeConfig();

    var dateConfig = this.settingsCategories.find(
      (f) => f.id == SettingKey.DateRangeConfig
    );
    if (dateConfig) {
      this.bStorageService.setDateRangeConfig(
        dateConfig.values,
        this.CompanyId.toString()
      );
    }

    this.bStorageService.removeSelectedDateRange();
    //#endregion

    if (this.itemUpdatedModel.modelType == "UserProfileConfig") {
      this.settingsService
      .putUserProfileSettings(SettingsApi.UserProfileConfig, this.itemUpdatedModel.values)
      .subscribe(
        (res) => {
          this.showMessage(this.updateMsg, MessageType.Succes);
        },
        (error) => {
          this.errorMessages = this.errorHandlingService.handleError(error);
        }
      );
    } else {
      this.settingsService
        .putSettingsCategories(SettingsApi.AllSettings, this.settingsCategories)
        .subscribe(
          (res) => {
            this.showMessage(this.updateMsg, MessageType.Succes);
          },
          (error) => {
            this.errorMessages = this.errorHandlingService.handleError(error);
          }
        );
    }
  }

  saveChanges(item){
    this.updateListHandler(item);
    this.onSaveSettingsList();
  }

  onDefaultSettings() {
    this.settingForm.defaultSettingsHandler();
  }
}
