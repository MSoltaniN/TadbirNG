


import { AddCommandDirective } from "@progress/kendo-angular-grid/dist/es2015/editing/add-command.directive";
import { Button } from "@progress/kendo-angular-buttons";
import { Directive, Renderer, ElementRef, Host, Input, Self, HostBinding, ViewChild, SkipSelf, ViewContainerRef, Renderer2, OnInit } from "@angular/core";
import { EnviromentComponent } from "../../class/enviroment.component";
import { SecureEntity } from "../../security/secureEntity";
import { AccountPermissions, Permissions } from "../../security/permissions";
import { DefaultComponent } from "../../class/default.component";


@Directive({
    selector: '[SppcCheckAccess]',
    providers: [EnviromentComponent, Permissions, DefaultComponent]
})

export class SppcCheckAccess implements OnInit {


    @Input('SppcCheckAccess') value: string;
    
    constructor(private renderer: Renderer2, private elRef: ElementRef, private enviroment: EnviromentComponent,
        public parentComponet: ViewContainerRef, private permissionKeys: Permissions, private defaultComponent : DefaultComponent) {
        
       
    }


    ngOnInit() {
        var checkAccess = this.elRef.nativeElement.attributes.sppccheckaccess;

        var permissions: string = checkAccess.nodeValue;

        var permissionId: number = 0;

        var entityType = (<any>this.parentComponet)._view.component.entityType

        var allPermission = permissions.split(',');
        for (var pr in allPermission) {
            if (pr == "") continue;
            permissionId = this.permissionKeys.getPermission(entityType, allPermission[pr]);

            if (permissionId > 0)
                var isAccess: boolean = false;
                isAccess = this.enviroment.isAccess(entityType, permissionId);
                if (isAccess == false) {
                    if (allPermission[pr] == "View") {

                        while (this.elRef.nativeElement.hasChildNodes()) {
                            this.elRef.nativeElement.removeChild(this.elRef.nativeElement.lastChild);
                        }
                        var notViewAccessText = this.defaultComponent.getText('App.NotViewAccess');
                        var textElement = this.renderer.createText(notViewAccessText);
                        this.renderer.appendChild(this.elRef.nativeElement, textElement);

                    }
                    else {
                        this.elRef.nativeElement.remove();// setElementStyle(elRef.nativeElement, 'display', 'none');
                    }
                    //elRef.nativeElement.outerHTML = '';

                }
        }     
    }
    
}