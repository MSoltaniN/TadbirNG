


import { AddCommandDirective } from "@progress/kendo-angular-grid/dist/es2015/editing/add-command.directive";
import { Button } from "@progress/kendo-angular-buttons";
import { Directive, Renderer, ElementRef, Host, Input, Self, HostBinding, ViewChild, SkipSelf, ViewContainerRef } from "@angular/core";
import { EnviromentComponent } from "../../class/enviroment.component";
import { SecureEntity } from "../../security/secureEntity";
import { AccountPermissions, Permissions } from "../../security/permissions";
import { DefaultComponent } from "../../class/default.component";


@Directive({
    selector: '[SppcCheckAccess]',
    providers: [EnviromentComponent, Permissions]
})

export class SppcCheckAccess  {


    @Input('SppcCheckAccess') value: string;
    
    constructor(renderer: Renderer, private elRef: ElementRef, private enviroment: EnviromentComponent,
         public parentComponet: ViewContainerRef, private permissionKeys: Permissions) {
        
        var checkAccess = elRef.nativeElement.attributes.sppccheckaccess;
        
        var permissions: string = checkAccess.nodeValue;
            
        var permissionId : number = 0;
            
        var entityType = (<any>parentComponet)._view.component.entityType    

        var allPermission = permissions.split(',');
        for (var pr in allPermission) {
            if (pr == "") continue;
            permissionId = permissionKeys.getPermission(entityType, pr);

            if (permissionId > 0)
                if (!enviroment.isAccess(entityType, permissionId))
                    renderer.setElementStyle(elRef.nativeElement, 'display', 'none');
        }     
    }
    
}