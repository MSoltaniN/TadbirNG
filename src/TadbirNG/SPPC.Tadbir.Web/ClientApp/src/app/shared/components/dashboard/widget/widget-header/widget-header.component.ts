import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "widget-header",
  templateUrl: "./widget-header.component.html",
  styleUrls: ["./widget-header.component.css"],
})
export class WidgetHeaderComponent implements OnInit {
  @Input() title: string;
  @Input() editMode: boolean;

  constructor() {}

  ngOnInit() {}
}
