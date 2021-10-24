import { Component, OnInit, Renderer2, NgZone, ChangeDetectorRef, ViewEncapsulation, ViewChild, OnChanges } from '@angular/core';
import { GridComponent, DataStateChangeEvent, RowClassArgs } from "@progress/kendo-angular-grid";
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { TranslateService } from '@ngx-translate/core';
import { ColumnComponent } from "@progress/kendo-angular-grid";
import { Layout } from '@sppc/shared/enum/metadata';
import { MetaDataService, GridService, BrowserStorageService } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { AutoGeneratedGridComponent, Property } from '@sppc/shared/class';
import { QuickReportConfigInfo, QuickReportColumnConfigInfo, ColumnViewDeviceConfig, ColumnViewConfig } from '@sppc/shared/models';
import { ReportManagementComponent } from '@sppc/shared/components/reportManagement/reportManagement.component';
import { QuickReportPageSettingComponent } from '@sppc/shared/components/reportManagement/quick-report-page-setting.component';
import { ReportPageSetting } from '@sppc/shared/models/reportPageSetting';


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
 
  gridGroupColumnNames: any[] = [];
  currentViewId: number;
  createdGroupNames: Array<any> = [];

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
    this.createdGroupNames = new Array<any>();   
            
    var jsonString = this.bStorageService.getQuickReportSetting(viwId.toString(),this.UserId.toString());
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

          if (this.createdGroupNames.filter(c => c.groupName === col.groupName).length == 0 && col.groupName != "")
            this.createdGroupNames.push(col);
        });

        this.initPageSetting();
        this.viewData.columns.push(jsonData);

        this.active = true;

        this.computeWidthAndFixGrid();

      }
      else {
        if (!rManager.ViewIdentity.IsDynamicColumns) {
          this.gridGroupColumnNames = new Array<any>();
          this.loadSettingFromParentGrid(viwId);
        }
        else {
          this.loadSettingFromDynamicMetadata();
        }
    }
    this.dialogWidth = innerWidth;
  }    
  

  closeDialog() {
    this.active = false;
    this.viewInfo = null;
  }

  //** checkReprotSetting **//
  initPageSetting() {    
    if (!this.viewInfo.reportPageSetting) {
      this.viewInfo.reportPageSetting = new ReportPageSetting();
    }
  }

  loadGridSettings(gridColumns: Property[], viwId: number) {  

   
  }

  loadSettingFromParentGrid(viwId : number) {
    this.getAllMetaDataByViewIdAsync(viwId).then(response => {

      if (response) {
        var propeties = response;

        var columnIndex = 0;
        var thArray = this.reportManager.Grid.wrapper.nativeElement.getElementsByTagName('TH');
        var columns: Array<QuickReportColumnConfigInfo> = new Array<QuickReportColumnConfigInfo>();
        this.reportManager.Grid.leafColumns.forEach((item) => {
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
            if (property.length == 0)
              return;

            if (property[0].visibility == "AlwaysHidden")
              return;

            if (property.length > 0)
              qr.dataType = property[0].dotNetType;
            qr.visible = true;
            qr.title = column.displayTitle;
            qr.displayIndex = columnIndex;

            if (property[0].storageType == "money")
              qr.type = property[0].storageType;
            else if (property[0].scriptType.toLowerCase() != "date")
              qr.type = property[0].scriptType;
            else
              qr.type = property[0].type;

            qr.groupName = property[0].groupName;
            
            columns.push(qr);

            if (qr.groupName != "" && (this.gridGroupColumnNames.filter(g => g.groupName.toLowerCase() === qr.groupName.toLowerCase()).length == 0
              || this.gridGroupColumnNames.length == 0)) {
              this.gridGroupColumnNames.push(qr);
            }

            if (this.createdGroupNames.filter(c => c.groupName === qr.groupName).length == 0 && qr.groupName != "")
              this.createdGroupNames.push(qr);

            columnIndex++;
          }
        });

        propeties.forEach((property) => {
          var findex = columns.findIndex(f => f.name.toLowerCase() === property.name.toLowerCase());
          if (findex < 0) {
            var qr: QuickReportColumnConfigInfo = new QuickReportColumnConfigInfo();

            if (property.visibility == "AlwaysHidden")
              return;

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
            else if (property.scriptType.toLowerCase() != "date")
              qr.type = property.scriptType;
            else
              qr.type = property[0].type;

            qr.groupName = property.groupName;          
            columns.push(qr);

            if (qr.groupName != "" && (this.gridGroupColumnNames.filter(g => g.groupName.toLowerCase() === qr.groupName.toLowerCase()).length == 0
              || this.gridGroupColumnNames.length == 0)) {
              this.gridGroupColumnNames.push(qr);
            }

            if (this.createdGroupNames.filter(c => c.groupName === qr.groupName).length == 0 && qr.groupName != "")
              this.createdGroupNames.push(qr);

            columnIndex++;
          }
        });

        var dpi_x = document.getElementById('dpi').offsetWidth;
        this.viewInfo = new QuickReportConfigInfo();
        this.viewInfo.columns = columns;
        this.viewInfo.inchValue = dpi_x;        

        this.initPageSetting();

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

  /**
   * این متد برای لود ستون های گزارش فوری از متادیتا داینامیک مثل سود و زیان   
   */
  loadSettingFromDynamicMetadata() {   
     
        var propeties = this.reportManager.ViewIdentity.DynamicMetadata;

        var columnIndex = 0;
        var thArray = this.reportManager.Grid.wrapper.nativeElement.getElementsByTagName('TH');
        var columns: Array<QuickReportColumnConfigInfo> = new Array<QuickReportColumnConfigInfo>();
        this.reportManager.Grid.leafColumns.forEach((item) => {
          var qr: QuickReportColumnConfigInfo = new QuickReportColumnConfigInfo();
          var column = item as ColumnComponent;
          if (column.field) {
            qr.name = column.field;

            if (column.width)
              qr.width = column.width;
            else
              qr.width = thArray[columnIndex].offsetWidth;

            qr.userTitle = column.displayTitle;

            var property = propeties[columnIndex]; //.filter(p => p.name.toLowerCase() === column.field.toLowerCase());

            if (property.visibility == "AlwaysHidden")
              return;

            if (property.length > 0)
              qr.dataType = property.dotNetType;
            qr.visible = true;
            qr.title = column.displayTitle;
            qr.displayIndex = columnIndex;

            if (property.storageType == "money")
              qr.type = property.storageType;
            else if (property.scriptType.toLowerCase() != "date")
              qr.type = property.scriptType;
            else
              qr.type = property[0].type;

            qr.groupName = property.groupName;

            columns.push(qr);

            if (qr.groupName && qr.groupName != "" && (this.gridGroupColumnNames.filter(g => g.groupName.toLowerCase() === qr.groupName.toLowerCase()).length == 0
              || this.gridGroupColumnNames.length == 0)) {
              this.gridGroupColumnNames.push(qr);
            }

            if (qr.groupName && this.createdGroupNames.filter(c => c.groupName === qr.groupName).length == 0 && qr.groupName != "")
              this.createdGroupNames.push(qr);

            columnIndex++;
          }
        });

        propeties.forEach((property) => {
          //var findex = columns.findIndex(f => f.name.toLowerCase() === property.name.toLowerCase());         
          var findex = columns.findIndex(f => f.name.toLowerCase() === property.name.toLowerCase());
          if (findex < 0) {
            var qr: QuickReportColumnConfigInfo = new QuickReportColumnConfigInfo();

            if (property.visibility == "AlwaysHidden")
              return;

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
            else if (property.scriptType.toLowerCase() != "date")
              qr.type = property.scriptType;
            else
              qr.type = property[0].type;

            qr.groupName = property.groupName;
            columns.push(qr);

            if (qr.groupName && qr.groupName != "" && (this.gridGroupColumnNames.filter(g => g.groupName.toLowerCase() === qr.groupName.toLowerCase()).length == 0
              || this.gridGroupColumnNames.length == 0)) {
              this.gridGroupColumnNames.push(qr);
            }

            if (qr.groupName && this.createdGroupNames.filter(c => c.groupName === qr.groupName).length == 0 && qr.groupName != "")
              this.createdGroupNames.push(qr);

            columnIndex++;
          }
        });

        var dpi_x = document.getElementById('dpi').offsetWidth;
        this.viewInfo = new QuickReportConfigInfo();
        this.viewInfo.columns = columns;
        this.viewInfo.inchValue = dpi_x;

        this.initPageSetting();

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
    var wd = 0;
    this.viewInfo.columns.forEach((col) => {
      wd = wd + col.width;
    });
    this.gridWidth = wd;
  }

  getAllColumns() {
    return this.viewInfo.columns;
  }

  getSimpleColumns(item) {
    if(this.reportManager.ViewIdentity.IsDynamicColumns)
      return this.viewInfo.columns.filter(col => col.title === item.title);
    else
      return this.viewInfo.columns.filter(col => col.name === item.name);
  }

  //getGroupColumnNames(item) {
  //  if (this.createdGroupNames.filter(p => p === item.groupName).length === 0) {
  //    this.createdGroupNames.push(item.groupName);
  //    return this.viewInfo.columns.filter(col => col.groupName === item.groupName)[0];
  //  }
  //}

  groupColumnNotCreated(vc) {
    return (this.createdGroupNames.filter(p => p.name === vc.name).length > 0);  
  }

  getColumnRows(item) {   
    return this.viewInfo.columns.filter(col => col.groupName === item.groupName);    
  }

  showReport() {


    this.active = false;
    //this.reportManager.Grid = this.grid;
    //update  viewInfo
    var newViewInfo: QuickReportConfigInfo = new QuickReportConfigInfo();
    newViewInfo.columns = new Array<QuickReportColumnConfigInfo>();

    var i = 0;
    var thArrayTemp = this.grdQRSetting.wrapper.nativeElement.getElementsByTagName('TH');
    var thArray = new Array<any>();

    for (let item of thArrayTemp) {
      if (this.gridGroupColumnNames.filter(g => g.groupName.toLowerCase() === item.outerText).length == 0)
        thArray.push(item);
    }

    this.grdQRSetting.leafColumns.toArray().forEach((item, index, arr) => {

      var column = <ColumnComponent>item;      

      var indexCol = this.viewInfo.columns.findIndex(f => f.name === column.field);
      
      this.viewInfo.columns[indexCol].width = item.width;
      this.viewInfo.columns[indexCol].displayIndex = i;

      newViewInfo.columns.push(this.viewInfo.columns[indexCol]);

      i++;
    });

    this.viewInfo.columns = newViewInfo.columns;
    //update  viewInfo
       
    var viwString = JSON.stringify(this.viewInfo);
    this.bStorageService.setQuickReportSetting(this.currentViewId.toString(),this.UserId.toString(), viwString);   

    this.reportManager.showDefaultReport(this.viewInfo);

    this.viewInfo = null;
    this.viewData = {
      columns: []
    };
  }

  clearCurrentState(viewId) {
    this.bStorageService.removeQuickReportSetting(viewId.toString(),this.UserId.toString());
  }

  showPaperSetting() {
    this.dialogRef = this.dialogService.open({
      content: QuickReportPageSettingComponent,
      title: this.getText('Report.PageSetting.ReportPageSetting'),
      minWidth:600
    });

    this.dialogModel = this.dialogRef.content.instance;
    if (this.viewInfo.reportPageSetting) {
      this.dialogModel.pageSizeSelected = this.viewInfo.reportPageSetting.pageSize;
      this.dialogModel.pageOrientationSelected = this.viewInfo.reportPageSetting.pageOrientation;
      this.dialogModel.columnFitPage = this.viewInfo.reportPageSetting.columnFitPage;
      this.dialogModel.bottomMargin = this.viewInfo.reportPageSetting.marginBottom;
      this.dialogModel.leftMargin = this.viewInfo.reportPageSetting.marginLeft;
      this.dialogModel.topMargin = this.viewInfo.reportPageSetting.marginTop;
      this.dialogModel.rightMargin = this.viewInfo.reportPageSetting.marginRight;
    }
    else {
      this.viewInfo.reportPageSetting = new ReportPageSetting();
    }

    this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
    });

    this.dialogRef.content.instance.result.subscribe((res) => {      
      this.viewInfo.reportPageSetting = res;
      this.dialogRef.close();
    });
  }
}