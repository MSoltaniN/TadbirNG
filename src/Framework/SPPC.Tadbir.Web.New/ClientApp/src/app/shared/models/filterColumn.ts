
export class FilterColumn  {
  name: string;
  title: string;        
  dataType: string;
  scriptType: string;
}

export class Braces {
  outerId: string;
  brace: string;  
}

export class FilterRow {
  id: string;
  columnName: string;
  columnTitle: string;
  value: string;
  operator: string;
  operatorTitle: string;
  logicalOperatorTitle: string;
  logicOperator: string;
  index: number;
  filterTitle: string;
  order: number;
  braces: Array<Braces>;
}

export class GroupFilter {
  id: string;
  name: string;
  filters: Array<FilterRow>;  
}

export enum NumberOperatorResource {  
  EQ = "AdvanceFilter.EQ",  
  NEQ = "AdvanceFilter.NEQ",
  GT = "AdvanceFilter.GT",
  GTE = "AdvanceFilter.GTE",
  LTE = "AdvanceFilter.LTE",
  LT = "AdvanceFilter.LT"
}

export enum LoginOperatorResource {
  And = "AdvanceFilter.And",
  Or = "AdvanceFilter.Or",  
}

export enum StringOperatorResource {
  EQ = "AdvanceFilter.EQ",
  NEQ = "AdvanceFilter.NEQ",
  StartWith = "AdvanceFilter.StartWith",
  EndsWith = "AdvanceFilter.EndsWith",
  Like = "AdvanceFilter.Contains",
  NotLike = "AdvanceFilter.DoesNotContain"
}

export enum BooleanOperatorResource {
  EQ = "AdvanceFilter.EQ",
  NEQ = "AdvanceFilter.NEQ"  
}

export class Guid {
  static newGuid() {
    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
      var r = Math.random() * 16 | 0,
        v = c == 'x' ? r : (r & 0x3 | 0x8);
      return v.toString(16);
    });
  }
}



