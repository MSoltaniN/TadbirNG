import {
  Directive,
  OnInit,
  OnDestroy,
  Input,
  Output,
  EventEmitter,
  HostListener,
  ViewContainerRef,
  AfterContentChecked,
} from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { ViewName, Permissions } from "@sppc/shared/security";
import { EnviromentComponent } from "@sppc/shared/class";

@Directive({
  selector: "[SppcPermissionCheck]",
  providers: [Permissions],
})
export class SppcPermissionCheckDirective
  implements OnInit, OnDestroy
{
  @Input("SppcPermissionCheck") permissionKey: string;
  @Input("EntityName") entityName: string;

  private permissions: string;
  private enum: string;

  @Output() sppcClick = new EventEmitter();
  private clicks = new Subject();
  private subscription: Subscription;

  constructor(
    public parentComponet: ViewContainerRef,
    private enviroment: EnviromentComponent,
    private permissionKeys: Permissions,
    public toastrService: ToastrService,
    public translate: TranslateService
  ) {}

  ngOnInit() {
    this.subscription = this.clicks
      .pipe()
      .subscribe((e) => this.sppcClick.emit(e));
  }

  ngOnDestroy(): void {
    this.subscription.unsubscribe();
  }

  @Input()
  set SppcPermissionCheck(val: string) {
    if (val) {
      //this.enum = val.split(';')[1];
      this.permissions = val; //.split(';')[0];
    }
  }

  @HostListener("click", ["$event"])
  clickEvent(event) {
    event.preventDefault();
    event.stopPropagation();

    if (this.haveAccess()) this.clicks.next(event);
    else
      this.translate.get("App.AccessDenied").subscribe((msg: string) => {
        this.toastrService.warning(msg);
      });
  }

  haveAccess(): boolean {
    var isAccess: boolean = true;

    if (!this.permissions || this.permissions == "") return isAccess;

    this.permissions.split("|").forEach((it) => {
      var eName = this.entityName;
      if (!this.entityName && !this.enum) {
        var viewId = this.parentComponet.parentInjector["_lView"][8].viewId;
        eName = viewId
          ? ViewName[viewId]
          : this.parentComponet.parentInjector["_lView"][8].entityType;
      }

      var enumName = "";
      var permissionName = it;

      if (it.toString().indexOf(";") == -1) {
        enumName = eName + "Permissions";
      } else {
        enumName = it.toString().split(";")[1];
        permissionName = it.toString().split(";")[0];
      }

      if (eName && this.permissions) {
        var permission = this.permissionKeys.getPermission(
          enumName,
          permissionName
        );

        if (permission)
          isAccess = isAccess && this.enviroment.isAccess(eName, permission);
      }
    });

    return isAccess;
  }
}
