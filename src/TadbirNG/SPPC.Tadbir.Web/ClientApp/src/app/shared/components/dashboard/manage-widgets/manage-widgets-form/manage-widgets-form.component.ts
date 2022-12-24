import { Component, ElementRef, EventEmitter, Input,
   OnInit, Output, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DialogRef, DialogService } from '@progress/kendo-angular-dialog';
import { ItemArgs } from '@progress/kendo-angular-dropdowns';
import { FullAccount } from '@sppc/finance/models';
import { CurrencyService } from '@sppc/finance/service';
import { DetailComponent } from '@sppc/shared/class';
import { Entities } from '@sppc/shared/enum/metadata';
import { Widget } from '@sppc/shared/models/widget';
import { WidgetFunction } from '@sppc/shared/models/widgetFunction';
import { WidgetType } from '@sppc/shared/models/widgetType';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, LookupService, MetaDataService } from '@sppc/shared/services';
import { ToastrService } from 'ngx-toastr';
import { WidgetService } from '../../services/widget.service';

export enum RequiredAccountFunctions {
  // گردش بدهکار
  DebtorTurnover = 1,
  // گردش بستانکار
  CreditTurnover = 2,
  // گردش خالص
  NetTurnover = 3,
  // مانده
  Balance = 4,
};

export enum GaugesTypes {
  Gauge_Circular = 10,
  Gauge_Digital = 11,
  Gauge_Linear = 12,
};

@Component({
  selector: 'app-manage-widgets-form',
  templateUrl: './manage-widgets-form.component.html',
  styleUrls: ['./manage-widgets-form.component.css']
})
export class ManageWidgetsFormComponent extends DetailComponent implements OnInit {

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public currencyService: CurrencyService,
    public lookupService: LookupService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public elem: ElementRef,
    public dialogService: DialogService,
    private widgetService: WidgetService,
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      Entities.Dashboard,
      ViewName.Dashboard,
      elem
    );
  }

  @Input() public model: Widget;
  @Input() public isNew: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public isWizard: boolean = false;

  @Input() functionsList: WidgetFunction[];
  @Input() typesList: WidgetType[];

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Widget> = new EventEmitter();

  widgetAccounts: FullAccount[] = [];
  selectedType: any;
  selectedFunction: any;
  accountRequired: boolean = false;

  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  ngOnInit() {

    this.disabledType = this.disabledType.bind(this);
    setTimeout(() => {
      this.editForm.reset(this.model);
      if (this.isNew) {
        this.editForm.patchValue({
          createdById: this.widgetService.UserId,
          createdByFullName: this.widgetService.UserName
        });
      } else {
        this.editForm.patchValue({
          createdByFullName: 'null'
        });
      }
      this.widgetAccounts = this.model.accounts;
    })
  }

  focusHandler(event) {
    console.log(event);
  }

  setWidgetAccounts(event) {
    this.widgetAccounts = event;
  }

  onChangeFunction(id:number) {
    if (id in RequiredAccountFunctions) {
      this.accountRequired = true;
    } else {
      this.accountRequired = false;
    }
    let functionName = this.functionsList.find(item => item.id == id).name;
    this.editForm.patchValue({
      functionName: functionName
    });
  }

  disabledType(item: ItemArgs) {
    if (this.selectedFunction in RequiredAccountFunctions && item.dataItem.id in GaugesTypes) {
      return true;
    } else {
      return false;
    }
  }

  onChangeType(id) {
    let typeName = this.typesList.find(item => item.id == id).name;
    this.editForm.patchValue({
      typeName: typeName
    });
  }

  public async onSave(e: any) {
    e.preventDefault();
    if (this.editForm.valid) {
      let values = this.editForm.value;
      values.Accounts = this.widgetAccounts;
      if (this.isNew) {
        values.id = 0;
        values.defaultSettings = '{"x":0,"y":0,"width":20,"height":20}';
      } else {
        values.defaultSettings = this.model.defaultSettings;
      }

      if (this.accountRequired && !this.widgetAccounts) {
        let msg = await this.translate.get('AllValidations.Widget.AccountIdIsRequired').toPromise();
        this.errorMessages = [msg];
      } else {
        this.errorMessages = [];
        this.save.emit(values);
      }

    }
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
}
