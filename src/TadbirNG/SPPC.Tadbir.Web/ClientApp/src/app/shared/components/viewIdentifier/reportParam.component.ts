import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'report-param',
  template: ''  
})
export class ReportParamComponent implements OnInit {

  @Input() public ParamName: string;
  @Input() public ParamValue: string;
  /** مقدار تهی برای این مشخصه به معنی پارامتر ساده است مقدار UrlParameter به معنی درج مقدار در آدرس سرویس است **/
  /** fperiods/company/{0} ===> UrlParamater ==> fperiods/company/1 **/
  @Input() public ParamType: string;
  /** مشخص کننده این هست که پارامتر در فرم ورودی پارامتر ها نمایش داده شود یا خیر **/
  @Input() public ParamReportVisible: boolean = true;
  /** مشخص کننده این هست که پارامتر در فیلتر ساخته شود یا خیر **/
  @Input() public ParamInFilter: boolean = true;

  constructor() { }

  ngOnInit() {
    
  }


 
}
