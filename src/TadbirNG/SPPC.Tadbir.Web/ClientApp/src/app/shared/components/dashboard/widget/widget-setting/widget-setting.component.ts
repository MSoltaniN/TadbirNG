import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
  selector: "app-widget-setting",
  templateUrl: "./widget-setting.component.html",
  styleUrls: ["./widget-setting.component.css"],
})
export class WidgetSettingComponent implements OnInit {
  constructor() {}

  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  @Input() chartType;
  widgetId: number;

  chartTypes: Array<{ text: string; value: string }> = [
    { text: "ستونی", value: "bar" },
    { text: "خطی", value: "line" },
  ];

  ngOnInit() {
    //if (this.chartType > 0) this.typeSelected = this.chartType;
  }

  onSave(e: any): void {
    this.save.emit({
      chartType: this.chartType.value,
    });
  }

  onCancel(e: any): void {
    this.cancel.emit();
  }
}
