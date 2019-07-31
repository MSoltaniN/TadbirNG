"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var source_1 = require("../../class/source");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var index_1 = require("../../model/index");
var detail_component_1 = require("../../class/detail.component");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var RoleDetailFormComponent = /** @class */ (function (_super) {
    __extends(RoleDetailFormComponent, _super);
    function RoleDetailFormComponent() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.showloadingPermissionMessage = true;
        _this.showloadingBranchesMessage = true;
        _this.showloadingUsersMessage = true;
        _this.treeData = new Array();
        _this.permissonDictionary = {};
        _this.roleDetail = false;
        _this.errorMessage = '';
        _this.cancelRoleDetail = new core_1.EventEmitter();
        return _this;
    }
    Object.defineProperty(RoleDetailFormComponent.prototype, "roleDetails", {
        set: function (roleDetails) {
            var level0Index = -1;
            var level1Index = 0;
            if (roleDetails != undefined) {
                var groupId = 0;
                this.treeData = new Array();
                if (this.CurrentLanguage == "fa")
                    this.treeData.push(new index_1.TreeNodeInfo(-1, undefined, "حسابداری"));
                else
                    this.treeData.push(new index_1.TreeNodeInfo(-1, undefined, "Accounting"));
                var indexId = 0;
                var selectAll = true;
                var sortedPermission = roleDetails.permissions.sort(function (a, b) {
                    return a.id - b.id;
                });
                for (var _i = 0, sortedPermission_1 = sortedPermission; _i < sortedPermission_1.length; _i++) {
                    var permissionItem = sortedPermission_1[_i];
                    if (groupId != permissionItem.groupId) {
                        this.treeData.push(new index_1.TreeNodeInfo(permissionItem.groupId, -1, permissionItem.groupName));
                        level0Index++;
                        level1Index = -1;
                        groupId = permissionItem.groupId;
                    }
                    if (groupId == permissionItem.groupId) {
                        this.treeData.push(new index_1.TreeNodeInfo(parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00'), permissionItem.groupId, permissionItem.name));
                        level1Index++;
                    }
                    this.permissonDictionary['0_' + level0Index.toString() + '_' + level1Index.toString()] = permissionItem;
                }
                //this.gridBranchesData = roleDetails.branches;
                this.gridUsersData = roleDetails.users;
                this.roleName = roleDetails.role.name;
                this.roleDescription = roleDetails.role.description != null ? roleDetails.role.description : "";
                this.showloadingPermissionMessage = !(this.treeData.length == 0);
                //this.showloadingBranchesMessage = !(this.gridBranchesData.length == 0);
                this.showloadingUsersMessage = !(this.gridUsersData.length == 0);
            }
            //this.gridPermissionData = roleDetails.permissions;
        },
        enumerable: true,
        configurable: true
    });
    ////create properties
    //////Events
    RoleDetailFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.closeForm();
    };
    RoleDetailFormComponent.prototype.closeForm = function () {
        this.roleDetail = false;
        //this.gridBranchesData = undefined;
        this.gridUsersData = undefined;
        this.cancelRoleDetail.emit();
    };
    RoleDetailFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    ////Events
    RoleDetailFormComponent.prototype.getTitleText = function (text) {
        return source_1.String.Format(text, this.roleName);
    };
    __decorate([
        core_1.Input()
    ], RoleDetailFormComponent.prototype, "roleDetail", void 0);
    __decorate([
        core_1.Input()
    ], RoleDetailFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], RoleDetailFormComponent.prototype, "roleDetails", null);
    __decorate([
        core_1.Output()
    ], RoleDetailFormComponent.prototype, "cancelRoleDetail", void 0);
    RoleDetailFormComponent = __decorate([
        core_1.Component({
            selector: 'role-detail-form-component',
            styles: ["\n       .user-dialog {width: 100% !important; height:100% !important}\n       /deep/ .user-dialog .k-dialog{ height:100% !important; min-width: unset !important; }\n"
            ],
            templateUrl: './role-detail-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], RoleDetailFormComponent);
    return RoleDetailFormComponent;
}(detail_component_1.DetailComponent));
exports.RoleDetailFormComponent = RoleDetailFormComponent;
//# sourceMappingURL=role-detail-form.component.js.map