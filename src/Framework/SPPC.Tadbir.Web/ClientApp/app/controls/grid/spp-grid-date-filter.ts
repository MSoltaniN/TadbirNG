import { Directive, OnInit, ElementRef, ViewContainerRef, Component, Input, ViewChild, Renderer2, ComponentFactoryResolver, Inject, ApplicationRef, Injector, EmbeddedViewRef, AfterViewInit, Host } from "@angular/core";
import { StringFilterComponent } from "@progress/kendo-angular-grid/dist/es2015/filtering/string-filter.component";
import { LocalizationService } from "@progress/kendo-angular-l10n";
import { FilterService, BaseFilterCellComponent } from "@progress/kendo-angular-grid";
import { CompositeFilterDescriptor } from "@progress/kendo-data-query";
import { DefaultComponent } from "../../class/default.component";
import { DatePickerDirective } from "ng2-jalali-date-picker";
import { SppcDatepicker } from "../datepicker/sppc-datepicker";
import { SppcGridDatepicker } from "../datepicker/sppc-grid-datepicker";
declare var jquery: any;
declare var $: any;

@Component({
    selector: 'sppc-grid-date-filter',
    template: `<kendo-grid-string-filter-cell testdr [column]="column" [filter]="filter">
    <kendo-filter-eq-operator></kendo-filter-eq-operator>
    <kendo-filter-neq-operator></kendo-filter-neq-operator>    
</kendo-grid-string-filter-cell>` 
})
export class SppcGridDateFilter extends BaseFilterCellComponent implements OnInit {

    //public get selectedValue(): any {
    //    const filter = this.filterByField(this.value);
    //    return filter ? filter.value : null;
    //}

    @Input() public filter: CompositeFilterDescriptor;
    
    @Input() public column: string;
    
    constructor(filterService: FilterService, con: ViewContainerRef, el: ElementRef) {
        var i = 0;

        super(filterService);

    }

    ngOnInit() {

    }
   
    
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
    selector: '[testdr]'
    
})
export class TestDr implements OnInit  {
   

    private conn: ViewContainerRef;

    private elrf: ElementRef;

    private hiddenId: string;

    private factoryResolver: ComponentFactoryResolver;

    constructor(public con: ViewContainerRef, public el: ElementRef,
        private renderer: Renderer2, private componentFactoryResolver: ComponentFactoryResolver,
       private appRef: ApplicationRef, private injector: Injector) {
        this.conn = con;
        this.elrf = el;
        this.factoryResolver = componentFactoryResolver;

       
    }

    ngOnInit(): void {
        
        var id = Guid.newGuid();
        this.hiddenId = id;

        this.elrf.nativeElement.childNodes[1].childNodes[2].style = 'visibility:hidden;width: 0px;margin:0;padding:0;';
        this.elrf.nativeElement.childNodes[1].childNodes[2].setAttribute('id', id);        
        
        setTimeout(() => {
            this.appendComponent(SppcGridDatepicker, this.elrf.nativeElement.childNodes[1].childNodes[2], this.elrf.nativeElement.childNodes[1]);
        }, 1);
       
    }

    ngAfterContentInit() {
        
        
    }

    appendComponent(component: any,before : any , host : any) {
        const componentRef = this.componentFactoryResolver
            .resolveComponentFactory(component)
            .create(this.injector);


        (<SppcGridDatepicker>componentRef.instance).destinationElementId = this.hiddenId;
            

        this.appRef.attachView(componentRef.hostView);

        const domElem = (componentRef.hostView as EmbeddedViewRef<any>)
            .rootNodes[0] as HTMLElement;
        
        var input = (domElem.childNodes[0].childNodes[1].childNodes[1].childNodes[1]) as HTMLElement;
        input.id = "date_" + this.hiddenId;
        
        this.renderer.insertBefore(host, domElem,before);

    }

    onChange(event: any) {        
        var elHidden = document.getElementById(this.hiddenId);
        if(elHidden)
            elHidden.nodeValue = event.value;
    }
    


}



class Guid {
    static newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}