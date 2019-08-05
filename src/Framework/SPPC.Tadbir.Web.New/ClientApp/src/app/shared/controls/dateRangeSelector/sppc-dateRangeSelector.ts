import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'
import { ToastrService } from 'ngx-toastr';
import * as moment from 'jalali-moment';
import { BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { MessageType } from '@sppc/env/environment';
import { BaseComponent } from '@sppc/shared/class';




@Component({
  selector: 'sppc-dateRangeSelector',
  templateUrl: './sppc-dateRangeSelector.html',
  //template: ``,
  styles: [`
#drs-content{
    margin-bottom: 10px;}
.float-right{float:right;}
`]
})
export class SppcDateRangeSelector extends BaseComponent implements OnInit {

  rtl: boolean = true;
  myForm = new FormGroup({
    fromDate: new FormControl(),
    toDate: new FormControl()
  });

  @Input() viewName: string;
  @Input() minDate: any;
  @Input() maxDate: any;
  @Input() isDisplayFromDate: boolean = true;
  @Input() isDisplayToDate: boolean = true;

  public displayFromDate: any;
  public displayToDate: any;

  public fpStartDate: Date;
  public fpEndDate: Date;

  @Output() valueChange = new EventEmitter();

  constructor(public settingService: SettingService, public toastrService: ToastrService, public bStorageService: BrowserStorageService) {
    super(toastrService, bStorageService);
  }

  async ngOnInit() {

    this.fpStartDate = this.FiscalPeriodStartDate;
    this.fpEndDate = this.FiscalPeriodEndDate;

    this.displayFromDate = await this.settingService.getDateConfigAsync("start");
    this.displayToDate = await this.settingService.getDateConfigAsync("end");

    this.getFromDate();
    this.getToDate();


    var lang: string = "fa";
    var item: string | null;
    item = this.bStorageService.getLanguage();
    if (item)
      lang = item;

    if (lang == "fa")
      this.rtl = true;
    else
      this.rtl = false;

    this.myForm.patchValue({ fromDate: this.displayFromDate, toDate: this.displayToDate });
    this.saveTemporarilyDate(this.displayFromDate, this.displayToDate);

    this.myForm.valueChanges.subscribe(val => {

      if (val.fromDate && val.toDate) {

        if (this.compareDate(val.fromDate, val.toDate) != 1) {
          if (this.compareDate(val.fromDate, this.fpStartDate) == -1) {
            this.showMessage("تاریخ ابتدا کوچکتر از ابتدای دوره مالی میباشد", MessageType.Warning);
            this.myForm.patchValue({ 'fromDate': this.fpStartDate });
          }
          else
            if (this.compareDate(val.toDate, this.fpEndDate) == 1) {
              this.showMessage("تاریخ انتها بزرگتر از انتهای دوره مالی میباشد", MessageType.Warning);
              this.myForm.patchValue({ 'toDate': this.fpEndDate });
            }
            else {

              this.valueChange.emit({
                fromDate: this.getEmitDate(val.fromDate, false),
                toDate: this.getEmitDate(val.toDate, true)
              });

              this.saveTemporarilyDate(val.fromDate, val.toDate);
            }
        }
        else {
          this.showMessage("محدوده تاریخی انتخابی معتبر نیست", MessageType.Warning);
          this.myForm.patchValue({ 'fromDate': this.fpStartDate, 'toDate': this.fpEndDate });
          this.saveTemporarilyDate(this.fpStartDate, this.fpEndDate);
        }
      }

    });
  }


  getEmitDate(date: Date, isToDate: boolean): any {
    var dateValue = moment(date).format('YYYY/MM/DD');
    if (isToDate) {
      var myDate = new Date(dateValue + ' ' + '23:59:59');
    }
    else {
      myDate = new Date(dateValue + ' ' + '00:00:01');
    }
    return moment(myDate).format('YYYY/MM/DD HH:mm:ss');
  }



  getFromDate() {

    var compareFromDateFpStart = this.compareDate(this.displayFromDate, this.fpStartDate);
    var compareFromDateFpEnd = this.compareDate(this.displayFromDate, this.fpEndDate);

    if (compareFromDateFpStart == -1 || compareFromDateFpEnd == 1) {
      this.displayFromDate = this.fpStartDate;
    }

    var tempFromDate = this.getTemporarilyFromDate();
    if (tempFromDate) {
      this.displayFromDate = tempFromDate;
    }
  }

  getToDate() {

    var compareToDateFpEnd = this.compareDate(this.displayToDate, this.fpEndDate);
    var compareToDateFpStart = this.compareDate(this.displayToDate, this.fpStartDate);

    if (compareToDateFpEnd == 1 || compareToDateFpStart == -1) {
      this.displayToDate = this.fpEndDate;
    }

    var tempToDate = this.getTemporarilyToDate();
    if (tempToDate) {
      this.displayToDate = tempToDate;
    }
  }


  compareDate(dateA: Date, dateB: Date): number {

    var dateValueA = moment(dateA).format('YYYY/MM/DD');
    var dateValueB = moment(dateB).format('YYYY/MM/DD');

    let dA = new Date(dateValueA + ' ' + '00:00:00');
    let dB = new Date(dateValueB + ' ' + '00:00:00');

    var diff = dA.getTime() - dB.getTime();
    var diffDays = Math.ceil(diff / (1000 * 3600 * 24));

    //اگر دو تاریخ مساوی باشد
    if (diffDays == 0) return 0;

    //اگر تاریخ اول از تاریخ دوم بزرگتر باشد
    if (diffDays > 0) return 1;

    //اگر تاریخ اول از تاریخ دوم کوچکتر باشد
    if (diffDays < 0) return -1;
  }

  /**
   * ذخیره موقت تاریخ در حافظه موقت مرورگر
   * @param fromDate
   * @param toDate
   */
  saveTemporarilyDate(fromDate: Date, toDate: Date) {
    if (this.viewName) {
      var sessionFromDate = "fromDate" + this.viewName;
      var sessionToDate = "toDate" + this.viewName;

      let sessionDateRangeArray: any[] = [];

      var dateStorage = this.bStorageService.getSelectedDateRange();
      if (dateStorage)
        sessionDateRangeArray = JSON.parse(dateStorage);

      if (sessionDateRangeArray.length > 0) {
        var fromIndex = sessionDateRangeArray.findIndex(f => f.key.toLowerCase() == sessionFromDate.toLowerCase());
        if (fromIndex > -1)
          sessionDateRangeArray.splice(fromIndex, 1);

        var toIndex = sessionDateRangeArray.findIndex(f => f.key.toLowerCase() == sessionToDate.toLowerCase());
        if (toIndex > -1)
          sessionDateRangeArray.splice(toIndex, 1);
      }

      this.bStorageService.removeSelectedDateRange();

      if (fromDate)
        sessionDateRangeArray.push({ key: sessionFromDate, value: fromDate.toString() });

      if (toDate)
        sessionDateRangeArray.push({ key: sessionToDate, value: toDate.toString() });

      if (sessionDateRangeArray.length > 0)
        this.bStorageService.setSelectedDaterange(sessionDateRangeArray);
    }
  }

  /**گرفتن تاریخ ابتدا از حافظه موقت */
  getTemporarilyFromDate(): Date {
    if (this.viewName) {
      var sessionFromDate = "fromDate" + this.viewName;

      let sessionDateRangeArray: any[] = [];

      var dateStorage = this.bStorageService.getSelectedDateRange();
      if (dateStorage) {
        sessionDateRangeArray = JSON.parse(dateStorage);
        var dateItem = sessionDateRangeArray.find(f => f.key.toLowerCase() == sessionFromDate.toLowerCase());
        if (dateItem) {
          return new Date(dateItem.value);
        }
      }
    }
  }

  /**گرفتن تاریخ انتها از حافظه موقت */
  getTemporarilyToDate(): Date {
    if (this.viewName) {
      var sessionToDate = "toDate" + this.viewName;

      let sessionDateRangeArray: any[] = [];

      var dateStorage = this.bStorageService.getSelectedDateRange();
      if (dateStorage) {
        sessionDateRangeArray = JSON.parse(dateStorage);
        var dateItem = sessionDateRangeArray.find(f => f.key.toLowerCase() == sessionToDate.toLowerCase());
        if (dateItem) {
          return new Date(dateItem.value);
        }
      }
    }
  }
}
