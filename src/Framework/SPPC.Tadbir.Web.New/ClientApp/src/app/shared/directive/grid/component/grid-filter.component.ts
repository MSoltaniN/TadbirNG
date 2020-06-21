
import { OnInit, OnDestroy, Component, Host, ElementRef, Input, EventEmitter, Output } from "@angular/core";

import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { GridComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { Layout } from "@sppc/env/environment";
import { BrowserStorageService } from "@sppc/shared/services";
import { SettingService } from "@sppc/config/service";
import { BaseComponent } from "@sppc/shared/class";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'grid-filter',
  templateUrl: './grid-filter.component.html',
  styleUrls: ['./grid-filter.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class GridFilterComponent extends BaseComponent implements OnInit, OnDestroy {

  rtl: boolean;
  @Input() public showClearFilter: number = 0;
  @Input() public parentComponent: any;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService,
    @Host() private grid: GridComponent, private elRef: ElementRef, public bStorageService: BrowserStorageService) {

    super(toastrService, bStorageService);
    if (this.grid.filter)
      this.grid.filter.filters = [];
  }

  ngOnDestroy(): void {

  }

  ngOnInit(): void {
    if (this.CurrentLanguage == 'fa')
      this.rtl = true;
    else
      this.rtl = false;

    var self = this;

    //document.addEventListener('keydown', function (ev: KeyboardEvent) {
    //    if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
    //        self.parentComponent.reloadGrid();
    //    }
    //});



    document.addEventListener('keydown', function (ev: KeyboardEvent) {
      if (ev.srcElement.hasAttribute('kendofilterinput') && ev.key == 'Enter') {
        setTimeout(() => {
          self.parentComponent.reloadGrid();
        }, 300);
        
      }
    });

  }

  @Output() reloadEvent = new EventEmitter();

  filterGridEmit(): void {

    this.reloadEvent.emit();
  }

  filterGrid(): void {

    this.showClearFilter = this.grid.filter.filters.length;

    this.parentComponent.reloadGrid();
  }

  removeFilterGrid(): void {
    this.grid.filter.filters = [];
    this.showClearFilter = this.grid.filter.filters.length;

    this.parentComponent.filterChange(this.grid.filter);
    this.parentComponent.reloadGrid();

  }

}
