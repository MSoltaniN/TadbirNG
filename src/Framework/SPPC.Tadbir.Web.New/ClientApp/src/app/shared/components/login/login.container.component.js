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
var default_component_1 = require("../../class/default.component");
var LoginContainerComponent = /** @class */ (function (_super) {
    __extends(LoginContainerComponent, _super);
    function LoginContainerComponent(route, router, authenticationService, toastrService, translate, renderer, metadata, settingService) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, '', '') || this;
        _this.route = route;
        _this.router = router;
        _this.authenticationService = authenticationService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.step1 = false;
        _this.step2 = false;
        _this.step2 = false;
        return _this;
    }
    LoginContainerComponent.prototype.ngOnInit = function () {
        if (this.authenticationService.isRememberMe() || this.UserId != 0) {
            if (this.route.snapshot.queryParams['returnUrl'] == undefined) {
                this.step1 = false;
                this.step2 = true;
            }
            else {
                this.router.navigate(this.route.snapshot.queryParams['returnUrl']);
            }
        }
        else {
            this.step1 = true;
            this.step2 = false;
        }
    };
    LoginContainerComponent = __decorate([
        core_1.Component({
            selector: 'login-container',
            templateUrl: 'login.container.component.html',
            styleUrls: ['./login.container.component.css']
        })
    ], LoginContainerComponent);
    return LoginContainerComponent;
}(default_component_1.DefaultComponent));
exports.LoginContainerComponent = LoginContainerComponent;
//# sourceMappingURL=login.container.component.js.map