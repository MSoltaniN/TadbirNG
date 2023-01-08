import { Component, OnInit, Output, EventEmitter, Input, Renderer2, ChangeDetectorRef, NgZone, ViewChild } from "@angular/core";
import { RTL } from "@progress/kendo-angular-l10n";
import { Layout, Entities } from "@sppc/shared/enum/metadata";
import { AutoGeneratedClientGridComponent } from "@sppc/shared/class/autoGeneratedClientGrid.component";
import { ViewName } from "@sppc/shared/security";
import { SettingService } from "@sppc/config/service";
import { MetaDataService, GridService, BrowserStorageService } from "@sppc/shared/services";
import { ToastrService } from "ngx-toastr";
import { TranslateService } from "@ngx-translate/core";
import { DialogService } from "@progress/kendo-angular-dialog";
import { GridComponent } from "@progress/kendo-angular-grid";

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'error-list',
  templateUrl: './errorList.component.html',
  styles: [`
  ::ng-deep .sppc-errorlist-form > .k-grid { min-height:500px;max-width:800px; }
  ::ng-deep .sppc-errorlist-form .k-grid-norecords td { vertical-align: top; }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class ErrorListComponent extends AutoGeneratedClientGridComponent implements OnInit {

  @ViewChild(GridComponent, {static: true}) grid: GridComponent;
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Input() totalItemsCount: number;    
  totalItems: number;

  constructor(public toastrService: ToastrService, public translate: TranslateService, public dialogService: DialogService, public gridService: GridService,
    public cdref: ChangeDetectorRef, public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService,
    public ngZone: NgZone, public bStorageService: BrowserStorageService) {
    super(toastrService, translate, gridService, renderer, metadata, settingService, bStorageService, cdref, ngZone);
  }

  ngOnInit(): void {
    this.entityName = Entities.GroupActionResult;
    this.viewId = ViewName[this.entityTypeName];
    this.cdref.detectChanges();   

    this.reloadGrid();

  }  

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.cancel.emit();
  }

}
