import { Directive, OnChanges, OnInit, ElementRef, HostListener, ViewContainerRef } from "@angular/core";

@Directive({ selector: 'input.k-input , input.k-textbox' })
export class InputDirective implements OnInit, OnChanges {  
  currentElement: any;
  constructor(private elementRef: ElementRef, public parentComponet: ViewContainerRef) {
    this.currentElement = elementRef.nativeElement;
  }

  ngOnInit() {
    
  }

  ngOnChanges() {

    
  }

  @HostListener('change')
  onChange() {   

    this.currentElement.value = this.currentElement.value.replaceBadChars(this.currentElement.value);

    var controlName = "";
    if (this.currentElement.attributes["formcontrolname"]) {
      controlName = this.currentElement.attributes["formcontrolname"].nodeValue;
    }

    var component = this.parentComponet['_hostLView'][8];
    Object.values(component).forEach((item:any) => {
      if (item && item.controls) {
        if (item.controls[controlName]) {
          var value = item.controls[controlName].value.toString();
          var newValue = value.replaceBadChars(value);

          item.controls[controlName].setValue(newValue);
          item.controls[controlName].value = newValue;

          console.log('replace bad chars');
        }
      }
    });
    
  }
}
