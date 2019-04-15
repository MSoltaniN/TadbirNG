

export interface QuickReportViewModel
{
    reportTitle:string;

    inchValue:number;
    
    columns:Array<QuickReportColumnModel>;
    
}

export interface QuickReportColumnModel
{       
    name:string;    
    defaultText:string;   
    index:number;
    sortMode : number;    
    sortOrder:number;
    width : number;
    enabled : boolean;    
    order : number;
    userText : string;    
    dataType : number;    
    visible : boolean;
}