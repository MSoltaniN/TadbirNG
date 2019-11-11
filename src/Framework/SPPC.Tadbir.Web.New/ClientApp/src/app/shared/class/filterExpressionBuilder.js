import { FilterExpression } from "./filterExpression";
import { Filter } from "./filter";
import { FilterExpressionOperator } from "./filterExpressionOperator";
export class FilterExpressionBuilder {
    constructor() { }
    New(filter) {
        var exp = new FilterExpression();
        exp.filter = filter;
        this.Expression = exp;
        return this;
    }
    And(filter) {
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
    Or(filter) {
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
    Build() {
        return this.Expression;
    }
}
//# sourceMappingURL=filterExpressionBuilder.js.map