
import { Directive, ElementRef, HostListener, Input,  AfterViewInit, Renderer2 } from "@angular/core";




@Directive({
  selector: '[sppcFullCodeTest]',
})

export class FullCodeTestDirective implements AfterViewInit {

  @Input() sppcFullCodeTest: string;
  @Input() parentFullCode: string;

  fullCodeElement: any;

  ngAfterViewInit(): void {
    this.fullCodeElement = document.getElementById(this.sppcFullCodeTest) as any;
  }

  constructor(public el: ElementRef, private renderer: Renderer2) { }

  @HostListener('input') onEvent() {
    let code = this.el.nativeElement.value;

    this.fullCodeElement.value = this.parentFullCode + code;

    let event: Event = document.createEvent("Event");
    event.initEvent('input', true, true);
    Object.defineProperty(event, 'target', { value: this.fullCodeElement, enumerable: true });
    this.renderer.selectRootElement(this.fullCodeElement).dispatchEvent();
  }
}
