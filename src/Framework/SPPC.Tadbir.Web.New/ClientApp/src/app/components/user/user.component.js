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
var index_1 = require("../../service/index");
var kendo_angular_grid_1 = require("@progress/kendo-angular-grid");
require("rxjs/Rx");
var source_1 = require("../../class/source");
var default_component_1 = require("../../class/default.component");
var environment_1 = require("../../../environments/environment");
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var index_2 = require("../../service/api/index");
var secureEntity_1 = require("../../security/secureEntity");
var permissions_1 = require("../../security/permissions");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var UserComponent = /** @class */ (function (_super) {
    __extends(UserComponent, _super);
    //#endregion
    //#region Constructor
    function UserComponent(toastrService, translate, sppcLoading, userService, renderer, metadata, settingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, environment_1.Entities.User, environment_1.Metadatas.User) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.sppcLoading = sppcLoading;
        _this.userService = userService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.selectedRows = [];
        _this.showloadingMessage = true;
        _this.rolesList = false;
        _this.editDataItem = undefined;
        return _this;
    }
    //#endregion
    //#region Events
    UserComponent.prototype.ngOnInit = function () {
        this.viewAccess = this.isAccess(secureEntity_1.SecureEntity.User, permissions_1.UserPermissions.View);
        this.reloadGrid();
    };
    UserComponent.prototype.selectionKey = function (context) {
        if (context.dataItem == undefined)
            return "";
        return context.dataItem.id;
    };
    UserComponent.prototype.onSelectedKeysChange = function (checkedState) {
        //if (this.selectedRows.length > 1)
        //    this.groupDelete = true;
        //else
        //    this.groupDelete = false;
    };
    UserComponent.prototype.filterChange = function (filter) {
        var isReload = false;
        if (this.currentFilter && this.currentFilter.children.length > filter.filters.length)
            isReload = true;
        this.currentFilter = this.getFilters(filter);
        if (isReload) {
            this.reloadGrid();
        }
    };
    UserComponent.prototype.sortChange = function (sort) {
        this.sort = sort.filter(function (f) { return f.dir != undefined; });
        this.reloadGrid();
    };
    UserComponent.prototype.pageChange = function (event) {
        this.skip = event.skip;
        this.reloadGrid();
    };
    UserComponent.prototype.editHandler = function (arg) {
        var _this = this;
        var recordId = this.selectedRows[0];
        this.grid.loading = true;
        this.userService.getById(source_1.String.Format(index_2.UserApi.User, recordId)).subscribe(function (res) {
            _this.editDataItem = res;
            _this.grid.loading = false;
        });
        this.isNew = false;
        this.errorMessage = '';
    };
    UserComponent.prototype.cancelHandler = function () {
        this.editDataItem = undefined;
        this.isNew = false;
        this.errorMessage = '';
    };
    UserComponent.prototype.saveHandler = function (model) {
        var _this = this;
        this.grid.loading = true;
        if (!this.isNew) {
            this.userService.edit(source_1.String.Format(index_2.UserApi.User, model.id), model)
                .subscribe(function (response) {
                _this.isNew = false;
                _this.editDataItem = undefined;
                _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
                _this.reloadGrid();
            }, (function (error) {
                _this.errorMessage = error;
                _this.grid.loading = false;
            }));
        }
        else {
            this.userService.insert(index_2.UserApi.Users, model)
                .subscribe(function (response) {
                _this.isNew = false;
                _this.editDataItem = undefined;
                _this.showMessage(_this.insertMsg, environment_1.MessageType.Succes);
                var insertedModel = response;
                _this.reloadGrid(insertedModel);
            }, (function (error) {
                _this.isNew = true;
                _this.errorMessage = error;
                _this.grid.loading = false;
            }));
        }
    };
    UserComponent.prototype.rolesHandler = function (userId) {
        var _this = this;
        this.rolesList = true;
        this.grid.loading = true;
        this.userService.getUserRoles(userId).subscribe(function (res) {
            _this.userRolesData = res;
            _this.grid.loading = false;
        });
        this.errorMessage = '';
    };
    UserComponent.prototype.cancelUserRolesHandler = function () {
        this.rolesList = false;
        this.errorMessage = '';
    };
    UserComponent.prototype.saveUserRolesHandler = function (userRoles) {
        var _this = this;
        this.grid.loading = true;
        this.userService.modifiedUserRoles(userRoles)
            .subscribe(function (response) {
            _this.rolesList = false;
            _this.showMessage(_this.getText("User.UpdateRoles"), environment_1.MessageType.Succes);
            _this.grid.loading = false;
        }, (function (error) {
            _this.grid.loading = false;
            _this.errorMessage = error;
        }));
    };
    //#endregion
    //#region Methods
    UserComponent.prototype.reloadGridEvent = function () {
        this.reloadGrid();
    };
    UserComponent.prototype.reloadGrid = function (insertedModel) {
        var _this = this;
        if (this.viewAccess) {
            this.grid.loading = true;
            var filter = this.currentFilter;
            if (this.totalRecords == this.skip && this.totalRecords != 0) {
                this.skip = this.skip - this.pageSize;
            }
            if (insertedModel)
                this.goToLastPage(this.totalRecords);
            this.userService.getAll(source_1.String.Format(index_2.UserApi.Users, this.FiscalPeriodId, this.BranchId), this.pageIndex, this.pageSize, this.sort, filter).subscribe(function (res) {
                var resData = res.body;
                var totalCount = 0;
                if (res.headers != null) {
                    var headers = res.headers != undefined ? res.headers : null;
                    if (headers != null) {
                        var retheader = headers.get('X-Total-Count');
                        if (retheader != null)
                            totalCount = parseInt(retheader.toString());
                    }
                }
                _this.rowData = {
                    data: resData,
                    total: totalCount
                };
                _this.showloadingMessage = !(resData.length == 0);
                _this.totalRecords = totalCount;
                _this.grid.loading = false;
            });
        }
        else {
            this.rowData = {
                data: [],
                total: 0
            };
        }
    };
    UserComponent.prototype.addNew = function () {
        this.isNew = true;
        this.editDataItem = new index_1.UserInfo();
        this.errorMessage = '';
    };
    __decorate([
        core_1.ViewChild(kendo_angular_grid_1.GridComponent)
    ], UserComponent.prototype, "grid", void 0);
    UserComponent = __decorate([
        core_1.Component({
            selector: 'user',
            templateUrl: './user.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        })
    ], UserComponent);
    return UserComponent;
}(default_component_1.DefaultComponent));
exports.UserComponent = UserComponent;
//# sourceMappingURL=user.component.js.map