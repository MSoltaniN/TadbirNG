﻿import { PermissionBrief } from "../model/index";


export class EnviromentComponent {



    constructor() {

    }

    public get CurrentLanguage(): string {

        var lang: string = "fa";

        if (localStorage.getItem('lang') != null) {
            var item: string | null;
            item = localStorage.getItem('lang');

            if (item)
                lang = item;

        }

        return lang;
    }


    public get FiscalPeriodId(): number {

        var fpId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            fpId = currentContext ? parseInt(currentContext.fpId) : 0;

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            fpId = currentContext ? parseInt(currentContext.fpId) : 0;

        }

        return fpId;
    }

    public get BranchId(): number {

        var branchId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            branchId = currentContext ? parseInt(currentContext.branchId) : 0;

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            branchId = currentContext ? parseInt(currentContext.branchId) : 0;

        }
        return branchId;
    }

    public get CompanyId(): number {

        var companyId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            companyId = currentContext ? parseInt(currentContext.companyId) : 0;

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            companyId = currentContext ? parseInt(currentContext.companyId) : 0;

        }

        return companyId;
    }

    public get Ticket(): string {

        var ticket = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            ticket = currentContext ? currentContext.ticket.toString() : '';

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            ticket = currentContext ? currentContext.ticket.toString() : '';

        }
        return ticket;
    }

    public get UserId(): number {

        var userId = 0;

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            var jsonContext = atob(currentContext.ticket);
            var context = JSON.parse(jsonContext);

            userId = currentContext ? parseInt(context.User.Id) : 0;

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            var jsonContext = atob(currentContext.ticket);
            var context = JSON.parse(jsonContext);

            userId = currentContext ? parseInt(context.User.Id) : 0;

        }

        return userId;
    }

    public get UserName(): string {

        var userName = '';

        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            userName = currentContext ? currentContext.userName.toString() : '';

        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            userName = currentContext ? currentContext.userName.toString() : '';

        }
        return userName;
    }

    public get Permissions(): Array<PermissionBrief> {
        let permission: Array<PermissionBrief> = [];
        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            permission = currentContext.permissions;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            permission = currentContext.permissions;
        }
        return permission;
    }

    public get FiscalPeriodStartDate(): Date {
        var startDate = undefined;

        if (localStorage.getItem('fiscalPeriod') != null) {
            var item: string | null;
            item = localStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            startDate = currentContext ? currentContext.startDate : undefined;

        }
        else if (sessionStorage.getItem('fiscalPeriod') != null) {
            var item: string | null;
            item = sessionStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            startDate = currentContext ? currentContext.startDate : undefined;

        }
        return startDate;
    }

    public get FiscalPeriodEndDate(): Date {
        var endDate = undefined;

        if (localStorage.getItem('fiscalPeriod') != null) {
            var item: string | null;
            item = localStorage.getItem('fiscalPeriod');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            endDate = currentContext ? currentContext.endDate : undefined;

        }
        else if (sessionStorage.getItem('fiscalPeriod') != null) {
            var item: string | null;
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
    public isAccess(entityName: string, action: number): boolean {
        let access: boolean = false;
        let permissions: Array<PermissionBrief> = this.Permissions;
        let permission: PermissionBrief;
        let permissionIndex = permissions.findIndex(f => f.EntityName == entityName);
        if (permissionIndex >= 0) {
            permission = permissions[permissionIndex];
            if ((permission.Flags & action) == action)
                access = true;
        }
        return access;
    }

}