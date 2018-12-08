
import { Directive, Host, Input, ElementRef, Renderer2 } from "@angular/core";
import { ColumnComponent, FilterMenuTemplateDirective } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { DefaultComponent } from "../../class/default.component";
import { String } from '../../class/source';
import { Observable } from "rxjs/Observable";
import { Subscription } from "rxjs";
import { GridFilterComponent } from "./component/grid-filter.component";
//import { Component } from '@angular/core'




@Directive({
  selector: '[sppc-grid-column]',
  providers: [String, DefaultComponent, GridFilterComponent]
})

export class SppcGridColumn {
  constructor( @Host() private hostColumn: ColumnComponent,
    @Host() public hostColumn1: DefaultComponent, private elementRef: ElementRef,
    private renderer: Renderer2, private translate: TranslateService, @Host() private gridSetting: GridFilterComponent) {
    //var props = def.properties;
  }


  @Input('sppc-grid-column') value: string;

  ngOnInit() {
    var item = this.hostColumn1;

  }


  ngOnChanges() {

    this.hostColumn.resizable = true;
    this.hostColumn.sortable = true;

    var parts = this.value.split('.');

    //this.hostColumn.field = parts[1];

    //var key = parts[0] + "." + parts[1].charAt(0).toUpperCase() + parts[1].slice(1);

    this.hostColumn.field = parts.slice(1).join('.');

    for (var i = 0; i < parts.length; i++) {
      parts[i] = parts[i].charAt(0).toUpperCase() + parts[i].slice(1);
    }
    
    var key = parts.join('.');

    this.translate.get(key).subscribe((msg: string) => {
      this.hostColumn.title = msg;
    });

  }

  ngAfterContentInit(): void {


    //this.elementRef.nativeElement.addEventListener('keydown', this.myEventHandler, false);

  }

  ngAfterViewInit() {

    var self = this.hostColumn;

    var items = document.getElementsByTagName('kendo-dropdownlist');

    if (items.length > 0) {

      for (var i = 0; i < items.length; i++) {
        var element = items.item(i);
        if (element.getAttribute('used') == null) {
          var observer = new MutationObserver(mutations => {
            mutations.forEach(function (mutation) {
              //console.log(mutation.type);
              if (mutation.type == 'attributes' && mutation.attributeName == "ng-reflect-value") {
                var temp = <any>self.filterCellTemplate.templateRef;
                temp._parentView.component.reloadGrid();
                //self..dataStateChange.emit(self)
                //observer.disconnect();

              }
            });
          });

          var config = { attributes: true, childList: true, characterData: true };

          observer.observe(element, config);
          element.setAttribute('used', '1');
        }
      }

    }

  }

  getAllElementsWithAttribute(attribute) {

    var matchingElements = [];
    var allElements = document.getElementsByTagName('*');
    for (var i = 0, n = allElements.length; i < n; i++) {
      if (allElements[i].getAttribute(attribute) !== null) {
        // Element exists with attribute. Add to array.
        matchingElements.push(allElements[i]);
      }
    }
    return matchingElements;
  }

  //my new properties
}
