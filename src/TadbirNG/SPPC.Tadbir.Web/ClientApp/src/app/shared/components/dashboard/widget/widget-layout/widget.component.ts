import { Component, Input, OnInit } from "@angular/core";

@Component({
  selector: "widget",
  templateUrl: "./widget.component.html",
  styleUrls: ["./widget.component.css"],
})
export class WidgetComponent implements OnInit {
  @Input() headerTitle: string;
  @Input() isEditMode: boolean;

  constructor() {}

  ngOnInit() {}
}
