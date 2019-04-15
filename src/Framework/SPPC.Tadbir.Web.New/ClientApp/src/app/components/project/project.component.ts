import { Component, OnInit, Renderer2 } from '@angular/core';
import { Layout, Entities, Metadatas, MessageType } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService, ProjectInfo, GridService } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { ProjectApi } from '../../service/api';
import { Project } from '../../model';
import { String } from '../../class/source';
import { DialogService } from '@progress/kendo-angular-dialog';
import { ViewName } from '../../security/viewName';
import { GridExplorerComponent } from '../../class/gridExplorer.component';
import { ProjectFormComponent } from './project-form.component';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'project',
  templateUrl: './project.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class ProjectComponent extends GridExplorerComponent<Project> {



  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, Entities.Project, Metadatas.Project,
      "Project.LedgerProject", "Project.EditorTitleNew", "Project.EditorTitleEdit",
      ProjectApi.EnvironmentProjects, ProjectApi.EnvironmentProjectsLedger, ProjectApi.Project, ProjectApi.ProjectChildren, ViewName.Project)
  }


  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {

    var errorMsg = this.getText('Messages.TreeLevelsAreTooDeep');
    var editorTitle = this.getEditorTitle(isNew);

    if (this.levelConfig)
      if (this.levelConfig.isEnabled) {
        this.dialogRef = this.dialogService.open({
          title: editorTitle,
          content: ProjectFormComponent,
        });

        this.dialogModel = this.dialogRef.content.instance;
        this.dialogModel.parent = this.parent;
        this.dialogModel.model = this.editDataItem;
        this.dialogModel.isNew = isNew;
        this.dialogModel.errorMessage = undefined;


        this.dialogRef.content.instance.save.subscribe((res) => {
          this.saveHandler(res, isNew);
        });

        const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
          this.dialogRef.close();
        });

      }
      else {
        this.showMessage(String.Format(errorMsg, (this.levelConfig.no - 1).toString()), MessageType.Warning);
      }
  }

  addNew() {
    this.editDataItem = new ProjectInfo();
    this.openEditorDialog(true);
  }

}

