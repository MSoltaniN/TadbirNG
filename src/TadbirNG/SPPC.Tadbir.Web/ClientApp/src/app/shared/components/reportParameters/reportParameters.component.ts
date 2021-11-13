import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ParameterInfo } from '@sppc/shared/services';
import { PrintInfo } from '@sppc/shared/models';



@Component({
  selector: 'report-parameters',
  templateUrl: './reportParameters.component.html',
  styleUrls: ['./reportParameters.component.css']
})
export class ReportParametersComponent implements OnInit {

  @Input() reportId:number;
  @Input() active:boolean;
  @Output() onOkClick: EventEmitter<any> = new EventEmitter();
  

  fieldArray: Array<ParameterInfo> = [];
  public parameterForm = new FormGroup({});

  constructor() { 

  }

  ngOnInit() {

  }

  public showDialog(printInfo : PrintInfo)
  {   
      this.active = true;
      //add sample parameters
      //TODO: get paramaters from service by reportId
      var paramsForm = new FormGroup({});
      var paramArrays  = new Array<ParameterInfo>();      
      printInfo.parameters.forEach(function(param){

        var paramInfo : ParameterInfo = new ParameterInfo();
        paramInfo.fieldName = param.fieldName;

        paramInfo.controlType = param.controlType;
        paramInfo.id = param.id;
        paramInfo.defaultValue = param.defaultValue? param.defaultValue : "";
        paramInfo.captionKey = param.captionKey;
        paramInfo.operator = param.operator;
        paramInfo.dataType = param.dataType;
        paramInfo.descriptionKey = param.descriptionKey;
        paramInfo.name = param.name;

        paramArrays.push(paramInfo);
        //paramsForm.addControl(paramInfo.fieldName,new FormControl())
        paramsForm.addControl(paramInfo.name,new FormControl('',Validators.required));
        
      });

      this.fieldArray = paramArrays;
      this.parameterForm = paramsForm;
      //show dialog
      
  }

  getControl(name:string)
  {
    return this.parameterForm.get(name);
  }

  public cancelDialog()
  {
      this.active = false;      
  }

  public okDialog()
  {      
      this.active = false;
      this.onOkClick.emit({params : this.fieldArray});
  }

}

export class TabInfo {
  index: number; 
  showReportViewer: boolean;
  reportId: string;
  title: string;
  
}
