import { Component, OnInit, Input, ViewContainerRef } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'app-bread-cumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadCumbComponent implements OnInit {

  @Input() public set entityTypeName(name: string) {
    if (name) {
      this.getEntityFromParent = false;
      this.getEntityTitle(name);
    }

  }

  getEntityFromParent: boolean = true;
  title: string;

  constructor(public parentComponet: ViewContainerRef, public translate: TranslateService) { }

  ngOnInit() {
    if (this.getEntityFromParent) {
      var entityTypeName = (<any>this.parentComponet)._view.component.entityTypeName;

      if (entityTypeName) {
        this.getEntityTitle(entityTypeName.toString());
      }
      else {
        var entityType = (<any>this.parentComponet)._view.component.entityType;
        if (entityType)
          this.getEntityTitle(entityType.toString());
      }      
    }
  }

  getText(key: string): void {
    this.translate.get(key).subscribe((msg: string) => {
      this.title = msg;
    });
  }


  getEntityTitle(entityType: string) {
    switch (entityType.toLowerCase()) {
      case "account":
        this.getText("Entity.Account");
        break;
      case "voucher":
        this.getText("Entity.Voucher");
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
    }
  }
}
