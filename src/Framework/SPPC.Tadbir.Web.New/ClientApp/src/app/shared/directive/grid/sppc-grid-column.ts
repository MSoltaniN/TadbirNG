
import { Directive, Host, Input, ElementRef, Renderer2 } from "@angular/core";
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { GridFilterComponent } from "./component/grid-filter.component";
import { DefaultComponent } from "@sppc/shared/class";




@Directive({
  selector: '[sppc-grid-column]',
  providers: [String, DefaultComponent, GridFilterComponent]
})

export class SppcGridColumn {
  constructor(@Host() private hostColumn: ColumnComponent, private elementRef: ElementRef,
    private translate: TranslateService, @Host() private gridSetting: GridFilterComponent) {
    //var props = def.properties;
  }


  @Input('sppc-grid-column') value: string;

  ngOnInit() {
  }


  ngOnChanges() {

    this.hostColumn.resizable = true;
    this.hostColumn.sortable = true;

    var parts = this.value.split('.');

    //this.hostColumn.field = parts[1];

    //var key = parts[0] + "." + parts[1].charAt(0).toUpperCase() + parts[1].slice(1);

    //this.hostColumn.field = parts.slice(1).join('.');

    for (var i = 0; i < parts.length; i++) {
      parts[i] = parts[i].charAt(0).toUpperCase() + parts[i].slice(1);
    }
    
    var key = parts.join('.');

    this.translate.get(key).subscribe((msg: string) => {
      this.hostColumn.title = msg;
    });

    var secondParts = parts.slice(1);
    for (var i = 0; i < secondParts.length; i++) {
      secondParts[i] = secondParts[i].charAt(0).toLowerCase() + secondParts[i].slice(1);
    }

    this.hostColumn.field = secondParts.join('.');
  }

  ngAfterContentInit(): void {


    //this.elementRef.nativeElement.addEventListener('keydown', this.myEventHandler, false);

  }

  ngAfterViewInit() {

    var self = this.hostColumn;

    //var items = document.getElementsByTagName('kendo-dropdownlist');
    var items = document.getElementsByClassName('k-dropdown-operator');
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
