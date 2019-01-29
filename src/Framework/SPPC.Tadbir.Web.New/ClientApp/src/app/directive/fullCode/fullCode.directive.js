"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var FullCodeDirective = /** @class */ (function () {
    function FullCodeDirective(el, renderer, fullCodeService) {
        this.el = el;
        this.renderer = renderer;
        this.fullCodeService = fullCodeService;
    }
    FullCodeDirective.prototype.ngAfterViewInit = function () {
        var _this = this;
        this.fullCodeElement = document.getElementById(this.sppcFullCode);
        if (this.apiUrl) {
            this.fullCodeService.getAll(this.apiUrl).subscribe(function (res) {
                var codeValue = _this.el.nativeElement.value;
                _this.parentFullCode = res.body;
                var fullCode = _this.parentFullCode + codeValue;
                if (fullCode.length > 0) {
                    _this.fullCodeElement.value = fullCode;
                    var event_1 = document.createEvent("Event");
                    event_1.initEvent('input', true, true);
                    Object.defineProperty(event_1, 'target', { value: _this.fullCodeElement, enumerable: true });
                    _this.renderer.invokeElementMethod(_this.fullCodeElement, 'dispatchEvent', [event_1]);
                }
            });
        }
    };
    FullCodeDirective.prototype.onEvent = function () {
        var code = this.el.nativeElement.value;
        this.fullCodeElement.value = this.parentFullCode + code;
        var event = document.createEvent("Event");
        event.initEvent('input', true, true);
        Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
        this.renderer.invokeElementMethod(this.fullCodeElement, 'dispatchEvent', [event]);
    };
    __decorate([
        core_1.Input()
    ], FullCodeDirective.prototype, "sppcFullCode", void 0);
    __decorate([
        core_1.Input()
    ], FullCodeDirective.prototype, "apiUrl", void 0);
    __decorate([
        core_1.HostListener('input')
    ], FullCodeDirective.prototype, "onEvent", null);
    FullCodeDirective = __decorate([
        core_1.Directive({
            selector: '[sppcFullCode]',
        })
    ], FullCodeDirective);
    return FullCodeDirective;
}());
exports.FullCodeDirective = FullCodeDirective;
//# sourceMappingURL=fullCode.directive.js.map