import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { OperationLog } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { MetaDataService } from '../../service/metadata/metadata.service';
import { Entities } from '../../../environments/environment';
import { SettingService } from '../../service/index';
import { ViewName } from '../../security/viewName';



@Component({
    selector: 'operationLogs-detail-component',
    styles: [`
         /deep/ #log-detail > .k-dialog { width: 800px; }
@media screen and (max-width:800px) {
    /deep/ #log-detail > .k-dialog { width: 90%; min-width:250px; }
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


    constructor(public toastrService: ToastrService, public translate: TranslateService,
      public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {

      super(toastrService, translate, renderer, metadata, settingService, Entities.OperationLog, ViewName.OperationLog);
    }

}
