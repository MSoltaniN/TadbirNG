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
var core_1 = require("@angular/core");
var default_component_1 = require("../../class/default.component");
var core_2 = require("@angular/core");
var common_1 = require("@angular/common");
var LoginComponent = /** @class */ (function (_super) {
    __extends(LoginComponent, _super);
    function LoginComponent(route, router, authenticationService, toastrService, translate, parent, renderer, metadata, settingService, document) {
        var _this = _super.call(this, toastrService, translate, renderer, metadata, settingService, '', '') || this;
        _this.route = route;
        _this.router = router;
        _this.authenticationService = authenticationService;
        _this.toastrService = toastrService;
        _this.translate = translate;
        _this.parent = parent;
        _this.renderer = renderer;
        _this.metadata = metadata;
        _this.settingService = settingService;
        _this.document = document;
        _this.model = {};
        _this.loading = false;
        _this.firstStep = true;
        _this.ticket = '';
        _this.lang = '';
        _this.stepOne = true;
        _this.lang = _this.currentlang;
        return _this;
    }
    LoginComponent.prototype.ngOnInit = function () {
        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
    };
    LoginComponent.prototype.changeLang = function (language) {
        this.changeLanguage(language);
        this.lang = language;
        if (this.currentlang == 'fa') {
            this.renderer.addClass(document.body, 'tRtl');
            this.renderer.removeClass(document.body, 'tLtr');
            if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans.css')
                this.document.getElementById('sppcFont').setAttribute('href', 'assets/resources/IranSans.css');
        }
        if (this.currentlang == 'en') {
            this.renderer.addClass(document.body, 'tLtr');
            this.renderer.removeClass(document.body, 'tRtl');
            if (this.document.getElementById('sppcFont').getAttribute('href') != 'assets/resources/IranSans-en.css')
                this.document.getElementById('sppcFont').setAttribute('href', 'assets/resources/IranSans-en.css');
        }
        // if(language == 'fa')
        // {
        //     if(this.document.getElementById('adminlte').getAttribute('href') != '../assets/dist/css/AdminLTE.Rtl.css')
        //        this.document.getElementById('adminlte').setAttribute('href', '../assets/dist/css/AdminLTE.Rtl.css');
        //     // this.cssUrl = '../assets/dist/css/AdminLTE.Rtl.css';
        // }
        // else
        // {
        //    if(this.document.getElementById('adminlte').getAttribute('href') != '../assets/dist/css/AdminLTE.min.css')
        //        this.document.getElementById('adminlte').setAttribute('href', '../assets/dist/css/AdminLTE.min.css');
        //     //this.cssUrl = '../assets/dist/css/AdminLTE.min.css';
        // }
        // if(this.currentlang == 'fa')
        //     this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.Rtl.css');
        //  else
        //     this.document.getElementById('adminlte').setAttribute('href', 'assets/dist/css/AdminLTE.min.css');
    };
    LoginComponent.prototype.disableLink = function (fileName) {
        var links = document.getElementsByTagName("link");
        for (var i = 0; i < links.length; i++) {
            var link = links[i];
            if (link.getAttribute("rel").indexOf("style") != -1 && link.getAttribute("href")) {
                //link.disabled = true;
                if (link.getAttribute("href") === fileName)
                    link.disabled = true;
            }
        }
    };
    LoginComponent.prototype.login = function () {
        var _this = this;
        this.loading = true;
        this.authenticationService.login(this.model.username, this.model.password, this.model.remember)
            .subscribe(function (data) {
            //this.router.navigate([this.returnUrl]);
            //if(localStorage.getItem('currentContext') != undefined)
            if (_this.authenticationService.islogin()) {
                _this.parent.step1 = false;
                _this.parent.step2 = true;
                ////type Activity = typeof Metadatas;
                //Object.values(Metadatas).map(val => {
                //  //this.saveMetadataInCache(val);
                //});
            }
        }, function (error) {
            _this.toastrService.error(_this.getText("Login.UserIncorrect"), '', { positionClass: 'toast-top-center' });
            _this.loading = false;
        });
    };
    LoginComponent = __decorate([
        core_1.Component({
            selector: 'login',
            templateUrl: 'login.component.html',
            styleUrls: ['./login.component.css']
        }),
        __param(5, core_2.Host()),
        __param(9, core_1.Inject(common_1.DOCUMENT))
    ], LoginComponent);
    return LoginComponent;
}(default_component_1.DefaultComponent));
exports.LoginComponent = LoginComponent;
//# sourceMappingURL=login.component.js.map