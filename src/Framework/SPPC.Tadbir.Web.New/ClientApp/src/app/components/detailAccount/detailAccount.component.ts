import { Component, OnInit, Renderer2} from '@angular/core';
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService, GridService, DetailAccountInfo } from '../../service';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailAccountApi } from '../../service/api';
import { DetailAccount } from '../../model';
import { String } from '../../class/source';
import { DialogService } from '@progress/kendo-angular-dialog';
import { ViewName } from '../../security/viewName';
import { GridExplorerComponent } from '../../class/gridExplorer.component';
import { DetailAccountFormComponent } from './detailAccount-form.component';


export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'detailAccount',
  templateUrl: './detailAccount.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class DetailAccountComponent extends GridExplorerComponent<DetailAccount> {


  constructor(public toastrService: ToastrService, public translate: TranslateService, public service: GridService, public dialogService: DialogService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService) {
    super(toastrService, translate, service, dialogService, renderer, metadata, settingService, Entities.DetailAccount, Metadatas.DetailAccount, "DetailAccount.LedgerDetailAccount",
      DetailAccountApi.EnvironmentDetailAccounts, DetailAccountApi.EnvironmentDetailAccountsLedger, DetailAccountApi.DetailAccount, DetailAccountApi.DetailAccountChildren)
  }

  /**باز کردن و مقداردهی اولیه به فرم ویرایشگر */
  openEditorDialog(isNew: boolean) {

    this.dialogRef = this.dialogService.open({
      title: this.getEditorTitle(isNew),
      content: DetailAccountFormComponent,
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

  getEditorTitle(isNew: boolean): string {
    var editorTitle = '';

    var config = this.getViewTreeSettings(ViewName.DetailAccount);

    if (config) {
      var level = this.parent ? this.parent.level + 2 : 1;
      var viewConfig = config.levels.find(f => f != null && f.no == level);

      if (viewConfig)
        editorTitle = viewConfig.name;
    }

    return String.Format(this.getText(isNew ? 'DetailAccount.EditorTitleNew' : 'DetailAccount.EditorTitleEdit'), editorTitle);
  }

  addNew() {
    this.editDataItem = new DetailAccountInfo();
    this.openEditorDialog(true);
  }

}


