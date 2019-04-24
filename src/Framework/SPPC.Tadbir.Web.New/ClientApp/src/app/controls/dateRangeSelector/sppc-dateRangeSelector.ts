import { Component, OnInit, Input, Output, EventEmitter, ViewContainerRef } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'
import { SettingService } from '../../service/index';
import { ToastrService } from 'ngx-toastr';
import { BaseComponent } from '../../class/base.component';
import { MessageType } from '../../../environments/environment';



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

  constructor(public settingService: SettingService, public toastrService: ToastrService) {
    super(toastrService);
  }

  async ngOnInit() {

    this.fpStartDate = this.FiscalPeriodStartDate;
    this.fpEndDate = this.FiscalPeriodEndDate;

    this.displayFromDate = await this.settingService.getDateConfigAsync("start");
    this.displayToDate = await this.settingService.getDateConfigAsync("end");

    this.getFromDate();
    this.getToDate();


    var lang: string = "fa";
    if (localStorage.getItem('lang') != null) {
      var item: string | null;
      item = localStorage.getItem('lang');
      if (item)
        lang = item;
    }
    else
      if (sessionStorage.getItem('lang') != null) {
        var item: string | null;
        item = sessionStorage.getItem('lang');
        if (item)
          lang = item;
      }

    if (lang == "fa")
      this.rtl = true;
    else
      this.rtl = false;

    this.myForm.patchValue({ fromDate: this.displayFromDate, toDate: this.displayToDate });
    this.saveTemporarilyDate(this.displayFromDate, this.displayToDate);

    this.myForm.valueChanges.subscribe(val => {

      if (val.fromDate && val.toDate) {
        debugger;
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
              this.valueChange.emit({ fromDate: val.fromDate, toDate: val.toDate });
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

    let d2 = new Date(dateB);
    let d1 = new Date(dateA);

    let same = d1.getTime() === d2.getTime();

    //اگر دو تاریخ مساوی باشد
    if (same) return 0;

    //اگر تاریخ اول از تاریخ دوم بزرگتر باشد
    if (d1 > d2) return 1;

    //اگر تاریخ اول از تاریخ دوم کوچکتر باشد
    if (d1 < d2) return -1;
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

      sessionStorage.removeItem(sessionFromDate);
      sessionStorage.removeItem(sessionToDate);

      if (fromDate)
        sessionStorage.setItem(sessionFromDate, fromDate.toString());
      if (toDate)
        sessionStorage.setItem(sessionToDate, toDate.toString());
    }
  }

  /**گرفتن تاریخ ابتدا از حافظه موقت */
  getTemporarilyFromDate(): Date {
  if (this.viewName) {
    var sessionFromDate = "fromDate" + this.viewName;
    var value = sessionStorage.getItem(sessionFromDate);
    if (value) {
      return new Date(value);
    }
  }
  }

  /**گرفتن تاریخ انتها از حافظه موقت */
  getTemporarilyToDate(): Date {
  if (this.viewName) {
    var sessionToDate = "toDate" + this.viewName;
    var value = sessionStorage.getItem(sessionToDate);
    if (value) {
      return new Date(value);
    }
  }
  }
}
