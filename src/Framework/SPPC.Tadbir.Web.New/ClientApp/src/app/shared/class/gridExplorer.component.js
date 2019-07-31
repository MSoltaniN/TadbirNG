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
var default_component_1 = require("./default.component");
var kendo_angular_grid_1 = require("@progress/kendo-angular-grid");
var GridExplorerComponent = /** @class */ (function (_super) {
    __extends(GridExplorerComponent, _super);
    function GridExplorerComponent(toastrService, translate, accountService, dialogService, renderer, metadata, settingService, entityName, metadataType) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, entityName, metadataType) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.accountService = accountService;
        _this.dialogService = dialogService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.entityName = entityName;
        _this.metadataType = metadataType;
        _this.firstTreeNode = [];
        _this.treeNodes = [];
        _this.expandedKeys = [-1];
        _this.selectedKeys = [-1];
        _this.breadCrumbList = [];
        _this.contextmenuItems = [
            { text: 'Account.New', icon: 'file-add', mode: 'New' },
            { text: 'Buttons.Edit', icon: 'edit', mode: 'Edit' },
            { text: 'Buttons.Delete', icon: 'delete', mode: 'Remove' }
        ];
        _this.contextmenuLimitedItems = [
            { text: 'Account.New', icon: 'file-add', mode: 'New' }
        ];
        _this.selectedRows = [];
        _this.groupDelete = false;
        _this.showloadingMessage = true;
        _this.clickedRowItem = undefined;
        return _this;
    }
    __decorate([
        core_1.ViewChild('treemenu')
    ], GridExplorerComponent.prototype, "treeContextMenu", void 0);
    __decorate([
        core_1.ViewChild('treemenulimited')
    ], GridExplorerComponent.prototype, "treeContextMenuLimited", void 0);
    __decorate([
        core_1.ViewChild(kendo_angular_grid_1.GridComponent)
    ], GridExplorerComponent.prototype, "grid", void 0);
    GridExplorerComponent = __decorate([
        core_1.Injectable()
    ], GridExplorerComponent);
    return GridExplorerComponent;
}(default_component_1.DefaultComponent));
exports.GridExplorerComponent = GridExplorerComponent;
//# sourceMappingURL=gridExplorer.component.js.map