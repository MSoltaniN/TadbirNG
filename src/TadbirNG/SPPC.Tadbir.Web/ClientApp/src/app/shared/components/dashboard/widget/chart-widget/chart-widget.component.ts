import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "chart-widget",
  templateUrl: "./chart-widget.component.html",
  styleUrls: ["./chart-widget.component.css"],
})
export class ChartWidgetComponent implements OnInit {
  @Input() type: string;
  @Input() data;
  @Input() options;

  constructor() {}

  ngOnInit() {}
}
