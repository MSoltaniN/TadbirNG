import { PermissionBrief } from "../model/index";
import { BrowserStorageService } from "../service/browserStorage.service";
import { Inject } from "@angular/core";


export class EnviromentComponent {

  constructor(@Inject(BrowserStorageService) public bStorageService: BrowserStorageService) {

  }

  public version: string = '1.0.0';

  public get CurrentLanguage(): string {

    var lang: string = "fa";

    if (this.bStorageService.getLanguage() != null) {
      var item: string | null;
      item = this.bStorageService.getLanguage();

      if (item)
        lang = item;

    }

    return lang;
  }


  public get FiscalPeriodId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.fpId : 0;
  }

  public get BranchId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.branchId : 0;;
  }

  public get CompanyId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.companyId : 0;;
  }

  public get Ticket(): string {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.ticket : '';
  }

  public get UserId(): number {
    var currentContext = this.bStorageService.getCurrentUser();
    var userId = 0;

    if (currentContext) {
      var jsonContext = atob(currentContext.ticket);
      var context = JSON.parse(jsonContext);
      userId = currentContext ? parseInt(context.user.id) : 0;
    }
    return userId;
  }

  public get UserName(): string {
    var currentContext = this.bStorageService.getCurrentUser();
    return currentContext ? currentContext.userName : '';
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
    let permissions: Array<PermissionBrief> = this.Permissions;
    let permission: PermissionBrief;
    let permissionIndex = permissions.findIndex(f => f.entityName == entityName);
    if (permissionIndex >= 0) {
      permission = permissions[permissionIndex];
      if ((permission.flags & action) == action)
        access = true;
    }
    return access;
  }

  /** مدیا جاری را براساس bootstrap 4 برمیگرداند */
  public get media(): string {

    var currentMedia: string = "md";

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

  public get screenSize(): string {

    var size: string;

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

}
