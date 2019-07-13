
import { Directive, OnInit, OnDestroy, Input, Output, EventEmitter, HostListener, ViewContainerRef } from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { ViewName } from "../security/viewName";
import { EnviromentComponent } from "../class/enviroment.component";
import { Permissions } from "../security/permissions";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";


@Directive({
  selector: '[SppcPermissionCheck]'
})

export class SppcPermissionCheckDirective implements OnInit, OnDestroy {

  @Input('SppcPermissionCheck') permissionKey: string;
  @Input('EntityName') entityName: string;

  @Output() sppcClick = new EventEmitter();
  private clicks = new Subject();
  private subscription: Subscription;

  constructor(public parentComponet: ViewContainerRef, private enviroment: EnviromentComponent, private permissionKeys: Permissions,
    public toastrService: ToastrService, public translate: TranslateService) {
  }

  ngOnInit() {
    this.subscription = this.clicks.pipe().subscribe(e => this.sppcClick.emit(e));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();   
  }

  @HostListener('click', ['$event'])
  clickEvent(event) {
    event.preventDefault();
    event.stopPropagation();
    if (this.haveAccess())
      this.clicks.next(event);
    else
      this.translate.get('App.AccessDenied').subscribe((msg: string) => {
        this.toastrService.warning(msg);
      });     
  }


  haveAccess(): boolean {
    debugger;
    var eName = this.entityName;
    if (!this.entityName) {
      var viewId = (<any>this.parentComponet)._view.component.viewId;
      eName = viewId ? ViewName[viewId] : (<any>this.parentComponet)._view.component.entityType;
    }

    if (eName && this.permissionKey) {
      var permission = this.permissionKeys.getPermission(eName, this.permissionKey);

      if (permission)
        var isAccess = this.enviroment.isAccess(eName, permission);

      return isAccess;
    }

    return false;
  }


}
