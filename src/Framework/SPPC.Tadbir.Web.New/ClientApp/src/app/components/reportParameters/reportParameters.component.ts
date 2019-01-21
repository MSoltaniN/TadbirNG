import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Parameter } from '../../model/parameter';
import { FormGroup, FormControl } from '@angular/forms';
import { PrintInfo } from '../../model/printInfo';
import { ReportManagementComponent } from '../reportManagement/reportManagement.component';
import { ParameterInfo } from '../../service/report/reporting.service';

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


        paramArrays.push(paramInfo);
        paramsForm.addControl(paramInfo.fieldName,new FormControl())
      });

      this.fieldArray = paramArrays;
      this.parameterForm = paramsForm;
      //show dialog
      this.active = true;
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

