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
var __param = (this && this.__param) || function (paramIndex, decorator) {
    return function (target, key) { decorator(target, key, paramIndex); }
};
Object.defineProperty(exports, "__esModule", { value: true });
var kendo_angular_l10n_1 = require("@progress/kendo-angular-l10n");
var environment_1 = require("../../../../environments/environment");
var core_1 = require("@angular/core");
var base_component_1 = require("../../../class/base.component");
var settings_service_1 = require("../../../service/settings.service");
var viewName_1 = require("../../../security/viewName");
function getLayoutModule(layout) {
    return layout.getLayout();
}
exports.getLayoutModule = getLayoutModule;
var AutoGeneratedGridSettingComponent = /** @class */ (function (_super) {
    __extends(AutoGeneratedGridSettingComponent, _super);
    function AutoGeneratedGridSettingComponent(toastrService, translate, settingService, grid, elRef, defaultComponent) {
        var _this = _super.call(this, toastrService) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.settingService = settingService;
        _this.grid = grid;
        _this.elRef = elRef;
        _this.defaultComponent = defaultComponent;
        _this.displayBtnSetting = true;
        _this.columnsList = new core_1.EventEmitter();
        _this.rowData = null;
        _this.gridRowData = null;
        //public rowData: GridDataResult;
        /**
         *ستون های قابل نمایش در گرید
         */
        _this.gridColumn = [];
        /**
       *تمام ستون های گرید که در کش ذخیره شده یا از سرویس خوانده میشوند
       */
        _this.allGridColumn = [];
        return _this;
    }
    Object.defineProperty(AutoGeneratedGridSettingComponent.prototype, "entityTypeName", {
        set: function (entityName) {
            this.viewId = viewName_1.ViewName[entityName];
        },
        enumerable: true,
        configurable: true
    });
    Object.defineProperty(AutoGeneratedGridSettingComponent.prototype, "metadataType", {
        set: function (metaType) {
            this.rowData = null;
            this.gridRowData = null;
            this.gridColumn = [];
            switch (this.metadataUrlType) {
                case 1:
                    this.allGridColumn = this.defaultComponent.getAllMetaData(metaType);
                    break;
                case 2:
                    this.allGridColumn = this.defaultComponent.getAllMetaDataByViewId(this.viewId, metaType);
                    break;
                default:
            }
            this.loadSetting();
        },
        enumerable: true,
        configurable: true
    });
    AutoGeneratedGridSettingComponent.prototype.ngOnDestroy = function () {
        //#region Save View Setting
        if (!this.viewId)
            return;
        var currentSetting = this.settingService.getSettingByViewId(this.viewId);
        if (currentSetting)
            this.settingService.putUserSettings(this.UserId, currentSetting).subscribe(function (response) {
            }, (function (error) {
            }));
        //#endregion
    };
    AutoGeneratedGridSettingComponent.prototype.ngOnInit = function () {
    };
    /** چپ چین کردن دکمه تنظیمات و لود کردن ستون ها در گرید */
    AutoGeneratedGridSettingComponent.prototype.loadSetting = function () {
        var _this = this;
        if (this.CurrentLanguage == 'fa')
            this.rtl = true;
        else
            this.rtl = false;
        if (!this.viewId)
            return;
        var currentSetting = this.settingService.getSettingByViewId(this.viewId);
        this.setSettingGridRow(currentSetting);
        var size = this.screenSize;
        if (currentSetting) {
            this.rowData = currentSetting;
            currentSetting.columnViews.forEach(function (item) {
                var columnSetting = item[size];
                if (columnSetting.visibility == environment_1.ColumnVisibility.AlwaysVisible ||
                    columnSetting.visibility == environment_1.ColumnVisibility.Visible ||
                    columnSetting.visibility == environment_1.ColumnVisibility.Default) {
                    var colItem = _this.allGridColumn.filter(function (f) { return f.name.toLowerCase() == item.name.toLowerCase(); });
                    if (colItem.length > 0) {
                        _this.gridColumn.push(colItem[0]);
                    }
                }
            });
            this.allGridColumn.forEach(function (item) {
                var colItem = JSON.parse(item.settings);
                if (colItem[size].visibility == environment_1.ColumnVisibility.AlwaysVisible &&
                    _this.gridColumn.filter(function (f) { return f.name.toLowerCase() == item.name.toLowerCase(); }).length == 0) {
                    _this.gridColumn.push(item);
                }
            });
        }
        else {
            this.rowData = new settings_service_1.ListFormViewConfigInfo(this.viewId, 10);
            this.allGridColumn.forEach(function (item) {
                if (_this.gridRowData.filter(function (f) { return f.name.toLowerCase() == item.name.toLowerCase(); }).length > 0) {
                    _this.gridColumn.push(item);
                }
                _this.rowData.columnViews.push(JSON.parse(item.settings));
            });
        }
        this.emitGridColumns();
    };
    AutoGeneratedGridSettingComponent.prototype.setSettingGridRow = function (currentSetting) {
        var _this = this;
        this.gridRowData = [];
        var size = this.screenSize;
        this.allGridColumn.forEach(function (item) {
            var itemSetting = JSON.parse(item.settings);
            var rowItem = new settings_service_1.SettingViewModelInfo();
            rowItem.name = itemSetting.name;
            var columnView;
            columnView = itemSetting[size];
            if (columnView.visibility != environment_1.ColumnVisibility.AlwaysHidden) {
                rowItem.designIndex = columnView.designIndex;
                rowItem.index = columnView.index;
                rowItem.title = columnView.title;
                rowItem.width = columnView.width;
                rowItem.visibility = columnView.visibility == environment_1.ColumnVisibility.AlwaysVisible ||
                    columnView.visibility == environment_1.ColumnVisibility.Visible || columnView.visibility == environment_1.ColumnVisibility.Default ?
                    true : false;
                rowItem.disabled = columnView.visibility == environment_1.ColumnVisibility.AlwaysVisible ? true : false;
                if (currentSetting) {
                    var currentSettingItem = currentSetting.columnViews.filter(function (f) { return f.name.toLowerCase() == rowItem.name.toLowerCase(); });
                    if (currentSettingItem.length > 0 && (currentSettingItem[0][size].visibility == environment_1.ColumnVisibility.Hidden || currentSettingItem[0][size].visibility == environment_1.ColumnVisibility.AlwaysHidden)) {
                        rowItem.visibility = false;
                    }
                }
                _this.gridRowData.push(JSON.parse(JSON.stringify(rowItem)));
            }
        });
        this.gridRowData.sort(function (obj1, obj2) {
            if (obj1.index > obj2.index) {
                return 1;
            }
            if (obj1.index < obj2.index) {
                return -1;
            }
            return 0;
        });
    };
    /**
     * رویداد نمایش یا عدم نمایش ستون در گرید و ذخیره آن در حافظه مرورگر
     * @param name نام ستون مربوطه در گرید
     * @param event پارامتر رویداد
     */
    AutoGeneratedGridSettingComponent.prototype.changeVisibility = function (dataItem, event) {
        if (!this.viewId)
            return;
        var hidden = !event.target.checked;
        var rowItemIndex = this.gridRowData.indexOf(dataItem);
        this.gridRowData[rowItemIndex].visibility = !hidden;
        var size = this.screenSize;
        if (hidden) {
            this.gridColumn = this.gridColumn.filter(function (f) { return f.name.toLowerCase() != dataItem.name.toLowerCase(); });
            var rowdataItem = this.rowData.columnViews.filter(function (f) { return f.name.toLowerCase() == dataItem.name.toLowerCase(); });
            if (rowdataItem.length > 0) {
                rowdataItem[0][size].visibility = environment_1.ColumnVisibility.Hidden;
            }
        }
        else {
            var item = this.allGridColumn.filter(function (f) { return f.name.toLowerCase() == dataItem.name.toLowerCase(); });
            if (item.length > 0) {
                this.gridColumn.push(item[0]);
                var rowdataItem = this.rowData.columnViews.filter(function (f) { return f.name.toLowerCase() == dataItem.name.toLowerCase(); });
                if (rowdataItem.length > 0) {
                    rowdataItem[0][size].visibility = environment_1.ColumnVisibility.Visible;
                }
            }
        }
        this.emitGridColumns();
        if (this.rowData) {
            this.settingService.setSettingByViewId(this.viewId, this.rowData);
        }
    };
    /**
     *لیست ستون های گرید را مرتب میکند و برای نمایش به گرید میفرستد
     * */
    AutoGeneratedGridSettingComponent.prototype.emitGridColumns = function () {
        var size = this.screenSize;
        this.gridColumn.sort(function (obj1, obj2) {
            var settingsObj1 = JSON.parse(obj1.settings);
            var settingsObj2 = JSON.parse(obj2.settings);
            if (settingsObj1[size].designIndex > settingsObj2[size].designIndex) {
                return 1;
            }
            if (settingsObj1[size].designIndex < settingsObj2[size].designIndex) {
                return -1;
            }
            return 0;
        });
        this.columnsList.emit(this.gridColumn);
    };
    /** نمایش فرم تنظیمات گرید */
    AutoGeneratedGridSettingComponent.prototype.showSetting = function () {
        this.show = true;
    };
    /** بستن فرم تنظیمات گرید */
    AutoGeneratedGridSettingComponent.prototype.closeSetting = function () {
        this.show = false;
    };
    __decorate([
        core_1.Input()
    ], AutoGeneratedGridSettingComponent.prototype, "metadataUrlType", void 0);
    __decorate([
        core_1.Input()
    ], AutoGeneratedGridSettingComponent.prototype, "entityTypeName", null);
    __decorate([
        core_1.Input()
    ], AutoGeneratedGridSettingComponent.prototype, "metadataType", null);
    __decorate([
        core_1.Input()
    ], AutoGeneratedGridSettingComponent.prototype, "displayBtnSetting", void 0);
    __decorate([
        core_1.Output()
    ], AutoGeneratedGridSettingComponent.prototype, "columnsList", void 0);
    AutoGeneratedGridSettingComponent = __decorate([
        core_1.Component({
            selector: 'auto-generated-grid-setting',
            templateUrl: './auto-generated-grid-setting.component.html',
            providers: [{
                    provide: kendo_angular_l10n_1.RTL,
                    useFactory: getLayoutModule,
                    deps: [environment_1.Layout]
                }]
        }),
        __param(3, core_1.Host()), __param(5, core_1.Host())
    ], AutoGeneratedGridSettingComponent);
    return AutoGeneratedGridSettingComponent;
}(base_component_1.BaseComponent));
exports.AutoGeneratedGridSettingComponent = AutoGeneratedGridSettingComponent;
//# sourceMappingURL=auto-generated-grid-setting.component.js.map