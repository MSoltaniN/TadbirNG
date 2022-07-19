import { AfterContentInit, AfterViewInit, Directive, ElementRef, HostListener } from "@angular/core";

@Directive({
  selector: "[sppcAutoFocus]",
})
export class AutoFocusDirective implements AfterContentInit {
  constructor(private elementRef: ElementRef) {}

  @HostListener('focus',['$event']) select(event:Event) {
    (<HTMLInputElement>event.target).select();
  }
  ngAfterContentInit():void {

    if (document.activeElement instanceof HTMLElement) {
      document.activeElement.blur();      
    }
    setTimeout(() => {
      this.elementRef.nativeElement.focus();
    }, 500);
  }
}
