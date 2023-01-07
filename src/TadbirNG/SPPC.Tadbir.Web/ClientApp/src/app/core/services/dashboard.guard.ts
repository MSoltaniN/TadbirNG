import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UserProfileConfig } from '@sppc/config/models/userProfileConfig';
import { SettingService } from '@sppc/config/service';
import { SettingsApi } from '@sppc/config/service/api';
import { EnviromentComponent } from '@sppc/shared/class';
import { DashboardPermissions } from '@sppc/shared/security';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DashboardGuard implements CanActivate {

  constructor(private router: Router,
     private settingService: SettingService,
     private enviroment: EnviromentComponent) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    this.navigateToHome();
    return true;
  }

  navigateToHome() {
    if (!this.enviroment.isAccess('Dashboard',DashboardPermissions.ManageDashboard)) {
      this.router.navigate(["/tadbir/home"]);
      return false;
    } else {
      this.settingService
          .getSettingsCategories(SettingsApi.UserProfileConfig)
          .pipe(
            take(2)
          )
          .subscribe((res: UserProfileConfig) => {
            let showDashboard = res.showDashboardAtStartup
            if (!showDashboard) {
              this.router.navigate(["/tadbir/home"]);
              return false;
            }
          })
    }
  }

}
