import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { StatusFilterTypeKey, StatusFilterTypeValue } from '@sppc/shared/enum/quickFilterTypes';
import { Item } from '@sppc/shared/models';

@Component({
  selector: 'quick-filters',
  templateUrl: './quick-filters.component.html',
  styleUrls: ['./quick-filters.component.css']
})
export class QuickFiltersComponent implements OnInit {

  constructor() { }

  @Input() parentComponent: any;
  
  // Status Filters
  @Input() statusFilter = false;
  getDataUrl: string;
  statusFilters: Item[] = [
    {key: StatusFilterTypeKey.Active, value: StatusFilterTypeValue.Active},
    {key: StatusFilterTypeKey.Inactive, value: StatusFilterTypeValue.Inactive},
    {key: StatusFilterTypeKey.All, value: StatusFilterTypeValue.All},
  ]
  selectedStatusFilter = StatusFilterTypeValue.Active;
  @Output() changeStatus: EventEmitter<StatusFilterTypeValue> = new EventEmitter();

  ngOnInit(): void {
    this.getDataUrl = this.parentComponent.getDataUrl;    
    console.log('onChangeStatusFilter',this.getDataUrl);
  }

  onChangeFilterByStatusDropDown(e) {
    switch (e) {
      case StatusFilterTypeValue.Active:
        this.parentComponent.getDataUrl = this.getDataUrl;
        break;

      case StatusFilterTypeValue.Inactive:
        this.parentComponent.getDataUrl = this.getDataUrl + "/inactive";
        break;

      case StatusFilterTypeValue.All:
        this.parentComponent.getDataUrl = this.getDataUrl + "/all";
        break;

      default:
        break;
    }

    this.parentComponent.reloadGrid();
    // this.changeStatus.emit(e);
  }

}
