import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { WidgetSettingComponent } from "../widget-setting/widget-setting.component";

@Component({
  selector: "widget-header",
  templateUrl: "./widget-header.component.html",
  styleUrls: ["./widget-header.component.css"],
})
export class WidgetHeaderComponent implements OnInit {
  @Input() title: string;
  @Input() editMode: boolean;
  @Input() widgetId: number;

  dialogRef;
  dialogModel;
  settingTitle: string;

  @Output() closeClick: EventEmitter<number> = new EventEmitter();
  @Output() settingChange: EventEmitter<any> = new EventEmitter();

  constructor(
    private dialogService: DialogService,
    private translateService: TranslateService
  ) {}

  onClose() {
    debugger;
    this.closeClick.emit();
  }

  onSettingClick() {
    this.dialogRef = this.dialogService.open({
      title: this.settingTitle,
      content: WidgetSettingComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.widgetId = this.widgetId;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.settingChange.emit(res);
      this.dialogRef.close();
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  ngOnInit() {
    this.translateService
      .get("Dashboard.WidgetSetting")
      .subscribe((text: string) => {
        this.settingTitle = text;
      });
  }
}
