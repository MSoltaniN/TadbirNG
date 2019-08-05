import { Component, Input, Output, EventEmitter, Renderer2 } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { RowArgs } from '@progress/kendo-angular-grid';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/env/environment';
import { DetailComponent } from '@sppc/shared/class';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { Permission } from '@sppc/core';
import { Role, RoleFullViewModel } from '@sppc/admin/models';
import { RoleFullInfo } from '@sppc/admin/service';
import { TreeNodeInfo } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-form-component',
  styles: [`
        input[type=text],textarea { width: 100%; }
        .permission-dialog {width: 100% !important; min-width: 250px !important; height:100%}
        /deep/ .permission-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
  ],
  templateUrl: './role-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class RoleFormComponent extends DetailComponent {



  public treeData: TreeNodeInfo[] = new Array<TreeNodeInfo>();

  //create properties    
  public selectedRows: number[] = [];
  active: boolean = false;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set model(role: Role) {

    this.editForm.reset(role);
    this.active = role !== undefined || this.isNew;
  }

  public checkedKeys: string[] = [];

  private permissonDictionary: { [id: string]: Permission; } = {}

  @Input() public set permissionModel(permission: any) {

    var level0Index: number = -1;
    var level1Index: number = 0;
    var selectAll: boolean = true;


    if (permission != undefined) {
      var groupId = 0;

      this.checkedKeys = [];
      this.treeData = new Array<TreeNodeInfo>();

      if (this.CurrentLanguage == "fa")
        this.treeData.push(new TreeNodeInfo(-1, undefined, "حسابداری"));
      else
        this.treeData.push(new TreeNodeInfo(-1, undefined, "Accounting"));

      var checkedParent: string = '';
      var indexId: number = 0;

      for (let permissionItem of permission) {


        if (groupId != permissionItem.groupId) {
          this.treeData.push(new TreeNodeInfo(permissionItem.groupId, -1, permissionItem.groupName))

          level0Index++;
          level1Index = -1;

          checkedParent = '0_' + level0Index.toString();

          this.checkedKeys.push(checkedParent);

          indexId = this.checkedKeys.length - 1;

          groupId = permissionItem.groupId;



        }

        if (groupId == permissionItem.groupId) {
          this.treeData.push(new TreeNodeInfo(parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00')
            , permissionItem.groupId, permissionItem.name))

          level1Index++;
        }


        if (permissionItem.isEnabled) {
          this.checkedKeys.push('0_' + level0Index.toString() + '_' + level1Index.toString());
        }
        else {
          if (indexId >= 0 && this.checkedKeys[indexId].split('_').length == 2) {
            this.checkedKeys.splice(indexId, 1);
            indexId = -1;
            selectAll = false;
          }
        }

        this.permissonDictionary['0_' + level0Index.toString() + '_' + level1Index.toString()] = permissionItem;

      }


      if (selectAll) {
        this.checkedKeys.push('0');
      }
    }

  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<RoleFullInfo> = new EventEmitter();
  //create properties



  ////Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      var permissionData: Array<Permission> = new Array<Permission>();
      var allChildChecked: Array<string> = new Array<string>();

      for (let key in this.permissonDictionary) {
        //permissionItem.isEnabled = false;
        this.permissonDictionary[key].isEnabled = false;
      }

      for (let checked of this.checkedKeys) {
        if (checked.split('_').length == 3) {
          this.permissonDictionary[checked].isEnabled = true;
        }

        if (checked.split('_').length == 2) {
          allChildChecked.push(checked);
        }
      }


      for (let key in this.permissonDictionary) {
        //permissionItem.isEnabled = false;    
        var parentKey: string = '';
        if (key.split('_').length == 3) parentKey = key.split('_')[0] + '_' + key.split('_')[1];

        if (allChildChecked.filter(k => k == parentKey).length > 0) {
          this.permissonDictionary[key].isEnabled = true;
        }

        if (permissionData.filter(p => p.id == this.permissonDictionary[key].id).length == 0)
          permissionData.push(this.permissonDictionary[key]);

      }


      var viewModel: RoleFullViewModel;
      viewModel = {
        id: this.editForm.value.id,
        role: this.editForm.value,
        permissions: permissionData
      }

      this.save.emit(viewModel);
      this.active = true;
      this.selectedRows = [];
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.selectedRows = [];
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.active = false;
    this.selectedRows = [];
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
  ////Events

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

  constructor(public bStorageService: BrowserStorageService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Role, ViewName.Role);
  }


}
