import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { Project } from '../../model/index';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";

import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
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
    `
    input[type=text],textarea { width: 100%; } /deep/ .k-dialog-buttongroup {border-color: #f1f1f1;}
    `
  ],
  templateUrl: './project-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]

})

export class ProjectFormComponent extends DetailComponent implements OnInit {

  //create properties
  viewId: number;
  parentScopeValue: number = 0;
  parentFullCode: string = '';

  level: number = 0;

  @Input() public parent: Project;
  @Input() public model: Project;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Output() save: EventEmitter<Project> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      let model: Project = this.editForm.value;
      if (this.model.id > 0) {
        model.branchId = this.model.branchId;
        model.fiscalPeriodId = this.model.fiscalPeriodId;
        model.companyId = this.model.companyId;
      }
      else {
        let model: Project = this.editForm.value;
        model.branchId = this.BranchId;
        model.fiscalPeriodId = this.FiscalPeriodId;
        model.companyId = this.CompanyId;
        model.parentId = this.parent ? this.parent.id : undefined;
        model.level = this.level;
      }
      this.save.emit(model);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.cancel.emit();
  }

  escPress() {
    this.cancel.emit();
  }
  //Events

  ngOnInit(): void {
    this.viewId = ViewName.Project;

    this.editForm.reset();

    this.parentScopeValue = 0;

    if (this.parent) {
      this.parentFullCode = this.parent.fullCode;
      this.model.fullCode = this.parentFullCode;
      this.parentScopeValue = this.parent.branchScope;
      this.level = this.parent.level + 1;
    }
    else {
      this.level = 0;
    }

    if (this.model && this.model.code)
      this.model.fullCode = this.parentFullCode + this.model.code;

    setTimeout(() => {
      this.editForm.reset(this.model);
    })

  }

  constructor(public toastrService: ToastrService, public translate: TranslateService,
    public renderer: Renderer2, public metadata: MetaDataService) {
    super(toastrService, translate, renderer, metadata, Entities.Project, ViewName.Project);
  }


}
