
import { Component, Input, Host } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";  //npm shrinkwrap
import { ColumnComponent } from '@progress/kendo-angular-grid';

@Component({
  selector: 'sppc-auto-grid-filter',
  templateUrl: './sppc-grid-filter.html'
})
export class SppcAutoGridFilter {

  @Input() public column: string;
  @Input() public filter: CompositeFilterDescriptor;
  @Input() public isNumber: boolean = false;
  @Input() public isString: boolean = false;
  @Input() public isDate: boolean = false;
  @Input() public isDateTime: boolean = false;
  @Input() public allowFiltering: boolean = true;
  @Input() public metaDataItem: any;  

  @Input('metaData')
  public set metaData(value: string | undefined) {
    if (value == undefined) return;
    switch (value.toLowerCase()) {
      case "number":
        this.isNumber = true;
        break;
      case "string":
        this.isString = true;
        break;
      case "date":
        this.isDate = true;
        break;
      case "datetime":
        this.isDateTime = true;
        break;
      default:
        break;
    }

  }

  constructor(@Host() private hostColumn: ColumnComponent) {
    
  }

  ngOnInit() {
    if (this.metaDataItem)
      this.allowFiltering = this.metaDataItem.allowFiltering;
  }

 
  ngAfterViewInit() {   

    var self = this.hostColumn;

    var items = document.getElementsByClassName('k-dropdown-operator');
    if (items.length > 0) {

      for (var i = 0; i < items.length; i++) {
        var element = items.item(i);
        if (element.getAttribute('used') == null) {
          var observer = new MutationObserver(mutations => {
            mutations.forEach(function (mutation) {
              if (mutation.type == 'attributes' && mutation.attributeName == "ng-reflect-value") {
                //var temp = <any>self.filterCellTemplate.templateRef;
                //temp._parentView.component.reloadGrid();            
              }
            });
          });

          var config = { attributes: true, childList: true, characterData: true };

          observer.observe(element, config);
          element.setAttribute('used', '1');

          var btnRemoveFilter = element.nextElementSibling;          
          if (btnRemoveFilter) {
            btnRemoveFilter.addEventListener('click', () => {              
              //var inputFilter = btnRemoveFilter.previousElementSibling.previousElementSibling;
              //inputFilter.nodeValue = "";
              var temp = <any>self.filterCellTemplate.templateRef;
              temp._parentView.component.reloadGrid();
            });                 
                
            btnRemoveFilter.setAttribute('used', '1');

          }
        }
      }

    }

  }

}
