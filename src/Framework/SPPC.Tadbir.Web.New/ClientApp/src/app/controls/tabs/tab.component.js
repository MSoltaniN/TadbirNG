"use strict";
/**
 * A single tab page. It renders the passed template
 * via the @Input properties by using the ngTemplateOutlet
 * and ngTemplateOutletContext directives.
 */
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var reportViewer_component_1 = require("../../components/reportViewer/reportViewer.component");
var TabComponent = /** @class */ (function () {
    function TabComponent() {
        this.active = false;
        this.isCloseable = false;
    }
    TabComponent.prototype.callViewer = function () {
        this.reportViewer.showReportViewer(this.template, this.dataContext);
    };
    __decorate([
        core_1.Input('tabTitle')
    ], TabComponent.prototype, "title", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "active", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "isCloseable", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "template", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "dataContext", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "isViewer", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "isDesigner", void 0);
    __decorate([
        core_1.Input()
    ], TabComponent.prototype, "Id", void 0);
    __decorate([
        core_1.ViewChild(reportViewer_component_1.ReportViewerComponent)
    ], TabComponent.prototype, "reportViewer", void 0);
    TabComponent = __decorate([
        core_1.Component({
            selector: 'my-tab',
            styles: [
                "\n    .pane{\n      padding: 1em;\n    }\n  "
            ],
            template: "\n    <div [hidden]=\"!active\" class=\"pane\">\n      <report-viewer #viewer [Id]=\"Id\">                                                                \n      </report-viewer>\n      <div [id]=\"Id\" *ngIf='isDesigner'></div>\n    </div>\n  "
        })
    ], TabComponent);
    return TabComponent;
}());
exports.TabComponent = TabComponent;
//# sourceMappingURL=tab.component.js.map