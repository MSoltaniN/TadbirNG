"use strict";
/**
 * The main component that renders single TabComponent
 * instances.
 */
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var tab_component_1 = require("./tab.component");
var dynamic_tabs_directive_1 = require("./dynamic-tabs.directive");
var TabsComponent = /** @class */ (function () {
    /*
      Alternative approach of using an anchor directive
      would be to simply get hold of a template variable
      as follows
    */
    // @ViewChild('container', {read: ViewContainerRef}) dynamicTabPlaceholder;
    function TabsComponent(_componentFactoryResolver) {
        this._componentFactoryResolver = _componentFactoryResolver;
        this.dynamicTabs = [];
        this.CloseConfirm = false;
        this.showHint = false;
    }
    // contentChildren are set
    TabsComponent.prototype.ngAfterContentInit = function () {
        // get all active tabs
        var activeTabs = this.tabs.filter(function (tab) { return tab.active; });
        // if there is no active tab set, activate the first
        if (activeTabs.length === 0) {
            this.selectTab(this.tabs.first);
        }
    };
    TabsComponent.prototype.close = function (flag) {
        if (flag)
            this.closeTab(this.currentTab);
        this.CloseConfirm = false;
    };
    TabsComponent.prototype.save = function () {
        this.currentTab.Manager.invokeSaveReport();
        this.closeTab(this.currentTab);
        this.CloseConfirm = false;
    };
    TabsComponent.prototype.openTab = function (title, template, data, isCloseable, isViewer, isDesigner, id, manager) {
        if (isCloseable === void 0) { isCloseable = false; }
        if (isViewer === void 0) { isViewer = false; }
        if (isDesigner === void 0) { isDesigner = false; }
        var prefix;
        if (isViewer)
            prefix = 'viewerTab';
        if (isDesigner)
            prefix = 'designerTab';
        if (this.dynamicTabs.filter(function (p) { return p.Id == prefix + id; }).length > 0) {
            var tab = this.dynamicTabs.filter(function (p) { return p.Id == prefix + id; })[0];
            this.selectTab(tab);
            if (isDesigner)
                return false;
            if (isViewer) {
                tab.template = template;
                tab.callViewer();
                return false;
            }
        }
        // get a component factory for our TabComponent
        var componentFactory = this._componentFactoryResolver.resolveComponentFactory(tab_component_1.TabComponent);
        // fetch the view container reference from our anchor directive
        var viewContainerRef = this.dynamicTabPlaceholder.viewContainer;
        // alternatively...
        // let viewContainerRef = this.dynamicTabPlaceholder;
        // create a component instance
        var componentRef = viewContainerRef.createComponent(componentFactory);
        // set the according properties on our component instance
        var instance = componentRef.instance;
        instance.title = title;
        instance.template = template;
        instance.dataContext = data;
        instance.isCloseable = isCloseable;
        instance.active = true;
        instance.isViewer = isViewer;
        instance.isDesigner = isDesigner;
        instance.Manager = manager;
        instance.Id = prefix + id;
        instance.reportViewer.Id = prefix + id;
        // remember the dynamic component for rendering the
        // tab navigation headers
        this.dynamicTabs.push(componentRef.instance);
        // set it active
        this.selectTab(this.dynamicTabs[this.dynamicTabs.length - 1]);
        if (isViewer)
            componentRef.instance.callViewer();
        return true;
    };
    TabsComponent.prototype.selectTab = function (tab) {
        // deactivate all tabs
        this.tabs.toArray().forEach(function (tab) { return (tab.active = false); });
        this.dynamicTabs.forEach(function (tab) { return (tab.active = false); });
        // activate the tab the user has clicked on.
        if (tab)
            tab.active = true;
    };
    TabsComponent.prototype.showCloseConfirm = function (tab) {
        if (tab.isDesigner) {
            this.currentTab = tab;
            //this.CloseConfirm = true;
            if (this.currentTab.template != this.currentTab.Manager.report.saveToJsonString()) {
                this.CloseConfirm = true;
            }
            else {
                this.closeTab(tab);
            }
        }
        else if (tab.isViewer) {
            this.closeTab(tab);
        }
    };
    TabsComponent.prototype.closeTab = function (tab) {
        for (var i = 0; i < this.dynamicTabs.length; i++) {
            if (this.dynamicTabs[i] === tab) {
                // remove the tab from our array
                this.dynamicTabs.splice(i, 1);
                // destroy our dynamically created component again
                var viewContainerRef = this.dynamicTabPlaceholder.viewContainer;
                // let viewContainerRef = this.dynamicTabPlaceholder;
                viewContainerRef.remove(i);
                // set tab index to 1st one
                this.selectTab(this.tabs.first);
                break;
            }
        }
    };
    TabsComponent.prototype.closeActiveTab = function () {
        var activeTabs = this.dynamicTabs.filter(function (tab) { return tab.active; });
        if (activeTabs.length > 0) {
            // close the 1st active tab (should only be one at a time)
            this.closeTab(activeTabs[0]);
        }
    };
    __decorate([
        core_1.ContentChildren(tab_component_1.TabComponent)
    ], TabsComponent.prototype, "tabs", void 0);
    __decorate([
        core_1.ViewChild(dynamic_tabs_directive_1.DynamicTabsDirective)
    ], TabsComponent.prototype, "dynamicTabPlaceholder", void 0);
    TabsComponent = __decorate([
        core_1.Component({
            selector: 'my-tabs',
            template: "\n    <ul class=\"nav nav-tabs reportTab\">\n      <li *ngFor=\"let tab of tabs\" (click)=\"selectTab(tab)\" [class.active]=\"tab.active\">        \n        <a class='tablTitle'>{{tab.title}}\n        <span class=\"tab-close\" *ngIf=\"tab.isCloseable\" (click)=\"closeTab(tab)\">x</span>\n        </a>\n      </li>\n      <!-- dynamic tabs -->\n      <li *ngFor=\"let tab of dynamicTabs\" (click)=\"selectTab(tab)\" [class.active]=\"tab.active\">        \n        <a class='tablTitle'>\n        <span *ngIf=\"tab.isDesigner\" class=\"k-icon k-i-pencil\"></span>\n        <span *ngIf=\"tab.isViewer\" class=\"k-icon k-i-eye\"></span>\n        {{tab.title}} <span class=\"tab-close\" *ngIf=\"tab.isCloseable\" \n        (click)=\"showCloseConfirm(tab)\">x</span></a>\n      </li>\n    </ul>\n    <ng-content></ng-content>\n    <ng-template dynamic-tabs #container></ng-template>\n    <kendo-dialog title=\"{{'Report.Close' | translate}}\" *ngIf=\"CloseConfirm\" (close)=\"close(false)\" [minWidth]=\"250\"\n        [width]=\"450\">\n        <p>\n          {{'Report.ConfirmMsg' | translate}}\n        </p>\n        <kendo-dialog-actions>\n                <button class=\"k-button\" (click)=\"save()\" primary=\"true\">{{ 'Buttons.Save' | translate }}</button>\n                <button class=\"k-button\" (click)=\"close(true)\" primary=\"true\">{{ 'Buttons.Close' | translate }}</button>\n                <button class=\"k-button\" (click)=\"close(false)\">{{ 'Buttons.Cancel' | translate }}</button>\n        </kendo-dialog-actions>\n    </kendo-dialog>\n  ",
            styles: [
                "\n    .tab-close {\n      color: gray;\n      text-align: right;\n      cursor: pointer;\n    }\n\n    .tablTitle {      \n      text-decoration: underline;\n      cursor: pointer;\n    }\n\n    .k-window .k-overlay { opacity: .6 !important; }\n    \n    "
            ]
        })
    ], TabsComponent);
    return TabsComponent;
}());
exports.TabsComponent = TabsComponent;
//# sourceMappingURL=tabs.component.js.map