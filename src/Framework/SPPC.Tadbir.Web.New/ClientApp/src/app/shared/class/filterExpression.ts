import { Filter } from "./filter";


//*** this class add for filter values for gridview */

export class FilterExpression {

    filter: Filter;
    operator: string;
    parent: FilterExpression;
    children: FilterExpression[] = [];
    totalfilter: FilterExpression;
}