import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanDeactivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DeactivateGuard implements CanDeactivate<any> {

  constructor(private dialogService: DialogService) {}

  canDeactivate(
    component: any,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

    // return true;
    return this.isFormChanged(component);
  }
  
  async isFormChanged(component:any) {
    component.navigateOperation = true;
    if (component.isFormChanged()) {
      console.log('Form was Changed!');
      let res = await component.saveChangesConfirmDialog(null,true);

      return res;

    } else {
      return true;
    }
  }
}
