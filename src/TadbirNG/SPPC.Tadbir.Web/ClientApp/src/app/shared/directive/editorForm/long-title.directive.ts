import {
  AfterViewInit,
  Directive,
  ElementRef,
  HostBinding,
  Input,
  OnInit,
  OnDestroy,
} from "@angular/core";
import { DefaultComponent } from "@sppc/shared/class";

@Directive({
  selector: "[sppcLongTitle]",
})
export class LongTitleDirective implements AfterViewInit, OnDestroy {
  constructor(private elm: ElementRef) {}

  interval: any;

  @HostBinding("style.left") left: string;
  ngAfterViewInit(): void {
    this.elm.nativeElement.style.position = 'relative';
    let scrollLength = this.elm.nativeElement.parentNode.offsetWidth;
    let elementRect;

    let ii = -50;
    setTimeout(() => {
      elementRect = this.elm.nativeElement.getBoundingClientRect();

      if (scrollLength < elementRect.width) {

        this.interval = setInterval(() => {
          ii += 1;
          this.left = ii + "px";

          if (ii >= elementRect.width - scrollLength + 80) {
            ii = -50;
          }
        }, 10);

      }
    }, 200);
  }

  ngOnDestroy(): void {
    clearInterval(this.interval);
  }
}
