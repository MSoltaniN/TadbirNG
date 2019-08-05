
import { Directive, Host, HostListener, ElementRef } from "@angular/core";
import { GridComponent, ColumnComponent} from "@progress/kendo-angular-grid";
import { ToastrService } from "ngx-toastr";
import { DefaultComponent, BaseComponent, BrowserStorageService } from "@sppc/shared";
import { SettingService } from "@sppc/config/service";


@Directive({
  selector: '[sppc-auto-generated-grid-reorder]',
  providers: [String, DefaultComponent]
})

export class SppcAutoGeneratedGridReorder extends BaseComponent {

  size: string = this.screenSize;

  constructor(@Host() private grid: GridComponent, private elRef: ElementRef, public settingService: SettingService, public toastrService: ToastrService,
    @Host() public defaultComponent: DefaultComponent, public bStorageService: BrowserStorageService) {
    super(toastrService, bStorageService)
  }

  @HostListener('columnReorder', ['$event']) columnReorder(event: any) {
    this.reorderAndSave(event);
  }

  ngOnInit() {
    this.grid.reorderable = true;
  }

  /**
   * این متد چینش مربوط به ستون های گرید را در آرایه مرتب میکند و در حافظه مرورگر ذخیره میکند 
   * @param event
   */
  private reorderAndSave(event: any) {

    var gridColumn = this.grid.leafColumns.toArray();
    var destinationColumnName = (<ColumnComponent>gridColumn[event.newIndex]).field

    var sourceColumnName = event.column.field;

    var viewId: number = parseInt(this.elRef.nativeElement.id);
    var currentSetting = this.settingService.getSettingByViewId(viewId);

    if (currentSetting) {
      var sourceColumn = currentSetting.columnViews.find(f => f.name.toLowerCase() == sourceColumnName.toLowerCase());
      var sourceColumnSetting = sourceColumn[this.size];
      var sourceIndex = sourceColumnSetting.index;

      var destinationColumn = currentSetting.columnViews.find(f => f.name.toLowerCase() == destinationColumnName.toLowerCase());
      var destinationIndex = destinationColumn[this.size].index;

      if (destinationIndex - sourceIndex < 0) {

        currentSetting.columnViews.forEach(item => {
          var columnItem = item[this.size];

          if (columnItem.index >= destinationIndex && columnItem.index < sourceIndex) {
            columnItem.index++;

            var itemIndex = currentSetting.columnViews.indexOf(item);
            currentSetting.columnViews[itemIndex][this.size] = columnItem;
          }

        })

        sourceColumnSetting.index = destinationIndex;
        var itemIndex = currentSetting.columnViews.indexOf(sourceColumn);
        currentSetting.columnViews[itemIndex][this.size] = sourceColumnSetting;

      }
      else
        if (destinationIndex - sourceIndex > 0) {

          currentSetting.columnViews.forEach(item => {
            var columnItem = item[this.size];

            if (columnItem.index > sourceIndex && columnItem.index <= destinationIndex) {
              columnItem.index--;

              var itemIndex = currentSetting.columnViews.indexOf(item);
              currentSetting.columnViews[itemIndex][this.size] = columnItem;
            }

          })

          sourceColumnSetting.index = destinationIndex;
          var itemIndex = currentSetting.columnViews.indexOf(sourceColumn);
          currentSetting.columnViews[itemIndex][this.size] = sourceColumnSetting;

        }
    }

    if (currentSetting)
      this.settingService.setSettingByViewId(viewId, currentSetting);

  }

}
