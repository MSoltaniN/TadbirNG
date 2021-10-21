import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { String } from '@sppc/shared/class/source';

@Component({
  selector: 'company-role-component',
  templateUrl: './company-role.component.html'  
})
export class CompanyRoleComponent extends DetailComponent {

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public roleList: boolean;
  @Input() public companyName: string = '';

  @Input() public set companyRole(companyRole: RelatedItems) {
    this.model = companyRole;
    this.selectedRows = [];
    if (companyRole != undefined) {
      this.gridData = companyRole.relatedItems;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }
    }
  }

  @Output() cancelCompanyRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveCompanyRoles: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  //////Events
  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let companySelected of this.selectedRows) {
      let companyIndex = this.model.relatedItems.findIndex(f => f.id == companySelected);
      this.model.relatedItems[companyIndex].isSelected = true;
    }
    this.saveCompanyRoles.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.roleList = false;
    this.selectedRows = [];
    this.cancelCompanyRoles.emit();
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
    return String.Format(text, this.companyName);
  }

}
