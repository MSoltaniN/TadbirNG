
import { Component, OnInit, Input } from '@angular/core';




@Component({
    selector: 'sppc-mask-textbox',
    template: `<kendo-maskedtextbox
          [value]="value"
          [mask]="mask"          
        ></kendo-maskedtextbox>`   
})

export class SppcMaskTextBox implements OnInit {

    
    @Input() public value: string = "";

    @Input() public mask: string = "";

    public disabled: boolean = true;

    constructor() {
    }


    ngOnInit() {
    }


}