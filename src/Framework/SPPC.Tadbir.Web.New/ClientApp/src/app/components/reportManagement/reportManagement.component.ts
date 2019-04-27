import { Layout, environment } from "../../../environments/environment";
import { TreeNode } from '../../model/index';
import { Component, OnInit, Input, ViewChild, ViewChildren, QueryList, ContentChildren } from '@angular/core';
import { DetailComponent } from '../../class/detail.component';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeNodeInfo } from '../../model/index';
import { Renderer2, Optional, Inject } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from "../../service/metadata/metadata.service";
import { ReportingService, LocalReportInfo, ParameterInfo, QuickReportColumnInfo, QuickReportViewInfo } from "../../service/report/reporting.service";
import { ReportApi } from "../../service/api/reportApi";
import { of } from "rxjs/observable/of";
import { Report } from "../../model/report";
import { String } from '../../class/source';

import { ReportParametersComponent, TabInfo } from "../reportParameters/reportParameters.component";
import { TreeViewComponent } from "@progress/kendo-angular-treeview";
import { TreeItem } from "../../model/treeItem";
import { PrintInfo } from "../../model/printInfo";
import { FilterExpression } from "../../class/filterExpression";
import { Filter } from "../../class/filter";
import { FilterExpressionBuilder } from "../../class/filterExpressionBuilder";
import * as moment from 'jalali-moment';
import { ReportViewerComponent } from "../reportViewer/reportViewer.component";
import { FormGroup, FormControl, Validators } from "@angular/forms";
import { ReportSummary } from "../../model/reportSummary";
import { ReportParamComponent } from "../viewIdentifier/reportParam.component";
import { TabsComponent } from "../../controls/tabs/tabs.component";
import { SortDescriptor } from "@progress/kendo-data-query";
import { TabComponent } from "../../controls/tabs/tab.component";
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { ViewIdentifierComponent } from "../viewIdentifier/view-identifier.component";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

declare var Stimulsoft: any;

@Component({
  selector: 'report-management',
  templateUrl: './reportManagement.component.html',
  styleUrls: ['./reportManagement.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ReportManagementComponent extends DetailComponent implements OnInit {
  
  @Input() public baseId: string;
  
  treeData: any[];
  active: boolean = false;
  showReportDesigner:boolean = false;
  showReportViewer:boolean = false;
  showSaveAsDialog : boolean = false;
  innerWidth : number;
  innerHeight : number;
  selectedKeys: any[] = [];
  report: any = new Stimulsoft.Report.StiReport();
  
  currentReportId : any;
  currentViewId : any;
  qReport : boolean;
  currentReportName : string;
  currentPrintInfo : PrintInfo;
  currentFormParams:Array<ReportParamComponent>;
  currentDefaultReportId:any;
  showDesktopTab:boolean = false;
  currentFilter:FilterExpression;
  currentSort:SortDescriptor[];
  
  quickReportJsonDesign : string;
  quickReportRowData : any;

  expandedKeys :any[] = [];
  deleteConfirmMsg : string;
  deleteConfirm:boolean = false;
  deleteMsg :string;
  disableButtons:boolean = false;
  
  private reportForm = new FormGroup({    
    reportName: new FormControl("", [Validators.required, Validators.maxLength(256)]),    
  });

  //@ViewChild(ReportViewerComponent) reportViewer: ReportViewerComponent;  
  @ViewChild(ReportParametersComponent) reportParameter: ReportParametersComponent;
  @ViewChild(TreeViewComponent) treeView: TreeViewComponent;
  @ViewChild(TabsComponent) tabsComponent : TabsComponent;  
  //@ViewChild('viewer') reportViewer: ReportViewerComponent;  

  @ContentChildren(ReportViewerComponent) reportViewer: ReportViewerComponent;  
  @ViewChildren(ReportViewerComponent) viewers : QueryList<ReportViewerComponent>;
  
  reportTabs : Array<TabInfo>;
  
  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
      public renderer: Renderer2, 
      private metaDataService: MetaDataService,
    @Optional()
    @Inject('empty')
    public entityType: string,
    @Optional()
    @Inject('empty')
    public metaDataName: string,
    public reportingService:ReportingService) {
    super(toastrService, translate, renderer, metaDataService, entityType, metaDataName);
    
  }

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    this.innerHeight = window.screen.height;//window.innerHeight
    this.initViewer();
    this.disableButtons = true;    
  }
  
  onNodeClick(e :any)
  {
    var data = e.dataItem;
    this.qReport = false;
    if(data.id == -100) 
      this.qReport = true;
    if(!data.isGroup)
    {      
      if(data.id > -100)
        this.currentReportId = data.id;
      this.currentReportName = data.caption;
      this.deleteConfirmMsg = String.Format(this.getText("Report.DeleteReportConfirm"),data.caption);
      this.disableButtons = false;
    }
    else
      this.disableButtons = true;
  }
  
  onNodeDblClick(dataItem :any)
  {
    var data = dataItem;
    if(!data.isGroup)
    {      
      this.currentReportId = data.id;
      this.currentReportName = data.caption;
      this.deleteConfirmMsg = String.Format(this.getText("Report.DeleteReportConfirm"),data.caption);
      this.disableButtons = false;
      this.showReport();
    }
    else
      this.disableButtons = true;
  } 

  //متد نمایش گزارش فوری
  public showQuickReport(viewId:string,formParams:Array<ReportParamComponent>,
    filter:FilterExpression = null,sort:SortDescriptor[] = null,designJson:string,qrRowData:any)
  {
      this.active = true;
      
      var url = ReportApi.ReportsHierarchy;
      if(viewId)
      {
          this.currentViewId = viewId;
          url = String.Format(ReportApi.ReportsByView, viewId);
          this.showDesktopTab = false;
          this.currentFilter = filter;
          this.currentSort = sort;
      }
      else      
      {
          this.showDesktopTab = true;
          this.currentViewId = undefined;
      }

      this.reportingService.getAll(url)
      .subscribe((res: any) => {
          //var i = res;
          this.treeData = <Array<TreeItem>>res.body;       
          //expand treeview base on baseid
           if(viewId)
           {
             this.quickReportJsonDesign = designJson;
             this.quickReportRowData = qrRowData;
             this.addQReportToDefaultFolder(viewId,formParams);  
             this.currentFormParams = formParams;
           }
      });
  }

  public showDialog(viewId:string,formParams:Array<ReportParamComponent>,
    filter:FilterExpression = null,sort:SortDescriptor[] = null)
  {
      this.active = true;
      
      var url = ReportApi.ReportsHierarchy;
      if(viewId)
      {
          this.currentViewId = viewId;
          url = String.Format(ReportApi.ReportsByView, viewId);
          this.showDesktopTab = false;
          this.currentFilter = filter;
          this.currentSort = sort;
      }
      else      
      {
          this.showDesktopTab = true;
          this.currentViewId = undefined;
      }

      this.reportingService.getAll(url)
      .subscribe((res: any) => {
          //var i = res;
          this.treeData = <Array<TreeItem>>res.body;       
          //expand treeview base on baseid
           if(viewId)
           {
             this.expandAndSelectDefault(viewId,formParams);  
             this.currentFormParams = formParams;
           }
      });       
  }

  //select and expand tree node baseon report baseId
  public addQReportToDefaultFolder(viewId: string,formParams:Array<ReportParamComponent>) {
    var expandKeysArray: string[];

    var defaltReportUrl = String.Format(ReportApi.ReportsByViewDefault, viewId);
    this.reportingService.getAll(defaltReportUrl)
        .subscribe((res: any) => {
            var report = <ReportSummary>res.body;

            expandKeysArray = new Array<any>();
            this.selectedKeys = new Array<any>();

            // var nodeData = this.treeData.filter((p: any) => p.id == report.id)[0];
            
            // this.currentReportName = "گزارش فوری";
            
            // var qReportNode = 
            //     {
            //       caption: "گزارش فوری",
            //       id: -100,
            //       isDefault: false,
            //       isGroup: false,
            //       isSystem: false,
            //       parentId: nodeData.parentId,
            //       serviceUrl: null
            //     }                
                
            // this.treeData.push(qReportNode);           
            // this.selectedKeys.push(-100);
            
            // while (nodeData.parentId != null) {
            //     expandKeysArray.push(nodeData.parentId);                
            //     var parentNode = this.treeData.filter((p: any) => p.id == nodeData.parentId);
            //     nodeData = parentNode[0];
            // }

            var nodeData = this.treeData.filter((p: any) => p.id == report.id)[0];
            this.selectedKeys.push(nodeData.id);
            this.currentReportName = nodeData.caption;

            while (nodeData.parentId != null) {
                expandKeysArray.push(nodeData.parentId);
                var parentNode = this.treeData.filter((p: any) => p.id == nodeData.parentId);
                nodeData = parentNode[0];
            }

            this.qReport = true;
            this.expandedKeys = expandKeysArray;
            this.disableButtons = false;
            this.currentReportId = report.id;
            this.currentDefaultReportId = report.id;

            this.prepareReport(formParams);
        });

  }

  //select and expand tree node baseon report baseId
  public expandAndSelectDefault(viewId: string,formParams:Array<ReportParamComponent>) {
        var expandKeysArray: string[];

        var defaltReportUrl = String.Format(ReportApi.ReportsByViewDefault, viewId);
        this.reportingService.getAll(defaltReportUrl)
            .subscribe((res: any) => {
                var report = <ReportSummary>res.body;

                expandKeysArray = new Array<any>();
                this.selectedKeys = new Array<any>();

                var nodeData = this.treeData.filter((p: any) => p.id == report.id)[0];
                this.selectedKeys.push(nodeData.id);
                this.currentReportName = nodeData.caption;

                while (nodeData.parentId != null) {
                    expandKeysArray.push(nodeData.parentId);
                    var parentNode = this.treeData.filter((p: any) => p.id == nodeData.parentId);
                    nodeData = parentNode[0];
                }

                this.qReport = false;
                this.expandedKeys = expandKeysArray;
                this.disableButtons = false;
                this.currentReportId = report.id;
                this.currentDefaultReportId = report.id;

                this.prepareReport(formParams);
            });

  }

  public showReport()
  {
      if(this.currentDefaultReportId == this.currentReportId || this.currentViewId)
      {
        this.prepareReport(this.currentFormParams);
      }
      else
      {
        var formParams = new Array<ReportParamComponent>();
        this.prepareReport(formParams);
      }
  }

  public setDefaultForAll()
  {
      var defaltReportUrl = String.Format(ReportApi.ReportDefault, this.currentReportId);
      this.reportingService.setDefaultForAll(defaltReportUrl)
        .subscribe((res: any) => {            
        },
        (error) => {          
        },
        () => {
          this.showMessage(this.getText('Report.ReportSetDefaultForAll'));
        });

  }

  public showParameterForm(printInfo : PrintInfo)
  {
      this.reportParameter.showDialog(printInfo);
  }

  public onOkParams(event : any)
  {
      this.previewReport(event.params);
  }

  private initViewer()
  {    
    if (this.CurrentLanguage == "fa")    
        Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/fa.xml");          

    if (this.CurrentLanguage == "en")           
        Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/en.xml");      

     Stimulsoft.Base.StiLicense.key = "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";      
  }

  public createFilters(params:ParameterInfo[],cfilter?:FilterExpression)
  {

      //var currentFilter = filter ? filter : new FilterExpression();
      var filters:Filter[] = new Array<Filter>();

      params.forEach(function(param){
        
        var operator = "";
          switch (param.operator.toLowerCase()) {
            case "eq":
              operator = " == {0}";
              break;
            case "neq":
              operator = " != {0}";
              break;
            case "lte":
              operator = " <= {0}";
              break;
            case "gte":
              operator = " >= {0}";
              break;
            case "lt":
              operator = " < {0}";
              break;
            case "gt":
              operator = " > {0}";
              break;
            case "contains":
              operator = ".Contains({0})";
              break;
            case "doesnotcontain":
              operator = ".IndexOf({0}) == -1";
              break;
            case "startswith":
              operator = ".StartsWith({0})";
              break;
            case "endswith":
              operator = ".EndsWith({0})";
              break;
            default:
              operator = " == {0}";
          }

        var value = param.value ? param.value : "";
        var filter = new Filter(param.fieldName,value,operator,param.dataType); 
        filters.push(filter);
      });
      
      if(cfilter)
      {
        if(filters.findIndex(f=>f.FieldName.toLowerCase() == cfilter.filter.FieldName.toLowerCase()) == -1)
            filters.push(cfilter.filter);
        
        cfilter.children.forEach(function(f)
        {
          if(filters.findIndex(f=>f.FieldName.toLowerCase() == f.FieldName.toLowerCase()) == -1)
              filters.push(f.filter);
        });
      }


      var filterExpBuilder = new FilterExpressionBuilder();
      var filterExp = filterExpBuilder.And(filters)
      .Build();

    return filterExp;

  }

  saveAsReport()
  {
      if(this.currentReportId)
      {
          this.showSaveAsDialog = true;
          this.reportForm.controls.reportName.setValue("");
      }
  }

  okSaveAsClick()
  {
    var localReport = new LocalReportInfo();
    localReport.template = "";
    localReport.reportId = this.currentReportId;
    var localId = this.CurrentLanguage == 'fa' ? 2 : 1;
    localReport.localeId = localId;
    localReport.caption = this.reportForm.controls.reportName.value;

    var url = ReportApi.Reports;
    this.reportingService.saveAsReport(url,localReport).subscribe((response: any) => {
      this.showMessage(this.getText('Report.SaveAsIsOk'));

      //reload treeview
      var url = ReportApi.ReportsHierarchy;
      if(this.currentViewId)
      {
          url = String.Format(ReportApi.ReportsByView, this.currentViewId);
      }

      this.reportingService.getAll(url)
      .subscribe((res: any) => {          
          this.treeData = <Array<TreeItem>>res.body;                 
      });
      //reload treeview

      this.showSaveAsDialog = false; 
    }, (error => {
      this.showMessage(error);
    }));
  }

  cancelReportForm()
  {
    this.showSaveAsDialog = false;
  }

  public prepareReport(formParams:Array<ReportParamComponent>)
  {
      var url = String.Format(ReportApi.Report, this.currentReportId);
      this.reportingService.getAll(url).subscribe((res: Response) => {    

        var printInfo: PrintInfo = <any>res.body;
        if(printInfo.parameters.length > 0)
        {
          this.currentPrintInfo = printInfo;
          if(formParams == undefined || formParams.length == 0)
            this.showParameterForm(printInfo);  
          else
          {
             var formPrameters = this.setParamterFromForm(formParams);
             this.previewReport(formPrameters);
          }

        }
      });
  }

  setParamterFromForm(formParams:Array<ReportParamComponent>) : Array<ParameterInfo>
  {
    var paramArrays  = new Array<ParameterInfo>();  

    this.currentPrintInfo.parameters.forEach(function(param){
        var fparam = formParams.filter(f=>f.ParamName == param.name);
        
        var paramInfo : ParameterInfo = new ParameterInfo();
        paramInfo.fieldName = param.fieldName;

        paramInfo.controlType = param.controlType;
        paramInfo.id = param.id;
        paramInfo.defaultValue = param.defaultValue? param.defaultValue : "";
        paramInfo.captionKey = param.captionKey;
        paramInfo.operator = param.operator;
        paramInfo.dataType = param.dataType;
        paramInfo.descriptionKey = param.descriptionKey;
        paramInfo.name = param.name;
        paramInfo.value = fparam[0].ParamValue;

        paramArrays.push(paramInfo);
    });

    return paramArrays;
  }

  public previewReport(params : ParameterInfo[])
  {
    this.showReportViewer = true;
    this.showReportDesigner = false;    
    var serviceUrl = environment.BaseUrl + "/" + this.currentPrintInfo.serviceUrl;        

    var filterExpression = this.createFilters(params,this.currentFilter);
    serviceUrl = this.changeServiceUrl(serviceUrl,params);
    var sort = this.currentSort;

    this.reportingService.getAll(serviceUrl,
      sort, filterExpression).subscribe((response: any) => {

        // var fdate = moment(this.FiscalPeriodStartDate, 'YYYY-M-D HH:mm:ss')
        //   .locale(this.CurrentLanguage)
        //   .format('YYYY/M/D');

        // var tdate = moment(this.FiscalPeriodEndDate, 'YYYY-M-D HH:mm:ss')
        //   .locale(this.CurrentLanguage)
        //   .format('YYYY/M/D');          

        var reportData = {
          rows: response.body,           
          parameters : params
        };
        
        if(this.qReport)
          reportData.rows = reportData.rows; //this.quickReportRowData;

        var viewerIsCloseable : boolean = false;
        if(this.currentReportId != this.currentDefaultReportId)
          viewerIsCloseable = true;

        var reportTemplate : string;
        if(this.qReport)
          reportTemplate = this.quickReportJsonDesign;
        else
          reportTemplate = this.currentPrintInfo.template;

        this.tabsComponent.openTab(this.currentReportName,reportTemplate,
          reportData,viewerIsCloseable,true,false,this.currentReportId,this);

      });      
  } 

  changeServiceUrl(url:string,params : ParameterInfo[]) : string
  {      
      var queryStringParams = params.filter(p=>p.controlType === 'QueryString');
      if(queryStringParams.length > 0)
        url += '?';
      else
        return url;
      var itemCount = 0;
        queryStringParams.forEach(function(item){          
          url += item.name + '=' + item.value;
          if(itemCount > 0) url += '&';
          itemCount++;
        });      
      return url;
  }

  saveDesignOfReport(id:string)
  {
      var designer = new Stimulsoft.Designer.StiDesigner(null,"StiDesigner" + id.replace('designerTab',''), false);        
      designer.invokeSaveReport();
  }
 
  updateTemplateInTab(designer:any)
  {
      var tab = this.tabsComponent.dynamicTabs.find(t=>t.Id == "designerTab" + this.currentReportId);
      var designData = designer.report.saveToJsonString();
      tab.template = designData;
  }
 
  designReport()
  {   
    var current = this.currentReportId;
    if(this.qReport) 
      current = -100;

    var designer = new Stimulsoft.Designer.StiDesigner(null, "StiDesigner" + current, false);                
    
    var tabIsOpen = this.tabsComponent.openTab(this.currentReportName,null,null,
      true,false,true,this.currentReportId,designer);   

    if(!tabIsOpen) return;     

    var url = String.Format(ReportApi.ReportDesign, this.currentReportId);
    this.showReportViewer = false;
    this.showReportDesigner = true;
    
    this.reportingService.getAll(url).subscribe((res: Response) => {    

        var printInfo: PrintInfo = <any>res.body;

        var options = new Stimulsoft.Designer.StiDesignerOptions();
        options.appearance.fullScreenMode = true;
        options.toolbar.showPreviewButton = false;
        options.toolbar.showFileMenu = false;
        options.components.showImage = false;
        options.components.showShape = false;
        options.components.showPanel = false;
        options.components.showCheckBox = false;
        options.components.showSubReport = false;
         

        var rpt = new Stimulsoft.Report.StiReport();
        var reportTemplate : string;
        
        reportTemplate = printInfo.template;
        

        if(this.qReport)
         {
           reportTemplate = this.quickReportJsonDesign;
           current = -100;
        } 

        rpt.load(reportTemplate);        

        designer.report = rpt;        
      
        designer.renderHtml('designerTab' + current);       

        this.updateTemplateInTab(designer);

        var currentId = this.currentReportId;
        var service = this.reportingService;
        var localId = this.CurrentLanguage == 'fa' ? 2 : 1;
        var thisComponent = this;
        // Assign the onSaveReport event function
        designer.onSaveReport = function (e: any) {
          
          var jsonStr = e.report.saveToJsonString();
          
          var localReport = new LocalReportInfo();
          localReport.template = jsonStr;
          localReport.reportId = currentId;
          localReport.localeId = localId;
          thisComponent.updateTemplateInTab(designer);

          var url = String.Format(ReportApi.Report, currentId);
          service.saveReport(url,localReport).subscribe((response: any) => {
            
            thisComponent.showMessage(thisComponent.getText('Report.SaveIsOk'));
          }, (error => {
            thisComponent.showMessage(error);
          }));

        }
    });  
  }

  deleteReport(deleteFlag : boolean)
  {
      this.deleteConfirm = false;
      if(deleteFlag)
      {
          var reportId = this.currentReportId;
          var url = String.Format(ReportApi.Report,reportId);

          this.reportingService.deleteReport(url).subscribe((response: any) => {
            this.showMessage(this.getText('Report.ReportDeleted'));
            this.tabsComponent.closeTabByReportId(this.currentReportId);
            this.currentReportId = null;
            this.disableButtons = true;
            
            //reload treeview
            var url = ReportApi.ReportsHierarchy;
            if(this.currentViewId)
            {
                url = String.Format(ReportApi.ReportsByView, this.currentViewId);
            }        
            
            this.reportingService.getAll(url)
            .subscribe((res: any) => {          
                this.treeData = <Array<TreeItem>>res.body;                 
            });               
          }, (error => {
            this.showMessage(error);
          }));
      }      
  }

  showDeleteConfirm()
  {
    this.deleteConfirm = true;
  }

  closeDialog()
  {
      this.active = false;
  }

  public iconClass(dataItem: any): any {
    return {  
        'k-i-change-manually' : !dataItem.isGroup && !dataItem.isSystem ,  
        'k-i-ascx': dataItem.isGroup == false && dataItem.isSystem ,
        'k-i-folder': dataItem.isGroup == true,
        
    };
  }

  public setClass(dataItem: any): any {
    
    var cssClass = '';
    if(dataItem.isGroup)
        cssClass = 'rep-folder';
    if(!dataItem.isGroup && dataItem.isSystem)
       cssClass = 'rep-system';
    if(!dataItem.isGroup && !dataItem.isSystem)
       cssClass = 'rep-user';

    if(dataItem.id == this.currentDefaultReportId)
       cssClass += ' def';

    return cssClass;
  }
}


