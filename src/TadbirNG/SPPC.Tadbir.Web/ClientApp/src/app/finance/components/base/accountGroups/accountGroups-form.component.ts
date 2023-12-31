import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { LookupService, BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { AccountGroup } from '@sppc/finance/models';
import { DetailComponent } from '@sppc/shared/class';
import { ViewName } from '@sppc/shared/security';
import { LookupApi } from '@sppc/shared/services/api';




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
        ::ng-deep .ddl-category { width:100% }
    `],
  templateUrl: './accountGroups-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class AccountGroupsFormComponent extends DetailComponent implements OnInit{

  categoriesList: Array<Item> = [];
  //create properties
  categorySelected: string;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() public model: AccountGroup;


  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<AccountGroup> = new EventEmitter();
  //create properties

  //Events
  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      this.save.emit(this.editForm.value);
    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.isNew = false;
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
  //Events

  constructor(public toastrService: ToastrService, public translate: TranslateService, public lookupService: LookupService,
    public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService,public elem:ElementRef) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.AccountGroup, ViewName.AccountGroup,elem);
  }

  ngOnInit() {

    this.editForm.reset()

    this.getAccountGroupCategory();

    this.editForm.reset(this.model);
  }

  getAccountGroupCategory() {
    this.lookupService.getAll(LookupApi.AccountGroupCategories).subscribe(res => {

      this.categoriesList = res.body;

      if (this.model && this.categoriesList.length > 0) {
        var item = this.categoriesList.find(f => f.key.toLowerCase() == this.model.category.toLowerCase());
        this.categorySelected = item ? item.key : undefined;
      }
    })
  }
}
