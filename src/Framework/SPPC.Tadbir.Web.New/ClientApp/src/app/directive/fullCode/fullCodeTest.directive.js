"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var FullCodeTestDirective = /** @class */ (function () {
    function FullCodeTestDirective(el, renderer, fullCodeService) {
        this.el = el;
        this.renderer = renderer;
        this.fullCodeService = fullCodeService;
    }
    FullCodeTestDirective.prototype.ngAfterViewInit = function () {
        this.fullCodeElement = document.getElementById(this.sppcFullCodeTest);
    };
    FullCodeTestDirective.prototype.onEvent = function () {
        var code = this.el.nativeElement.value;
        this.fullCodeElement.value = this.parentFullCode + code;
        var event = document.createEvent("Event");
        event.initEvent('input', true, true);
        Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
        this.renderer.invokeElementMethod(this.fullCodeElement, 'dispatchEvent', [event]);
    };
    __decorate([
        core_1.Input()
    ], FullCodeTestDirective.prototype, "sppcFullCodeTest", void 0);
    __decorate([
        core_1.Input()
    ], FullCodeTestDirective.prototype, "parentFullCode", void 0);
    __decorate([
        core_1.HostListener('input')
    ], FullCodeTestDirective.prototype, "onEvent", null);
    FullCodeTestDirective = __decorate([
        core_1.Directive({
            selector: '[sppcFullCodeTest]',
        })
    ], FullCodeTestDirective);
    return FullCodeTestDirective;
}());
exports.FullCodeTestDirective = FullCodeTestDirective;
//# sourceMappingURL=fullCodeTest.directive.js.map