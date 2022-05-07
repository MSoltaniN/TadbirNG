import {
  ApplicationRef,
  Component,
  ComponentFactoryResolver,
  Directive,
  ElementRef,
  EmbeddedViewRef,
  Injector,
  Input,
  OnInit,
  Renderer2,
  ViewContainerRef,
} from "@angular/core";
import {
  BaseFilterCellComponent,
  FilterService,
} from "@progress/kendo-angular-grid";
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { Property } from "@sppc/shared/class";
import { SppcGridDatepicker } from "@sppc/shared/controls/datepicker/sppc-grid-datepicker";
declare var jquery: any;
declare var $: any;

@Component({
  selector: "sppc-grid-date-filter",
  template: `<kendo-grid-string-filter-cell
    testdr
    [column]="column"
    [filter]="filter"
  >
    <kendo-filter-eq-operator></kendo-filter-eq-operator>
    <kendo-filter-neq-operator></kendo-filter-neq-operator>
  </kendo-grid-string-filter-cell>`,
})
export class SppcGridDateFilter
  extends BaseFilterCellComponent
  implements OnInit
{
  //public get selectedValue(): any {
  //    const filter = this.filterByField(this.value);
  //    return filter ? filter.value : null;
  //}

  @Input() public filter: CompositeFilterDescriptor;

  @Input() public column: string;

  constructor(
    filterService: FilterService,
    con: ViewContainerRef,
    el: ElementRef
  ) {
    var i = 0;

    super(filterService);
  }

  ngOnInit() {}

  //public onChange(value: any): void {
  //    this.applyFilter(
  //        value === null ? // value of the default item
  //            this.removeFilter(this.valueField) : // remove the filter
  //            this.updateFilter({ // add a filter for the field with the value
  //                field: this.valueField,
  //                operator: 'eq',
  //                value: value
  //            })
  //    ); // update the root filter
  //}
}

@Directive({
  selector: "[FilterDatePickerDirective]",
})
export class FilterDatePickerDirective implements OnInit {
  private conn: ViewContainerRef;

  private elrf: ElementRef;

  private hiddenId: string;

  private factoryResolver: ComponentFactoryResolver;

  @Input("FilterDatePickerDirective") value: string;

  constructor(
    public con: ViewContainerRef,
    public el: ElementRef,
    private renderer: Renderer2,
    private componentFactoryResolver: ComponentFactoryResolver,
    private appRef: ApplicationRef,
    private injector: Injector,
    private _viewContainerRef: ViewContainerRef
  ) {
    this.conn = con;
    this.elrf = el;
    this.factoryResolver = componentFactoryResolver;
  }

  ngOnInit(): void {
    var id = Guid.newGuid();
    this.hiddenId = id;

    if (this.elrf.nativeElement.childNodes.length < 2) return;

    this.elrf.nativeElement.childNodes[1].childNodes[2].style =
      "visibility:hidden;width: 0px;margin:0;padding:0;";
    this.elrf.nativeElement.childNodes[1].childNodes[2].setAttribute("id", id);

    this.elrf.nativeElement.childNodes[1].childNodes[5].childNodes[3].setAttribute(
      "id",
      "btnClear_" + id
    );

    var mainElement = document.getElementById("btnClear_" + id);

    if (mainElement)
      mainElement.addEventListener("click", this.clearFilterClick.bind(this));

    let property = <Property>(
      this._viewContainerRef["_data"].componentView.parent.component
        .metaDataItem
    );

    setTimeout(() => {
      this.appendComponent(
        SppcGridDatepicker,
        this.elrf.nativeElement.childNodes[1].childNodes[2],
        this.elrf.nativeElement.childNodes[1],
        property.type
      );
    }, 1);
  }

  clearFilterClick(event: any) {
    var hiddenElement = document.getElementById("date_" + this.hiddenId) as any;
    hiddenElement.value = "";
  }

  ngAfterContentInit() {}

  appendComponent(component: any, before: any, host: any, displayType: string) {
    const componentRef = this.componentFactoryResolver
      .resolveComponentFactory(component)
      .create(this.injector);

    (<SppcGridDatepicker>componentRef.instance).destinationElementId =
      this.hiddenId;
    (<SppcGridDatepicker>componentRef.instance).mode = this.value;
    (<SppcGridDatepicker>componentRef.instance).displayType = displayType;

    this.appRef.attachView(componentRef.hostView);

    const domElem = (componentRef.hostView as EmbeddedViewRef<any>)
      .rootNodes[0] as HTMLElement;

    var input = domElem.childNodes[0].childNodes[1].childNodes[1]
      .childNodes[1] as HTMLElement;
    input.id = "date_" + this.hiddenId;

    this.renderer.insertBefore(host, domElem, before);
  }

  onChange(event: any) {
    var elHidden = document.getElementById(this.hiddenId);
    if (elHidden) elHidden.nodeValue = event.value;
  }
}

class Guid {
  static newGuid() {
    return "xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx".replace(
      /[xy]/g,
      function (c) {
        var r = (Math.random() * 16) | 0,
          v = c == "x" ? r : (r & 0x3) | 0x8;
        return v.toString(16);
      }
    );
  }
}
