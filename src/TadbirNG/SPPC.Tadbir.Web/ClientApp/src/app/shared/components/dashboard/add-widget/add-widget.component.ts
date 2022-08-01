import { Component, EventEmitter, OnInit, Output } from "@angular/core";
import { Widget } from "@sppc/shared/models/widget";

@Component({
  selector: "add-widget",
  templateUrl: "./add-widget.component.html",
  styleUrls: ["./add-widget.component.css"],
})
export class AddWidgetComponent implements OnInit {
  constructor() {}
  selectedWidgets: Widget[];
  selectedId;
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  ngOnInit() {}

  activate(id: number) {
    this.selectedId = id;
  }

  onSave(e: any): void {
    e.preventDefault();
    if (this.selectedId) {
      const index = this.selectedWidgets.findIndex(
        (w) => w.id === this.selectedId
      );

      this.selectedWidgets[index].selected = true;
      this.save.emit({
        widget: this.selectedWidgets[index],
        widgetList: this.selectedWidgets,
      });
    }
  }

  onCancel(e: any): void {
    this.cancel.emit();
  }
}
