import { Component, Input, Output, EventEmitter, Renderer2, OnInit } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent, RelatedItems, MetaDataService, BrowserStorageService, ViewName } from '@sppc/shared';
import { Layout, Entities } from '@sppc/env/environment';
import { BranchService } from '@sppc/organization';



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


  @Output() cancelBranchRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveBranchRoles: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties


  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public branchService: BranchService) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Branch, ViewName.Branch);
  }

  ngOnInit() {
    this.getBranchRoles();
  }

  getBranchRoles() {
    this.branchService.getBranchRoles(this.branchId).subscribe(res => {
      this.branchRolesData = res;

      this.gridData = this.branchRolesData.relatedItems;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }

    });
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
