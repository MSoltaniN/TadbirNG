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
Object.defineProperty(exports, "__esModule", { value: true });
var environment_1 = require("../../environments/environment");
var enviroment_component_1 = require("./enviroment.component");
var BaseComponent = /** @class */ (function (_super) {
    __extends(BaseComponent, _super);
    function BaseComponent(toastrService) {
        var _this = _super.call(this) || this;
        _this.toastrService = toastrService;
        return _this;
    }
    /**
     * show message box on screen
     * @param text is the text of message
     * @param type is type of message like Info,Succes,Warning
     * @param title is title of message window
     * @param position is position of message window in screen
     */
    BaseComponent.prototype.showMessage = function (text, type, title, position) {
        if (type === void 0) { type = environment_1.MessageType.Info; }
        if (title === void 0) { title = ''; }
        if (position === void 0) { position = environment_1.MessagePosition.TopLeft; }
        var pos = position;
        var lang = localStorage.getItem('lang');
        if (lang != 'fa')
            pos = environment_1.MessagePosition.TopRight;
        var posCss = 'toast-top-left';
        switch (pos) {
            case environment_1.MessagePosition.TopRight:
                posCss = 'toast-top-right';
                break;
            case environment_1.MessagePosition.TopCenter:
                posCss = 'toast-top-center';
                break;
        }
        switch (type) {
            case environment_1.MessageType.Info:
                this.toastrService.info(text, title, { positionClass: posCss });
                break;
            case environment_1.MessageType.Warning:
                this.toastrService.warning(text, title, { positionClass: posCss });
                break;
            case environment_1.MessageType.Succes:
                this.toastrService.success(text, title, { positionClass: posCss });
                break;
        }
    };
    return BaseComponent;
}(enviroment_component_1.EnviromentComponent));
exports.BaseComponent = BaseComponent;
//# sourceMappingURL=base.component.js.map