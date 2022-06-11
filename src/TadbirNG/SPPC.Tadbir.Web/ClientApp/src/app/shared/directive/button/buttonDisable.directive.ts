import { Directive, ElementRef, HostListener } from "@angular/core";
@Directive({ selector: "button.k-button , button[kendogridaddcommand]" })
export class SppcButtonDisable {
  constructor(public element: ElementRef) {}

  @HostListener("click") onClick() {
    if (this.element.nativeElement.classList.contains("not-change")) {
      return;
    }

    this.element.nativeElement.disabled = true;
    this.element.nativeElement.classList.add("current-clicked-button");

    setTimeout(() => {
      if (
        this.element.nativeElement.classList.contains("current-clicked-button")
      ) {
        this.element.nativeElement.disabled = false;
        this.element.nativeElement.classList.remove("current-clicked-button");
      }
    }, 350);
  }
}
