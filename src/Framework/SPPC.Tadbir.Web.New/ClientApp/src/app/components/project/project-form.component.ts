import { Component, Input, Output, EventEmitter, Renderer2, Host, OnInit } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { Project } from '../../model/index';

import { Property } from "../../class/metadata/property"
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';

import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { ProjectApi } from '../../service/api/index';
import { String } from '../../class/source';
import { DetailComponent } from '../../class/detail.component';
import { ViewName } from '../../security/viewName';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  Key: string,
  Value: string
}


@Component({
  selector: 'project-form-component',
  styles: [
    "input[type=text],textarea { width: 100%; }"
  ],
  templateUrl: './project-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class ProjectFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  active: boolean = false;
  fullCodeApiUrl: string;
  editModel: Project;
  parentModel: Project;
  parentScopeValue: number = 0;

  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set parent(parent: Project) {
    this.parentModel = parent;
    this.parentScopeValue = 0;
    this.fullCodeApiUrl = String.Format(ProjectApi.ProjectFullCode, 0);

    if (parent) {
      this.fullCodeApiUrl = String.Format(ProjectApi.ProjectFullCode, parent.id);
      this.parentScopeValue = parent.branchScope;
    }
  };

  @Input() public set model(project: Project) {
    this.editModel = project;
    this.editForm.reset(project);

    this.active = project !== undefined || this.isNew;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Project> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {

      if (this.editModel) {
        let model: Project = this.editForm.value;
        model.branchId = this.editModel.branchId;
        model.fiscalPeriodId = this.editModel.fiscalPeriodId;
        model.companyId = this.editModel.companyId;
        this.save.emit(model);
      }
      else
        this.save.emit(this.editForm.value);
      this.active = true;
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.active = false;
    this.cancel.emit();
  }
  //Events

  ngOnInit(): void {
    this.viewId = ViewName.Account;
  }

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.Project, Metadatas.Project);
  }


}
