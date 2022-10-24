import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  OnChanges,
  OnInit,
  Output,
  SimpleChanges,
} from "@angular/core";

import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { ChartWidgetComponent } from "../chart-widget/chart-widget.component";

@Component({
  selector: "widget",
  templateUrl: "./widget.component.html",
  styleUrls: ["./widget.component.css"],
})
export class WidgetComponent implements OnInit, OnChanges {
  @Input() headerTitle: string;
  @Input() widgetId: string;
  @Input() isEditMode: boolean;

  @Output() settingClick: EventEmitter<any> = new EventEmitter();
  @Output() closeWidget: EventEmitter<any> = new EventEmitter();

  @Input() setting: WidgetSetting;

  @ContentChild(ChartWidgetComponent) chart: ChartWidgetComponent;

  constructor() {}
  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }

  onCloseWidget() {
    this.closeWidget.emit(this.widgetId);
  }

  onSettingChange(changedSetting: WidgetSetting) {   
    this.chart.changeSettings(changedSetting);
  }

  ngOnInit() {}
}
