import { Component, Input, Output, EventEmitter, Renderer2, Host } from '@angular/core';
import { Validators, FormGroup, FormControl } from '@angular/forms';
import { AccountGroup } from '../../model/index';
import { Property } from "../../class/metadata/property"
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs/Observable';
import { ContextInfo } from "../../service/login/authentication.service";
import { DefaultComponent } from "../../class/default.component";
import { Layout, Entities, Metadatas } from "../../../environments/environment";
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { DetailComponent } from '../../class/detail.component';
import { LookupService } from '../../service/index';
import { LookupApi } from '../../service/api/index';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

interface Item {
  key: string,
  value: string
}

@Component({
  selector: 'accountGroups-form-component',
  styles: [`
        input[type=text],textarea ,input[type=password]{ width: 100%; }
        /deep/ .ddl-category { width:100% }
    `],
  templateUrl: './accountGroups-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class AccountGroupsFormComponent extends DetailComponent {

  categoriesList: Array<Item> = [];
  //create properties
  categorySelected: string;
  active: boolean = false;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set model(accountGroup: AccountGroup) {
    this.editForm.reset(accountGroup);

    if (accountGroup && this.categoriesList.length > 0) {
      var item = this.categoriesList.find(f => f.value == accountGroup.category);
      this.categorySelected = item ? item.key : undefined;
    }

    this.active = accountGroup !== undefined || this.isNew;
  }

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<AccountGroup> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
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

  constructor(public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService) {

    super(toastrService, translate, renderer, metadata, Entities.AccountGroup, Metadatas.AccountGroup);
    this.getAccountGroupCategory();
  }


  getAccountGroupCategory() {
    this.lookupService.getAll(LookupApi.AccountGroupCategories).subscribe(res => {

      this.categoriesList = res.body;
    })
  }
}
