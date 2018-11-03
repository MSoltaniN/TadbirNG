import { Component, OnInit, Input, ViewContainerRef } from "@angular/core";
import { Router } from '@angular/router';
import { Location } from '@angular/common';
import { TranslateService } from "@ngx-translate/core";

@Component({
  selector: 'app-bread-cumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.css']
})
export class BreadCumbComponent implements OnInit {

  title : string;  

  constructor( public parentComponet: ViewContainerRef,public translate: TranslateService) { 

     var entityType = (<any>this.parentComponet)._view.component.entityType
      switch(entityType.toString().toLowerCase())
      {
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
          case "settings":
            this.getText("Entity.Settings");                     
            break;
          case "viewRowPermission":
            this.getText("Entity.ViewRowPermission");            
            break;
          case "operationlog":          
            this.getText("Entity.OperationLog");
            break;          
      
      }
    
  }

  ngOnInit() {
  }

  getText(key : string):void
  {
    var result : string;
    this.translate.get(key).subscribe((msg: string) => {
      this.title = msg;
    });   
    
  }

}
