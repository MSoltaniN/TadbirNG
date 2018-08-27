
import { Directive, ElementRef, HostListener, Input, Inject, AfterViewInit, Renderer } from "@angular/core";
import { AbstractControl, Validator } from "@angular/forms";
import { DOCUMENT } from '@angular/common';


@Directive({
    selector: '[sppsFullCode]',
})

export class FullCodeDirective implements AfterViewInit {


    ngAfterViewInit(): void {
        debugger;
        this.fullCodeElement = document.getElementById(this.sppsFullCode) as any;
        var fullCodeValue = this.fullCodeElement.value;
        var codeValue = this.el.nativeElement.value;
        this.fullCode = fullCodeValue.substring(0, fullCodeValue.length - codeValue.length);
    }

    @Input() sppsFullCode: string;

    fullCodeElement: any;
    fullCode: string;

    constructor(public el: ElementRef, private renderer: Renderer) {

    }

    @HostListener('input') onEvent() {

        let code = this.el.nativeElement.value;

        this.fullCodeElement.value = this.fullCode + code;

        let event: Event = document.createEvent("Event");
        event.initEvent('input', true, true);
        Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
        this.renderer.invokeElementMethod(this.fullCodeElement, 'dispatchEvent', [event]);
    }
}