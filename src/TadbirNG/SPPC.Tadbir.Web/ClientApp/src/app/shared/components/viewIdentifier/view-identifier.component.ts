import { Component, OnInit, Input, ViewChild, ViewChildren, QueryList, AfterViewInit, ContentChildren } from '@angular/core';
import { ReportParamComponent } from './reportParam.component';


@Component({
  selector: 'view-identifier',
  template: `<ng-content></ng-content>`,
  styleUrls: ['./view-identifier.component.css']
})
export class ViewIdentifierComponent implements OnInit, AfterViewInit {
  
  @Input() public ViewID: string;

  @Input() public IsDynamicColumns: boolean;

  @Input() public DynamicMetadata: any[];

  @Input() public ID: string;

  //@ViewChildren(ReportParamComponent) reportParams: ReportParamComponent;
  @ContentChildren(ReportParamComponent) params : QueryList<ReportParamComponent>;
 
  constructor() { }

  ngOnInit() {
    
  }

  ngAfterViewInit(): void {
    
  }

 
}
