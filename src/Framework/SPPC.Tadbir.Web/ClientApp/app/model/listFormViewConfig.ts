import { ColumnViewConfig } from "./columnViewConfig";

export interface ListFormViewConfig {

    viewId: number;
    pageSize: number;

    columnViews: Array<ColumnViewConfig>
    

}