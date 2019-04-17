import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormGroup, FormControl } from '@angular/forms'
import { EnviromentComponent } from '../../class/enviroment.component';
import { SettingService } from '../../service/index';


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
export class SppcDateRangeSelector implements OnInit {

  rtl: boolean = true;
  myForm = new FormGroup({
    fromDate: new FormControl(),
    toDate: new FormControl()
  });

  @Input() minDate: any;
  @Input() maxDate: any;
  @Input() isDisplayFromDate: boolean = true;
  @Input() isDisplayToDate: boolean = true;

  public displayFromDate: any;
  public displayToDate: any;

  @Output() valueChange = new EventEmitter();

  constructor(private formBuilder: FormBuilder,public settingService:SettingService) {
    
  }

  async ngOnInit() {

    this.displayFromDate = await this.settingService.getDateConfig("start");
    this.displayToDate = await this.settingService.getDateConfig("end");
    
    //this.minDate = this.displayFromDate;
    //this.maxDate = this.displayToDate;

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

    this.myForm.patchValue({ 'fromDate': this.displayFromDate, 'toDate': this.displayToDate });

    this.myForm.valueChanges.subscribe(val => {
      this.valueChange.emit({ fromDate: val.fromDate, toDate: val.toDate });
    });
  }

}
