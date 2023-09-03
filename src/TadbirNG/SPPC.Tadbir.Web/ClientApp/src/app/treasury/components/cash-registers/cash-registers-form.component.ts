import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { CashRegisters } from '@sppc/treasury/models/cashRegisters';
import { CashRegistersInfo } from '@sppc/treasury/service/cash-registers.service';
import { ShortcutService } from '@sppc/shared/services/shortcut.service';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'cash-registers-form',
  styles: [`
        input[type=text],textarea { width: 100%; }
    `],
  templateUrl: './cash-registers-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class CashRegistersFormComponent extends DetailComponent implements OnInit {

  @Input() public model: CashRegisters;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public isWizard: boolean = false;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<CashRegisters> = new EventEmitter();

  selectedBranchScope = 0;

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
     public bStorageService: BrowserStorageService,
     public renderer: Renderer2,
     public metadata: MetaDataService,
     public elem:ElementRef,shortcutService:ShortcutService)
  {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CashRegister, ViewName.CashRegister,elem,shortcutService,CashRegistersInfo.getInstance());
  }

  ngOnInit(): void {
    this.editForm.reset();

    setTimeout(() => {
      if (this.isNew) {
        this.model.id = 0;
        this.model.branchId = this.BranchId;
        this.model.branchScope = this.selectedBranchScope;
        this.model.fiscalPeriodId = this.FiscalPeriodId;
      } else {
        this.selectedBranchScope = this.model.branchScope;                
      }
      
      this.editForm.reset(this.model);
    })
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

}
