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
var permissions_1 = require("../../security/permissions");
var default_component_1 = require("../../class/default.component");
var SppcCheckAccess = /** @class */ (function () {
    function SppcCheckAccess(renderer, elRef, enviroment, parentComponet, permissionKeys, defaultComponent) {
        this.renderer = renderer;
        this.elRef = elRef;
        this.enviroment = enviroment;
        this.parentComponet = parentComponet;
        this.permissionKeys = permissionKeys;
        this.defaultComponent = defaultComponent;
    }
    SppcCheckAccess.prototype.ngOnInit = function () {
        var checkAccess = this.elRef.nativeElement.attributes.sppccheckaccess;
        var permissions = checkAccess.nodeValue;
        var permissionId = 0;
        var entityType = this.parentComponet._view.component.entityType;
        var allPermission = permissions.split(',');
        for (var pr in allPermission) {
            if (pr == "")
                continue;
            permissionId = this.permissionKeys.getPermission(entityType, allPermission[pr]);
            if (permissionId > 0)
                var isAccess = false;
            isAccess = this.enviroment.isAccess(entityType, permissionId);
            if (isAccess == false) {
                if (allPermission[pr] == "View") {
                    while (this.elRef.nativeElement.hasChildNodes()) {
                        this.elRef.nativeElement.removeChild(this.elRef.nativeElement.lastChild);
                    }
                    var notViewAccessText = this.defaultComponent.getText('App.NotViewAccess');
                    var textElement = this.renderer.createText(notViewAccessText);
                    this.renderer.appendChild(this.elRef.nativeElement, textElement);
                }
                else {
                    this.elRef.nativeElement.remove(); // setElementStyle(elRef.nativeElement, 'display', 'none');
                }
                //elRef.nativeElement.outerHTML = '';
            }
        }
    };
    __decorate([
        core_1.Input('SppcCheckAccess')
    ], SppcCheckAccess.prototype, "value", void 0);
    SppcCheckAccess = __decorate([
        core_1.Directive({
            selector: '[SppcCheckAccess]',
            providers: [enviroment_component_1.EnviromentComponent, permissions_1.Permissions, default_component_1.DefaultComponent]
        })
    ], SppcCheckAccess);
    return SppcCheckAccess;
}());
exports.SppcCheckAccess = SppcCheckAccess;
//# sourceMappingURL=sppc-check-access.js.map