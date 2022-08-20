USE [@SysDbName]
GO

UPDATE [Reporting].[LocalReport] 
SET Template = N'{   "ReportVersion": "2019.2.3",   "ReportGuid": "50bf520f60f20f1161422368133ce196",   "ReportName": "Report",   "ReportAlias": "Report",   "ReportFile": "Report.mrt",   "ReportCreated": "/Date(1558459429000+0430)/",   "ReportChanged": "/Date(1558750637000+0430)/",   "EngineVersion": "EngineV2",   "CalculationMode": "Interpretation",   "ReportUnit": "Inches",   "PreviewSettings": 268435455,   "Styles": {     "0": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Report title",       "Name": "Tadbir_ReportTitle",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:255,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "1": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Report footer",       "Name": "Tadbir_ReportFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "2": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Page header",       "Name": "Tadbir_PageHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "3": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Page footer",       "Name": "Tadbir_PageFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;11.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseBorderSidesFromLocation": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "4": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Parameters title",       "Name": "Tadbir_ParameterTitle",       "HorAlignment": "Right",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "5": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Parameters value",       "Name": "Tadbir_ParameterValue",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:139,69,19",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "6": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Text column title",       "Name": "Tadbir_ColumnTextHeader",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "7": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Numeric column title",       "Name": "Tadbir_ColumnNumberHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "8": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Text data",       "Name": "Tadbir_ColumnTextData",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "9": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Numeric data",       "Name": "Tadbir_ColumnNumberData",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "10": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Date data",       "Name": "Tadbir_ColumnDateData",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "11": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "Page number",       "Name": "Tadbir_PageNumber",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;9.75;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     }   },   "Dictionary": {     "Variables": {       "0": {         "Name": "vReportTitle",         "DialogInfo": {           "DateTimeType": "DateAndTime"         },         "Alias": "report title",         "Type": "System.String"       },       "1": {         "Name": "vReportFirstTitle",         "DialogInfo": {           "DateTimeType": "DateAndTime"         },         "Alias": "report first title",         "Type": "System.String"       },       "2": {         "Name": "vReportSummaryTitle",         "DialogInfo": {           "DateTimeType": "DateAndTime"         },         "Alias": "report footer text",         "Type": "System.String"       }     }   },   "Pages": {     "0": {       "Ident": "StiPage",       "Name": "Page1",       "Guid": "19d54b1fd3494d4f9caefe40a5cfde4b",       "Interaction": {         "Ident": "StiInteraction"       },       "Border": ";;2;;;;;solid:Black",       "Brush": "solid:Transparent",       "Components": {         "0": {           "Ident": "StiReportTitleBand",           "Name": "ReportTitle",           "ClientRectangle": "0,0.2,7.49,0.4",           "Alias": "بخش عنوان گزارش - نمایش در صفحه اول گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportHeader",               "Guid": "f67cff7056872b69bf5946e8a49b65d4",               "ClientRectangle": "2.8,0,2.1,0.3",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportFirstTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "1": {           "Ident": "StiPageFooterBand",           "Name": "PageFooter",           "ClientRectangle": "0,11.29,7.49,0.4",           "Alias": "بخش فوتر صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageNumber",               "Guid": "591b0a4d9e59c4fd83ad1dbd4588ee4e",               "ClientRectangle": "3.2,-0.02,1.3,0.3",               "Alias": "شماره صفحه",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{PageNumber}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "Type": "Expression"             }           }         },         "2": {           "Ident": "StiPageHeaderBand",           "Name": "PageHeader",           "ClientRectangle": "0,1,7.49,0.8",           "Alias": "بخش عنوان صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageHeader",               "Guid": "557c3f85101d4160a57980988cfc1cc1",               "ClientRectangle": "2.9,0.1,2.1,0.5",               "ComponentStyle": "Tadbir_ReportTitle",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;14.25;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:255,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "3": {           "Ident": "StiTable",           "Name": "Table1",           "ClientRectangle": "0,2.2,7.49,0.3",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;None;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiTableCell",               "Name": "Table1_Cell1",               "Guid": "bc5fa7bb16d255d99db5551b5fec13e6",               "ClientRectangle": "0,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "from parameter"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 5,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Left",               "ID": 0             },             "1": {               "Ident": "StiTableCell",               "Name": "Table1_Cell2",               "Guid": "253ef13ce543bbad6a0da14fa476ad19",               "ClientRectangle": "1.9,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/01/01"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 9,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Left",               "ID": 1             },             "2": {               "Ident": "StiTableCell",               "Name": "Table1_Cell3",               "Guid": "38081d6522a5daa7f6fd1b1fbd5ece5f",               "ClientRectangle": "3.8,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "to parameter"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Left",               "ID": 2             },             "3": {               "Ident": "StiTableCell",               "Name": "Table1_Cell4",               "Guid": "3a930c25fea4cb09525477618d29cd96",               "ClientRectangle": "5.6,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/05/05"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Left",               "ID": 3             }           },           "MinHeight": 0.1,           "AutoWidth": "Page",           "RowCount": 1,           "ColumnCount": 4,           "NumberID": 25         },         "4": {           "Ident": "StiColumnHeaderBand",           "Name": "ColumnHeaderBand",           "CanShrink": true,           "ClientRectangle": "0,2.9,7.49,0.4",           "Alias": "بخش عناوین ستون ها",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnHeader",               "Guid": "10214f235f3f47c399dfd5a98a68f584",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnNumberHeader",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "numeric column"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "Margins": {                 "Left": 0,                 "Right": 6,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             },             "1": {               "Ident": "StiText",               "Name": "Text1",               "Guid": "fae635cfd25883991056df3bfbd59b71",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "4.6,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnTextHeader",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "text column"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "Margins": {                 "Left": 0,                 "Right": 6,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             }           }         },         "5": {           "Ident": "StiDataBand",           "Name": "DataBand1",           "CanShrink": true,           "ClientRectangle": "0,3.7,7.49,0.3",           "Alias": "بخش دیتا یا رکورد های اطلاعاتی",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnData",               "Guid": "3eb3c72204f94fb4a2508a352f28bae1",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "3.1,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnNumberData",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "numeric data"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "Margins": {                 "Left": 0,                 "Right": 6,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             },             "1": {               "Ident": "StiText",               "Name": "Text2",               "Guid": "0f15dfa8bd35d03493c8304d4af1ec81",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "4.6,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnTextData",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "text data"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "WordWrap": true               },               "Margins": {                 "Left": 0,                 "Right": 6,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             },             "2": {               "Ident": "StiText",               "Name": "Text3",               "Guid": "57df6bf0447330e932b52b060d73e56a",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "1.6,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnDateData",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "date data"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "Type": "Expression"             }           }         },         "6": {           "Ident": "StiReportSummaryBand",           "Name": "ReportSummary",           "ClientRectangle": "0,4.4,7.49,0.3",           "Alias": "بخش فوتر گزارش - نمایش در صفحه آخر گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportFooter",               "Guid": "f9efae84676440e7bae58c451dec3b9c",               "ClientRectangle": "2.5,0,2.6,0.3",               "ComponentStyle": "Tadbir_ReportFooter",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportSummaryTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         }       },       "PaperSize": "A4",       "TitleBeforeHeader": true,       "PageWidth": 8.27,       "PageHeight": 11.69,       "Watermark": {         "TextBrush": "solid:50,0,0,0"       },       "Margins": {         "Left": 0.39,         "Right": 0.39,         "Top": 0,         "Bottom": 0       }     }   } }'
WHERE ReportID = 43 AND LocaleID = 1

UPDATE [Reporting].[LocalReport] 
SET Template = N'{   "ReportVersion": "2019.2.3",   "ReportGuid": "5fc1901de64956deaed04f39b2311b32",   "ReportName": "Report",   "ReportAlias": "Report",   "ReportFile": "Report.mrt",   "ReportCreated": "/Date(1558556629000+0430)/",   "ReportChanged": "/Date(1558847837000+0430)/",   "EngineVersion": "EngineV2",   "CalculationMode": "Interpretation",   "ReportUnit": "Inches",   "PreviewSettings": 268435455,   "Styles": {     "0": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان گزارش",       "Name": "Tadbir_ReportTitle",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:255,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "1": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی گزارش",       "Name": "Tadbir_ReportFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "2": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان صفحه",       "Name": "Tadbir_PageHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;14.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "3": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "پاورقی صفحه",       "Name": "Tadbir_PageFooter",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;11.25;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseBorderSidesFromLocation": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "4": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان پارامتر ها",       "Name": "Tadbir_ParameterTitle",       "HorAlignment": "Right",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     },     "5": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "مقدار پارامتر ها",       "Name": "Tadbir_ParameterValue",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:139,69,19",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "6": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان ستون متنی",       "Name": "Tadbir_ColumnTextHeader",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "7": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "عنوان ستون عددی",       "Name": "Tadbir_ColumnNumberHeader",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;Bold;",       "Border": "All;155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:211,211,211",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "8": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "داده متنی",       "Name": "Tadbir_ColumnTextData",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "9": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "داده عددی",       "Name": "Tadbir_ColumnNumberData",       "HorAlignment": "Right",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "10": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "داده تاریخ",       "Name": "Tadbir_ColumnDateData",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;12;;",       "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:0,0,0",       "NegativeTextBrush": "solid:Transparent",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseTextFormat": true     },     "11": {       "Ident": "StiStyle",       "CollectionName": "Tadbir",       "Conditions": {         "0": {           "Ident": "StiStyleCondition",           "Type": "ComponentType, Placement, PlacementNestedLevel",           "Placement": "ReportTitle",           "ComponentType": "Text, Primitive, Image, CheckBox"         }       },       "Description": "شماره صفحه",       "Name": "Tadbir_PageNumber",       "HorAlignment": "Center",       "VertAlignment": "Center",       "Font": "IRANSansWeb;9.75;;",       "Border": ";155,155,155;;;;;;solid:0,0,0",       "Brush": "solid:Transparent",       "TextBrush": "solid:55,55,55",       "NegativeTextBrush": "solid:255,0,0",       "AllowUseHorAlignment": true,       "AllowUseVertAlignment": true,       "AllowUseNegativeTextBrush": true,       "AllowUseTextFormat": true     }   },   "Dictionary": {     "Variables": {       "0": {         "Name": "vReportTitle",         "Alias": "عنوان گزارش",         "Type": "System.String"       },       "1": {         "Name": "vReportFirstTitle",         "Alias": "عنوان ابتدایی گزارش",         "Type": "System.String"       },       "2": {         "Name": "vReportSummaryTitle",         "Alias": "متن پاورقی گزارش",         "Type": "System.String"       }     }   },   "Pages": {     "0": {       "Ident": "StiPage",       "Name": "Page1",       "Guid": "19d54b1fd3494d4f9caefe40a5cfde4b",       "Interaction": {         "Ident": "StiInteraction"       },       "Border": ";;2;;;;;solid:Black",       "Brush": "solid:Transparent",       "Components": {         "0": {           "Ident": "StiReportTitleBand",           "Name": "ReportTitle",           "ClientRectangle": "0,0.2,7.49,0.4",           "Alias": "بخش عنوان گزارش - نمایش در صفحه اول گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportHeader",               "Guid": "f67cff7056872b69bf5946e8a49b65d4",               "ClientRectangle": "2.8,0,2.1,0.3",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportFirstTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "1": {           "Ident": "StiPageFooterBand",           "Name": "PageFooter",           "ClientRectangle": "0,11.29,7.49,0.4",           "Alias": "بخش فوتر صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageNumber",               "Guid": "591b0a4d9e59c4fd83ad1dbd4588ee4e",               "ClientRectangle": "3.2,-0.02,1.3,0.3",               "Alias": "شماره صفحه",               "ComponentStyle": "Tadbir_PageNumber",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{PageNumber}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;9.75;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "Type": "Expression"             }           }         },         "2": {           "Ident": "StiPageHeaderBand",           "Name": "PageHeader",           "ClientRectangle": "0,1,7.49,0.8",           "Alias": "بخش عنوان صفحه - نمایش در همه صفحات گزارش",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtPageHeader",               "Guid": "557c3f85101d4160a57980988cfc1cc1",               "ClientRectangle": "2.9,0.1,2.1,0.5",               "ComponentStyle": "Tadbir_ReportTitle",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;14.25;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:255,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "3": {           "Ident": "StiTable",           "Name": "Table1",           "ClientRectangle": "0,2.2,7.49,0.3",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;None;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiTableCell",               "Name": "Table1_Cell1",               "Guid": "bc5fa7bb16d255d99db5551b5fec13e6",               "ClientRectangle": "0,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "از تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 5,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 0             },             "1": {               "Ident": "StiTableCell",               "Name": "Table1_Cell2",               "Guid": "253ef13ce543bbad6a0da14fa476ad19",               "ClientRectangle": "1.9,0,1.9,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/01/01"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Margins": {                 "Left": 9,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 1             },             "2": {               "Ident": "StiTableCell",               "Name": "Table1_Cell3",               "Guid": "38081d6522a5daa7f6fd1b1fbd5ece5f",               "ClientRectangle": "3.8,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterTitle",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "تا تاریخ"               },               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 2             },             "3": {               "Ident": "StiTableCell",               "Name": "Table1_Cell4",               "Guid": "3a930c25fea4cb09525477618d29cd96",               "ClientRectangle": "5.6,0,1.8,0.3",               "Restrictions": "AllowMove, AllowSelect, AllowChange",               "ComponentStyle": "Tadbir_ParameterValue",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "1397/05/05"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:139,69,19",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression",               "CellDockStyle": "Right",               "ID": 3             }           },           "MinHeight": 0.1,           "AutoWidth": "Page",           "RowCount": 1,           "ColumnCount": 4,           "NumberID": 25         },         "4": {           "Ident": "StiColumnHeaderBand",           "Name": "ColumnHeaderBand",           "CanShrink": true,           "ClientRectangle": "0,2.9,7.49,0.4",           "Alias": "بخش عناوین ستون ها",           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtColumnHeader",               "Guid": "10214f235f3f47c399dfd5a98a68f584",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "3.1,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnNumberHeader",               "Linked": true,               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "عنوان ستون عددی"               },               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             },             "1": {               "Ident": "StiText",               "Name": "Text2",               "Guid": "09ba599ceb81bfbc8b5d225b319a7be7",               "CanShrink": true,               "CanGrow": true,               "ClientRectangle": "4.6,0,1.5,0.4",               "ComponentStyle": "Tadbir_ColumnTextHeader",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "عنوان ستون متنی"               },               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": "All;155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:211,211,211",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         },         "5": {           "Ident": "StiDataBand",           "Name": "DataBand1",           "CanShrink": true,           "ClientRectangle": "0,3.7,7.49,0.3",           "Alias": "بخش دیتا یا رکورد های اطلاعاتی",           "Interaction": {             "Ident": "StiBandInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "Text1",               "Guid": "cb4f22991c5651ad674e5621cdd7d417",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "3.1,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnNumberData",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "داده عددی"               },               "CanBreak": true,               "HorAlignment": "Right",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true,                 "WordWrap": true               },               "Margins": {                 "Left": 6,                 "Right": 0,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             },             "1": {               "Ident": "StiText",               "Name": "Text3",               "Guid": "d23cd7875de59e1c86e54bf185ec1a46",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "4.6,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnTextData",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "داده متنی"               },               "CanBreak": true,               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true,                 "WordWrap": true               },               "Margins": {                 "Left": 0,                 "Right": 6,                 "Top": 0,                 "Bottom": 0               },               "Type": "Expression"             },             "2": {               "Ident": "StiText",               "Name": "Text4",               "Guid": "6fa74da912e564b3065fe4e3433742b4",               "CanShrink": true,               "CanGrow": true,               "GrowToHeight": true,               "ClientRectangle": "1.6,0,1.5,0.3",               "ComponentStyle": "Tadbir_ColumnDateData",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "داده تاریخ"               },               "CanBreak": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;;",               "Border": "Left, Right, Bottom;;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:0,0,0",               "TextOptions": {                 "RightToLeft": true,                 "WordWrap": true               },               "Type": "Expression"             }           }         },         "6": {           "Ident": "StiReportSummaryBand",           "Name": "ReportSummary",           "ClientRectangle": "0,4.4,7.49,0.3",           "Alias": "بخش فوتر گزارش - نمایش در صفحه آخر گزارش",           "Enabled": false,           "Interaction": {             "Ident": "StiInteraction"           },           "Border": ";;;;;;;solid:Black",           "Brush": "solid:Transparent",           "Components": {             "0": {               "Ident": "StiText",               "Name": "txtReportFooter",               "Guid": "f9efae84676440e7bae58c451dec3b9c",               "ClientRectangle": "2.5,0,2.6,0.3",               "ComponentStyle": "Tadbir_ReportFooter",               "Interaction": {                 "Ident": "StiInteraction"               },               "Text": {                 "Value": "{vReportSummaryTitle}"               },               "AutoWidth": true,               "HorAlignment": "Center",               "VertAlignment": "Center",               "Font": "IRANSansWeb;12;Bold;",               "Border": ";155,155,155;;;;;;solid:0,0,0",               "Brush": "solid:Transparent",               "TextBrush": "solid:55,55,55",               "TextOptions": {                 "RightToLeft": true               },               "Type": "Expression"             }           }         }       },       "PaperSize": "A4",       "TitleBeforeHeader": true,       "PageWidth": 8.27,       "PageHeight": 11.69,       "Watermark": {         "TextBrush": "solid:50,0,0,0"       },       "Margins": {         "Left": 0.39,         "Right": 0.39,         "Top": 0,         "Bottom": 0       }     }   } }'
WHERE ReportID = 43 AND LocaleID = 2

UPDATE [Reporting].[LocalReport]
SET Template = N'{
  "ReportVersion": "2019.2.3.0",
  "ReportGuid": "a56d6285a5474c6b881cc91ec4d611aa",
  "ReportName": "Report",
  "ReportAlias": "Report",
  "ReportCreated": "/Date(1542053910000+0330)/",
  "ReportChanged": "/Date(1544569474000+0330)/",
  "EngineVersion": "EngineV2",
  "ReportUnit": "Inches",
  "Script": "using System;\r\nusing System.Drawing;\r\nusing System.Windows.Forms;\r\nusing System.Data;\r\nusing Stimulsoft.Controls;\r\nusing Stimulsoft.Base.Drawing;\r\nusing Stimulsoft.Report;\r\nusing Stimulsoft.Report.Dialogs;\r\nusing Stimulsoft.Report.Components;\r\n\r\nnamespace Reports\r\n{\r\n    public class Report : Stimulsoft.Report.StiReport\r\n    {\r\n        public Report()        {\r\n            this.InitializeComponent();\r\n        }\r\n\r\n        #region StiReport Designer generated code - do not modify\r\n\t\t#endregion StiReport Designer generated code - do not modify\r\n    }\r\n}\r\n",
  "ReferencedAssemblies": {
    "0": "System.Dll",
    "1": "System.Drawing.Dll",
    "2": "System.Windows.Forms.Dll",
    "3": "System.Data.Dll",
    "4": "System.Xml.Dll",
    "5": "Stimulsoft.Controls.Dll",
    "6": "Stimulsoft.Base.Dll",
    "7": "Stimulsoft.Report.Dll"
  },
  "Dictionary": {
    "Variables": {
      "0": {
        "Name": "currentDate",
        "Alias": "currentDate",
        "Type": "System.String"
      },
      "1": {
        "Name": "date",
        "Alias": "date",
        "Type": "System.String"
      },
      "2": {
        "Name": "description",
        "Alias": "description",
        "Type": "System.String"
      },
      "3": {
        "Name": "id",
        "Alias": "id",
        "Type": "System.String"
      },
      "4": {
        "Name": "no",
        "Alias": "no",
        "Type": "System.String"
      }
    },
    "DataSources": {
      "0": {
        "Ident": "StiDataTableSource",
        "Name": "VouchersStdForm",
        "Alias": "VouchersStdForm",
        "Columns": {
          "0": {
            "Name": "accountFullCode",
            "Index": -1,
            "NameInSource": "accountFullCode",
            "Alias": "accountFullCode",
            "Type": "System.String"
          },
          "1": {
            "Name": "credit",
            "Index": -1,
            "NameInSource": "credit",
            "Alias": "credit",
            "Type": "System.Decimal"
          },
          "2": {
            "Name": "debit",
            "Index": -1,
            "NameInSource": "debit",
            "Alias": "debit",
            "Type": "System.Decimal"
          },
          "3": {
            "Name": "description",
            "Index": -1,
            "NameInSource": "description",
            "Alias": "description",
            "Type": "System.String"
          },
          "4": {
            "Name": "partialAmount",
            "Index": -1,
            "NameInSource": "partialAmount",
            "Alias": "partialAmount",
            "Type": "System.Decimal"
          }
        },
        "NameInSource": "Vouchers.Vouchers"
      }
    },
    "Databases": {
      "0": {
        "Ident": "StiJsonDatabase",
        "Name": "Vouchers",
        "Alias": "Vouchers"
      }
    }
  },
  "Pages": {
    "0": {
      "Ident": "StiPage",
      "Name": "Page1",
      "Guid": "3bd92efd509e4fe9bffa5620fcd0b140",
      "Interaction": {
        "Ident": "StiInteraction"
      },
      "Border": ";;2;;;;;solid:Black",
      "Brush": "solid:",
      "Components": {
        "0": {
          "Ident": "StiPageHeaderBand",
          "Name": "PageHeaderBand1",
          "CanShrink": true,
          "ClientRectangle": "0,0.2,8.07,1.1",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text1",
              "Guid": "61598abee58f4786bae84d76ef0986b6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0,2.5,0.4",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "سند حسابداری (فرم مرسوم)"
              },
              "Font": "B Titr;16;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text6",
              "Guid": "9b3462eb08f14a83b28a0aa8538243cf",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.6,0.8,0.8,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شماره سند :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text7",
              "Guid": "519bad8b0ab241cdb645cf73425d7f19",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.9,0.8,0.8,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تاریخ سند :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text8",
              "Guid": "b4bf24897b9c4fdf8f21e3e072147e4d",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6,0.81,0.7,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{no}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text9",
              "Guid": "bb3c8abb1ce64d9988b4339921c3cb4a",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.1,0.81,0.9,0.3",
              "Linked": true,
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{date}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;10;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiText",
              "Name": "Text11",
              "Guid": "4853aea835fd418099e3bb2b08c49e5d",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.4,0.5,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تاریخ گزارش :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "6": {
              "Ident": "StiText",
              "Name": "Text12",
              "Guid": "1bb2f39261bb49afb4efee45186f2047",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.7,0.53,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{currentDate}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "7": {
              "Ident": "StiText",
              "Name": "Text26",
              "Guid": "89ccf77ee3aa44258a3bd8c58d2e8122",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1,0.8,0.6,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شماره صفحه :"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "8": {
              "Ident": "StiText",
              "Name": "Text10",
              "Guid": "e056ed4a38bc4ce48b352dc4fd10daf9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.85,0.4,0.2",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{PageNumber.ToString() + \"-\" + TotalPageCount.ToString()}"
              },
              "HorAlignment": "Center",
              "Font": "B Zar;11;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "9": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive1",
              "Guid": "086eea2ac3d64b7494d8e10f08061cb7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.5,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            }
          },
          "CanGrow": false
        },
        "1": {
          "Ident": "StiPageFooterBand",
          "Name": "PageFooterBand1",
          "ClientRectangle": "0,10.79,8.07,0.7",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text3",
              "Guid": "a168af00042a4de0a9eaa1fa32f5e14a",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "5.7,-0.01,1.4,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شرح سند خرید و فروش کالا"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text5",
              "Guid": "4b0a8ed9957d496fa0229f1354480589",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.3,-0.01,5.4,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{description}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;9;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text16",
              "Guid": "807837d02a1a44e1bb18b2fd6027cac9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.1,0.29,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "تهیه کننده سند :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text17",
              "Guid": "f7660f3711a544fd91c3c713040a2bd5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.2,0.29,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مدیر امور مالی :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text18",
              "Guid": "8113c969ce5a4eb7b334c252ccabe4a5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.29,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مدیر عامل :"
              },
              "VertAlignment": "Center",
              "Font": "B Titr;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            }
          },
          "CanGrow": false
        },
        "2": {
          "Ident": "StiText",
          "Name": "DataVouchers_id",
          "CanGrow": true,
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "8.7,0.4,0.7,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Text": {
            "Value": "{Vouchers.id}"
          },
          "HorAlignment": "Center",
          "VertAlignment": "Center",
          "Font": "B Zar;;;",
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "TextBrush": "solid:Black",
          "TextOptions": {
            "WordWrap": true
          }
        },
        "3": {
          "Ident": "StiColumnHeaderBand",
          "Name": "ColumnHeaderBand1",
          "ClientRectangle": "0,1.7,8.07,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text2",
              "Guid": "903ebd75f2ad48eabcbac45411e6b772",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "4.9,0,0.6,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "شرح"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text4",
              "Guid": "0b33e32cceff4f7e8911431ff9fb46f9",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.7,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "بستانکار"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text13",
              "Guid": "95d746759f5a4a1ea90fd0c85cef280b",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "کد حساب"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text15",
              "Guid": "c411bb1be6a841849d811e63b8fb7889",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.9,0,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "مبلغ جزء"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text14",
              "Guid": "cf4339f31d404e0c9683f067b9818406",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.8,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "بدهکار"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive4",
              "Guid": "94d940813f7b45b3b99e62f82f6de1b5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "6": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive2",
              "Guid": "dff7fb07183d466c855c3941da6b4fcf",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "7": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2c6ebb0fc3254a1c934234d94c2b5057"
            },
            "8": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.3,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "2c6ebb0fc3254a1c934234d94c2b5057"
            },
            "9": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ac1b087b702c4406816d56d2370a52c9"
            },
            "10": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive2",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ac1b087b702c4406816d56d2370a52c9"
            },
            "11": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.9,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "52b0aa0f1f004274aa63df0bf07eecb6"
            },
            "12": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive3",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "52b0aa0f1f004274aa63df0bf07eecb6"
            },
            "13": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8f77d8139c7c4d8999cf3d92b3f8d27e"
            },
            "14": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive4",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "8f77d8139c7c4d8999cf3d92b3f8d27e"
            },
            "15": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7bae1ba5ede64ec3967c93671ebb1dfb"
            },
            "16": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive5",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7bae1ba5ede64ec3967c93671ebb1dfb"
            },
            "17": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7d92dea8f4344eb4a08c5839fb7b6ef9"
            },
            "18": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive6",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7d92dea8f4344eb4a08c5839fb7b6ef9"
            }
          }
        },
        "4": {
          "Ident": "StiDataBand",
          "Name": "DataVouchers",
          "ClientRectangle": "0,2.4,8.07,0.3",
          "Linked": true,
          "Interaction": {
            "Ident": "StiBandInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "DataVouchers_date",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.95,0,2.49,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.description}"
              },
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "RightToLeft": true,
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "DataVouchers_statusName",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.65,0,1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.credit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text19",
              "Guid": "fe3c47eee3554b4e9df93ac86601ed80",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.75,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.partialAmount}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "3": {
              "Ident": "StiText",
              "Name": "Text20",
              "Guid": "20293cc6e5bd42c2938f32a4668064ae",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.8,0,0.9,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.debit}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Expression"
            },
            "4": {
              "Ident": "StiText",
              "Name": "Text21",
              "Guid": "98bc4d90eeb54b549da14d4b841426c7",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.6,0,0.69,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{VouchersStdForm.accountFullCode}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "5": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "4a8f90e298a94f9eaf66671abcea1f57"
            },
            "6": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive10",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "4a8f90e298a94f9eaf66671abcea1f57"
            },
            "7": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive3",
              "Guid": "42ae6dd47770407db103ee1e07c89d69",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d8f54115cef04c8799b3aef6369f7b84"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive11",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "d8f54115cef04c8799b3aef6369f7b84"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.9,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "a3850fac806a4fe49c15a9772ddfc84a"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive12",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "3.7,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "a3850fac806a4fe49c15a9772ddfc84a"
            },
            "12": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "59fe0656965b473fb63d09f8c56869e7"
            },
            "13": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive13",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "59fe0656965b473fb63d09f8c56869e7"
            },
            "14": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive15",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "50e3f7e7d121482abd18a2481cf4ccdd"
            },
            "15": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive15",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "50e3f7e7d121482abd18a2481cf4ccdd"
            },
            "16": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive16",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.5,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "814789f33d1b40c7a3a16286942090f2"
            },
            "17": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive16",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "6.3,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "814789f33d1b40c7a3a16286942090f2"
            }
          },
          "DataSourceName": "VouchersStdForm",
          "RightToLeft": true
        },
        "5": {
          "Ident": "StiColumnFooterBand",
          "Name": "ColumnFooterBand1",
          "ClientRectangle": "0,3.1,8.07,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "Border": ";;;;;;;solid:Black",
          "Brush": "solid:",
          "Components": {
            "0": {
              "Ident": "StiText",
              "Name": "Text22",
              "Guid": "e8154825486744ddbc192d023828f61b",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0.5,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "جمع"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Titr;12;;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "Type": "Expression"
            },
            "1": {
              "Ident": "StiText",
              "Name": "Text24",
              "Guid": "1c034e68d16c4fd596ad0c64e72ce41c",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.debit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Totals"
            },
            "2": {
              "Ident": "StiText",
              "Name": "Text25",
              "Guid": "a91502d4d0c8468182e66a5645e58e35",
              "CanGrow": true,
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,1.1,0.3",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "Text": {
                "Value": "{Sum(DataVouchers,VouchersStdForm.credit)}"
              },
              "HorAlignment": "Center",
              "VertAlignment": "Center",
              "Font": "B Zar;;Bold;",
              "Border": ";;;;;;;solid:Black",
              "Brush": "solid:",
              "TextBrush": "solid:Black",
              "TextOptions": {
                "WordWrap": true
              },
              "TextFormat": {
                "Ident": "StiNumberFormatService",
                "NegativePattern": 1,
                "DecimalDigits": 0,
                "GroupSeparator": ",",
                "State": "DecimalDigits, GroupSeparator, GroupSize"
              },
              "Type": "Totals"
            },
            "3": {
              "Ident": "StiHorizontalLinePrimitive",
              "Name": "HorizontalLinePrimitive5",
              "Guid": "c3653ea5fe30406ba17de5adcf6555e1",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0.3,6.8,0.01",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "StartCap": ";;;",
              "EndCap": ";;;"
            },
            "4": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.7,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7dd160aa727f45609d505c408ca39962"
            },
            "5": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive7",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "1.5,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "7dd160aa727f45609d505c408ca39962"
            },
            "6": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.8,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6b5510863355489497a1cb082edbb95d"
            },
            "7": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive8",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "2.6,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "6b5510863355489497a1cb082edbb95d"
            },
            "8": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.4,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "9": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive9",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "7.2,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "f9a8f43d78a04be7899ef81b11031b90"
            },
            "10": {
              "Ident": "StiStartPointPrimitive",
              "Name": "StartPointPrimitive14",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.6,0,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ba8d15b78e1646b29b870c7a76b238b2"
            },
            "11": {
              "Ident": "StiEndPointPrimitive",
              "Name": "EndPointPrimitive14",
              "MinSize": "0,0",
              "MaxSize": "0,0",
              "ClientRectangle": "0.4,0.3,0,0",
              "Interaction": {
                "Ident": "StiInteraction"
              },
              "ReferenceToGuid": "ba8d15b78e1646b29b870c7a76b238b2"
            }
          }
        },
        "6": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive10",
          "Guid": "4a8f90e298a94f9eaf66671abcea1f57",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "7": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive11",
          "Guid": "d8f54115cef04c8799b3aef6369f7b84",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "8": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive13",
          "Guid": "a3850fac806a4fe49c15a9772ddfc84a",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "3.9,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "9": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive14",
          "Guid": "59fe0656965b473fb63d09f8c56869e7",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "10": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive16",
          "Guid": "50e3f7e7d121482abd18a2481cf4ccdd",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "11": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive17",
          "Guid": "814789f33d1b40c7a3a16286942090f2",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.5,2.4,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "12": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive1",
          "Guid": "2c6ebb0fc3254a1c934234d94c2b5057",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "6.5,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "13": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive3",
          "Guid": "ac1b087b702c4406816d56d2370a52c9",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "14": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive4",
          "Guid": "52b0aa0f1f004274aa63df0bf07eecb6",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "3.9,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "15": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive5",
          "Guid": "8f77d8139c7c4d8999cf3d92b3f8d27e",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "16": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive6",
          "Guid": "7bae1ba5ede64ec3967c93671ebb1dfb",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "17": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive8",
          "Guid": "7d92dea8f4344eb4a08c5839fb7b6ef9",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,1.7,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "18": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive19",
          "Guid": "7dd160aa727f45609d505c408ca39962",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "1.7,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "19": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive20",
          "Guid": "6b5510863355489497a1cb082edbb95d",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "2.8,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "20": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive22",
          "Guid": "f9a8f43d78a04be7899ef81b11031b90",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "7.4,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        },
        "21": {
          "Ident": "StiVerticalLinePrimitive",
          "Name": "VerticalLinePrimitive23",
          "Guid": "ba8d15b78e1646b29b870c7a76b238b2",
          "MinSize": "0,0",
          "MaxSize": "0,0",
          "ClientRectangle": "0.6,3.1,0.01,0.3",
          "Interaction": {
            "Ident": "StiInteraction"
          },
          "StartCap": ";;;",
          "EndCap": ";;;"
        }
      },
      "PaperSize": "A4",
      "PageWidth": 8.27,
      "PageHeight": 11.69,
      "Watermark": {
        "TextBrush": "solid:50,0,0,0"
      },
      "Margins": {
        "Left": 0.1,
        "Right": 0.1,
        "Top": 0.1,
        "Bottom": 0.1
      }
    }
  }
}'
WHERE ReportID = 40 AND LocaleID = 2
