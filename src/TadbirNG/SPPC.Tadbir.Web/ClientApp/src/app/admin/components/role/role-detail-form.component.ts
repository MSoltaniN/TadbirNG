import { Component, EventEmitter, Input, Output } from '@angular/core';
import { String } from '@sppc/shared/class/source';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { Permission } from '@sppc/core';
import { Layout } from '@sppc/shared/enum/metadata';
import { RoleDetails, RoleItem } from '@sppc/admin/models';
import { TreeNodeInfo } from '@sppc/shared/models';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-detail-form-component',
  styles: [`
       .user-dialog {width: 100% !important; height:100% !important}
       ::ng-deep .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }
`
  ],
  templateUrl: './role-detail-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class RoleDetailFormComponent extends DetailComponent {


  ////create properties
  public gridUsersData: any;

  public RoleIsNotAdmin:boolean = true;
  public showloadingPermissionMessage: boolean = true;
  public showloadingBranchesMessage: boolean = true;
  public showloadingUsersMessage: boolean = true;

  public roleName: string;
  public treeData: TreeNodeInfo[] = new Array<TreeNodeInfo>();
  public roleDescription: string;

  private permissonDictionary: { [id: string]: Permission; } = {}


  @Input() public roleDetail: boolean = false;
  //@Input() public errorMessage: string = '';


  @Input() public set roleDetails(roleDetails: RoleDetails) {

    let groupSubsystemId = [];
    let groupSourceTypeId = [];
    let groupId=[];
    // var level0Index: number = -1;
    // var level1Index: number = 0;

    if (roleDetails != undefined) {

      this.treeData = new Array<TreeNodeInfo>();

      enum PermissionsTreeNodeInfoLevel{
        groupSubsystem='1',
        groupSourceType='2',
        group='3'
      }

      var sortedPermission = roleDetails.permissions.sort(function (a: RoleItem, b: RoleItem) {
        return a.groupSourceTypeId - b.groupSourceTypeId;
      });
      
      for (let permissionItem of sortedPermission as RoleItem[]) {

        let permissionItemGroupSourceTypeId = permissionItem.groupSubsystemId.toString() + "0" + permissionItem.groupSourceTypeId;
       
        if(!groupSubsystemId.includes(permissionItem.groupSubsystemId)){
          groupSubsystemId.push(permissionItem.groupSubsystemId);
          this.treeData.push(new TreeNodeInfo(parseFloat(
            permissionItem.groupSubsystemId.toString() + '.' + PermissionsTreeNodeInfoLevel.groupSubsystem ),
            undefined,
            permissionItem.groupSubsystemName))
        }

        if(!groupSourceTypeId.includes(permissionItemGroupSourceTypeId)){
          groupSourceTypeId.push(permissionItemGroupSourceTypeId)
          this.treeData.push(
            new TreeNodeInfo(
              parseFloat(permissionItemGroupSourceTypeId.toString() + '.' + PermissionsTreeNodeInfoLevel.groupSourceType),
              parseFloat(permissionItem.groupSubsystemId.toString() + '.' + PermissionsTreeNodeInfoLevel.groupSubsystem ),
            permissionItem.groupSourceTypeName))
        }
              
        if(!groupId.includes( permissionItem.groupId)){
          groupId.push(permissionItem.groupId)
          this.treeData.push(new TreeNodeInfo(
            parseFloat(permissionItem.groupId.toString() + '.' + PermissionsTreeNodeInfoLevel.group),
            parseFloat(permissionItemGroupSourceTypeId + '.' + PermissionsTreeNodeInfoLevel.groupSourceType),
            permissionItem.groupName));
        }
        
        this.treeData.push(
          new TreeNodeInfo( 
            permissionItem.id,
            parseFloat(permissionItem.groupId.toString() + '.' + PermissionsTreeNodeInfoLevel.group),
            permissionItem.name )
        );


      }
      //this.gridBranchesData = roleDetails.branches;
      this.gridUsersData = roleDetails.users;
      this.RoleIsNotAdmin = roleDetails.role.id != 1;
      this.roleName = roleDetails.role.name;
      this.roleDescription = roleDetails.role.description != null ? roleDetails.role.description : "";

      this.showloadingPermissionMessage = !(this.treeData.length == 0);
      //this.showloadingBranchesMessage = !(this.gridBranchesData.length == 0);
      this.showloadingUsersMessage = !(this.gridUsersData.length == 0);


    }

    //this.gridPermissionData = roleDetails.permissions;

  }

  @Output() cancelRoleDetail: EventEmitter<any> = new EventEmitter();
  ////create properties



  //////Events

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.roleDetail = false;
    //this.gridBranchesData = undefined;
    this.gridUsersData = undefined;
    this.cancelRoleDetail.emit();
  }

  escPress() {
    this.closeForm();
  }
  ////Events



  getTitleText(text: string) {
    return String.Format(text, this.roleName);
  }

}
