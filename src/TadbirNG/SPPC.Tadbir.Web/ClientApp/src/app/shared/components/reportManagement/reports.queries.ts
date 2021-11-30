
import * as moment from 'jalali-moment';
import { ServiceLocator } from '@sppc/service.locator';
import { BrowserStorageService } from '@sppc/shared/services';
import { QuickReportConfigInfo } from '@sppc/shared/models/QuickReportConfig';

export class ReportsQueries {

  static language: string;

  public static registerReport(reportCode: string, report: any, data: any, currentLanguage:string,params: Array<any>) {
        
    var outReport = null;
    this.language = currentLanguage;

    switch (reportCode) {
      //VouchersStdForm Report
      case "Voucher-Std-Form":
        outReport = this.regVouchersStdForm(report, data);
        break;
      case "Voucher-Std-Form-Detail":
        outReport = this.regVouchersStdFormDetail(report, data);
        break;
      case "Voucher-By-Detail":
        outReport = this.regVoucherByDetail(report, data);
        break;
      case "Voucher-By-Ledger":
        outReport = this.regVoucherByLedger(report, data);
        break;
      case "Voucher-By-Subsid":
        outReport = this.regVoucherBySubsid(report, data);
        break;
      case "Journal-ByDate-ByLedger":
        outReport = this.regJournal_ByDate_ByLedger(report, data, params);
        break;
      case "Journal-ByDate-BySubsidiary":
        outReport = this.regJournal_ByDate_BySubsidiary(report, data,params);
        break;
      case "Journal-ByNo-ByLedger":
        outReport = this.regJournal_ByNo_ByLedger(report, data,params);
        break;
      case "Journal-ByNo-BySubsidiary":
        outReport = this.regJournal_ByNo_BySubsidiary(report, data,params);
      break;
        
    }

    return outReport;
  }

  private static getDate(date:any)
  {
    var browserStorageService = ServiceLocator.injector.get(BrowserStorageService);
   
    var lang = "";
    var calConfig = browserStorageService.getSystemConfig();
    if (calConfig) {
      var config = JSON.parse(calConfig);
      if (config.defaultCalendar == 0)
        lang = "fa";

      if (config.defaultCalendar == 1)
        lang = "en";
    }

    var momentDate = null;
    if (lang == "fa")
      momentDate = moment(date).locale(lang).format("YYYY/MM/DD");
    else
      momentDate = moment(date).locale(lang).format("YYYY/MM/DD");

    return momentDate;
  }

  public static regVouchersStdForm(report :any, data:any)
  {
    var reportData = data;  
    //set data in report
    report.regData("Vouchers", "VouchersStdForm", reportData.rows.lines);

    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = this.getDate(new Date());
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }

  public static regVoucherByDetail(report: any, data: any) {

    var reportData = data;
    //set data in report
    report.regData("Vouchers", "root", reportData.rows);
    report.regData("Vouchers", "root_lines", reportData.rows.lines);
    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = this.getDate(new Date());
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("issuerName").valueObject = reportData.rows.issuerName;    
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }

  public static regVoucherByLedger(report: any, data: any) {

   
    var reportData = data;
    //set data in report
    report.regData("Vouchers", "root", reportData.rows);
    report.regData("Vouchers", "root_lines", reportData.rows.lines);

    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = this.getDate(new Date());
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("issuerName").valueObject = reportData.rows.issuerName;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }

  public static regVoucherBySubsid(report: any, data: any) {
   
    var reportData = data;
    //set data in report
    report.regData("Vouchers", "root", reportData.rows);
    report.regData("Vouchers", "root_lines", reportData.rows.lines);
  
    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = this.getDate(new Date());
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("issuerName").valueObject = reportData.rows.issuerName;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }

  

  public static regVouchersStdFormDetail(report: any, data: any) {

    var reportData = data;
    //set data in report
    report.regData("Vouchers", "root", reportData.rows);
    report.regData("Vouchers", "root_lines", reportData.rows.lines);
  
    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = this.getDate(new Date());;
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }

  public static regJournal_ByDate_ByLedger(report: any, data: any , params: Array<any>) {
    
    var reportData = data;
    //set data in report
    report.regData("Journal_ByDate_ByLedger", "items", reportData.rows.items);     
    
    var fromDate = new Date(params.filter(p=>p.name == "fromDate")[0].value);
    report.dictionary.variables.getByName("fromDate").valueObject = this.getDate(fromDate);

    var toDate = new Date(params.filter(p=>p.name == "toDate")[0].value);
    report.dictionary.variables.getByName("toDate").valueObject = this.getDate(toDate);

    //set parameters in report
    report.dictionary.variables.getByName("debitSum").valueObject = reportData.rows.debitSum;;
    report.dictionary.variables.getByName("creditSum").valueObject = reportData.rows.creditSum;
    report.dictionary.variables.getByName("totalCount").valueObject = reportData.rows.totalCount;
    report.dictionary.variables.getByName("startCredit").valueObject = reportData.rows.startCredit;
    report.dictionary.variables.getByName("startDebit").valueObject = reportData.rows.startDebit;        
    
    return report;
  }

  public static regJournal_ByDate_BySubsidiary(report: any, data: any, params: Array<any>) {
    
    var reportData = data;
    //set data in report
    report.regData("Journal_ByDate_BySubsidiary", "items", reportData.rows.items);     

    var fromDate = new Date(params.filter(p=>p.name == "fromDate")[0].value);
    report.dictionary.variables.getByName("fromDate").valueObject = this.getDate(fromDate);

    var toDate = new Date(params.filter(p=>p.name == "toDate")[0].value);
    report.dictionary.variables.getByName("toDate").valueObject = this.getDate(toDate);

    //set parameters in report
    report.dictionary.variables.getByName("debitSum").valueObject = reportData.rows.debitSum;;
    report.dictionary.variables.getByName("creditSum").valueObject = reportData.rows.creditSum;
    report.dictionary.variables.getByName("totalCount").valueObject = reportData.rows.totalCount;
    report.dictionary.variables.getByName("startCredit").valueObject = reportData.rows.startCredit;
    report.dictionary.variables.getByName("startDebit").valueObject = reportData.rows.startDebit; 

    return report;
  }

  public static regJournal_ByNo_ByLedger(report: any, data: any, params: Array<any>) {
    
    var reportData = data;
    //set data in report
    report.regData("Journal_ByNo_ByLedger", "items", reportData.rows.items);     

    report.dictionary.variables.getByName("fromNo").valueObject = params.filter(p=>p.name == "fromNo")[0].value;
    report.dictionary.variables.getByName("toNo").valueObject = params.filter(p=>p.name == "toNo")[0].value;  

    //set parameters in report
    report.dictionary.variables.getByName("debitSum").valueObject = reportData.rows.debitSum;;
    report.dictionary.variables.getByName("creditSum").valueObject = reportData.rows.creditSum;
    report.dictionary.variables.getByName("totalCount").valueObject = reportData.rows.totalCount;
    report.dictionary.variables.getByName("startCredit").valueObject = reportData.rows.startCredit;
    report.dictionary.variables.getByName("startDebit").valueObject = reportData.rows.startDebit; 

    return report;
  }

  public static regJournal_ByNo_BySubsidiary(report: any, data: any, params: Array<any>) {
    
    var reportData = data;
    //set data in report
    report.regData("Journal_ByNo_BySubsidiary", "items", reportData.rows.items);     

    report.dictionary.variables.getByName("fromNo").valueObject = params.filter(p=>p.name == "fromNo")[0].value;
    report.dictionary.variables.getByName("toNo").valueObject = params.filter(p=>p.name == "toNo")[0].value;  
    //set parameters in report
    report.dictionary.variables.getByName("debitSum").valueObject = reportData.rows.debitSum;;
    report.dictionary.variables.getByName("creditSum").valueObject = reportData.rows.creditSum;
    report.dictionary.variables.getByName("totalCount").valueObject = reportData.rows.totalCount;
    report.dictionary.variables.getByName("startCredit").valueObject = reportData.rows.startCredit;
    report.dictionary.variables.getByName("startDebit").valueObject = reportData.rows.startDebit; 
    
    return report;
  }

}
