import { HttpHeaders } from '@angular/common/http';
import { Component, Input, OnInit, Renderer2, ViewEncapsulation } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { SettingService } from '@sppc/config/service';
import { DefaultComponent } from '@sppc/shared/class';
import { CalendarType, Entities } from '@sppc/shared/enum/metadata';
import { OperationId } from '@sppc/shared/enum/operationId';
import { QuickReportColumnConfig, QuickReportConfigInfo, Report } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, MetaDataService, ParameterInfo, ReportingService } from '@sppc/shared/services';
import * as moment from 'jalali-moment';
import { ToastrService } from 'ngx-toastr';
// import "rxjs/Rx";
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { ReportsQueries } from '../reportManagement/reports.queries';


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
  @Input() public url: string;
  @Input() public title: string;


  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService, public settingService: SettingService,
    public reporingService: ReportingService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, Entities.Voucher, ViewName.Voucher);
  }

  innerWidth: number;

  ngOnInit() {
    this.innerWidth = window.innerWidth;

    this.initViewer();
  }

  closeForm() {
    this.active = false;
  }

  private initViewer() {
    if (this.CurrentLanguage == "fa") {
      Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/fa.xml");
    }

    if (this.CurrentLanguage == "en") {
      Stimulsoft.Base.Localization.StiLocalization.setLocalizationFile("assets/reports/localization/en.xml");
    }

    Stimulsoft.Base.StiLicense.key = "6vJhGtLLLz2GNviWmUTrhSqnOItdDwjBylQzQcAOiHn0s4gy0Fr5YoUZ9V00Y0igCSFQzwEqYBh/N77k4f0fWXTHW5rqeBNLkaurJDenJ9o97TyqHs9HfvINK18Uwzsc/bG01Rq+x3H3Rf+g7AY92gvWmp7VA2Uxa30Q97f61siWz2dE5kdBVcCnSFzC6awE74JzDcJMj8OuxplqB1CYcpoPcOjKy1PiATlC3UsBaLEXsok1xxtRMQ283r282tkh8XQitsxtTczAJBxijuJNfziYhci2jResWXK51ygOOEbVAxmpflujkJ8oEVHkOA/CjX6bGx05pNZ6oSIu9H8deF94MyqIwcdeirCe60GbIQByQtLimfxbIZnO35X3fs/94av0ODfELqrQEpLrpU6FNeHttvlMc5UVrT4K+8lPbqR8Hq0PFWmFrbVIYSi7tAVFMMe2D1C59NWyLu3AkrD3No7YhLVh7LV0Tttr/8FrcZ8xirBPcMZCIGrRIesrHxOsZH2V8t/t0GXCnLLAWX+TNvdNXkB8cF2y9ZXf1enI064yE5dwMs2fQ0yOUG/xornE";
    //Stimulsoft.Base.StiLicense.key = "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";

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
    this.addViewerEvents();
  }

  fillResourceVariables(reportObject: Report, stiReport: any) {

    reportObject.resourceKeys.split(',').forEach(function (resKey) {
      var resValue = reportObject.resourceMap[resKey];
      var found = stiReport.dictionary.variables.items.find(x => x.name === resKey)
      if (found)
        stiReport.dictionary.variables.getByName(found.name).valueObject = resValue;


    });


  }

  showVoucherReport(reportObject: Report, reportData: any) {
    this.active = true;

    setTimeout(() => {


      console.log('Load report from url');
      var reportTemplate: string;

      this.report.load(reportTemplate);

      this.report.regData("Vouchers", "", reportData.rows);
      this.report.dictionary.variables.getByName("FDate").valueObject = reportData.fromDate;
      this.report.dictionary.variables.getByName("TDate").valueObject = reportData.toDate;

      this.fillResourceVariables(reportObject, this.report);

      //this.report.render();
      this.report.renderAsync();
      
      this.viewer.report = this.report;

      console.log('Rendering the viewer to selected element');
      this.viewer.renderHtml('viewer');

    }, 10);
  }

  showVoucherStdFormReport(report: Report, reportData: any) {
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

      //this.report.render();
      this.report.renderAsync();
      this.viewer.report = this.report;

      console.log('Rendering the viewer to selected element');
      this.viewer.renderHtml('viewer');

    }, 10);


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
    //this.report.render();
    this.report.renderAsync();
    this.viewer.report = localReport;

    console.log('Rendering the viewer to selected element');
    this.viewer.renderHtml(this.Id);
    
  }

  addViewerEvents() {
    let title = this.title;
    let url = this.url;
    let headers = this.reporingService.httpHeaders;
    let http = this.reporingService.http;
    // Assign the onPrintReport event function
    this.viewer.onPrintReport = function () {
      console.log('onPrintReport');
      this.report.reportName = title;
      // for Print Log.
      let postItem = {listChanged:true,operation:OperationId.Print}
      let postBody = JSON.stringify(postItem);
      let base64Body = btoa(encodeURIComponent(postBody));
      if (headers)
        headers = headers.set("X-Tadbir-GridOptions", base64Body);
      if (url)
        http.get(url, { headers: headers, observe: "response" }).subscribe(res => {
            console.log("PrintLog.")
          })
    }

    // Assign the onReportExport event function
    this.viewer.onEndExportReport = function () {
      console.log('onEndExportReport');
    }
  }

  showReportViewer(reportTemplate: string, reportData: any, manager: any, isQuickReport: boolean, quickReportViewInfo: QuickReportConfigInfo) {
    this.active = true;

    var options = new Stimulsoft.Viewer.StiViewerOptions();
    options.appearance.fullScreenMode = true;

    this.viewer = new Stimulsoft.Viewer.StiViewer(null, this.Id, false);
    this.quickReport = isQuickReport;
    this.reportManager = manager;

    setTimeout(() => {


      console.log('Load report from url');
      this.report.load(reportTemplate);
      var dataSet = new Stimulsoft.System.Data.DataSet('dataset');

      //report is quick report
      if (this.quickReport) {

        var reportRows = null;

        if (reportData.rows instanceof Array) {
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
          var dateColumns = quickReportViewInfo.columns.filter(c => c.dataType && (c.dataType.toLowerCase() === "system.datetime"
            || c.dataType.toLowerCase() === "system.date"));

          var timeSpanColumns = quickReportViewInfo.columns.filter(c => c.dataType && (c.dataType.toLowerCase() === "system.timespan"));
          var boolColumns = quickReportViewInfo.columns.filter(c => c.dataType && (c.dataType.toLowerCase() === "system.boolean"));

          if (dateColumns.length > 0 && dateColumns.filter(c=>c.type == CalendarType.Jalali).length > 0) {
            var convertedData = reportRows;
            convertedData = this.convertToShamsiDate(convertedData, dateColumns);
            convertedData = this.formatTimeSpan(convertedData, timeSpanColumns);
            convertedData = this.formatBoolean(convertedData, boolColumns);

            this.report.regData("data", "data", convertedData);
            registerData = true;
          }
          else if (dateColumns.length > 0 ) {
            var convertedData = reportRows;
            convertedData = this.convertToMiladiDate(convertedData, dateColumns);
            convertedData = this.formatTimeSpan(convertedData, timeSpanColumns);
            convertedData = this.formatBoolean(convertedData, boolColumns);

            this.report.regData("data", "data", convertedData);
            registerData = true;
          }
        }

        if (!registerData) { //Add data to datastore
          let bools = (<Array<any>>dataSet.tables.list[0].columns.list).filter(col => col.dataType.name == 'Boolean');
          this.getBooleanTranslate(bools);

          if (this.CurrentLanguage == 'fa') {
            let rowNo = (<Array<any>>dataSet.tables.list[0].columns.list).filter(col => col.columnName == 'rowNo');
            let code = (<Array<any>>dataSet.tables.list[0].columns.list).filter(col => col.columnName == 'code');
            let fullCode = (<Array<any>>dataSet.tables.list[0].columns.list).filter(col => col.columnName == "fullCode");
            let allStringCols = (<Array<any>>dataSet.tables.list[0].columns.list).filter(col => col.dataType.name == "String");
            this.enNumsToFa(allStringCols);
            this.enNumsToFa(rowNo);
            this.enNumsToFa(code);
            this.enNumsToFa(fullCode);
          }
          
          this.report.regData("data", "data", dataSet);
        }

        this.report.dictionary.dataSources.add(dataSource);

        var parameters: Array<ParameterInfo> = reportData.parameters;
        var localReport = this.report;

        let lang:string;
        let config: any;
        var calConfig = this.bStorageService.getSystemConfig();
        if (calConfig) {
          config = JSON.parse(calConfig);
          if (config.defaultCalendar == 0)
            lang = "fa";

          if (config.defaultCalendar == 1)
            lang = "en";
        }        

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
        
        //this.report.render();
        this.report.renderAsync();
        this.viewer.report = this.report;

        //this.addViewerEvents(this.viewer);

        console.log('Rendering the viewer to selected element');
        this.viewer.renderHtml(this.Id);


      }
      else {
        
        this.report = ReportsQueries.registerReport(this.Code, this.report, reportData,this.bStorageService.getLanguage(),this.reportManager.reportParameter.fieldArray)
        this.showDesginedReportViewer(reportData, this.report);
      }
    }, 10);
  }

  enNumsToFa(args:any) {
    const farsiDigits = ['۰', '۱', '۲', '۳', '۴', '۵', '۶', '۷', '۸', '۹'];
    args.forEach(col => {
      let arrays = [];
      (<Array<any>>col.storage.values).forEach(item => {
        item = item.toString().replace(/\d/g, x => farsiDigits[x]);
        arrays.push(item);
      });
      col.storage.values = arrays;
    });
  }

  getBooleanTranslate(args:any) {
    args.forEach(col => {
      let arrays = [];
      (<Array<any>>col.storage.values).forEach(item => {
        item = item == true? this.translate.instant('Report.TrueLabel'): this.translate.instant('Report.FalseLabel');
        arrays.push(item);
      });
      col.storage.values = arrays;
    });
  }

  convertToShamsiDate(rows: any, cols: Array<QuickReportColumnConfig>) {

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name] && item.type == CalendarType.Jalali) {
          let momentDate = moment(rows[index][item.name]).locale('fa').format("YYYY/MM/DD");
          rows[index][item.name] = momentDate;
        }
      })


    }
    return rows;
  }

  formatTimeSpan(rows: any, cols: Array<QuickReportColumnConfig>) {

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name]) {
          var time = (rows[index][item.name]).toString().split('.')[0];          
          rows[index][item.name] = time;
        }
      })
    }
    return rows;
  }

  formatBoolean(rows: any, cols: Array<QuickReportColumnConfig>) {
    let trueText = this.getText('Account.Active');
    let falseText = this.getText('Account.Inactive');

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name] != null) {          
          if (rows[index][item.name] == true)
            rows[index][item.name] = trueText;

          if (rows[index][item.name] == false)
            rows[index][item.name] = falseText;
        }
      })
    }
    return rows;
  }

  convertToMiladiDate(rows: any, cols: Array<QuickReportColumnConfig>) {

    for (var index = 0; index < rows.length; index++) {
      cols.forEach(function (item) {
        if (rows[index][item.name] && item.type == CalendarType.Gregorian) {
          let momentDate = moment(rows[index][item.name]).locale('en').format("YYYY/MM/DD");
          rows[index][item.name] = momentDate;
        }
      })
    }
    return rows;
  }

}
