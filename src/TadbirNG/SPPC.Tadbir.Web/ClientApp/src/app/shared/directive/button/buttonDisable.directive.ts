import { Directive, ElementRef, HostListener } from "@angular/core";
import { GridService } from "@sppc/shared/services";
import { take } from "rxjs/operators";
@Directive({ selector: "button.k-button , button[kendogridaddcommand]" })
export class SppcButtonDisable {
  constructor(public element: ElementRef,private service:GridService) {}

  @HostListener("click") onClick() {
    //preventing duplicate click 
    if (this.element.nativeElement.classList.contains("prevent-duplicate")) {
      this.service.isSubmitted().pipe(
        take(2)
      ).subscribe(res => {
        this.element.nativeElement.disabled = res;
      })
    }


  }
}
