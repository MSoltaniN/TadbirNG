import { Component, OnInit, Host, Input, EventEmitter, Output } from '@angular/core';
import { BaseComponent } from '@sppc/shared/class';
import { Layout } from '@sppc/env/environment';
import { RTL } from '@progress/kendo-angular-l10n';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { SettingService } from '@sppc/config/service';
import { BrowserStorageService } from '@sppc/shared/services';
import { GridComponent } from '@progress/kendo-angular-grid';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'left-action-toolbar',
  templateUrl: './leftActionToolbar.component.html',
  styleUrls: ['./leftActionToolbar.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class LeftActionToolbarComponent extends BaseComponent implements OnInit {

  @Input() viewId: number;
  @Input() parentComponent: any;
  @Input() reportSetting: any;
  @Input() reportManager: any;
  @Input() grid: GridComponent;

  @Output() onFilterOk = new EventEmitter();
  @Output() onFilterCancel = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService, public bStorageService: BrowserStorageService) {

    super(toastrService, bStorageService);
  }

  ngOnInit() {
  }

  showAdvanceFilterComponent() {
    this.parentComponent.showAdvanceFilterComponent(this.viewId, this.onFilterOk, this.onFilterCancel);
  }

  showQuickReportSetting() {  
    this.parentComponent.showQuickReportSetting(this.viewId,this.parentComponent,this.reportSetting,this.reportManager);
  }

  showReportManager() {
    this.parentComponent.showReportManager(this.viewId, this.parentComponent, this.reportSetting, this.reportManager);    
  }

  


  
}
