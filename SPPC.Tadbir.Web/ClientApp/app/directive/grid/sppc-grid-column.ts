
import { Directive, Host, Input } from "@angular/core";
import { ColumnComponent, FilterMenuTemplateDirective } from "@progress/kendo-angular-grid";
import { TranslateService } from "ng2-translate";




@Directive({
    selector: '[sppc-grid-column]'
})

export class SppcGridColumn {
    constructor( @Host() private hostColumn: ColumnComponent, private translate: TranslateService)
    {
        
    }

    @Input('sppc-grid-column') value: string;

    ngOnInit() {
        
    }

    ngOnChanges() {

        
        this.hostColumn.resizable = true;
        this.hostColumn.sortable = true;

        var parts = this.value.split('.');        

        this.hostColumn.field = parts[1];
        
        var key = parts[0] + "." + parts[1].charAt(0).toUpperCase() + parts[1].slice(1);
        this.translate.get(key).subscribe((msg: string) => {
            this.hostColumn.title = msg;
        });

    }

    ngAfterContentInit(): void {
        
        
    }

   
    //my new properties
}