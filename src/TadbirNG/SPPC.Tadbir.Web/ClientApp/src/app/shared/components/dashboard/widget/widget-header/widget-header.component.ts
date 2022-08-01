import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";

@Component({
  selector: "widget-header",
  templateUrl: "./widget-header.component.html",
  styleUrls: ["./widget-header.component.css"],
})
export class WidgetHeaderComponent implements OnInit {
  @Input() title: string;
  @Input() editMode: boolean;

  @Output() closeClick: EventEmitter<number> = new EventEmitter();

  constructor() {}

  onClose() {
    debugger;
    this.closeClick.emit();
  }

  ngOnInit() {}
}
