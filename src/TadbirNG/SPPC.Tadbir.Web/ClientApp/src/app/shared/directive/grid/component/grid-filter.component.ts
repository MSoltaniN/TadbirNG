import {
  Component,
  ElementRef,
  EventEmitter,
  Host,
  HostListener,
  Input,
  OnDestroy,
  OnInit,
  Output,
  Renderer2,
} from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { GridComponent } from "@progress/kendo-angular-grid";
import { RTL } from "@progress/kendo-angular-l10n";
import { SettingService } from "@sppc/config/service";
import { BaseComponent } from "@sppc/shared/class";
import { ReloadOption } from "@sppc/shared/class/reload-option";
import { ReloadStatusType } from "@sppc/shared/enum";
import { Layout } from "@sppc/shared/enum/metadata";
import { BrowserStorageService } from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: "grid-filter",
  templateUrl: "./grid-filter.component.html",
  styleUrls: ["./grid-filter.component.css"],
  providers: [
    {
      provide: RTL,
      useFactory: getLayoutModule,
      deps: [Layout],
    },
  ],
})
export class GridFilterComponent
  extends BaseComponent
  implements OnInit, OnDestroy
{
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

  constructor(
    public toastrService: ToastrService,
    public translate: TranslateService,
    public settingService: SettingService,
    @Host() private grid: GridComponent,
    private elRef: ElementRef,
    public bStorageService: BrowserStorageService,
    private renderer: Renderer2
  ) {
    super(toastrService, bStorageService);
    if (this.grid.filter) this.grid.filter.filters = [];
  }

  ngOnDestroy(): void {}

  //@HostListener('window:keydown', ['$event'])
  //keyEvent(event: KeyboardEvent) {
  //  console.log(event);
  //}

  @HostListener("document:keydown", ["$event"])
  handleClick(event: KeyboardEvent) {
    var parent = this.grid.wrapper.nativeElement.offsetParent;
    if (parent != null && parent.contains(event.target)) {
      if (event.key == "Enter") {
        console.log("enter pressed");
        var element: any = event.target;
        element.value = element.value.replaceBadChars(element.value);

        var filterInput = false;
        var element: any = event.srcElement;
        var object = element;
        if (
          element.hasAttribute("kendofilterinput") ||
          element.classList.contains("dp-picker-input")
        ) {
          filterInput = true;
        } else {
          var level = 5;
          while (object.offsetParent && level > 0) {
            object = object.offsetParent;
            level = level - 1;
            if (
              element.hasAttribute("kendofilterinput") ||
              object.hasAttribute("kendofilterinput")
            ) {
              filterInput = true;
              break;
            }
          }
        }

        if (filterInput) {
          event.stopPropagation();
          setTimeout(() => {
            var reloadOption = new ReloadOption();
            reloadOption.Status = ReloadStatusType.AfterFilter;

            this.parentComponent.reloadGrid(reloadOption);
          }, 300);
        }
      }
    }
  }

  ngOnInit(): void {
    if (this.CurrentLanguage == "fa") this.rtl = true;
    else this.rtl = false;

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

    this.parentComponent.currentFilter = undefined;
    this.parentComponent.state.filter = {
      logic: "and",
      filters: [],
    };

    this.parentComponent.reloadGrid();
  }

  removeFilterGridOnly(): void {
    this.grid.filter.filters = [];
    this.showClearFilter = this.grid.filter.filters.length;

    this.parentComponent.filterChange(this.grid.filter);
  }

  clearFilterGrid(): void {
    this.grid.filter.filters = [];
    this.showClearFilter = this.grid.filter.filters.length;
  }
}
