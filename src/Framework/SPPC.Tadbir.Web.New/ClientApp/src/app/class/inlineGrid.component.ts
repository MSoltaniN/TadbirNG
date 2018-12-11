import { BaseComponent } from "./base.component";
import { Injectable, Renderer2, Optional, Inject, Host } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from "../service/metadata/metadata.service";
import { FormGroup, FormControl, ValidatorFn, Validators, FormBuilder } from "@angular/forms";
import { Property } from "./metadata/property";
import { String } from './source';
import { DefaultComponent } from "../class/default.component";
import { SettingService } from "../service/index";
import { Entities, Metadatas } from "../../environments/environment";



@Injectable()
export class InlineGridComponent extends DefaultComponent {

  public form: FormGroup;
  public properties: { [id: string]: Array<Property>; } = {}

  constructor(public toastrService: ToastrService, public translate: TranslateService
    , public renderer: Renderer2, public formBuilder: FormBuilder, public metadataService: MetaDataService, public settingService: SettingService,
    @Optional() @Inject('empty') public entityType: string, @Optional() @Inject('empty') public metaDataName: string) {
    super(toastrService, translate, renderer, metadataService, settingService, Entities.Project, Metadatas.Project);

  }

  public createFormGroup(dataItem: any): FormGroup {
    return this.formBuilder.group({
      'name': [dataItem.name, Validators.required],
      'code': [dataItem.code, Validators.required],
      'fullCode': [dataItem.fullCode, Validators.required],
      'description': dataItem.description
    });
  }

  


  public get editForm(): FormGroup {
    if (this.form == undefined) {
      this.form = this.formBuilder.group({});
      this.form.addControl('id', this.formBuilder.control(0))
      //this.form = new FormGroup({ id: new FormControl() });
      if (this.properties[this.metaDataName] == undefined) {
        this.metadataService.getMetaData(this.metaDataName).finally(() => {
          //debugger;
          this.fillFormValidators();
         return this.form;
        }).subscribe((res1: any) => {
          //debugger;
          this.properties[this.metaDataName] = res1.columns;
          localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))
          return
        });
      }
    }
    else {
      this.fillFormValidators();
    }
    return this.form;
  }

  private fillFormValidators() {
    var p: Property | undefined = undefined;
    if (this.properties[this.metaDataName] == undefined) return;
    for (let entry of this.properties[this.metaDataName]) {
      var name: string = entry.name.toLowerCase().substring(0, 1) + entry.name.substring(1);
      var validators: ValidatorFn[] = [];
      if (entry.length > 0) validators.push(Validators.maxLength(entry.length));
      if (entry.minLength > 0) validators.push(Validators.minLength(entry.minLength));
      if (!entry.isNullable) validators.push(Validators.required);
      if (!this.form.contains(name)) {
        //this.form.addControl(name, new FormControl("", validators));
        this.form.addControl(name, this.formBuilder.control("", validators))
      }
    }
  }

  //private initializeFrom() {
  //  if (this.properties[this.metaDataName] == undefined) {
  //    this.metadataService.getMetaData(this.metaDataName).finally(() => {
  //      this.fillFormValidators();
  //    }).subscribe((res1: any) => {
  //      this.properties[this.metaDataName] = res1.properties;
  //      localStorage.setItem(this.metaDataName, JSON.stringify(this.properties[this.metaDataName]))
  //      return
  //    });
  //  }
  //  else {
  //    this.fillFormValidators();
  //  }
  //}


}
