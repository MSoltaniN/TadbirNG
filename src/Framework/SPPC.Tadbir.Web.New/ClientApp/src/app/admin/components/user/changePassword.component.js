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
var forms_1 = require("@angular/forms");
var environment_1 = require("../../../environments/environment");
var detail_component_1 = require("../../class/detail.component");
var ChangePasswordComponent = /** @class */ (function (_super) {
    __extends(ChangePasswordComponent, _super);
    function ChangePasswordComponent(toastrService, translate, sppcLoading, userService, renderer, metadata) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, environment_1.Entities.Password, environment_1.Metadatas.User) || this;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.sppcLoading = sppcLoading;
        _this.userService = userService;
        _this.renderer = renderer;
        _this.metadata = metadata;
        ////create a form controls
        _this.editForm1 = new forms_1.FormGroup({
            userName: new forms_1.FormControl(""),
            oldPassword: new forms_1.FormControl("", [forms_1.Validators.required]),
            newPassword: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.minLength(6), forms_1.Validators.maxLength(32)]),
            repeatPassword: new forms_1.FormControl("", [forms_1.Validators.required, forms_1.Validators.minLength(6), forms_1.Validators.maxLength(32)]),
        });
        _this.user_Name = "";
        _this.errorMessage = "";
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            _this.user_Name = currentContext ? currentContext.userName.toString() : "";
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            _this.user_Name = currentContext ? currentContext.userName.toString() : "";
        }
        return _this;
    }
    //Events
    ChangePasswordComponent.prototype.onSave = function (e) {
        var _this = this;
        e.preventDefault();
        //this.sppcLoading.show();
        this.model = this.editForm1.value;
        this.model.userName = this.user_Name;
        this.userService.changePassword(this.model).subscribe(function (res) {
            _this.editForm1.reset();
            _this.errorMessage = "";
            _this.showMessage(_this.updateMsg, environment_1.MessageType.Succes);
        }, (function (error) {
            _this.errorMessage = error;
        }));
        //this.sppcLoading.hide();
    };
    ChangePasswordComponent = __decorate([
        core_1.Component({
            selector: 'changePassword-component',
            styles: [
                "input[type=text],input[type=password] { width: 100%; }"
            ],
            templateUrl: './changePassword.component.html'
        })
    ], ChangePasswordComponent);
    return ChangePasswordComponent;
}(detail_component_1.DetailComponent));
exports.ChangePasswordComponent = ChangePasswordComponent;
//# sourceMappingURL=changePassword.component.js.map