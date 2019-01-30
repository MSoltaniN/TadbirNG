"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var enviroment_component_1 = require("../../class/enviroment.component");
var default_component_1 = require("../../class/default.component");
var SppcViewTreeConfig = /** @class */ (function () {
    //@Input('SppcViewTreeConfig') value: string;
    function SppcViewTreeConfig(renderer, elRef, enviroment, parentComponet, defaultComponent) {
        this.renderer = renderer;
        this.elRef = elRef;
        this.enviroment = enviroment;
        this.parentComponet = parentComponet;
        this.defaultComponent = defaultComponent;
    }
    SppcViewTreeConfig.prototype.ngOnInit = function () {
        ////debugger;
        //var level = 4;
        //let config: ViewTreeConfig;
        //config = this.defaultComponent.getViewTreeSettings(1);
        //var configLevels = config.levels.filter(f => f != null);
        //var configLevel = config.levels.filter(f => f != null && f.no == level);
        //if (configLevel.length == 0) {
        //  this.elRef.nativeElement.remove();
        //}
        //////var checkAccess = this.elRef.nativeElement.attributes.sppccheckaccess;
        //////var permissions: string = checkAccess.nodeValue;
        //////var permissionId: number = 0;
        //////var entityType = (<any>this.parentComponet)._view.component.entityType
        ////    permissionId = this.permissionKeys.getPermission(entityType, allPermission[pr]);
        ////    if (permissionId > 0)
        ////        var isAccess: boolean = false;
        ////        isAccess = this.enviroment.isAccess(entityType, permissionId);
        ////        if (isAccess == false) {
        ////            if (allPermission[pr] == "View") {
        ////                while (this.elRef.nativeElement.hasChildNodes()) {
        ////                    this.elRef.nativeElement.removeChild(this.elRef.nativeElement.lastChild);
        ////                }
        ////                var notViewAccessText = this.defaultComponent.getText('App.NotViewAccess');
        ////                var textElement = this.renderer.createText(notViewAccessText);
        ////                this.renderer.appendChild(this.elRef.nativeElement, textElement);
        ////            }
        ////            else {
        ////                this.elRef.nativeElement.remove();// setElementStyle(elRef.nativeElement, 'display', 'none');
        ////            }
        ////            //elRef.nativeElement.outerHTML = '';
        ////        }  
    };
    SppcViewTreeConfig = __decorate([
        core_1.Directive({
            selector: '[SppcViewTreeConfig]',
            providers: [enviroment_component_1.EnviromentComponent, default_component_1.DefaultComponent]
        })
    ], SppcViewTreeConfig);
    return SppcViewTreeConfig;
}());
exports.SppcViewTreeConfig = SppcViewTreeConfig;
//# sourceMappingURL=sppc-viewTree-config.js.map