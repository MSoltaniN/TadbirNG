//#region client models for advance filter
export class FilterColumn {
}
export class Braces {
}
export class FilterRow {
}
//#endregion
export class GroupFilter {
}
export var NumberOperatorResource;
(function (NumberOperatorResource) {
    NumberOperatorResource["EQ"] = "AdvanceFilter.EQ";
    NumberOperatorResource["NEQ"] = "AdvanceFilter.NEQ";
    NumberOperatorResource["GT"] = "AdvanceFilter.GT";
    NumberOperatorResource["GTE"] = "AdvanceFilter.GTE";
    NumberOperatorResource["LTE"] = "AdvanceFilter.LTE";
    NumberOperatorResource["LT"] = "AdvanceFilter.LT";
})(NumberOperatorResource || (NumberOperatorResource = {}));
export var LoginOperatorResource;
(function (LoginOperatorResource) {
    LoginOperatorResource["And"] = "AdvanceFilter.And";
    LoginOperatorResource["Or"] = "AdvanceFilter.Or";
})(LoginOperatorResource || (LoginOperatorResource = {}));
export var StringOperatorResource;
(function (StringOperatorResource) {
    StringOperatorResource["EQ"] = "AdvanceFilter.EQ";
    StringOperatorResource["NEQ"] = "AdvanceFilter.NEQ";
    StringOperatorResource["StartWith"] = "AdvanceFilter.StartWith";
    StringOperatorResource["EndsWith"] = "AdvanceFilter.EndsWith";
    StringOperatorResource["Like"] = "AdvanceFilter.Contains";
    StringOperatorResource["NotLike"] = "AdvanceFilter.DoesNotContain";
})(StringOperatorResource || (StringOperatorResource = {}));
export var BooleanOperatorResource;
(function (BooleanOperatorResource) {
    BooleanOperatorResource["EQ"] = "AdvanceFilter.EQ";
    BooleanOperatorResource["NEQ"] = "AdvanceFilter.NEQ";
})(BooleanOperatorResource || (BooleanOperatorResource = {}));
export class Guid {
    static newGuid() {
        return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
            return v.toString(16);
        });
    }
}
//# sourceMappingURL=filterColumn.js.map