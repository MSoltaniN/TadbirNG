import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Resolve, RouterStateSnapshot, UrlTree } from '@angular/router';
import { TranslateService } from '@ngx-translate/core';
import { BaseComponent, EnviromentComponent } from '@sppc/shared/class';
import { Entities, MessageType } from '@sppc/shared/enum/metadata';
import { DraftVoucherPermissions, SecureEntity, SpecialVoucherPermissions, VoucherPermissions } from '@sppc/shared/security';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VoucherViewAccessGuard implements Resolve<boolean> {

  constructor(private envComponent:EnviromentComponent,
     private translate:TranslateService,
     private baseComponent:BaseComponent) {}

  /**
   * 'draft' or none(normal)
   */
  type:string;
  mode:string;

  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | Promise<boolean> | Observable<boolean> {
    this.type = route.paramMap.get('type');
    this.mode = route.paramMap.get('mode');

    return this.checkViewAccess(this.mode);
  }

  checkViewAccess(mode:string = ''):boolean {
    let access = true;
    let entityType;
    let permission;
    switch (mode) {
      case "opening-voucher":
        entityType = SecureEntity.SpecialVoucher;
        permission = SpecialVoucherPermissions.IssueOpeningVoucher;
        break;
      case "closing-voucher":
        entityType = SecureEntity.SpecialVoucher;
        permission = SpecialVoucherPermissions.IssueClosingVoucher;
        break;
      case "close-temp-accounts":
        entityType = SecureEntity.SpecialVoucher;
        permission = SpecialVoucherPermissions.IssueClosingTempAccountsVoucher;
        break;
      default:
        entityType = this.type == 'draft'? Entities.DraftVouchers: Entities.Vouchers;
        permission = this.type == 'draft'? DraftVoucherPermissions.View : VoucherPermissions.View;
        break;
    }
    
    if (!this.envComponent.isAccess(entityType, permission)) {
      access = false;
      this.showAccessDeniedMessage();
    }

    return access;
  }

  showAccessDeniedMessage() {
    let msgText = this.translate.instant("App.AccessDenied");
    this.baseComponent.showMessage(
      msgText,
      MessageType.Warning
    );
  }

}
