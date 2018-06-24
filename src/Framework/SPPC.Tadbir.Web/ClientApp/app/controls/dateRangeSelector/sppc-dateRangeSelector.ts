import { Component, OnInit, Input, forwardRef, OnChanges, OnDestroy, ViewChild, Host, ElementRef } from '@angular/core';
import { ControlValueAccessor, NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, FormBuilder, FormGroup } from '@angular/forms'
import { DatePipe } from '@angular/common'
import { BaseFilterCellComponent, FilterService } from '@progress/kendo-angular-grid';
import { Filter } from '../../class/filter';
import { CompositeFilterDescriptor } from '@progress/kendo-data-query';


@Component({
    selector: 'sppc-dateRangeSelector',
    templateUrl: './sppc-dateRangeSelector.html',
    //template: ``,
    styles: [`
#drs-content{
    margin-bottom: 10px;}
.float-right{float:right;}
`]
})
export class SppcDateRangeSelector extends BaseFilterCellComponent implements OnInit {

    rtl: boolean = true;
    myForm: FormGroup;
    filter: CompositeFilterDescriptor;

    @Input() Field: string;
    @Input() minDate: any;
    @Input() maxDate: any;
    @Input() isDisplayFromDate: boolean = true;
    @Input() isDisplayToDate: boolean = true;
    @Input() displayFromDate: any;
    @Input() displayToDate: any;

    constructor(filterService: FilterService, private formBuilder: FormBuilder) {
        super(filterService);
    }

    ngOnInit(): void {

        var lang: string = "fa";
        if (localStorage.getItem('lang') != null) {
            var item: string | null;
            item = localStorage.getItem('lang');
            if (item)
                lang = item;
        }
        else
            if (sessionStorage.getItem('lang') != null) {
                var item: string | null;
                item = sessionStorage.getItem('lang');
                if (item)
                    lang = item;
            }

        if (lang == "fa")
            this.rtl = true;
        else
            this.rtl = false;
        
        this.myForm = this.formBuilder.group({
            fromDate: '',
            toDate: ''
        });

        this.onChanges();
    }

    onChanges(): void {

        this.myForm.valueChanges.subscribe(val => {

            const filters = [];

            if (val.fromDate != undefined && val.fromDate != "") {
                filters.push({
                    field: this.Field,
                    operator: "gte",
                    value: val.fromDate
                });
            }

            if (val.toDate != undefined && val.toDate != "") {
                filters.push({
                    field: this.Field,
                    operator: "lte",
                    value: val.toDate
                });
            }

            this.filterService.filter({
                logic: "and",
                filters: filters
            });

        });
    }

}
