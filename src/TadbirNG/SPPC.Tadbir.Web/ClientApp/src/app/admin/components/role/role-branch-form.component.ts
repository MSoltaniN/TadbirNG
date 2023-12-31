import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { String } from '@sppc/shared/class/source';
import { Layout } from '@sppc/shared/enum/metadata';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { CheckableSettings, TreeItem, TreeItemLookup } from '@progress/kendo-angular-treeview';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-branch-form-component',
  styles: [`
       .user-dialog {width: 100% !important; height:100% !important}
       ::ng-deep .k-treeview .k-i-collapse, ::ng-deep .k-treeview .k-i-expand {margin-left: -5px !important}
       ::ng-deep .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
  ],
  templateUrl: './role-branch-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class RoleBranchFormComponent extends DetailComponent {


  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;
  public checkableSettings: CheckableSettings = {
      checkChildren: true,
      checkDisabledChildren: false,
      checkParents: false,
      enabled: true,
      // mode: this.checkMode,
      // checkOnClick: this.checkOnClick,
  }

  @Input() public inputRoleBranches: boolean = false;
 // @Input() public errorMessage: string = '';
  @Input() public roleName: string = '';

  @Input() public set roleBranches(roleBranches: RelatedItems) {
    this.model = roleBranches;
    this.selectedRows = [];
    if (roleBranches != undefined) {
      this.gridData = roleBranches.relatedItems;

      for (let branchItem of this.gridData) {
        if (branchItem.isSelected) {
          this.selectedRows.push(branchItem.id)
        }
      }
    }
  }

  @Output() cancelRoleBranches: EventEmitter<any> = new EventEmitter();
  @Output() saveRoleBranches: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  checkById(item: TreeItem) {
    return item.dataItem.id;
  }

  handleCheckingRows(item: TreeItemLookup) {

  }

  //////Events
  public onSave(e: any): void {
    e.preventDefault();

    this.model.relatedItems.forEach(f => f.isSelected = false);
    for (let branchSelected of this.selectedRows) {
      let branchIndex = this.model.relatedItems.findIndex(f => f.id == branchSelected);
      this.model.relatedItems[branchIndex].isSelected = true;
    }
    this.saveRoleBranches.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.inputRoleBranches = false;
    this.selectedRows = [];
    this.cancelRoleBranches.emit();
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
