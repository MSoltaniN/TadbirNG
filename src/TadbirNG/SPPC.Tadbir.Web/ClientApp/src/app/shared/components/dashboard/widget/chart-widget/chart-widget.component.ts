import {
  Component,
  EventEmitter,
  Input,
  OnInit,
  Output,
  ViewChild,
} from "@angular/core";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { ChartService } from "@sppc/shared/services/widget.service";
import { UIChart } from "primeng/chart";

@Component({
  selector: "chart-widget",
  templateUrl: "./chart-widget.component.html",
  styleUrls: ["./chart-widget.component.css"],
})
export class ChartWidgetComponent implements OnInit {
  @Input() type: string = "bar";
  @Input() data;
  @Input() options;
  @Input() title;

  @ViewChild("chart") chart: UIChart;

  constructor(private chartService: ChartService) {}

  ngOnInit() {}

  changeSettings(settings: WidgetSetting) {
    this.data = this.chartService.applyChartSetting(settings, this.data);
    this.type = this.chartService.getAdjustedChartType(settings);

    setTimeout(() => {
      this.chart.reinit();
    }, 10);
  }
}
