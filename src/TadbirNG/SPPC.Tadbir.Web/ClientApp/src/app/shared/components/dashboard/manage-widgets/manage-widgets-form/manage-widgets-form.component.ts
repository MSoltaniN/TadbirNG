import {
  Component,
  ElementRef,
  EventEmitter,
  Input,
  OnInit,
  Output,
  Renderer2,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { FullAccount } from "@sppc/finance/models";
import { CurrencyService } from "@sppc/finance/service";
import { DetailComponent } from "@sppc/shared/class";
import { Entities } from "@sppc/shared/enum/metadata";
import { Widget } from "@sppc/shared/models/widget";
import { WidgetFunction } from "@sppc/shared/models/widgetFunction";
import { WidgetType } from "@sppc/shared/models/widgetType";
import { ViewName } from "@sppc/shared/security";
import {
  BrowserStorageService,
  LookupService,
  MetaDataService,
} from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import { map } from "rxjs/operators";
import { WidgetService } from "../../services/widget.service";

@Component({
  selector: "app-manage-widgets-form",
  templateUrl: "./manage-widgets-form.component.html",
  styleUrls: ["./manage-widgets-form.component.css"],
})
export class ManageWidgetsFormComponent
  extends DetailComponent
  implements OnInit
{
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
    private widgetService: WidgetService
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
  @Input() public errorMessage: string = "";

  @Input() public isWizard: boolean = false;

  @Input() functionsList: WidgetFunction[];
  @Input() typesList: WidgetType[];

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<Widget> = new EventEmitter();

  private dialogRef: DialogRef;

  widgetAccounts: FullAccount[] = [];
  selectedType: any;
  selectedFunction: any;

  @Output() setFocus: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    setTimeout(() => {
      this.editForm.reset(this.model);
      if (this.isNew) {
        this.editForm.patchValue({
          createdById: this.widgetService.UserId,
          createdByFullName: this.widgetService.UserName,
        });
      } else {
        this.editForm.patchValue({
          createdByFullName: "null",
        });
      }
      console.log(this.model);

      this.widgetAccounts = this.model.accounts;
    });
  }

  focusHandler(event) {
    console.log(event);
  }

  setWidgetAccounts(event) {
    this.widgetAccounts = event;
  }

  onChangeFunction(id) {
    let functionName = this.functionsList.find((item) => item.id == id).name;
    this.editForm.patchValue({
      functionName: functionName,
    });
  }

  onChangeType(id) {
    let typeName = this.typesList.find((item) => item.id == id).name;
    this.editForm.patchValue({
      typeName: typeName,
    });
  }

  public onSave(e: any): void {
    e.preventDefault();
    if (this.editForm.valid) {
      let values = this.editForm.value;
      values.Accounts = this.widgetAccounts;
      if (this.isNew) {
        values.id = 0;
        values.defaultSettings = '{"x":0,"y":0,"width":10,"height":8}';
      } else {
        values.defaultSettings = this.model.defaultSettings;
      }

      this.save.emit(values);
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
