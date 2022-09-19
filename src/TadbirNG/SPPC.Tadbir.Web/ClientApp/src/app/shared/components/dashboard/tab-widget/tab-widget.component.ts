import { Component, EventEmitter, OnInit, Output } from "@angular/core";

@Component({
  selector: "tab-widget",
  templateUrl: "./tab-widget.component.html",
  styleUrls: ["./tab-widget.component.css"],
})
export class TabWidgetComponent implements OnInit {
  tabName: string;
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<any> = new EventEmitter();

  constructor() {}

  ngOnInit() {}

  onSave(event) {
    this.save.emit(this.tabName);
  }

  onCancel(event) {
    this.cancel.emit();
  }
}
