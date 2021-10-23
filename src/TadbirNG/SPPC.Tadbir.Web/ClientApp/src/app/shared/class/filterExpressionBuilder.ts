import { FilterExpression } from "./filterExpression";
import { Filter } from "./filter";
import { FilterExpressionOperator } from "./filterExpressionOperator";



export class FilterExpressionBuilder{           

    public Expression: FilterExpression;

    constructor() {}

    public New(filter: Filter): FilterExpressionBuilder {
        var exp = new FilterExpression();
        exp.filter = filter;
        this.Expression = exp;
        return this;
    }

    public And(filter: Filter | Filter[]): FilterExpressionBuilder {
        if (filter instanceof Filter) {
            if (this.Expression == undefined) {
                this.New(filter);
            }
            else {
                var exp = new FilterExpression();
                exp.filter = filter;
                exp.operator = FilterExpressionOperator.And;
                //exp.parent = this.Expression;
                this.Expression.children.push(exp);
            }
        }
        else {
            for (let item of filter) {
                if (this.Expression == undefined) {
                    this.New(item);
                }
                else {
                    this.And(item);
                }
            }
        }
        return this;
    }

    public Or(filter: Filter | Filter[]): FilterExpressionBuilder {
        if (filter instanceof Filter) {
            if (this.Expression == undefined) {
                this.New(filter);
            }
            else {
                var exp = new FilterExpression();
                exp.filter = filter;
                exp.operator = FilterExpressionOperator.Or;
                //exp.parent = this.Expression;
                this.Expression.children.push(exp);
            }            
        }
        else {
            for (let item of filter) {
                if (this.Expression == undefined) {
                    this.New(item);
                }
                else {
                    this.Or(item);
                }         
            }
        }
        return this;
    }

    public Build(): FilterExpression {
        return this.Expression;
    }
}
