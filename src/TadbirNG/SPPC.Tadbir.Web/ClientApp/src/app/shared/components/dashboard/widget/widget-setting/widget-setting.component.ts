import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";

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

  @Input() setting: WidgetSetting;

  chartTitle: string;
  chartColors = new WidgetSetting().Colors;

  chartTypes: Array<{ text: string; value: string }> = [
    { text: "نمودار ستونی", value: "1" },
    { text: "نمودار میله ای", value: "2" },
    { text: "نمودار خطی", value: "3" },
  ];

  ngOnInit() {
    //if (this.chartType > 0) this.typeSelected = this.chartType;
    debugger;
    this.chartTitle = this.setting.title;
  }

  onSave(e: any): void {
    this.save.emit(this.setting);
  }

  onChangeChartTypes(item: any, index: number) {
    this.setting.series[index].type = item;
  }

  onChangeColor(item: any, index: number) {
    this.setting.series[index].backgroundColor = item;
  }

  onCancel(e: any): void {
    this.cancel.emit();
  }
}
