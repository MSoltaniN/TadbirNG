
export class FilterColumn  {
  name: string;
  title: string;        
  dataType: string;  
}

export class FilterRow {
  columnName: string;
  columnTitle: string;
  value: string;
  operator: string;
  operatorTitle: string;
  logicalOperatorTitle: string;
  logicOperator: string;
  index: number;
  filterTitle: string;
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



