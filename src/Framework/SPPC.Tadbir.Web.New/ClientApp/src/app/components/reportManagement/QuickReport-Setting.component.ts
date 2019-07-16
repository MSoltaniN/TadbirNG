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
import { QuickReportConfigInfo } from "../../model/QuickReportConfig";
import { QuickReportColumnConfigInfo } from "../../model/QuickReportColumnConfig";

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

export class QuickReportSettingComponent extends AutoGeneratedGridComponent  {
    
  @ViewChild('grdQRSetting') grdQRSetting: GridComponent;
  //gridColumnsRow: Property[] = [];
  active: boolean;
  dialogWidth: any;
  reportManager: ReportManagementComponent
  viewInfo: QuickReportConfigInfo;
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
    
    var userId = this.UserId;

    //temp code
    var viwString = JSON.stringify(this.viewInfo);
    var jsonString = localStorage.getItem("s" + viwId.toString() + this.currentlang + this.UserId);
    this.currentViewId = viwId;

    if (jsonString) {
      var response = <QuickReportConfigInfo>JSON.parse(jsonString);

      this.viewInfo = response;
      //load grid row 0
      this.viewData.columns = [];

      var jsonData = {};
      this.viewInfo.columns.forEach((col) => {
        var columnName = col.name;
        jsonData[columnName] = col.visible;
      });

      this.viewData.columns.push(jsonData);

      this.active = true;

      this.computeWidthAndFixGrid();

    }
    else {
      this.loadSettingFromParentGrid(viwId);
    }
    //temp code

    /*
    this.settingService.getQuickReportSettingsByUserAndView(userId, viwId)
      .subscribe((response: any) => {
        if (response) {

          this.viewInfo = response;
          //load grid row 0
          this.viewData.columns = [];

          var jsonData = {};
          this.viewInfo.columns.forEach((col) => {
            var columnName = col.name;
            jsonData[columnName] = col.visible;
          });

          this.viewData.columns.push(jsonData);

          this.active = true;

          this.computeWidthAndFixGrid();

        }       
        else {
          this.loadSettingFromParentGrid(viwId);
        }
      });   */
    
    this.dialogWidth = innerWidth;
  }

  closeDialog() {
    this.active = false;

    this.viewInfo = null;

  }

  loadGridSettings(gridColumns: Property[], viwId: number) {

    

    //var viwString = JSON.stringify(this.viewInfo);
    //var jsonString = localStorage.getItem("s" + this.currentViewId.toString() + this.currentlang);

   

    
    //this.settingService.getQuickReportSettingsByUserAndView(userId, viewId)
    //  .subscribe((response: any) => {
    //    if (response) {

    //      this.viewInfo = response;
    //      //load grid row 0
    //      this.viewData.columns = [];

    //      var jsonData = {};
    //      this.viewInfo.columns.forEach((col) => {
    //        var columnName = col.name;
    //        jsonData[columnName] = col.visible;
    //      });

    //      this.viewData.columns.push(jsonData);

    //      this.computeWidthAndFixGrid();
    //    }
    //    else {
    //      this.loadSettingFromParentGrid(viewId);
    //    }
    //  });
  }

  loadSettingFromParentGrid(viwId : number) {
    this.getAllMetaDataByViewIdAsync(viwId).then(response => {

      if (response) {
        var propeties = response;


        var columnIndex = 0;
        var thArray = this.reportManager.Grid.wrapper.nativeElement.getElementsByTagName('TH');
        var columns: Array<QuickReportColumnConfigInfo> = new Array<QuickReportColumnConfigInfo>();
        this.reportManager.Grid.leafColumns.forEach(function (item) {
          var qr: QuickReportColumnConfigInfo = new QuickReportColumnConfigInfo();
          var column = item as ColumnComponent;
          if (column.field) {
            qr.name = column.field;

            if (column.width)
              qr.width = column.width;
            else
              qr.width = thArray[columnIndex].offsetWidth;

            qr.userTitle = column.displayTitle;

            var property = propeties.filter(p => p.name.toLowerCase() === column.field.toLowerCase());
            if (property.length > 0)
              qr.dataType = property[0].dotNetType;
            qr.visible = true;
            qr.title = column.displayTitle;
            qr.displayIndex = columnIndex;

            if (property[0].storageType == "money")
              qr.type = property[0].storageType;
            else
              qr.type = property[0].scriptType;

            columns.push(qr);

            columnIndex++;
          }
        });

        propeties.forEach((property) => {
          var findex = columns.findIndex(f => f.name.toLowerCase() === property.name.toLowerCase());
          if (findex < 0) {
            var qr: QuickReportColumnConfigInfo = new QuickReportColumnConfigInfo();

            qr.name = property.name;           
            qr.visible = true;

            var columnViewDeviceConfig: ColumnViewDeviceConfig | undefined = undefined;
            var arrayItem = <ColumnViewConfig>JSON.parse(property.settings);
            var columnViewDeviceConfig = <ColumnViewDeviceConfig>this.settingService.getCurrentColumnViewConfig(arrayItem);

            qr.userTitle = columnViewDeviceConfig.title;
            qr.dataType = property.dotNetType;

            qr.width = columnViewDeviceConfig.width;

            qr.visible = false;
            qr.title = columnViewDeviceConfig.title;           
            qr.displayIndex = columnIndex;

            if (property.storageType == "money")
              qr.type = property.storageType;
            else
              qr.type = property.scriptType;

            columns.push(qr)

            columnIndex++;
          }
        });

        var dpi_x = document.getElementById('dpi').offsetWidth;
        this.viewInfo = new QuickReportConfigInfo();
        this.viewInfo.columns = columns;
        this.viewInfo.inchValue = dpi_x;        

        //load grid row 0
        this.viewData.columns = [];

        var jsonData = {};
        this.viewInfo.columns.forEach((col) => {
          var columnName = col.name;
          jsonData[columnName] = col.visible;
        });

        this.viewData.columns.push(jsonData);

        this.active = true;

        this.computeWidthAndFixGrid();
      }
    });
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
    if (index >= 0) {
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
    var newViewInfo: QuickReportConfigInfo = new QuickReportConfigInfo();
    newViewInfo.columns = new Array<QuickReportColumnConfigInfo>();

    var i = 0;
    var thArray = this.grdQRSetting.wrapper.nativeElement.getElementsByTagName('TH');

    this.grdQRSetting.leafColumns.toArray().forEach((item, index, arr) => {

      var column = <ColumnComponent>item;
      var indexCol = this.viewInfo.columns.findIndex(f => f.name === column.field);
      var width = thArray[i].offsetWidth;

      this.viewInfo.columns[indexCol].width = width;
      this.viewInfo.columns[indexCol].displayIndex = i;

      newViewInfo.columns.push(this.viewInfo.columns[indexCol]);

      i++;
    });

    this.viewInfo.columns = newViewInfo.columns;
    //update  viewInfo

    //temp save viw info in localstoreage
    var viwString = JSON.stringify(this.viewInfo);
    localStorage.setItem("s" + this.currentViewId.toString() + this.currentlang + this.UserId, viwString);
    //temp save viw info in localstoreage

    this.reportManager.DecisionMakingForShowReport(this.viewInfo);

    this.viewInfo = null;
    this.viewData = {
      columns: []
    };;
  }

}
