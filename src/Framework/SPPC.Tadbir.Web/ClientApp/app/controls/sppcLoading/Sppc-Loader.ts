


import { AddCommandDirective } from "@progress/kendo-angular-grid/dist/es2015/editing/add-command.directive";
import { Button } from "@progress/kendo-angular-buttons";
import { Directive, Renderer, ElementRef, Host, Input, Self, HostBinding, ViewChild, SkipSelf, ViewContainerRef, Renderer2, OnInit } from "@angular/core";
import { EnviromentComponent } from "../../class/enviroment.component";
import { SecureEntity } from "../../security/secureEntity";
import { AccountPermissions, Permissions } from "../../security/permissions";
import { DefaultComponent } from "../../class/default.component";
import { LoaderService } from "./loader.service";



@Directive({
    selector: '[sppc-loader]',
    providers: [EnviromentComponent,LoaderService]
})

export class SppcLoader implements OnInit 
{
    ngOnInit(): void {
        //throw new Error("Method not implemented.");

        //this.elRef.nativeElement
        if (this.loader.isLoading) {
            var textElement = this.renderer.createText("loading");
            textElement.id = "sppc-loader-text";
            this.renderer.appendChild(this.elRef.nativeElement, textElement);
        }

        
    }

    constructor(private renderer: Renderer2, private elRef: ElementRef, private enviroment: EnviromentComponent,
        public parentComponet: ViewContainerRef, private defaultComponent: DefaultComponent,private loader:LoaderService ) {
        
          
    }


}
