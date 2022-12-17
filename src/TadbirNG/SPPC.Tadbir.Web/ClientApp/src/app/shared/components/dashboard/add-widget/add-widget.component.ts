import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { RowArgs, RowClassArgs } from "@progress/kendo-angular-grid";
import { Widget } from "@sppc/shared/models/widget";
import { DashboardService } from "@sppc/shared/services";

@Component({
  selector: "add-widget",
  templateUrl: "./add-widget.component.html",
  styleUrls: ["./add-widget.component.css"],
})
export class AddWidgetComponent implements OnInit {
  constructor(private dashboardService: DashboardService) {}
  @Input() selectedWidgets: Widget[];

  selectedId;
  widgets: Widget[];
  selectedKeys: any[];

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.dashboardService.getWidgetList().subscribe((widgetList: Widget[]) => {
      this.widgets = widgetList;
    });
    this.selectionToggleClass = this.selectionToggleClass.bind(this);
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
