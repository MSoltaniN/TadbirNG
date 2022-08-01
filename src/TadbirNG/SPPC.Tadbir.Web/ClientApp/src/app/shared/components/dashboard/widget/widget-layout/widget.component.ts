import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
  selector: "widget",
  templateUrl: "./widget.component.html",
  styleUrls: ["./widget.component.css"],
})
export class WidgetComponent implements OnInit {
  @Input() headerTitle: string;
  @Input() widgetId: string;
  @Input() isEditMode: boolean;

  @Output() closeWidget: EventEmitter<any> = new EventEmitter();

  constructor() {}

  onCloseWidget() {
    this.closeWidget.emit(this.widgetId);
  }

  ngOnInit() {}
}
