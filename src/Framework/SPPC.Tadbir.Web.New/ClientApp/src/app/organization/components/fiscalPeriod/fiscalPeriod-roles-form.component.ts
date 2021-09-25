import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout } from '@sppc/shared/enum/metadata';
import { RelatedItems } from '@sppc/shared/models';
import { DetailComponent } from '@sppc/shared/class';
import { SecureEntity, FiscalPeriodPermissions } from '@sppc/shared/security';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'fiscalPeriod-roles-form-component',
  templateUrl: './fiscalPeriod-roles-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class FiscalPeriodRolesFormComponent extends DetailComponent implements OnInit {

  //permission flag   
  AssignRolesAccess: boolean;

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public rolesList: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set fiscalPeriodRoles(fPeriodRoles: RelatedItems) {
    this.model = fPeriodRoles;
    this.selectedRows = [];
    if (fPeriodRoles != undefined) {
      this.gridData = fPeriodRoles.relatedItems;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }
    }
  }

  @Output() cancelFiscalPeriodRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveFiscalPeriodRoles: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  ngOnInit() {
    this.AssignRolesAccess = this.isAccess(SecureEntity.FiscalPeriod, FiscalPeriodPermissions.AssignRoles);
  }

  //////Events
  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let roleSelected of this.selectedRows) {
      let roleIndex = this.model.relatedItems.findIndex(f => f.id == roleSelected);
      this.model.relatedItems[roleIndex].isSelected = true;
    }
    this.saveFiscalPeriodRoles.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.rolesList = false;
    this.selectedRows = [];
    this.cancelFiscalPeriodRoles.emit();
  }

  escPress() {
    this.closeForm();
  }
  ////Events

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }
}
