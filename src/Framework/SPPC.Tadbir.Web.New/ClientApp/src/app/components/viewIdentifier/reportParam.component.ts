import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'report-param',
  template: ''  
})
export class ReportParamComponent implements OnInit {

  @Input() public ParamName: string;
  @Input() public ParamValue: string;  

  constructor() { }

  ngOnInit() {
    
  }


 
}
