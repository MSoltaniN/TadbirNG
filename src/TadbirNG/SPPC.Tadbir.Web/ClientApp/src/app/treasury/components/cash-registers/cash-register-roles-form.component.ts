import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { Entities, Layout } from '@sppc/shared/enum/metadata';
import { RelatedItems } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { CashRegistersService } from '@sppc/treasury/service/cash-registers.service';
import { ToastrService } from 'ngx-toastr';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'cash-register-roles-form',
  templateUrl: './cash-register-roles-form.component.html',
  styles: [''],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class CashRegisterRolesFormComponent extends DetailComponent implements OnInit {

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  //public model: RelatedItems;
  cashRegisterRolesData: RelatedItems;


  @Input() public cashRegisterId: number;
  @Input() public errorMessage: string = '';
  @Input() public set cashRegisterRoles(selectedRoles: RelatedItems) {
    this.cashRegisterRolesData = selectedRoles;
    this.selectedRows = [];
    if (selectedRoles != undefined) {
      this.gridData = selectedRoles.relatedItems;
      this.showloadingMessage = selectedRoles.relatedItems.length? true: false;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }
    }
  }

  @Output() cancelCashRegisterRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveCashRegisterRoles: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public cashRegisterService: CashRegistersService,
    public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CashRegister, ViewName.CashRegister,elem);
  }

  ngOnInit() { }

  public onSave(e: any): void {
    e.preventDefault();
    let data:RelatedItems = {
      id: this.cashRegisterRolesData.id,
      relatedItems: []
    };
    this.cashRegisterRolesData.relatedItems.forEach(f => f.isSelected = false);

    for (let roleSelected of this.selectedRows) {
      let roleIndex = this.cashRegisterRolesData.relatedItems.findIndex(f => f.id == roleSelected);
      this.cashRegisterRolesData.relatedItems[roleIndex].isSelected = true;
      data.relatedItems.push(this.cashRegisterRolesData.relatedItems[roleIndex])
    }
    // return
    this.saveCashRegisterRoles.emit(data);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.selectedRows = [];
    this.cancelCashRegisterRoles.emit();
  }

  escPress() {
    this.closeForm();
  }


  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

}
