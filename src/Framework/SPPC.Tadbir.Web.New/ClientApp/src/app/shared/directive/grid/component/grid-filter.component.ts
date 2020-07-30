
import { OnInit, OnDestroy, Component, Host, ElementRef, Input, EventEmitter, Output, HostListener, Renderer2 } from "@angular/core";

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

  //@HostListener('document:keypress', ['$event'])
  //handleKeyboardEvent(event: KeyboardEvent) {
  //  if (event.key == 'Enter') {
  //    var filterInput = false;
  //    var element: any = event.srcElement;
  //    var object = element;
  //    if (element.hasAttribute('kendofilterinput')) {
  //      filterInput = true;
  //    }
  //    else {
  //      var level = 5;
  //      while (object.offsetParent && level > 0) {
  //        object = object.offsetParent;
  //        level = level - 1;
  //        if (element.hasAttribute('kendofilterinput') || object.hasAttribute('kendofilterinput')) {
  //          filterInput = true;
  //          break;
  //        }
  //      }
  //    }
      

  //    if (filterInput) {

  //      event.stopPropagation();
  //      setTimeout(() => {
  //        this.parentComponent.reloadGrid();
  //      }, 300);

  //    }

  //  }
  //}

  globalListenFunc: Function;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public settingService: SettingService,
    @Host() private grid: GridComponent, private elRef: ElementRef, public bStorageService: BrowserStorageService, private renderer: Renderer2) {

    super(toastrService, bStorageService);
    if (this.grid.filter)
      this.grid.filter.filters = [];
  }

  ngOnDestroy(): void {

  }

  //@HostListener('window:keydown', ['$event'])
  //keyEvent(event: KeyboardEvent) {
  //  console.log(event);
  //}

  @HostListener('document:keydown', ['$event'])
  handleClick(event: KeyboardEvent) {
    if (this.elRef.nativeElement.offsetParent.contains(event.target)) {
        if (event.key == 'Enter') {
          var filterInput = false;
          var element: any = event.srcElement;
          var object = element;
          if (element.hasAttribute('kendofilterinput')) {
            filterInput = true;
          }
          else {
            var level = 5;
            while (object.offsetParent && level > 0) {
              object = object.offsetParent;
              level = level - 1;
              if (element.hasAttribute('kendofilterinput') || object.hasAttribute('kendofilterinput')) {
                filterInput = true;
                break;
              }
            }
          }

          if (filterInput) {

            event.stopPropagation();
            setTimeout(() => {
              this.parentComponent.reloadGrid();
            }, 300);
          }
      }
    }
  }

  ngOnInit(): void {
    if (this.CurrentLanguage == 'fa')
      this.rtl = true;
    else
      this.rtl = false;    

    //const elements = <any>document.querySelectorAll('[kendofilterinput]');
    //elements.forEach(element => {

    //  this.globalListenFunc = this.renderer.listen(element, 'keypress', e => {
    //    console.log(e);
    //    if (e.key == 'Enter') {
    //      setTimeout(() => {
    //        this.parentComponent.reloadGrid();
    //      }, 300);
    //    }

    //  });
    //});
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
