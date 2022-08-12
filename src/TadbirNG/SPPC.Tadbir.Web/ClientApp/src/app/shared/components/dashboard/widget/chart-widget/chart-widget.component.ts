import { Component, Input, OnInit, ViewChild } from "@angular/core";
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

  @ViewChild("chart") chart: UIChart;

  constructor() {}

  ngOnInit() {}

  changeType(chartType: string) {
    this.type = chartType;

    setTimeout(() => {
      this.chart.reinit();
    }, 10);
  }
}
