
import { Directive, OnInit, OnDestroy, Input, Output, EventEmitter, HostListener, ViewContainerRef } from "@angular/core";
import { Subject, Subscription } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { ViewName, Permissions } from "@sppc/shared/security";
import { EnviromentComponent } from "@sppc/shared/class";


@Directive({
  selector: '[SppcPermissionCheck]',
  providers:[Permissions]
})

export class SppcPermissionCheckDirective implements OnInit, OnDestroy {

  @Input('SppcPermissionCheck') permissionKey: string;
  @Input('EntityName') entityName: string;

  private permissions :string;
  private enum :string; 

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

  @Input()
  set SppcPermissionCheck(val : string) {
    if (val) {
      this.enum = val.split(';')[1];
      this.permissions = val.split(';')[0];
    }  
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

  //function getEnumKeyByEnumValue(myEnum, enumValue) {
  //let keys = Object.keys(myEnum).filter(x => myEnum[x] == enumValue);
  //return keys.length > 0 ? keys[0] : null;
  //}


  haveAccess(): boolean {    
    var eName = this.entityName;
    if (!this.entityName && !this.enum) {
      var viewId = (<any>this.parentComponet)._view.component.viewId;
      eName = viewId ? ViewName[viewId] : (<any>this.parentComponet)._view.component.entityType;
    }

    if (!this.enum) {
      eName = eName + "Permissions"
    }
    else {
      eName = this.enum;
    }

    if (eName && this.permissions) {
      var permission = this.permissionKeys.getPermission(eName, this.permissions);

      if (permission)
        var isAccess = this.enviroment.isAccess(eName, permission);

      return isAccess;
    }
    debugger;
   


    return false;
  }


}
