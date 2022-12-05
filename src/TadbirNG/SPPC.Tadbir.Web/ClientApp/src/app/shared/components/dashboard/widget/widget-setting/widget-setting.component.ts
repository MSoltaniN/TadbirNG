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

  clonedSetting: WidgetSetting;

  @Input() set setting(value: WidgetSetting) {
    if (value) this.clonedSetting = JSON.parse(JSON.stringify(value));
  }

  chartTitle: string;
  chartColors = new WidgetSetting().Colors;

  chartTypes: Array<{ text: string; value: string }> = [
    { text: "نمودار ستونی", value: "1" },
    { text: "نمودار میله ای", value: "2" },
    { text: "نمودار خطی", value: "3" },
    { text: "نمودار دایره ای", value: "4" },
    { text: "گیج عقریه ای", value: "10" },
  ];

  ngOnInit() {
    if (this.clonedSetting.series.findIndex((s) => s.type == "4") >= 0) {
      this.pieColors = JSON.parse(
        JSON.stringify(this.clonedSetting.series[0].backgroundColor)
      );
    }

    this.chartTitle = this.clonedSetting.title;
  }

  onSave(e: any): void {
    // if (this.setting.series.findIndex((s) => s.type == "4") >= 0) {
    //   this.setting.series[0].backgroundColor = this.pieColors;
    // }
    this.clonedSetting.title = this.chartTitle;
    this.save.emit(this.clonedSetting);
  }

  onChangeChartTypes(item: any, index: number) {
    if (this.clonedSetting.series[index].type == "4" && item != "4") {
      this.clonedSetting.series[index].backgroundColor =
        this.clonedSetting.series[index].backgroundColor[0];
    }
    if (this.clonedSetting.series[index].type != "4" && item == "4") {
      this.clonedSetting.series[index].backgroundColor = JSON.parse(
        JSON.stringify(this.pieColors)
      );
    }
    this.clonedSetting.series[index].type = item;
  }

  // onChangeColor(item: any, index: number) {
  //   this.setting.series[index].backgroundColor = item;
  // }

  onCancel(e: any): void {
    this.cancel.emit();
  }

  pieColors: any[] = [];
}
