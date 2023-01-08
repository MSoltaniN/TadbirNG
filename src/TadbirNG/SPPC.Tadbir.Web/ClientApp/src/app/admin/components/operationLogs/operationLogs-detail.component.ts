import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { OperationLog } from '@sppc/admin/models/index';
import { DefaultComponent } from '@sppc/shared/class';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { Entities } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';



@Component({
  selector: 'operationLogs-detail-component',
  styles: [`
         ::ng-deep #log-detail > .k-dialog { width: 800px; }
@media screen and (max-width:800px) {
    ::ng-deep #log-detail > .k-dialog { width: 90%; min-width:250px; }
}
`],
  templateUrl: './operationLogs-detail.component.html'
})

export class OperationLogsDetailComponent extends DefaultComponent {


  //create properties
  active: boolean = false;
  logDetail: OperationLog;

  @Input() public set model(log: OperationLog) {
    this.active = log !== undefined;
    this.logDetail = log;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  //create properties

  //Events

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.active = false;
    this.cancel.emit();
  }
  //Events
  language = localStorage.getItem('lang');
  dateDisplayType: string;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {

    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.OperationLog, ViewName.OperationLog);
    if (this.language == 'fa'){
      this.dateDisplayType = 'jalali'
    } else {
      this.dateDisplayType = 'gregorian'
    }

  }

}
