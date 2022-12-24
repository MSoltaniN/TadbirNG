import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { RowArgs, RowClassArgs } from "@progress/kendo-angular-grid";
import { Widget } from "@sppc/shared/models/widget";
import { BrowserStorageService, DashboardService } from "@sppc/shared/services";

@Component({
  selector: "add-widget",
  templateUrl: "./add-widget.component.html",
  styleUrls: ["./add-widget.component.css"],
})
export class AddWidgetComponent implements OnInit {
  constructor(private dashboardService: DashboardService,public bStorageService: BrowserStorageService) {}
  @Input() selectedWidgets: Widget[];

  selectedId;
  widgets: Widget[];
  selectedKeys: any[];
  viewId: number;

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  public get CurrentLanguage(): string {
    var lang: string = "fa";

    if (this.bStorageService.getLanguage() != null) {
      var item: string | null;
      item = this.bStorageService.getLanguage();

      if (item) lang = item;
    }

    return lang;
  }

  ngOnInit() {
    this.dashboardService.getWidgetList().subscribe((widgetList: Widget[]) => {
      this.widgets = widgetList;
    });
    this.selectionToggleClass = this.selectionToggleClass.bind(this);
    this.viewId = 68; // !!! Hard-coded Value !!! TODO: Fix undeclared variable in html file, line 20
  }

  widgetIsUsed(widgetId) {
    if (this.selectedWidgets)
      return (
        this.selectedWidgets.findIndex((w: any) => w.widgetId == widgetId) >= 0
      );

    return false;
  }

  selectionToggleClass(context:RowClassArgs){
    let isDisabled = false;
    if (this.selectedWidgets)
      isDisabled = this.selectedWidgets.findIndex((w: any) => w.widgetId == context.dataItem.id) >= 0
    return { 'k-disabled' : isDisabled };
  }

  activate(id: number) {
    this.selectedId = id;
  }

  getSelectedRow(item: RowArgs) {
    return item.dataItem.id;
  }

  onSelectedKeyChange(keys) {
    this.selectedId = keys[0];
  }

  onSave(e: any): void {
    e.preventDefault();
    if (this.selectedId) {
      const index = this.widgets.findIndex((w) => w.id === this.selectedId);

      this.save.emit({
        widget: this.widgets[index],
        widgetList: this.widgets,
      });
    }
  }

  onCancel(e: any): void {
    this.cancel.emit();
  }
}
