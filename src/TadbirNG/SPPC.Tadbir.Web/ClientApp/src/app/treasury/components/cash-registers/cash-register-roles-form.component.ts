import { Component, ElementRef, EventEmitter, Input, OnInit, Output, Renderer2, ViewChild } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { GridComponent, RowArgs } from '@progress/kendo-angular-grid';
import { RTL } from '@progress/kendo-angular-l10n';
import { DetailComponent } from '@sppc/shared/class';
import { Entities, Layout } from '@sppc/shared/enum/metadata';
import { RelatedItems } from '@sppc/shared/models';
import { ViewName } from '@sppc/shared/security';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { CashRegistersService } from '@sppc/treasury/service/cash-registers.service';
import { ToastrService } from 'ngx-toastr';

export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'cash-register-roles-form',
  templateUrl: './cash-register-roles-form.component.html',
  styles: [`
    .main-box{
      display: flex;
      width:600px;
    }
    .canToAddDiv, .addedDiv {
      width:50%;
      min-height:400px
    }
    ::ng-deep .addedDiv .k-grid tr > td {
      background: transparent;
    }

    @media (max-width: 500px) {
      .main-box{
        width:100%;
      }
      h5 {
        padding:0 2px;
        height:40px;
        line-height: 1.4;
      }
    }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})
export class CashRegisterRolesFormComponent extends DetailComponent implements OnInit {

  @ViewChild('resultGrid', {static: true}) grid: GridComponent;

  @Input() public cashRegisterId: number;
  @Input() public errorMessage: string = '';
  @Input() public set cashRegisterRoles(selectedRoles: RelatedItems) {
    this.cashRegisterUsersData = selectedRoles;
    this.selectedResultRows = [];
    this.selectedCanBeAssignRows = [];

    if (selectedRoles != undefined) {
      this.usersCanBeAssign = selectedRoles.relatedItems.filter(u => u.isValid && !u.isSelected);
      this.assignedUsers = selectedRoles.relatedItems.filter(u => u.isSelected);
      for (let roleItem of this.assignedUsers) {
        this.selectedResultRows.push(roleItem.id);
      }
    }
  }

  @Output() cancelCashRegisterRoles: EventEmitter<any> = new EventEmitter();
  @Output() saveCashRegisterRoles: EventEmitter<RelatedItems> = new EventEmitter();
  
  public usersCanBeAssign = [];
  public assignedUsers = [];
  public selectedResultRows: number[] = [];
  public selectedCanBeAssignRows: number[] = [];
  public cashRegisterUsersData: RelatedItems;
  public get showloadingMessage(): boolean{
    return this.cashRegisterUsersData.relatedItems.length && this.usersCanBeAssign.length? true: false;
  };
  public get showloadingMessage2(): boolean{
    return this.cashRegisterUsersData.relatedItems.length && this.assignedUsers.length? true: false;
  };

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public bStorageService: BrowserStorageService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public cashRegisterService: CashRegistersService,
    public elem:ElementRef
    ) {
    super(toastrService, translate, bStorageService, renderer, metadata, Entities.CashRegister, ViewName.CashRegister,elem);
  }

  ngOnInit() {}

  public onSave(e: any): void {
    e.preventDefault();
    let data:RelatedItems = {
      id: this.cashRegisterUsersData.id,
      relatedItems: []
    };
    this.assignedUsers.forEach(f => f.isSelected = true);
    data.relatedItems = this.assignedUsers;

    this.saveCashRegisterRoles.emit(data);
  }

  public onCancel(e: any): void {
    e.preventDefault();
    this.closeForm();
  }

  private closeForm(): void {
    this.selectedResultRows = [];
    this.selectedCanBeAssignRows = [];
    this.cancelCashRegisterRoles.emit();
  }

  onSelectedAssignedKeysChange(e) {
    let item = this.cashRegisterUsersData.relatedItems.find(i => i.id == e[0]);
    let selectedIndex = this.assignedUsers.findIndex(i => i.id == e[0]);
  
    if (selectedIndex>-1) {
      this.assignedUsers.splice(selectedIndex,1);
    }
    if (!this.usersCanBeAssign.find(i => i.id == item.id) && item.isValid)
      this.usersCanBeAssign.push(item);
    
    let idx = this.selectedResultRows.findIndex(id => id == e[0])
    if (idx>-1)
      this.selectedResultRows.splice(idx,1);

    this.checkAllAssignedUsers();

  }

  onSelectedCanBeAssignKeysChange(e) {
    let item = this.cashRegisterUsersData.relatedItems.find(i => i.id == e[0]);
    let selectedIndex = this.usersCanBeAssign.findIndex(i => i.id == e[0]);
    if (selectedIndex>-1) {
      this.usersCanBeAssign.splice(selectedIndex,1);
    }
    this.selectedCanBeAssignRows = [];

    if (!this.assignedUsers.find(i => i.id == item.id))
      this.assignedUsers.push(item);

    if (!this.selectedResultRows.includes(item.id)) {
      this.selectedResultRows.push(item.id);
    }

    this.checkAllAssignedUsers();
  }

  onSelectAll(state:string,event:Event) {
  
    if (state == 'assign') {
      this.usersCanBeAssign.forEach(user => {
        if (!this.assignedUsers.find(i => i.id == user.id))
          this.assignedUsers.push(user);
        if (!this.selectedResultRows.includes(user.id))
          this.selectedResultRows.push(user.id);
      });

      this.usersCanBeAssign = [];
      this.selectedCanBeAssignRows = []
      this.checkAllAssignedUsers();
    }

    if (state == 'assigned') {
      this.assignedUsers.forEach(user => {
        if (!this.usersCanBeAssign.find(i => i.id == user.id) && user.isValid)
          this.usersCanBeAssign.push(user);
      });

      this.assignedUsers = [];
      this.selectedResultRows = []
    }

    (<HTMLInputElement>event.target).checked = false
  }

  checkAllAssignedUsers() {
    let checkbox = document.querySelectorAll("#resultGrid tr input[type=checkbox]");
    Array.from(checkbox).map((el:HTMLInputElement) => {
      el.checked = true;
    })
  }

  escPress() {
    this.closeForm();
  }

  selectionKey(context: RowArgs): string {
    if (context.dataItem == undefined) return "";
    return context.dataItem.id;
  }

}
