import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { soucrceAppType, SourceApp } from '@sppc/treasury/models/soucrceApp';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'app-sourceApp-form',
  templateUrl: './sourceApp-form.component.html',
  styles: [`
    input[type=text],textarea { width: 100%; }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class SourceAppFormComponent extends DetailComponent implements OnInit {

  @Input() public model: SourceApp;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() public isWizard: boolean = false;
  
  @Input() public set preferedCode(code:string) {
    this.setNewCode(code);
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<SourceApp> = new EventEmitter();

  selectedBranchScope = 0;
  selectedType = 0;
  sourceAppTypes = [
    {key: 'SourceApp.Source', value: soucrceAppType.Source},
    {key: 'SourceApp.Application', value: soucrceAppType.App}
  ];
  newCode = '';

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.SourceApp, ViewName.SourceApp,elem);
  }

  ngOnInit(): void {
    this.editForm.reset();

    setTimeout(() => {
      if (this.model.id == 0) {
        this.model.branchId = this.BranchId;
        this.model.branchScope = this.selectedBranchScope;
        this.model.type = this.selectedType;
        this.model.fiscalPeriodId = this.FiscalPeriodId;
        this.model.code = this.newCode;
      } else {
        this.selectedBranchScope = this.model.branchScope;
        this.selectedType = this.model.type;
      }
      this.editForm.reset(this.model);
    });
    
  }

  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.save.emit(this.editForm.value);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }

  onChangeTypeDropDown(e) {
    this.editForm.patchValue({
      type: e
    });
  }

  setNewCode(code) {
    if (code) {
      let serial = <string>code.replace(/\d/g,'_');
      let startNo:any = <string>code.replace(/\D+/g, '_');
      let endNumber;

      startNo = startNo.split('_');

      if (!isNaN(startNo[startNo.length - 1]))
        endNumber = +startNo[startNo.length - 1] + 1;
      
      if (serial[serial.length-1] != '_') {
        serial = serial + '_';
        endNumber = startNo[startNo.length - 1] + '1';
      }

      startNo[startNo.length - 1] = endNumber.toString();

      let endNo = startNo.join('');
      endNo.split('').forEach(d => {
        serial = serial.replace('_',d);
      })
      this.newCode = serial;

    }
  }

}
