
import { Directive, ElementRef, HostListener, Input, Inject, AfterViewInit, Renderer2 } from "@angular/core";
import { FullCodeService } from "@sppc/finance/service";




@Directive({
    selector: '[sppcFullCode]',
})

export class FullCodeDirective implements AfterViewInit {

    @Input() sppcFullCode: string;
    @Input() apiUrl: string;

    fullCodeElement: any;
    parentFullCode: string;

    ngAfterViewInit(): void {
        this.fullCodeElement = document.getElementById(this.sppcFullCode) as any;
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
                    this.renderer.selectRootElement(this.fullCodeElement).dispatchEvent();
                }                
            })
        }
        
        
    }

    constructor(public el: ElementRef, private renderer: Renderer2, private fullCodeService: FullCodeService) { }

    @HostListener('input') onEvent() {

        let code = this.el.nativeElement.value;

        this.fullCodeElement.value = this.parentFullCode + code;

        let event: Event = document.createEvent("Event");
        event.initEvent('input', true, true);
        Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
        this.renderer.selectRootElement(this.fullCodeElement).dispatchEvent();
    }
}
