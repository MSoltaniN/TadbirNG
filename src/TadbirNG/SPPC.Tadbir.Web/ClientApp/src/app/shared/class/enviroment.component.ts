import { Inject } from "@angular/core";
import { ContextInfo } from "@sppc/core";
import { PermissionBrief } from "@sppc/core/models/permissionBrief";
import { BrowserStorageService } from "@sppc/shared/services/browserStorage.service";

export class EnviromentComponent {
  constructor(
    @Inject(BrowserStorageService) public bStorageService: BrowserStorageService
  ) {}

  public version: string = "1.0.764";

  public get CurrentLanguage(): string {
    var lang: string = "fa";

    if (this.bStorageService.getLanguage() != null) {
      var item: string | null;
      item = this.bStorageService.getLanguage();

      if (item) lang = item;
    }

    return lang;
  }

  public get FiscalPeriodId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.fpId : 0;
  }

  public get InventoryMode(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.inventoryMode : 0;
  }

  public get BranchId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.branchId : 0;
  }

  public get BranchName(): string {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.branchName : "";
  }

  public get CompanyId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.companyId : 0;
  }

  public get Ticket(): string {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.ticket : "";
  }

  public get UserId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    var userId = 0;

    if (currentContext) {
      var context = this.parseJwt(currentContext.ticket);
      //var context = JSON.parse(jsonContext);
      userId = currentContext ? parseInt(context.TadbirContext.Id) : 0;
    }
    return userId;
  }

  public extractUserId(currentContext: ContextInfo): number {
    var userId = 0;
    if (currentContext) {
      var context = this.parseJwt(currentContext.ticket);
      //var context = JSON.parse(jsonContext);
      userId = currentContext ? parseInt(context.TadbirContext.Id) : 0;
    }
    return userId;
  }

  public get IsAdmin(): boolean {
    var currentContext = this.bStorageService.getCurrentUser();
    let isAdmin: boolean = false;
    if (currentContext) {
      var adminRole = currentContext.roles.find((f) => f == 1);
      isAdmin = adminRole ? true : false;
    }
    return isAdmin;
  }

  public get UserName(): string {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.userName : "";
  }

  public get Permissions(): Array<PermissionBrief> {
    let permission: Array<PermissionBrief> = [];
    var currentContext = this.bStorageService.getCurrentUser();
    if (currentContext) {
      permission = currentContext.permissions;
    }
    return permission;
  }

  public get FiscalPeriodStartDate(): Date {
    var startDate = undefined;
    var item = this.bStorageService.getFiscalPeriod();

    if (item) {
      var fp = JSON.parse(item != null ? item.toString() : "");
      startDate = fp ? fp.startDate : undefined;
    }
    return startDate;
  }

  public get FiscalPeriodEndDate(): Date {
    var endDate = undefined;
    var item = this.bStorageService.getFiscalPeriod();

    if (item) {
      var fp = JSON.parse(item != null ? item.toString() : "");
      endDate = fp ? fp.endDate : undefined;
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
    var currentContext = this.bStorageService.getCurrentUser();
    var adminRole = currentContext.roles.find((f) => f == 1);
    if (adminRole) access = true;
    else {
      let permissions: Array<PermissionBrief> = this.Permissions;
      let permission: PermissionBrief;
      let permissionIndex = permissions.findIndex(
        (f) => f.entityName == entityName
      );
      if (permissionIndex >= 0) {
        permission = permissions[permissionIndex];
        if ((permission.flags & action) == action) access = true;
      }
    }
    return access;
  }

  /** مدیا جاری را براساس bootstrap 4 برمیگرداند */
  public get media(): string {
    var currentMedia: string = "md";

    if (window.innerWidth < 576) currentMedia = "xs";

    if (window.innerWidth >= 576 && window.innerWidth < 768)
      currentMedia = "sm";

    if (window.innerWidth >= 768 && window.innerWidth < 992)
      currentMedia = "md";

    if (window.innerWidth >= 992 && window.innerWidth < 1200)
      currentMedia = "l";

    if (window.innerWidth >= 1200) currentMedia = "el";

    return currentMedia;
  }

  public get screenSize(): string {
    var size: string;

    switch (this.media) {
      case "xs": {
        size = "extraSmall";
        break;
      }
      case "sm": {
        size = "small";
        break;
      }
      case "md": {
        size = "medium";
        break;
      }
      case "l": {
        size = "large";
        break;
      }
      case "el": {
        size = "extraLarge";
        break;
      }
    }
    return size;
  }

  parseJwt(token) {
    var base64Url = token.split(".")[1];
    var base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
    var jsonPayload = decodeURIComponent(
      atob(base64)
        .split("")
        .map(function (c) {
          return "%" + ("00" + c.charCodeAt(0).toString(16)).slice(-2);
        })
        .join("")
    );

    return JSON.parse(jsonPayload);
  }
}
