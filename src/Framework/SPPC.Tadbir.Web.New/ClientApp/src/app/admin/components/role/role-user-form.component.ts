import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout } from '@sppc/env/environment';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { String } from '@sppc/shared/class';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-user-form-component',
  templateUrl: './role-user-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class RoleUserFormComponent extends DetailComponent {


  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public usersList: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() public roleName: string = '';

  @Input() public set roleUser(roleUser: RelatedItems) {
    this.model = roleUser;
    this.selectedRows = [];
    if (roleUser != undefined) {
      this.gridData = roleUser.relatedItems;

      for (let userItem of this.gridData) {
        if (userItem.isSelected) {
          this.selectedRows.push(userItem.id)
        }
      }
    }
  }

  @Output() cancelRoleUsers: EventEmitter<any> = new EventEmitter();
  @Output() saveRoleUsers: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  //////Events
  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let userSelected of this.selectedRows) {
      let userIndex = this.model.relatedItems.findIndex(f => f.id == userSelected);
      this.model.relatedItems[userIndex].isSelected = true;
    }
    this.saveRoleUsers.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.usersList = false;
    this.selectedRows = [];
    this.cancelRoleUsers.emit();
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
