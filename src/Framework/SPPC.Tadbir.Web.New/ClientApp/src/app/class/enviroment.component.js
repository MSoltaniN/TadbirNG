import { SessionKeys } from "../../environments/environment";
import { DateRangeType } from "../enum/dateRangeType";
export class EnviromentComponent {
    constructor() {
    }
    get CurrentLanguage() {
        var lang = "fa";
        if (localStorage.getItem('lang') != null) {
            var item;
            item = localStorage.getItem('lang');
            if (item)
                lang = item;
        }
        return lang;
    }
    get FiscalPeriodId() {
        var fpId = 0;
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            fpId = currentContext ? parseInt(currentContext.fpId) : 0;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            fpId = currentContext ? parseInt(currentContext.fpId) : 0;
        }
        return fpId;
    }
    get BranchId() {
        var branchId = 0;
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            branchId = currentContext ? parseInt(currentContext.branchId) : 0;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            branchId = currentContext ? parseInt(currentContext.branchId) : 0;
        }
        return branchId;
    }
    get CompanyId() {
        var companyId = 0;
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            companyId = currentContext ? parseInt(currentContext.companyId) : 0;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            companyId = currentContext ? parseInt(currentContext.companyId) : 0;
        }
        return companyId;
    }
    get Ticket() {
        var ticket = '';
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            ticket = currentContext ? currentContext.ticket.toString() : '';
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            ticket = currentContext ? currentContext.ticket.toString() : '';
        }
        return ticket;
    }
    get UserId() {
        var userId = 0;
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            var jsonContext = atob(currentContext.ticket);
            var context = JSON.parse(jsonContext);
            userId = currentContext ? parseInt(context.user.id) : 0;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            var jsonContext = atob(currentContext.ticket);
            var context = JSON.parse(jsonContext);
            userId = currentContext ? parseInt(context.user.id) : 0;
        }
        return userId;
    }
    get UserName() {
        var userName = '';
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            userName = currentContext ? currentContext.userName.toString() : '';
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            userName = currentContext ? currentContext.userName.toString() : '';
        }
        return userName;
    }
    get Permissions() {
        let permission = [];
        if (localStorage.getItem('currentContext') != null) {
            var item;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            permission = currentContext.permissions;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            permission = currentContext.permissions;
        }
        return permission;
    }
    get FiscalPeriodStartDate() {
        var startDate = undefined;
        if (localStorage.getItem('fiscalPeriod') != null) {
            var item;
            item = localStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            startDate = currentContext ? currentContext.startDate : undefined;
        }
        else if (sessionStorage.getItem('fiscalPeriod') != null) {
            var item;
            item = sessionStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            startDate = currentContext ? currentContext.startDate : undefined;
        }
        return startDate;
    }
    get FiscalPeriodEndDate() {
        var endDate = undefined;
        if (localStorage.getItem('fiscalPeriod') != null) {
            var item;
            item = localStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            endDate = currentContext ? currentContext.endDate : undefined;
        }
        else if (sessionStorage.getItem('fiscalPeriod') != null) {
            var item;
            item = sessionStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            endDate = currentContext ? currentContext.endDate : undefined;
        }
        return endDate;
    }
    /**
     * اگر کاربر حق دسترسی داشته باشه مقدار true وگرنه مقدار false برمیگرداند
     * @param entityName نام entity (app/security/SecureEntity.ts)
     * @param action مجوز دسترسی (app/security/permissions.ts)
     */
    isAccess(entityName, action) {
        let access = false;
        let permissions = this.Permissions;
        let permission;
        let permissionIndex = permissions.findIndex(f => f.entityName == entityName);
        if (permissionIndex >= 0) {
            permission = permissions[permissionIndex];
            if ((permission.flags & action) == action)
                access = true;
        }
        return access;
    }
    /** مدیا جاری را براساس bootstrap 4 برمیگرداند */
    get media() {
        var currentMedia = "md";
        if (window.innerWidth < 576)
            currentMedia = "xs";
        if (window.innerWidth >= 576 && window.innerWidth < 768)
            currentMedia = "sm";
        if (window.innerWidth >= 768 && window.innerWidth < 992)
            currentMedia = "md";
        if (window.innerWidth >= 992 && window.innerWidth < 1200)
            currentMedia = "l";
        if (window.innerWidth >= 1200)
            currentMedia = "el";
        return currentMedia;
    }
    get screenSize() {
        var size;
        switch (this.media) {
            case "xs":
                {
                    size = 'extraSmall';
                    break;
                }
            case "sm":
                {
                    size = 'small';
                    break;
                }
            case "md":
                {
                    size = 'medium';
                    break;
                }
            case "l":
                {
                    size = 'large';
                    break;
                }
            case "el":
                {
                    size = 'extraLarge';
                    break;
                }
        }
        return size;
    }
    getDateConfig(type) {
        let dateRange = DateRangeType.CurrentToCurrent;
        let fromDate;
        let toDate;
        if (localStorage.getItem(SessionKeys.DateRangeConfig) != null) {
            var range = JSON.parse(localStorage.getItem(SessionKeys.DateRangeConfig));
            dateRange = range ? range.defaultDateRange : DateRangeType.CurrentToCurrent;
        }
        switch (dateRange) {
            case DateRangeType.CurrentToCurrent: {
                fromDate = new Date();
                toDate = new Date();
                break;
            }
            case DateRangeType.FiscalStartToCurrent: {
                fromDate = this.FiscalPeriodStartDate;
                toDate = new Date();
                break;
            }
            case DateRangeType.FiscalStartToFiscalEnd: {
                fromDate = this.FiscalPeriodStartDate;
                toDate = this.FiscalPeriodEndDate;
                break;
            }
            default:
        }
        if (type == "start") {
            return fromDate;
        }
        else if (type == "end") {
            return toDate;
        }
        return undefined;
    }
}
//# sourceMappingURL=enviroment.component.js.map