import { Directive, ElementRef, HostListener, Input } from "@angular/core";;

@Directive({ selector: 'button.k-button , [kendogridaddcommand] , input[type=button] , type=button' })
export class SppcButtonDisable {
   

  constructor(public element: ElementRef) {
        
  }   

  @HostListener('click') onClick() {    
    this.element.nativeElement.classList.add("current-clicked-button");
    this.element.nativeElement.disabled = true;

    setTimeout(() => {
      if (this.element.nativeElement.classList.contains("current-clicked-button")) {
        this.element.nativeElement.classList.remove("current-clicked-button");
        this.element.nativeElement.disabled = false;
      }
    }, 200);

  }
}
