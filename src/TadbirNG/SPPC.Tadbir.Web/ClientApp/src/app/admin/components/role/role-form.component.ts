import { Component, Input, Output, EventEmitter, Renderer2, ElementRef } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { DetailComponent } from '@sppc/shared/class';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { Permission } from '@sppc/core';
import { Role, RoleFullViewModel, RoleItem } from '@sppc/admin/models';
import { RoleFullInfo } from '@sppc/admin/service';
import { TreeNodeInfo } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';
import { TreeItemLookup } from '@progress/kendo-angular-treeview';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-form-component',
  styles: [`
        input[type=text],textarea { width: 100%; }
        .permission-dialog {width: 100% !important; min-width: 250px !important; height:100%}
        ::ng-deep .permission-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
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

  @Input() public active: boolean = false;
  @Input() public isNew: boolean = false;
  //@Input() public errorMessage: string = '';

  @Input() public set model(role: Role) {
    this.editForm.reset(role);
    this.active = role !== undefined || this.isNew;
  }

  public checkedKeys: string[] = [];

  private permissonDictionary: { [id: string]: Permission; } = {}
  disabledViewAccessKeys = [];
  disabledKeys = [];

  @Input() public set permissionModel(permission: RoleItem[]) {
    let parent0Index = -1;
    let parent1Index = -1;
    var level0Index: number = -1;
    var level1Index: number = 0;
    var selectAll: boolean[] = [true,true,true];
    
    if (permission != undefined) {
      let groupSubsystemId = [];
      let groupSourceTypeId;
      var groupId = 0;

      this.checkedKeys = [];
      this.treeData = new Array<TreeNodeInfo>();
      
      var checkedParent: string = '';
      let groupSourceIndexId = 0;
      var indexId: number = 0;

      for (let permissionItem of permission) {

        parent0Index = permissionItem.groupSubsystemId-1;

        if (!groupSubsystemId.includes(permissionItem.groupSubsystemId)) {
          this.treeData.push(new TreeNodeInfo(permissionItem.groupSubsystemId-100, undefined, permissionItem.groupSubsystemName));
          groupSubsystemId.push(permissionItem.groupSubsystemId);
        }
        // بخاطر وجود نداشتن "فرمهای عملیاتی " در ساب سیستم راهبری " ایندکس بخش "گزارشات" اشتباه میشد
        if (groupSourceTypeId == '13') {
          parent1Index = 1;
        } else {
          parent1Index = permissionItem.groupSourceTypeId-1;
        }

        if (groupSourceTypeId !=
           permissionItem.groupSubsystemId.toString() + permissionItem.groupSourceTypeId.toString()
        ) {
          groupSourceTypeId = permissionItem.groupSubsystemId.toString() + permissionItem.groupSourceTypeId.toString();
          this.treeData.push(new TreeNodeInfo((+groupSourceTypeId)-100, permissionItem.groupSubsystemId-100, permissionItem.groupSourceTypeName));

          if (groupSourceTypeId == '13') {
            parent1Index = 1;
          } else {
            parent1Index = permissionItem.groupSourceTypeId-1;
          }

          checkedParent = parent0Index.toString() + '_' + parent1Index.toString();
          this.checkedKeys.push(checkedParent);

          groupSourceIndexId = this.checkedKeys.length - 1;
          level0Index = -1;
        }

        if (groupId != permissionItem.groupId &&
          groupSourceTypeId == permissionItem.groupSubsystemId.toString() +
            permissionItem.groupSourceTypeId.toString()
        )
        {
          this.treeData.push(new TreeNodeInfo(permissionItem.groupId, (+groupSourceTypeId)-100, permissionItem.groupName));

          level0Index++;
          level1Index = -1;

          checkedParent = parent0Index.toString() + '_' + parent1Index.toString() + '_' + level0Index.toString();

          this.checkedKeys.push(checkedParent);

          indexId = this.checkedKeys.length - 1;

          groupId = permissionItem.groupId;

        }

        if (groupId == permissionItem.groupId) {
          let newId = parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00');
          this.treeData.push(
            new TreeNodeInfo( newId,
            permissionItem.groupId,
            permissionItem.name )
          );
          if (
            !permissionItem.isEnabled &&
             permissionItem.flag == 1 &&
             (permissionItem.name == this.translate.instant('Role.ViewAccess') ||
              permissionItem.name == this.translate.instant('Role.ManageDashboardAccess')
             )
            )
             this.disabledViewAccessKeys.push(permissionItem.groupId);

          if (
            this.disabledViewAccessKeys.find(gId => gId == permissionItem.groupId) &&
            permissionItem.flag != 1
            )
             this.disabledKeys.push(newId);

          level1Index++;
        }
        
        if (permissionItem.isEnabled) {
          this.checkedKeys.push(parent0Index.toString() + '_' +
           parent1Index.toString() + '_' +
           level0Index.toString() + '_' +
           level1Index.toString()
          );
        }
        else {
          if (indexId >= 0 && this.checkedKeys[indexId].split('_').length == 3) {
            this.checkedKeys.splice(indexId, 1);
            indexId = -1;

            selectAll[permissionItem.groupSubsystemId-1] = false;
          }
          if (groupSourceIndexId >= 0 && this.checkedKeys[groupSourceIndexId].split('_').length == 2){
            selectAll[permissionItem.groupSubsystemId-1] = false;
            this.checkedKeys.splice(groupSourceIndexId, 1);
            groupSourceIndexId = -1;
          }
        }

        this.permissonDictionary[
          parent0Index.toString() + '_' +
          parent1Index.toString() + '_' +
          level0Index.toString() + '_' +
          level1Index.toString()
        ] = permissionItem;

      }
      selectAll.forEach((value,index) => {
        if (value) {
          this.checkedKeys.push(index.toString());
        }
      })
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
        if (checked.split('_').length == 4) {
          this.permissonDictionary[checked].isEnabled = true;
        }

        if (checked.split('_').length == 3) {
          allChildChecked.push(checked);
        }
      }

      for (let key in this.permissonDictionary) {
        //permissionItem.isEnabled = false;
        var parentKey: string = '';
        if (key.split('_').length == 4) parentKey = key.split('_')[0] + '_' + key.split('_')[1] + '_' + key.split('_')[2];

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
      this.disabledKeys = [];
      this.disabledViewAccessKeys = [];
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
    this.disabledKeys = [];
    this.disabledViewAccessKeys = [];
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }

  onCheckChange(event:TreeItemLookup) {
    let teammateItems;
    if (this.checkedKeys.find(index => index == event.item.index)) {
      if (event.children.length) {
        console.log('checked && hasChildren');
        teammateItems = event.children;
        this.enableItems(teammateItems);
      } else if (
        event.item.dataItem.name == this.translate.instant('Role.ViewAccess') ||
        event.item.dataItem.name == this.translate.instant('Role.ManageDashboardAccess')
      ) {
        teammateItems = Object.values(this.permissonDictionary).filter((item:any) => item.groupId == event.item.dataItem.parentId);
        teammateItems.forEach(item => {
          let newId = parseInt(item.id.toString() + item.groupId.toString() + '00');
          let index = this.disabledKeys.findIndex(id => id == newId);
          if (index > -1)
            this.disabledKeys.splice(index,1);
        });
      }
    } else {
      if (event.children.length) {
        // deSelected parent item
        console.log('deSelected parent item');
        teammateItems = event.children;
        let disableLowestLevelItems = (items) => {
          items.forEach(child2 => {
            if (!child2.children.length) {
              this.disableItem(child2);
            } else {
              disableLowestLevelItems(child2.children);
            }
          });
        }
        teammateItems.forEach(child => {
          if (child.children.length) {
            disableLowestLevelItems(child.children);
          } else {
            this.disableItem(child)
          }
        });
      } else if (
        event.item.dataItem.name == this.translate.instant('Role.ViewAccess') ||
        event.item.dataItem.name == this.translate.instant('Role.ManageDashboardAccess')
      ) {
        teammateItems = Object.values(this.permissonDictionary).filter((item:any) => item.groupId == event.item.dataItem.parentId)
        event.parent.children.forEach((item) => {
          // this.permissonDictionary[item.index].isEnabled = false;
          let index = this.checkedKeys.findIndex(i => i == item.index);
          if (index > -1) {
            this.checkedKeys.splice(index,1)
          }
        })
        teammateItems.forEach(item => {
          let newId = parseInt(item.id.toString() + item.groupId.toString() + '00');
          if (item.flag != 1 && !this.disabledKeys.find(i => i==newId)) {
            this.disabledKeys.push(newId);
          }
        });
      }
    }
  }

  ////Events

  enableItems(items) {
    items.forEach(child => {
      let index = this.disabledKeys.findIndex(id => id == child.item.dataItem.id);
      if (index > -1) 
        this.disabledKeys.splice(index,1);

      if (child.children)
        this.enableItems(child.children);

    });
  }

  disableItem(item) {
    if (item.item.dataItem.name != this.translate.instant('Role.ViewAccess') &&
      item.item.dataItem.name != this.translate.instant('Role.ManageDashboardAccess')
    ) {
      let indx = this.checkedKeys.findIndex(i => i == item.item.index);
      if (indx > -1)
        this.checkedKeys.splice(indx, 1);

      if (!this.disabledKeys.includes(item.item.dataItem.id))
        this.disabledKeys.push(item.item.dataItem.id);
    }
  }

  selectionKey(context: RowArgs) {
    return context.index.toString();
  }

  constructor(public bStorageService: BrowserStorageService,
    public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Role, ViewName.Role,elem);
  }

}
