
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent, FilterMenuTemplateDirective } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";




@Directive({
    selector: '[sppc-number-column]'
})

export class SppcNumberColumn {
    constructor( @Host() private hostColumn: ColumnComponent, private translate: TranslateService)
    {
        
    }

    @Input('sppc-number-column') value: string;

    ngOnInit() {
        
    }

    ngOnChanges() {

        
        this.hostColumn.resizable = true;
        this.hostColumn.sortable = true;

        var parts = this.value.split('.');        

        this.hostColumn.field = parts[1].toLowerCase();
        
        this.translate.get(this.value).subscribe((msg: string) => {
            this.hostColumn.title = msg;
        });

    }

    ngAfterContentInit(): void {
        
        
    }

   
    //my new properties
}