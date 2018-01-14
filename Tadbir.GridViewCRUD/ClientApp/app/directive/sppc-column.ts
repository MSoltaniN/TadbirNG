
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent } from "@progress/kendo-angular-grid";




@Directive({
    selector: '[sppc-number-column]'
})

export class SppcNumberColumn {
    constructor( @Host() private hostColumn: ColumnComponent)
    {
        this.hostColumn.title = (this.value == "true" ? "numeric" : "text");
    }

    @Input('sppc-number-column') value: string;

    ngOnInit() {
        
    }

    ngOnChanges() {
        
    }

    ngAfterContentInit(): void {
        
        
    }

   
    //my new properties
}