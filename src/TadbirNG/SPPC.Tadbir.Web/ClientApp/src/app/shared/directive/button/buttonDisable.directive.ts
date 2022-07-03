import { Directive, ElementRef, HostListener } from "@angular/core";
import { GridService } from "@sppc/shared/services";
import { take } from "rxjs/operators";
@Directive({ selector: "button.k-button , button[kendogridaddcommand]" })
export class SppcButtonDisable {
  constructor(public element: ElementRef,private service:GridService) {}

  @HostListener("click") onClick() {
    if (this.element.nativeElement.classList.contains("not-change")) {
      return;
    }
    if (this.element.nativeElement.classList.contains("submit")) {
      this.service.isSubmitted().pipe(
        take(2)
      ).subscribe(res => {
        this.element.nativeElement.disabled = res;
      })
    } else {
      this.element.nativeElement.disabled = true
    }
    this.element.nativeElement.classList.add("current-clicked-button");

    setTimeout(() => {
      if (
        this.element.nativeElement.classList.contains("current-clicked-button")
      ) {
        if (!this.element.nativeElement.classList.contains("submit"))
          this.element.nativeElement.disabled = false;
        
        this.element.nativeElement.classList.remove("current-clicked-button");
      }
    }, 350);
  }
}
