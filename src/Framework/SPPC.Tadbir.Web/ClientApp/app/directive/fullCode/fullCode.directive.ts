
import { Directive, ElementRef, HostListener, Input, Inject, AfterViewInit, Renderer } from "@angular/core";
import { AbstractControl, Validator } from "@angular/forms";
import { DOCUMENT } from '@angular/common';
import { FullCodeService } from "../../service/index";



@Directive({
    selector: '[sppsFullCode]',
})

export class FullCodeDirective implements AfterViewInit {

    @Input() sppsFullCode: string;
    @Input() apiUrl: string;

    fullCodeElement: any;
    parentFullCode: string;

    ngAfterViewInit(): void {
        this.fullCodeElement = document.getElementById(this.sppsFullCode) as any;
        if (this.apiUrl) {
            this.fullCodeService.getAll(this.apiUrl).subscribe(res => {
                var codeValue = this.el.nativeElement.value;
                this.parentFullCode = res.body;

                var fullCode = this.parentFullCode + codeValue;

                if (fullCode.length>0) {
                    this.fullCodeElement.value = fullCode;
                    let event: Event = document.createEvent("Event");
                    event.initEvent('input', true, true);
                    Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
                    this.renderer.invokeElementMethod(this.fullCodeElement, 'dispatchEvent', [event]);
                }                
            })
        }
        
        
    }

    constructor(public el: ElementRef, private renderer: Renderer, private fullCodeService: FullCodeService) { }

    @HostListener('input') onEvent() {

        let code = this.el.nativeElement.value;

        this.fullCodeElement.value = this.parentFullCode + code;

        let event: Event = document.createEvent("Event");
        event.initEvent('input', true, true);
        Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
        this.renderer.invokeElementMethod(this.fullCodeElement, 'dispatchEvent', [event]);
    }
}