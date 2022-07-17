import { DOCUMENT } from "@angular/common";
import {
  AfterViewInit,
  Directive,
  ElementRef,
  HostBinding,
  Input,
  OnInit,
  OnDestroy,
  HostListener,
  Renderer2,
  Inject,
} from "@angular/core";
import { DefaultComponent } from "@sppc/shared/class";

@Directive({
  selector: "[sppcTooltip]",
})
export class TooltipDirective  {
  constructor(private elm: ElementRef,private renderer:Renderer2) {}


  @Input() sppcTooltip:string

  div : Node;
  @HostListener('mouseenter',['$event']) onMouseEnter(e:MouseEvent){

    let value = this.sppcTooltip? this.sppcTooltip: this.elm.nativeElement.innerHTML

    this.div = this.renderer.createElement('div');
    this.renderer.addClass(this.div,'sppc-tooltip')

    this.renderer.setProperty(this.div,'innerHTML',value)
    this.renderer.appendChild(this.elm.nativeElement.parentNode,this.div)
    if (e.offsetY-40>10) {
      this.renderer.addClass(this.div,'tooltip-top')
      this.renderer.setStyle(this.div,'top',e.offsetY-40+'px')
    } else {
      this.renderer.addClass(this.div,'tooltip-bottom')
      this.renderer.setStyle(this.div,'top',e.offsetY+22+'px')
    }

  }

  @HostListener('mouseleave',['$event']) onMouseOut(e:MouseEvent){
    this.renderer.removeChild(this.elm.nativeElement.parentNode,this.div)
  }

}
