
import { Component, Input, Host, OnChanges } from '@angular/core';
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";  //npm shrinkwrap
import { BaseFilterCellComponent, ColumnComponent, FilterService } from '@progress/kendo-angular-grid';
import * as moment from 'jalali-moment';
import { BrowserStorageService } from '@sppc/shared/services';

@Component({
  selector: 'sppc-auto-grid-filter',
  templateUrl: './sppc-grid-filter.html'
})
export class SppcAutoGridFilter extends BaseFilterCellComponent {

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

  get currentLang() {
    return this.bgStroge.getLanguage();
  }

  filterValue;

  constructor(@Host() private hostColumn: ColumnComponent,
    filterService:FilterService,
    private bgStroge:BrowserStorageService) {
    super(filterService)
  }

  ngOnInit() {
    if (this.metaDataItem)
      this.allowFiltering = this.metaDataItem.allowFiltering;
  }

  convertShamsiToMiladi($e:KeyboardEvent) {
    if (this.currentLang == 'fa') {
      let value = (<any>$e.target).value as string;
  
      if (value.length == 10 && value.split('/').length == 3) {
        let currentFilter = this.filter.filters.find((f:any) => f.field.toLowerCase().includes('date'));
        let miladi = +value.split('/')[0] < 1600? this.toMiladiDate(value) :value;
  
        if (+value.split('/')[0] < 1600) {
          this.filterValue = value;
        }

        this.filter = this.removeFilter((<any>currentFilter).field);
        const filters = [];
        filters.push({
          field: (<any>currentFilter).field,
          operator: (<any>currentFilter).operator,
          value: miladi
        });
        const root: CompositeFilterDescriptor = this.filter || {
          logic: "and",
          filters: []
        };
        if (filters.length) {
          root.filters.push(...filters);
        }

        this.updateFilter(filters[0]);
  
        this.filterService.filter(root);
  
        // for displaing typed the string date typed by client
        if ($e.key == 'Enter' && this.filterValue) {
          setTimeout(() => {
            (<any>$e.target).value = this.filterValue;
            console.log($e.key,(<any>$e.target).value);
          }, 100);
        }
      }
    }
  }

  toTrim($e:KeyboardEvent) {
    let value = (<any>$e.target).value as string;
    let currentFilter = this.filter.filters.find((f:any) => f.field == 'name' );
    if (($e.key == ' ' || $e.key == 'Enter') && currentFilter) {
      
      this.filterValue = value;
      this.filter = this.removeFilter((<any>currentFilter)?.field);
      const filters = [];
      filters.push({
        field: (<any>currentFilter).field,
        operator: (<any>currentFilter).operator,
        value: value.trim()
      });
      const root: CompositeFilterDescriptor = this.filter || {
        logic: "and",
        filters: []
      };
      if (filters.length) {
        root.filters.push(...filters);
      }
  
      this.filterService.filter(root);
    }
  }

  toMiladiDate(value:string) {
    let format: string = "YYYY/MM/DD";
    moment.locale('fa');
    let MomentDate = moment(value).locale('en').format(format);    
    return MomentDate;
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
