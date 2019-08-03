import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout, Entities } from 'environments/environment';
import { DetailComponent, LookupService, BrowserStorageService, MetaDataService, ViewName, LookupApi } from '@sppc/shared';
import { AccountGroup } from '@sppc/finance';




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
    public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService) {

    super(toastrService, translate, bStorageService, renderer, metadata, Entities.AccountGroup, ViewName.AccountGroup);
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
        var item = this.categoriesList.find(f => f.value == this.model.category);
        this.categorySelected = item ? item.key : undefined;
      }
    })
  }
}
