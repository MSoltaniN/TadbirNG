
import { Component, OnInit, Input } from '@angular/core';




@Component({
    selector: 'sppc-numberbox',
    template: `<kendo-numerictextbox
          [spinners]="spinners"
          [step]="step"
          [value]="value"        
        ></kendo-numerictextbox>`
})

export class SppcNumberBox implements OnInit {

    @Input() public spinners: boolean = false;

    @Input() public step: string = "1";

    @Input() public value: string = "";
    

    constructor() {
    }


    ngOnInit() {
    }


}