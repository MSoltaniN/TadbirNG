
import * as moment from 'jalali-moment';

export class ReportsQueries {

  public static registerReport(reportCode: string, report: any, data: any) {
        
    var outReport = null;
    switch (reportCode) {
      //VouchersStdForm Report
      case "Voucher-Std-Form":
        outReport = this.regVouchersStdForm(report, data);
        break;
    }

    return outReport;
  }

  public static regVouchersStdForm(report :any, data:any)
  {
    var reportData = data;  
    //set data in report
    report.regData("Vouchers", "VouchersStdForm", reportData.rows.lines);

    moment.locale('en');
    var momentDate = moment(new Date()).locale('fa').format("YYYY/MM/DD");

    //set parameters in report
    report.dictionary.variables.getByName("currentDate").valueObject = momentDate;
    report.dictionary.variables.getByName("date").valueObject = reportData.rows.date;
    report.dictionary.variables.getByName("id").valueObject = reportData.rows.id;
    report.dictionary.variables.getByName("description").valueObject = reportData.rows.description;
    report.dictionary.variables.getByName("no").valueObject = reportData.rows.no;

    return report;
  }
}
