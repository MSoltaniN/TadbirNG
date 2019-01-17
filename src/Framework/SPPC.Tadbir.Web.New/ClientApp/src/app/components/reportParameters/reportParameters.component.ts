import { Component, OnInit, Input } from '@angular/core';
import { Parameter } from '../../model/parameter';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'report-parameters',
  templateUrl: './reportParameters.component.html',
  styleUrls: ['./reportParameters.component.css']
})
export class ReportParametersComponent implements OnInit {

  @Input() reportId:number;
  @Input() active:boolean;

  fieldArray: Array<Parameter> = [];
  public parameterForm = new FormGroup({});

  constructor() { 

  }

  ngOnInit() {

  }

  public showDialog(reportId:number)
  {   
      //add sample parameters
      //TODO: get paramaters from service by reportId
      this.fieldArray = [];

          var param : Parameter = new ParameterInfo();
          param.fieldName = "Id";
          param.controlType = "TextBox";
          param.id = 1;
          param.defaultValue = "1";
          param.captionKey = "کد سند";
          this.fieldArray.push(param);
          this.parameterForm.addControl(param.fieldName,new FormControl())

          var param1: Parameter = new ParameterInfo();
          param1.fieldName = "IsActive";
          param1.controlType = "CheckBox";
          param1.id = 2;
          param1.defaultValue = "true";
          param1.captionKey = "فعال";
          this.fieldArray.push(param1);

          this.parameterForm.addControl(param1.fieldName,new FormControl())
      //add sample parameters

      //add controls
     
      

      //show dialog
      this.active = true;
  }

  public closeDialog()
  {
      this.active = false;
  }

}

export interface ParameterFields {
  value: string;
}

export class ParameterInfo implements Parameter,ParameterFields {
  value: string;
  id: number;  fieldName: string;
  operator: string;
  dataType: string;
  controlType: string;
  captionKey: string;
  defaultValue: string;
  minValue: string;
  maxValue: string;
  descriptionKey: string;

  
}