import { Layout } from "../../../environments/environment";
import { Component, OnInit, Renderer2, NgZone, ChangeDetectorRef, ViewEncapsulation, ViewChild, OnChanges } from '@angular/core';
import { GridComponent, DataStateChangeEvent, RowClassArgs } from "@progress/kendo-angular-grid";
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { AutoGeneratedGridComponent } from "../../class/autoGeneratedGrid.component";
import { GridService, SettingService } from "../../service/index";
import { MetaDataService } from "../../service/metadata/metadata.service";
import { QuickReportColumnInfo, QuickReportViewInfo } from "../../service/report/reporting.service";
import { TranslateService } from '@ngx-translate/core';
import { Property } from "../../class/metadata/property";
import { ReportManagementComponent } from "./reportManagement.component";
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { ColumnViewConfig } from "../../model/columnViewConfig";
import { ColumnViewDeviceConfig } from "../../model/columnViewDeviceConfig";
import { BrowserStorageService } from "../../service/browserStorage.service";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}
@Component({
  encapsulation: ViewEncapsulation.None,
  selector: 'report-setting',
  templateUrl: './QuickReport-Setting.component.html',
  styleUrls: ['./QuickReport-Setting.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class QuickReportSettingComponent extends AutoGeneratedGridComponent{

  @ViewChild('grdQRSetting') grdQRSetting: GridComponent;
  //gridColumnsRow: Property[] = [];
  active: boolean;
  dialogWidth: any;
  reportManager: ReportManagementComponent
  viewInfo: QuickReportViewInfo;
  viewData: any = {
    columns: []
  };

  gridWidth: number;

  currentViewId: number;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public bStorageService: BrowserStorageService,
    public settingService: SettingService, public ngZone: NgZone) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }


  dataStateChange(state: DataStateChangeEvent): void {

  }

  showReportSetting(gridColumns: Property[], entityTypeName: string, viwId: number, rManager: ReportManagementComponent) {
    this.entityName = entityTypeName;
    this.viewId = viwId;
    this.reportManager = rManager;
    this.active = true;

    var loadDBSetting = false;
    if (loadDBSetting) {

    }
    else {

      this.createSettingFromBaseGrid(gridColumns, viwId);

    }

    this.dialogWidth = innerWidth;
  }

  closeDialog() {
    this.active = false;

    this.viewInfo = null;

  }

  createSettingFromBaseGrid(gridColumns: Property[], viwId: number) {

    this.currentViewId = viwId;
    var viwString = JSON.stringify(this.viewInfo);
    var jsonString = localStorage.getItem("s" + this.currentViewId.toString() + this.currentlang);

    if (jsonString) {
      var saveViewInfo = <QuickReportViewInfo>JSON.parse(jsonString);
      this.viewInfo = saveViewInfo;

      //load grid row 0
      this.viewData.columns = [];

      var jsonData = {};
      this.viewInfo.columns.forEach((col) => {
        var columnName = col.name;
        jsonData[columnName] = col.visible;
      });

      this.viewData.columns.push(jsonData);

      this.computeWidthAndFixGrid();
    }
    else {
      this.getAllMetaDataByViewIdAsync(viwId).then(response => {

        if (response) {
          var propeties = response;


          var columnIndex = 0;
          var thArray = this.reportManager.Grid.wrapper.nativeElement.getElementsByTagName('TH');
          var columns: Array<QuickReportColumnInfo> = new Array<QuickReportColumnInfo>();
          this.reportManager.Grid.leafColumns.forEach(function (item) {
            var qr: QuickReportColumnInfo = new QuickReportColumnInfo();
            var column = item as ColumnComponent;
            if (column.field) {
              qr.name = column.field;
              qr.index = columnIndex;

              if (column.width)
                qr.width = column.width;
              else
                qr.width = thArray[columnIndex].offsetWidth;

              qr.userText = column.displayTitle;
              qr.sortOrder = 0;
              qr.sortMode = 0;

              var property = propeties.filter(p => p.name.toLowerCase() === column.field.toLowerCase());
              if (property.length > 0)
                qr.dataType = property[0].dotNetType;
              qr.visible = true;
              qr.defaultText = column.displayTitle;
              qr.enabled = true;
              qr.order = columnIndex;
              qr.type = property[0].storageType;
              columns.push(qr);

              columnIndex++;
            }
          });

          propeties.forEach((property) => {
            var findex = columns.findIndex(f => f.name.toLowerCase() === property.name.toLowerCase());
            if (findex < 0) {
              var qr: QuickReportColumnInfo = new QuickReportColumnInfo();

              qr.name = property.name;
              qr.index = columnIndex;
              qr.visible = true;

              qr.sortOrder = 0;
              qr.sortMode = 0;

              var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
              var arrayItem = <ColumnViewConfig>JSON.parse(property.settings);
              var columnViewDeviceConfig = <ColumnViewDeviceConfig>this.settingService.getCurrentColumnViewConfig(arrayItem);

              qr.userText = columnViewDeviceConfig.title;
              qr.dataType = property.dotNetType;

              qr.width = columnViewDeviceConfig.width;

              qr.visible = false;
              qr.defaultText = columnViewDeviceConfig.title;
              qr.enabled = true;
              qr.order = columnIndex;
              qr.type = property.storageType;
              columns.push(qr)

              columnIndex++;
            }
          });

          var dpi_x = document.getElementById('dpi').offsetWidth;
          this.viewInfo = new QuickReportViewInfo();
          this.viewInfo.columns = columns;
          this.viewInfo.inchValue = dpi_x;
          this.viewInfo.reportLang = this.CurrentLanguage;

          //load grid row 0
          this.viewData.columns = [];

          var jsonData = {};
          this.viewInfo.columns.forEach((col) => {
            var columnName = col.name;
            jsonData[columnName] = col.visible;
          });

          this.viewData.columns.push(jsonData);

          this.computeWidthAndFixGrid();
        }
      });
    }




  }

  rowCallback(context: RowClassArgs) {


    var input = <any>document.querySelector('#hdnGridWidth');
    if (input.value != '') {
      var width = input.value;
      var tableWrap = document.querySelector('kendo-grid#grdQRSetting > div > kendo-grid-list > div > div.k-grid-table-wrap');
      var innerTable = document.querySelector('kendo-grid#grdQRSetting > div > div > div > table');
      this.renderer.setStyle(tableWrap, 'width', width.toString() + 'px');
      this.renderer.setStyle(innerTable, 'width', width.toString() + 'px');
      this.renderer.setStyle(tableWrap, 'overflow: ', 'hidden');

      input.value = '';
    }
    //this.renderer.setStyle(grid, 'width: ', '100%');
  }

  computeWidthAndFixGrid() {

    this.gridWidth = 0;
    this.viewInfo.columns.forEach((col) => {
      this.gridWidth = this.gridWidth + col.width;
    });

    var input = <any>document.querySelector('#hdnGridWidth');
    input.value = this.gridWidth;
  }

  switchChange(checked: any, column: any) {
    var index = this.viewInfo.columns.findIndex(f => f.name === column.field);
    if (index > 0) {
      this.viewInfo.columns[index].visible = checked;
    }
  }

  gridColumnResized(event: any) {
    //column: any, newWidth: number, oldWidth: number
    //var index = this.viewInfo.columns.findIndex(f => f.name === event[0].column.field);
    //if (index > 0) {
    //  this.viewInfo.columns[index].width = event[0].newWidth;
    //}
    //this.grdQRSetting.wrapper.find("table").width(1000);

    var wd = 0;
    this.viewInfo.columns.forEach((col) => {
      wd = wd + col.width;
    });
    this.gridWidth = wd;

  }



  showReport() {


    this.active = false;
    //this.reportManager.Grid = this.grid;
    //update  viewInfo
    var newViewInfo: QuickReportViewInfo = new QuickReportViewInfo();
    newViewInfo.columns = new Array<QuickReportColumnInfo>();

    var i = 0;
    var thArray = this.grdQRSetting.wrapper.nativeElement.getElementsByTagName('TH');

    this.grdQRSetting.leafColumns.toArray().forEach((item, index, arr) => {

      var column = <ColumnComponent>item;
      var indexCol = this.viewInfo.columns.findIndex(f => f.name === column.field);
      var width = thArray[i].offsetWidth;

      this.viewInfo.columns[indexCol].width = width;
      this.viewInfo.columns[indexCol].order = i;

      newViewInfo.columns.push(this.viewInfo.columns[indexCol]);

      i++;
    });

    this.viewInfo.columns = newViewInfo.columns;
    //update  viewInfo

    //temp save viw info in localstoreage
    var viwString = JSON.stringify(this.viewInfo);
    localStorage.setItem("s" + this.currentViewId.toString() + this.currentlang, viwString);
    //temp save viw info in localstoreage



    this.reportManager.DecisionMakingForShowReport(this.viewInfo);

    this.viewInfo = null;
    this.viewData = {
      columns: []
    };;
  }

}
