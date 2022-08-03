
import { Component, OnInit, Renderer2 } from '@angular/core';
// import "rxjs/Rx";
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { DefaultComponent } from '@sppc/shared/class';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService, LogCheckItem, LogSettingItemViewModel } from '@sppc/config/service';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'log-settings',
  templateUrl: './operationLogs-setting.component.html',
  styles: [`
.section-option { margin-top: 15px; background-color: #f6f6f6; border: solid 1px #dadde2; padding: 15px 15px 0; }
.section-option label,input[type=text] { width:100% } /deep/.section-option kendo-dropdownlist { width:100% }
.btn-compute-default {margin-top: 5px; border: 2px solid #337ab7; color: #337ab7; padding: 5px 25px;}
.btn-compute { color: #337ab7; transition: All 0.3s 0.1s ease-out;}
.check-item { margin-top: 20px;}
.btn-show-log {padding-left:0px;}
.section-border { border: solid 1px #337ab7; padding: 7px 10px;min-height:calc(100vh - 30vh);max-height: calc(100vh - 40vh);overflow-y: auto;}
.btn-compute-selectable{ color: #fff; background-image: linear-gradient(#c1e3ff, #337ab7);}
`],providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  },DefaultComponent]
})

export class LogSettingComponent extends DefaultComponent implements OnInit {

  settingModel: any;
  isSystem: boolean = false;
  isChecked: boolean = false;
  detailItem: Array<any>;
  selectedNodeId: number;
  selectedItems:Array<LogCheckItem>

  //#region constructor
  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    private settingsService: SettingService, public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingsService, Entities.Setting, undefined);
  }
  //#endregion

  //#region Events
  ngOnInit(): void {
    this.chkIsSystemChanged();
    this.selectedItems = new Array<LogCheckItem>();
    
  }

  chkIsSystemChanged() {
    if (!this.isSystem) {
      this.settingService.getLogSettings().subscribe((res) => {
        this.settingModel = res;
      });
    }
    else {
      this.settingService.getSystemLogSettings().subscribe((res) => {
        this.settingModel = res;
      });      
    }
    this.selectedItems = new Array<LogCheckItem>(); 
    this.detailItem = [];
    this.selectedNodeId = 0;
  }

  handleSelection(item: any): void {    
    var items: Array<LogSettingItemViewModel> = item.dataItem.items;
    this.selectedNodeId = item.dataItem.id;

    items.forEach((it) => {      
      var result = this.selectedItems.filter(f => f.detailId === it.id && f.nodeId === this.selectedNodeId);
      if (result && result.length > 0) {
        var enabled = result[result.length - 1].isEnabled;
        var index = items.findIndex(i => i.id === it.id);
        items[index].isEnabled = enabled;
      }
    });
    this.detailItem = items;
    
  }

  chkItemClick(id: number, operationId:number,event:any) {
    var checkItem = new LogCheckItem(this.selectedNodeId, id, operationId, event.target.checked);
    var existItemIndex = this.selectedItems.findIndex(f => f.nodeId === this.selectedNodeId && f.detailId === id);
    if (existItemIndex >= 0)
    {
      this.selectedItems[existItemIndex] = event.target.checked;
    }
    else
      this.selectedItems.push(checkItem);
  }

  //#endregion


  //#region Methods
  saveSettings() {
    var arrayLogSetting: LogSettingItemViewModel[] = [];
    if (this.selectedItems.length > 0) {
      this.selectedItems.forEach((si) => {
        var item = new LogSettingItemViewModel(si.detailId, si.operationId, "", si.isEnabled);
        arrayLogSetting.push(item);
      });
    }
    if (!this.isSystem)
      this.settingService.putLogSettings(arrayLogSetting).subscribe(() =>
      {
        this.showMessage(this.getText("Messages.OperationSuccessful"));
      });
    else
      this.settingService.putSystemLogSettings(arrayLogSetting).subscribe(() => {
        this.showMessage(this.getText("Messages.OperationSuccessful"));
      });
  }

  //#endregion
}
