import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { Layout } from '@sppc/env/environment';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { String } from '@sppc/shared/class/source';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-company-form-component',
  templateUrl: './role-company-form.component.html'  
})
export class RoleCompanyFormComponent extends DetailComponent {

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public companyList: boolean;
  @Input() public roleName: string = '';

  @Input() public set roleCompany(roleCompany: RelatedItems) {
    this.model = roleCompany;
    this.selectedRows = [];
    if (roleCompany != undefined) {
      this.gridData = roleCompany.relatedItems;

      for (let companyItem of this.gridData) {
        if (companyItem.isSelected) {
          this.selectedRows.push(companyItem.id)
        }
      }
    }
  }

  @Output() cancelRoleCompanies: EventEmitter<any> = new EventEmitter();
  @Output() saveRoleCompanies: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  //////Events
  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let companySelected of this.selectedRows) {
      let companyIndex = this.model.relatedItems.findIndex(f => f.id == companySelected);
      this.model.relatedItems[companyIndex].isSelected = true;
    }
    this.saveRoleCompanies.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.companyList = false;
    this.selectedRows = [];
    this.cancelRoleCompanies.emit();
  }

  escPress() {
    this.closeForm();
  }
  ////Events

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }


  getTitleText(text: string) {
    return String.Format(text, this.roleName);
  }

}
