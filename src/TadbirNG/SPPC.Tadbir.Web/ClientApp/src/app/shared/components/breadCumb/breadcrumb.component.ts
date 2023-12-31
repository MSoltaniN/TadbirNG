import { Component, OnInit, Input, ViewContainerRef, HostListener, Host, OnDestroy } from "@angular/core";
import { Title } from "@angular/platform-browser";
import { TranslateService } from "@ngx-translate/core";
import { Subscription } from "rxjs";

@Component({
  selector: 'app-bread-cumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadCumbComponent implements OnInit,OnDestroy {
    

  @Input() public set entityTypeName(name: string) {
    if (name && !this.hasCustomTitle) {
      this.getEntityFromParent = false;
      this.getEntityTitle(name);
    }
  }

  @Input() public set customTitle(title: string) {
    if (title) {
      this.hasCustomTitle = true;
      this.setCustomTitle(title);
    }
  }

  hasCustomTitle: boolean = false;
  getEntityFromParent: boolean = true;
  title: string;
  subs$: Subscription;

  constructor(public parentComponet: ViewContainerRef,
     public translate: TranslateService,
     private titleService: Title) { }

  ngOnInit() {
    
    if (this.getEntityFromParent && !this.hasCustomTitle) {
      var entityTypeName = this.parentComponet['_hostLView'][8].entityTypeName;

      if (entityTypeName) {
        this.getEntityTitle(entityTypeName.toString());
      }
      else {
        var entityType = this.parentComponet['_hostLView'][8].entityType;
        if (entityType)
          this.getEntityTitle(entityType.toString());
      }      
    }
  }

  ngOnDestroy(): void {
    this.subs$?.unsubscribe();
  }

  getText(key: string): void {
    this.subs$ = this.translate.get(key).subscribe((msg: string) => {
      this.title = msg;
      this.titleService.setTitle(msg)
    });
  }

  setCustomTitle(title:string) {
    this.title = title;
    this.titleService.setTitle(title)
  }

  getEntityTitle(entityType: string) {
    switch (entityType.toLowerCase()) {
      case "account":
        this.getText("Entity.Account");
        break;
      case "voucher":
        this.getText("Entity.Voucher");
        break;
      case "vouchereditor":
        this.getText("Entity.VoucherEditor");
        break;
      case "draftvouchereditor":
        this.getText("Entity.DraftVoucherEditor");
        break;
      case "user":
        this.getText("Entity.User");
        break;
      case "role":
        this.getText("Entity.Role");
        break;
      case "password":
        this.getText("ChangePassword.Title");
        break;
      case "detailaccount":
        this.getText("Entity.DetailAccount");
        break;
      case "costcenter":
        this.getText("Entity.CostCenter");
        break;
      case "project":
        this.getText("Entity.Project");
        break;
      case "fiscalperiod":
        this.getText("Entity.FiscalPeriod");
        break;
      case "branch":
        this.getText("Entity.Branch");
        break;
      case "company":
        this.getText("Entity.Company");
        break;
      case "accountrelations":
        this.getText("Entity.AccountRelations");
        break;
      case "setting":
        this.getText("Entity.Setting");
        break;
      case "rowaccess":
        this.getText("Entity.RowAccess");
        break;
      case "operationlog":
        this.getText("Entity.OperationLog");
        break;
      case "accountgroup":
        this.getText("Entity.AccountGroup");
        break;
      case "accountcollection":
        this.getText("Entity.AccountCollection");
        break;
      case "journal":
        this.getText("Entity.Journal");
        break;
      case "accountbook":
        this.getText("Entity.AccountBook");
        break;
      case "currency":
        this.getText("Entity.Currency");
        break;
      case "currencyrate":
        this.getText("Entity.CurrencyRate");
        break;
      case "testbalance":
        this.getText("Entity.TestBalance");
        break;
      case "itembalance":
        this.getText("Entity.ItemBalance");
        break;
      case "currencybook":
        this.getText("Entity.CurrencyBook");
        break;
      case "systemissue":
        this.getText("Entity.SystemIssue");
        break;
      case "balancebyaccount":
        this.getText("Entity.BalanceByAccount");
        break;
      case "profitlost":
        this.getText("Entity.ProfitLoss");
        break;
      case "balancesheet":
        this.getText("Entity.BalanceSheet");
        break;
      case "dashboard":
        this.getText("Entity.Widget");
        break;
      case "cashregister":
        this.getText("CashRegisters.CashRegisters");
        break;
      case "checkbook":
        this.getText("Entity.CheckBook");
        break;
      case "newcheckbook":
        this.getText("CheckBook.New");
        break;
      case "lastcheckbook":
        this.getText("CheckBook.LastCheckBook");
        break;
      case "checkbookreport":
        this.getText("Entity.CheckBookReport");
        break;
      case "sourceapp":
        this.getText("Entity.SourceApp");
        break;
      case "receipt":
        this.getText("Entity.Receipt");
        break;
      case "payment":
        this.getText("Entity.Payment");
        break;
      case "logsetting":
        this.getText("Entity.LogSetting");
        break;
    }
  }

  
}
