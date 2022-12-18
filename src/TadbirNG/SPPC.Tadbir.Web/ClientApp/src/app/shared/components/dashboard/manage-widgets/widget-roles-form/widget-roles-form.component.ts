import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2 } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DefaultComponent, DetailComponent } from '@sppc/shared/class';
import { Entities, Layout } from '@sppc/shared/enum/metadata';
import { RelatedItems } from '@sppc/shared/models';
import { Widget } from '@sppc/shared/models/widget';
import { DashboardPermissions, SecureEntity, ViewName } from '@sppc/shared/security';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { ToastrService } from 'ngx-toastr';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'widget-roles-form',
  templateUrl: './widget-roles-form.component.html',
  styles: [''],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }, DefaultComponent]
})
export class WidgetRolesFormComponent extends DetailComponent implements OnInit {

  //permission flag   
  AssignRolesAccess: boolean;

  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public rolesList: boolean = false;
  @Input() public errorMessage: string = '';

  @Input() public set widgetRoles(wRoles: RelatedItems) {
    this.model = wRoles;
    this.selectedRows = [];
    if (wRoles != undefined) {
      this.gridData = wRoles.relatedItems;

      for (let roleItem of this.gridData) {
        if (roleItem.isSelected) {
          this.selectedRows.push(roleItem.id)
        }
      }
    }
  }
  @Output() cancel: EventEmitter<any> = new EventEmitter();
  @Output() save: EventEmitter<RelatedItems> = new EventEmitter();

  constructor(public toastrService: ToastrService, public translate: TranslateService, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService,public elem:ElementRef) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.Dashboard, ViewName.Dashboard,elem);
  }

  ngOnInit() {
    this.AssignRolesAccess = this.isAccess(SecureEntity.Dashboard, DashboardPermissions.ManageWidgets);
  }

  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let roleSelected of this.selectedRows) {
      let roleIndex = this.model.relatedItems.findIndex(f => f.id == roleSelected);
      this.model.relatedItems[roleIndex].isSelected = true;
    }
    this.save.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.rolesList = false;
    this.selectedRows = [];
    this.cancel.emit();
  }

  escPress() {
    this.closeForm();
  }
  ////Events

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

}
