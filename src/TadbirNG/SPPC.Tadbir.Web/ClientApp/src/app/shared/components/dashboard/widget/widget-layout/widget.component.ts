import {
  Component,
  ContentChild,
  EventEmitter,
  Input,
  OnInit,
  Output,
} from "@angular/core";
import { ChartWidgetComponent } from "../chart-widget/chart-widget.component";

@Component({
  selector: "widget",
  templateUrl: "./widget.component.html",
  styleUrls: ["./widget.component.css"],
})
export class WidgetComponent implements OnInit {
  @Input() headerTitle: string;
  @Input() widgetId: string;
  @Input() isEditMode: boolean;

  @Output() settingClick: EventEmitter<any> = new EventEmitter();
  @Output() closeWidget: EventEmitter<any> = new EventEmitter();

  @ContentChild(ChartWidgetComponent) chart: ChartWidgetComponent;

  constructor() {}

  onCloseWidget() {
    this.closeWidget.emit(this.widgetId);
  }

  onSettingChange(setting) {
    this.chart.changeType(setting.chartType);
  }

  ngOnInit() {}
}
