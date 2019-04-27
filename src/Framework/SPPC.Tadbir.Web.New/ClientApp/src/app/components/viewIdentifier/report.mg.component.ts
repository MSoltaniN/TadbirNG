import { Component, OnInit, AfterViewInit, Input, ViewChild, Host } from "@angular/core";
import { GridComponent, ColumnComponent } from "@progress/kendo-angular-grid";
import { ViewIdentifierComponent } from "./view-identifier.component";
import { SortDescriptor } from "@progress/kendo-data-query";
import { FilterExpression } from "../../class/filterExpression";
import { QuickReportColumnInfo, QuickReportViewInfo, ReportingService } from "../../service/report/reporting.service";
import { ReportApi } from "../../service/api/reportApi";
import { ReportManagementComponent } from "../reportManagement/reportManagement.component";
import { AutoGeneratedGridComponent } from "../../class/autoGeneratedGrid.component";
import { TreeItem } from "@progress/kendo-angular-treeview";
import { String } from '../../class/source';

@Component({
    selector: 'report-mg',
    templateUrl: `./report.mg.component.html`,
    styleUrls: [`./report.mg.component.css`],
    providers:[AutoGeneratedGridComponent]
  })

export class ReportMGComponent implements OnInit, AfterViewInit {
    
    @Input() public ViewIdentity: ViewIdentifierComponent;  
    @Input() public Grid: GridComponent;  
    @Input() public Sort: SortDescriptor[];
    @Input() public Filter: FilterExpression;
    @Input() public RowData: any;
    @Input() public MetadataType:string;
    

    @ViewChild(ReportManagementComponent) reportManager: ReportManagementComponent;
    

    constructor(public reportingService:ReportingService,
      @Host() public masterComponent:AutoGeneratedGridComponent) { }
  
    ngOnInit() {
      
    }    

    public showReport()
    {
      var showQReport : boolean = false; 
      var treeData : Array<TreeItem> = null;      
      var url = String.Format(ReportApi.ReportsByView,this.ViewIdentity.ViewID);

      this.reportingService.getAll(url)
      .subscribe((res: any) => {        
        treeData = <Array<TreeItem>>res.body; 
        if(treeData.filter((t : any) => t.isDynamic == true).length > 0) 
          showQReport = true;  
        this.switchReport(showQReport,treeData);          
      });
    }

    switchReport(showQReport : boolean,treeData : any)
    {
      var columnIndex = 0; 
      if(showQReport)
      {
        var properties = this.masterComponent.getAllMetaData(this.MetadataType);
        
        var columns: Array<QuickReportColumnInfo> = new Array<QuickReportColumnInfo>();
        this.Grid.leafColumns.forEach(function (item) {              
          var qr: QuickReportColumnInfo = new QuickReportColumnInfo();
          var column = item as ColumnComponent;
          if (column.field) {
            qr.name = column.field;
            qr.index = columnIndex;
            qr.visible = true;
            qr.width = column.width;
            qr.userText = column.displayTitle;
            qr.sortOrder = 0;
            qr.sortMode = 0;
            
            var property = properties.filter(p=>p.name.toLowerCase() === column.field.toLowerCase());
            if(property.length > 0) 
              qr.dataType = property[0].dotNetType;

            qr.defaultText = column.displayTitle;
            qr.enabled = true;
            qr.order = columnIndex;  
            columns.push(qr)

            columnIndex++;
          }
        });
    
        var dpi_x = document.getElementById('dpi').offsetWidth;    
        var viewInfo = new QuickReportViewInfo();
        viewInfo.columns = columns;
        viewInfo.inchValue = dpi_x;
        viewInfo.reportTitle = "گزارش فوری";
        viewInfo.row = this.RowData.data[0];
        
        this.reportingService.putEnvironmentUserQuickReport(ReportApi.EnvironmentQuickReport, viewInfo)
          .subscribe((response: any) => {
    
            var design = response.designJson;
            var id = this.ViewIdentity.ViewID;
            var params = null;
            if (this.ViewIdentity.params.length > 0)
              params = this.ViewIdentity.params.toArray();
            
            this.reportManager.showQuickReport(id, params, this.Filter, this.Sort, design, treeData );
    
          });
      }
      else
      {
        
      }
    }

  
    ngAfterViewInit(): void {
      
    }
  
   
  }