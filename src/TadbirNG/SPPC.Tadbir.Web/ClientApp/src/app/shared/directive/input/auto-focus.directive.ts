import { AfterContentInit, AfterViewInit, Directive, ElementRef, HostListener, Input } from "@angular/core";

@Directive({
  selector: "[sppcAutoFocus]",
})
export class AutoFocusDirective implements AfterContentInit {
  constructor(private elementRef: ElementRef) {}

  @Input('sppcAutoFocus') sppcAutoFocus;

  @HostListener('focus',['$event']) select(event:Event) {
    (<HTMLInputElement>event.target).select();
  }

  ngAfterContentInit():void {
    if (this.sppcAutoFocus !== false) {
      if (document.activeElement instanceof HTMLElement) {
        document.activeElement.blur();      
      }
      setTimeout(() => {
        this.elementRef.nativeElement.focus();
      }, this.sppcAutoFocus? this.sppcAutoFocus: 500);
    }
  }
}
