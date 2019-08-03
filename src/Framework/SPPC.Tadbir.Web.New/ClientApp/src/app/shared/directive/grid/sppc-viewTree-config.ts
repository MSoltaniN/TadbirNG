


import { AddCommandDirective } from "@progress/kendo-angular-grid/dist/es2015/editing/add-command.directive";
import { Button } from "@progress/kendo-angular-buttons";
import { Directive, Renderer, ElementRef, Host, Input, Self, HostBinding, ViewChild, SkipSelf, ViewContainerRef, Renderer2, OnInit, Inject } from "@angular/core";
import { DefaultComponent, EnviromentComponent } from "@sppc/shared";


@Directive({
  selector: '[SppcViewTreeConfig]',
  providers: [EnviromentComponent, DefaultComponent]
})

export class SppcViewTreeConfig implements OnInit {


  //@Input('SppcViewTreeConfig') value: string;

  constructor(private renderer: Renderer2, private elRef: ElementRef, private enviroment: EnviromentComponent,
    public parentComponet: ViewContainerRef, private defaultComponent: DefaultComponent) {


  }


  ngOnInit() {
    ////debugger;
    //var level = 4;
    //let config: ViewTreeConfig;
    //config = this.defaultComponent.getViewTreeSettings(1);
    //var configLevels = config.levels.filter(f => f != null);
    //var configLevel = config.levels.filter(f => f != null && f.no == level);

    //if (configLevel.length == 0) {
    //  this.elRef.nativeElement.remove();
    //}
    //////var checkAccess = this.elRef.nativeElement.attributes.sppccheckaccess;

    //////var permissions: string = checkAccess.nodeValue;

    //////var permissionId: number = 0;

    //////var entityType = (<any>this.parentComponet)._view.component.entityType

    ////    permissionId = this.permissionKeys.getPermission(entityType, allPermission[pr]);

    ////    if (permissionId > 0)
    ////        var isAccess: boolean = false;
    ////        isAccess = this.enviroment.isAccess(entityType, permissionId);
    ////        if (isAccess == false) {
    ////            if (allPermission[pr] == "View") {

    ////                while (this.elRef.nativeElement.hasChildNodes()) {
    ////                    this.elRef.nativeElement.removeChild(this.elRef.nativeElement.lastChild);
    ////                }
    ////                var notViewAccessText = this.defaultComponent.getText('App.NotViewAccess');
    ////                var textElement = this.renderer.createText(notViewAccessText);
    ////                this.renderer.appendChild(this.elRef.nativeElement, textElement);

    ////            }
    ////            else {
    ////                this.elRef.nativeElement.remove();// setElementStyle(elRef.nativeElement, 'display', 'none');
    ////            }
    ////            //elRef.nativeElement.outerHTML = '';

    ////        }  
  }

}
