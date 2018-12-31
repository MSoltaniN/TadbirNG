import { Layout } from "../../../environments/environment";
import { TreeNode } from '../../model/index';
import { Component, OnInit, Input } from '@angular/core';
import { DetailComponent } from '../../class/detail.component';
import { RTL } from '@progress/kendo-angular-l10n';
import { TreeNodeInfo } from '../../model/index';
import { Renderer2, Optional, Inject } from "@angular/core";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';
import { MetaDataService } from "../../service/metadata/metadata.service";
import { ReportingService } from "../../service/report/reporting.service";
import { ReportApi } from "../../service/api/reportApi";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'report-management',
  templateUrl: './reportManagement.component.html',
  styleUrls: ['./reportManagement.component.css'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class ReportManagementComponent extends DetailComponent implements OnInit {
  
  @Input() public baseId: string;
  treeData: TreeNodeInfo[] = new Array<TreeNodeInfo>();
  active: boolean = false;

  constructor(public toastrService: ToastrService,
     public translate: TranslateService,
      public renderer: Renderer2, 
      private metaDataService: MetaDataService,
    @Optional()
    @Inject('empty')
    public entityType: string,
    @Optional()
    @Inject('empty')
    public metaDataName: string,
    public reportingService:ReportingService) {
    super(toastrService, translate, renderer, metaDataService, entityType, metaDataName);
  }

  ngOnInit() {
    
    
  }

  public showDialog()
  {
    this.active = true;

    this.reportingService.getAll(ReportApi.ReportsHierarchy).subscribe((res: Response) => {
        var i = res;

        this.treeData = new Array<TreeNodeInfo>();
    });
    
  }
}
