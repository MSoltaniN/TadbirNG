import { Component, Input, Output, EventEmitter, Renderer2, OnInit, ElementRef } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { MetaDataService, BrowserStorageService } from '@sppc/shared/services';
import { Layout, Entities } from '@sppc/shared/enum/metadata';
import { BranchService } from '@sppc/organization/service';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'branch-roles-form-component',
  templateUrl: './branch-roles-form.component.html',
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class BranchRolesFormComponent extends DetailComponent implements OnInit {

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  //public model: RelatedItems;
  branchRolesData: RelatedItems;


  @Input() public branchId: number;
  @Input() public errorMessage: string = '';
  @Input() public set branchRoles(selectedRoles: RelatedItems) {
    this.branchRolesData = selectedRoles;
    this.selectedRows = [];
    if (selectedRoles != undefined) {
      this.gridData = selectedRoles.relatedItems;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }
    }
  }


  @Output() cancelBranchRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveBranchRoles: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties


  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public branchService: BranchService,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Branch, ViewName.Branch,elem);
  }

  ngOnInit() {

  }

  public onSave(e: any): void {
    e.preventDefault();
    this.branchRolesData.relatedItems.forEach(f => f.isSelected = false);

    for (let roleSelected of this.selectedRows) {
      let roleIndex = this.branchRolesData.relatedItems.findIndex(f => f.id == roleSelected);
      this.branchRolesData.relatedItems[roleIndex].isSelected = true;
    }
    this.saveBranchRoles.emit(this.branchRolesData);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.selectedRows = [];
    this.cancelBranchRoles.emit();
  }

  escPress() {
    this.closeForm();
  }


  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }
}
