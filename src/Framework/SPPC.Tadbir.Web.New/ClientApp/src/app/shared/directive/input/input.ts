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
    debugger;
    console.log('in change InputTextFilterDirective');

    this.currentElement.value = this.currentElement.value.replace('ي', 'ی');
    this.currentElement.value = this.currentElement.value.replace('ك', 'ک');

    var controlName = "";
    if (this.currentElement.attributes["formcontrolname"]) {
      controlName = this.currentElement.attributes["formcontrolname"].nodeValue;
    }

    var component = (<any>this.parentComponet)._view.component;
    Object.values(component).forEach((item:any) => {
      if (item && item.controls) {
        if (item.controls[controlName]) {
          item.controls[controlName].setValue(item.controls[controlName].value.replace('ي', 'ی'));
          item.controls[controlName].setValue(item.controls[controlName].value.replace('ك', 'ک'));          
        }
      }
    });
    
  }
}
