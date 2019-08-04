import { Component, Input, Output, EventEmitter } from '@angular/core';
import { RowArgs } from '@progress/kendo-angular-grid';
import { String } from '@sppc/shared/class';
import { Layout } from '@sppc/env/environment';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { RelatedItems } from '@sppc/shared';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}


@Component({
  selector: 'role-fiscalPeriod-form-component',
  templateUrl: './role-fiscalPeriod-form.component.html',
  styles: ['/deep/ .k-window-title{font-size:15px}'],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]

})

export class RoleFiscalPeriodFormComponent extends DetailComponent {


  ////create properties
  public gridData: any;
  public selectedRows: number[] = [];
  public showloadingMessage: boolean = true;
  public model: RelatedItems;


  @Input() public fiscalPeriodList: boolean = false;
  @Input() public errorMessage: string = '';
  @Input() public roleName: string = '';

  @Input() public set roleFiscalPeriod(roleFiscalPeriod: RelatedItems) {
    this.model = roleFiscalPeriod;
    this.selectedRows = [];
    if (roleFiscalPeriod != undefined) {
      this.gridData = roleFiscalPeriod.relatedItems;

      for (let fPeriodItem of this.gridData) {
        if (fPeriodItem.isSelected) {
          this.selectedRows.push(fPeriodItem.id)
        }
      }
    }
  }

  @Output() cancelRoleFiscalPeriod: EventEmitter<any> = new EventEmitter();
  @Output() saveRoleFiscalPeriod: EventEmitter<RelatedItems> = new EventEmitter();
  ////create properties

  //////Events
  public onSave(e: any): void {
    e.preventDefault();
    this.model.relatedItems.forEach(f => f.isSelected = false);

    for (let fPeriodSelected of this.selectedRows) {
      let userIndex = this.model.relatedItems.findIndex(f => f.id == fPeriodSelected);
      this.model.relatedItems[userIndex].isSelected = true;
    }
    this.saveRoleFiscalPeriod.emit(this.model);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.fiscalPeriodList = false;
    this.selectedRows = [];
    this.cancelRoleFiscalPeriod.emit();
  }

  escPress() {
    this.closeForm();
  }

  ////Events

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }


  getTitleText(text: string) {
    return String.Format(text, this.roleName);
  }
}
