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
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var index_1 = require("../../model/index");
var detail_component_1 = require("../../class/detail.component");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var RoleFormComponent = /** @class */ (function (_super) {
    __extends(RoleFormComponent, _super);
    function RoleFormComponent(roleService, formBuilder, toastrService, translate, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.Role, environment_1.Metadatas.Role) || this;
        _this.roleService = roleService;
        _this.formBuilder = formBuilder;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.treeData = new Array();
        //create properties    
        _this.selectedRows = [];
        _this.active = false;
        _this.isNew = false;
        _this.errorMessage = '';
        _this.checkedKeys = [];
        _this.permissonDictionary = {};
        _this.cancel = new core_1.EventEmitter();
        _this.save = new core_1.EventEmitter();
        return _this;
    }
    Object.defineProperty(RoleFormComponent.prototype, "model", {
        set: function (role) {
            this.editForm.reset(role);
            this.active = role !== undefined || this.isNew;
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(RoleFormComponent.prototype, "permissionModel", {
        set: function (permission) {
            var level0Index = -1;
            var level1Index = 0;
            var selectAll = true;
            if (permission != undefined) {
                var groupId = 0;
                this.checkedKeys = [];
                this.treeData = new Array();
                if (this.CurrentLanguage == "fa")
                    this.treeData.push(new index_1.TreeNodeInfo(-1, undefined, "حسابداری"));
                else
                    this.treeData.push(new index_1.TreeNodeInfo(-1, undefined, "Accounting"));
                var checkedParent = '';
                var indexId = 0;
                for (var _i = 0, permission_1 = permission; _i < permission_1.length; _i++) {
                    var permissionItem = permission_1[_i];
                    if (groupId != permissionItem.groupId) {
                        this.treeData.push(new index_1.TreeNodeInfo(permissionItem.groupId, -1, permissionItem.groupName));
                        level0Index++;
                        level1Index = -1;
                        checkedParent = '0_' + level0Index.toString();
                        this.checkedKeys.push(checkedParent);
                        indexId = this.checkedKeys.length - 1;
                        groupId = permissionItem.groupId;
                    }
                    if (groupId == permissionItem.groupId) {
                        this.treeData.push(new index_1.TreeNodeInfo(parseInt(permissionItem.id.toString() + permissionItem.groupId.toString() + '00'), permissionItem.groupId, permissionItem.name));
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
        },
        enumerable: true,
        configurable: true
    });
    //create properties
    ////Events
    RoleFormComponent.prototype.onSave = function (e) {
        var _this = this;
        e.preventDefault();
        if (this.editForm.valid) {
            var permissionData = new Array();
            var allChildChecked = new Array();
            for (var key in this.permissonDictionary) {
                //permissionItem.isEnabled = false;
                this.permissonDictionary[key].isEnabled = false;
            }
            for (var _i = 0, _a = this.checkedKeys; _i < _a.length; _i++) {
                var checked = _a[_i];
                if (checked.split('_').length == 3) {
                    this.permissonDictionary[checked].isEnabled = true;
                }
                if (checked.split('_').length == 2) {
                    allChildChecked.push(checked);
                }
            }
            var _loop_1 = function (key) {
                //permissionItem.isEnabled = false;    
                parentKey = '';
                if (key.split('_').length == 3)
                    parentKey = key.split('_')[0] + '_' + key.split('_')[1];
                if (allChildChecked.filter(function (k) { return k == parentKey; }).length > 0) {
                    this_1.permissonDictionary[key].isEnabled = true;
                }
                if (permissionData.filter(function (p) { return p.id == _this.permissonDictionary[key].id; }).length == 0)
                    permissionData.push(this_1.permissonDictionary[key]);
            };
            var this_1 = this, parentKey;
            for (var key in this.permissonDictionary) {
                _loop_1(key);
            }
            var viewModel;
            viewModel = {
                id: this.editForm.value.id,
                role: this.editForm.value,
                permissions: permissionData
            };
            this.save.emit(viewModel);
            this.active = true;
            this.selectedRows = [];
        }
    };
    RoleFormComponent.prototype.onCancel = function (e) {
        e.preventDefault();
        this.selectedRows = [];
        this.closeForm();
    };
    RoleFormComponent.prototype.closeForm = function () {
        this.isNew = false;
        this.active = false;
        this.selectedRows = [];
        this.cancel.emit();
    };
    RoleFormComponent.prototype.escPress = function () {
        this.closeForm();
    };
    ////Events
    RoleFormComponent.prototype.selectionKey = function (context) {
        if (context.dataItem == undefined)
            return "";
        return context.dataItem.id;
    };
    __decorate([
        core_1.Input()
    ], RoleFormComponent.prototype, "isNew", void 0);
    __decorate([
        core_1.Input()
    ], RoleFormComponent.prototype, "errorMessage", void 0);
    __decorate([
        core_1.Input()
    ], RoleFormComponent.prototype, "model", null);
    __decorate([
        core_1.Input()
    ], RoleFormComponent.prototype, "permissionModel", null);
    __decorate([
        core_1.Output()
    ], RoleFormComponent.prototype, "cancel", void 0);
    __decorate([
        core_1.Output()
    ], RoleFormComponent.prototype, "save", void 0);
    RoleFormComponent = __decorate([
        core_1.Component({
            selector: 'role-form-component',
            styles: ["\n        input[type=text],textarea { width: 100%; }\n        .permission-dialog {width: 100% !important; min-width: 250px !important; height:100%}\n        /deep/ .permission-dialog .k-dialog{ height:100% !important; min-width: unset !important; }\n"
            ],
            templateUrl: './role-form.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], RoleFormComponent);
    return RoleFormComponent;
}(detail_component_1.DetailComponent));
exports.RoleFormComponent = RoleFormComponent;
//# sourceMappingURL=role-form.component.js.map