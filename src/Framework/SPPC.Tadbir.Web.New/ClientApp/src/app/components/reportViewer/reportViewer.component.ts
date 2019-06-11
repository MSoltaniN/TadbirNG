import { Component, OnInit, Input, Renderer2, ChangeDetectorRef, ViewEncapsulation} from '@angular/core';
import { Http } from '@angular/http';
import { DefaultComponent } from '../../class/default.component';
import { VoucherService, VoucherInfo, FiscalPeriodService, SettingService } from '../../service/index';
import { Voucher, QuickReportColumnModel } from '../../model/index';
import { ToastrService } from 'ngx-toastr';
import { GridDataResult, DataStateChangeEvent, PageChangeEvent, RowArgs, SelectAllCheckboxState, GridComponent } from '@progress/kendo-angular-grid';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { String } from '../../class/source';
import { State, CompositeFilterDescriptor } from '@progress/kendo-data-query';
import { SortDescriptor, orderBy } from '@progress/kendo-data-query';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SppcLoadingService } from '../../controls/sppcLoading/index';
import { VoucherApi } from '../../service/api/index';
import { SecureEntity } from '../../security/secureEntity';
import { VoucherPermissions } from '../../security/permissions';
import { FilterExpression } from '../../class/filterExpression';
import { DocumentStatusValue } from '../../enum/documentStatusValue';
import { MessageType, Layout, Entities, Metadatas, environment } from "../../../environments/environment";
import { HttpErrorResponse, HttpClient, HttpHeaders, HttpResponse } from "@angular/common/http";
import { Report } from '../../model/report';
import { ReportingService, ParameterInfo, QuickReportViewInfo } from '../../service/report/reporting.service';
import * as moment from 'jalali-moment';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { TabsComponent } from '../../controls/tabs/tabs.component';
import { ReportsQueries } from '../reportManagement/reports.queries';
import { ViewName } from '../../security/viewName';



declare var Stimulsoft: any;

@Component({
  selector: 'report-viewer',
  templateUrl: './reportViewer.component.html',
  styleUrls: ['./reportViewer.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ReportViewerComponent extends DefaultComponent implements OnInit {

  viewer: any = new Stimulsoft.Viewer.StiViewer(null, 'StiViewer', false);
  
  report: any = new Stimulsoft.Report.StiReport();
  active: boolean = false;
  quickReport: boolean = false;
  reportManager: ReportManagementComponent;

  @Input() public baseId: number;
  @Input() public showViewer: boolean = false;
  @Input() public Id: string;
  @Input() public Code: string;

 
  
  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public sppcLoading: SppcLoadingService, private cdref: ChangeDetectorRef,
   private voucherService: VoucherService, public renderer: Renderer2,
    public metadata: MetaDataService, public settingService: SettingService,
    private http: HttpClient, public reporingService: ReportingService) {
    super(toastrService, translate, renderer, metadata, settingService, Entities.Voucher, ViewName.Voucher);
 }
  
  innerWidth : number;

  ngOnInit() {
    this.innerWidth = window.innerWidth;
    
    this.initViewer();
  }

  closeForm()
  {
      this.active = false;
  }  

  private initViewer()
  {    
    if (this.CurrentLanguage == "fa")
    {        
        Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/fa.xml");      
    }

    if (this.CurrentLanguage == "en")
    {       
        Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/en.xml");      
    } 

    Stimulsoft.Base.StiLicense.key = "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";

    //load fonts
    Stimulsoft.StiOptions.Export.Pdf.AllowImportSystemLibraries = true;
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar");
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BZar.ttf", "B Zar", Stimulsoft.System.Drawing.FontStyle.Bold);
    Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/BTitrBold.ttf", "B Titr", Stimulsoft.System.Drawing.FontStyle.Bold);
    if (this.CurrentLanguage == "fa") {

      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/ReportFont/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);
      //assets/resources/fonts/IranSans-En/ttf
      
    }
    else {
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb.ttf", "IRANSansWeb");
      Stimulsoft.Base.StiFontCollection.addOpentypeFontFile("assets/resources/fonts/IranSans/ttf/IRANSansWeb_Bold.ttf", "IRANSansWeb", Stimulsoft.System.Drawing.FontStyle.Bold);
    }
    //Stimulsoft.System.Drawing.FontStyle.Italic

  }

  fillResourceVariables(reportObject:Report,stiReport:any)
  {

    reportObject.resourceKeys.split(',').forEach(function(resKey)
    {
      var resValue = reportObject.resourceMap[resKey];
      var found = stiReport.dictionary.variables.items.find(x => x.name === resKey)      
      if(found)
          stiReport.dictionary.variables.getByName(found.name).valueObject = resValue;

      
    });
     

  }

  showVoucherReport(reportObject : Report, reportData: any)
  {   
    this.active = true;    
    
    setTimeout(() => {      
     
     
      console.log('Load report from url');
      var reportTemplate : string;    

      this.report.load(reportTemplate);
     
      this.report.regData("Vouchers", "", reportData.rows);
      this.report.dictionary.variables.getByName("FDate").valueObject = reportData.fromDate;
      this.report.dictionary.variables.getByName("TDate").valueObject = reportData.toDate;
      
      this.fillResourceVariables(reportObject,this.report);

      this.report.render();
      this.viewer.report = this.report;
      


      console.log('Rendering the viewer to selected element');
      this.viewer.renderHtml('viewer');

    }, 10);   
  }

  showVoucherStdFormReport(report: Report , reportData: any)
  {
    //url
    //this.rowData.data
    this.active = true;

    setTimeout(() => {      

      console.log('Load report from url');
      //comment by nouri
      //this.report.load(report.template);
      this.report.regData("Vouchers", "VouchersStdForm", reportData.rows.lines);

      this.report.dictionary.variables.getByName("currentDate").valueObject = reportData.currentDate;
      this.report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
      this.report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
      this.report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
      this.report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

      this.report.render();
      this.viewer.report = this.report;
      
      console.log('Rendering the viewer to selected element');
      this.viewer.renderHtml('viewer' );

    },10);

   
  }

  showDesginedReportViewer(data: any, report: any) {

    var parameters: Array<ParameterInfo> = data.parameters;
    var localReport = report;
    var lang = this.CurrentLanguage;

    parameters.forEach(function (param) {

      var value = param.value;

      if (param.dataType == "System.DateTime") {
        var fdate = moment(param.value, 'YYYY-M-D HH:mm:ss')
          .locale(lang)
          .format('YYYY/M/D');

        value = fdate;
      }

      if (localReport.dictionary.variables.getByName(param.name) != null)
        localReport.dictionary.variables.getByName(param.name).valueObject = value;
    });


    this.report = localReport;    
    this.report.render();
    this.viewer.report = localReport;

    console.log('Rendering the viewer to selected element');
    this.viewer.renderHtml(this.Id);
  }

  showReportViewer(reportTemplate: string, reportData: any, manager: any, isQuickReport: boolean, quickReportViewInfo: QuickReportViewInfo)
  {      
    this.active = true;

    var options = new Stimulsoft.Viewer.StiViewerOptions();
    options.appearance.fullScreenMode = true;
    
    this.viewer =  new Stimulsoft.Viewer.StiViewer(null, this.Id, false);
    this.quickReport = isQuickReport;
    this.reportManager = manager;

    setTimeout(() => {          

     
      console.log('Load report from url');      
      this.report.load(reportTemplate);
      var dataSet = new Stimulsoft.System.Data.DataSet('dataset');

      //report is quick report
      if (this.quickReport) {

        var reportRows = null;

        if (reportData.rows instanceof Array)
        {
          dataSet.readJson(reportData.rows);
          reportRows = reportData.rows;
        }
        else {
          //TODO : fire an event in report viewer component for invoke readjson method
          dataSet.readJson(reportData.rows.items)
          reportRows = reportData.rows.items;
        }

        var data = dataSet.tables.getByIndex(0);
        
        //Fill dictionary
        var dataSource = new Stimulsoft.Report.Dictionary.StiDataTableSource(data.tableName, data.tableName, data.tableName);
        data.columns.list.forEach(element => {
          var dataType = element.dataType.jsNamespace + "." + element.dataType.jsTypeName;          
          dataSource.columns.add(new Stimulsoft.Report.Dictionary.StiDataColumn(element.columnName, element.columnName,
            element.columnName, dataType));
        });

        var registerData = false;
        if (quickReportViewInfo) {
          registerData = true;
          var dateColumns = quickReportViewInfo.columns.filter(c => c.dataType.toLowerCase() === "system.date");
          if (dateColumns.length > 0 && this.CurrentLanguage == "fa") {
            var convertedData = reportRows;
            convertedData = this.convertToShamsiDate(convertedData, dateColumns);
            this.report.regData("data", "data", convertedData);           
          }
          else if (dateColumns.length > 0) {            
              var convertedData = reportRows;
              convertedData = this.convertToMiladiDate(convertedData, dateColumns);
              this.report.regData("data", "data", convertedData);                          
          }
        }

        if(!registerData)
        { //Add data to datastore
          this.report.regData("data", "data", dataSet);
        }

        this.report.dictionary.dataSources.add(dataSource);

        var parameters: Array<ParameterInfo> = reportData.parameters;
        var localReport = this.report;
        var lang = this.CurrentLanguage;

        if (parameters) {
          parameters.forEach(function (param) {

            var value = param.value;

            if (param.dataType == "System.DateTime") {
              var fdate = moment(param.value, 'YYYY-M-D HH:mm:ss')
                .locale(lang)
                .format('YYYY/M/D');

              value = fdate;
            }

            if (localReport.dictionary.variables.getByName(param.name) != null)
              localReport.dictionary.variables.getByName(param.name).valueObject = value;
            else {
              var parameter = new Stimulsoft.Report.Dictionary.StiVariable();
              parameter.name = param.name;
              parameter.alias = param.captionKey;
              parameter.value = value;

              localReport.dictionary.variables.add(parameter);
            }

          });
        }

        this.report = localReport;
        //this.fillResourceVariables(reportObject,this.report);
        this.report.render();
        this.viewer.report = this.report;

        console.log('Rendering the viewer to selected element');
        this.viewer.renderHtml(this.Id);
        
      }
      else {

        this.report = ReportsQueries.registerReport(this.Code,this.report, reportData);
        this.showDesginedReportViewer(reportData, this.report);        
      }         
    }, 10);   
  }

  convertToShamsiDate(rows: any,cols: Array<QuickReportColumnModel>) {

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name]) {          
          let momentDate = moment(rows[index][item.name]).locale('fa').format("YYYY/MM/DD");
          rows[index][item.name] = momentDate;
        }
      })      
    }
    return rows;
  }

  convertToMiladiDate(rows: any, cols: Array<QuickReportColumnModel>) {

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name]) {
          let momentDate = moment(rows[index][item.name]).locale('en').format("YYYY/MM/DD");
          rows[index][item.name] = momentDate;
        }
      })
    }
    return rows;
  }

}
