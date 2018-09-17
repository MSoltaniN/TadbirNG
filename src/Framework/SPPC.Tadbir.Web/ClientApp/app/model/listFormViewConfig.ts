import { ColumnViewConfig } from "./columnViewConfig";
import { IEntity } from "./IEntity";

export interface ListFormViewConfig extends IEntity{

    viewId: number;
    pageSize: number;

    columnViews: Array<ColumnViewConfig>
    

}