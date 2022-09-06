import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { Widget } from "@sppc/shared/models/widget";
import { DashboardService } from "@sppc/shared/services";

@Component({
  selector: "add-widget",
  templateUrl: "./add-widget.component.html",
  styleUrls: ["./add-widget.component.css"],
})
export class AddWidgetComponent implements OnInit {
  constructor(private dashboardService: DashboardService) {}
  selectedWidgets: Widget[];

  selectedId;
  widgets: Widget[];

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {
    this.dashboardService.getWidgetList().subscribe((widgetList: Widget[]) => {
      this.widgets = widgetList;
    });
  }

  widgetIsUsed(widgetId) {
    if (this.selectedWidgets)
      return (
        this.selectedWidgets.findIndex((w: any) => w.widgetId == widgetId) >= 0
      );

    return false;
  }

  activate(id: number) {
    this.selectedId = id;
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
