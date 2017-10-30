import { Component, OnInit } from '@angular/core';

import { AccountService, AccountInfo } from '../../service/index';

import { Account } from '../../model/index';

import { InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule } from 'primeng/primeng'; /** add components for test DataGrid functionality */
import { ToastrService, ToastConfig } from 'toastr-ng2'; /** add this component for message in client side */
import {LazyLoadEvent,FilterMetadata} from 'primeng/primeng';
import {Filter} from '../../class/filter';



import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";

import {TranslateService} from 'ng2-translate';
import { String } from '../../class/source';


@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})


export class AccountComponent implements OnInit {

    public rowData: any[];

    public totalRecords: number;


    currentFilter: Filter[] = [];
    currentOrder: string = "";

    //variable for visible delete confirm dialog
    displayDeleteDialog: boolean;
    deleteAccountId: number;

    //for add in delete messageText
    deleteConfirm: string;

    //variable for dialog
    displayDialog: boolean;

    config: ToastConfig;

    showloadingMessage: boolean = true;

    newAccount: boolean;
    account: Account = new AccountInfo

    updateMsg: string;
    insertMsg: string;
    deleteMsg: string;

    rtlClass: string = "ui-rtl";
    rtlUse: string = "rtl";

    ngOnInit() {
        //this.getRowsCount();
        
    }

    private translateService: TranslateService

    pageIndex?: number;
    count?: number;

    constructor(private accountService: AccountService, private toastrService: ToastrService, private translate: TranslateService)
    {
        translate.addLangs(["en", "fa"]);
        translate.setDefaultLang('fa');

        var browserLang = 'fa';//translate.getBrowserLang();
        translate.use(browserLang);
        
        this.translateService = translate;

        this.localizeMsg();
    }

    languageChange(value:string)
    {
        this.translateService.use(value);
        this.localizeMsg();
        switch (value)
        {
            case "fa":
            {
                this.rtlUse = "rtl";
                this.rtlClass = "ui-rtl"
                break;
            }
            case "en":
            {
                this.rtlUse = "ltr";
                this.rtlClass = ""
                break;
            }
        }
            
        
    }


    localizeMsg()
    {
        // read message format for crud operations
        var entityType = '';
        this.translateService.get("Entity.Account").subscribe((msg: string) => {
            entityType = msg;
        });

        this.translateService.get("Messages.Inserted").subscribe((msg: string) => {
            this.insertMsg = String.Format(msg, entityType);
        });

        this.translateService.get("Messages.Updated").subscribe((msg: string) => {
            this.updateMsg = String.Format(msg, entityType);;
        });

        this.translateService.get("Messages.Deleted").subscribe((msg: string) => {
            this.deleteMsg = String.Format(msg, entityType);;
        });
    }

    getRowsCount() {

        
            this.accountService.getCount(this.currentOrder, this.currentFilter).subscribe(res => {
                this.totalRecords = res;
            });
        
    }

    
    //getCount(orderby?: string, filters?: string) {
     
    //    this.accountService.getCount(orderby,filters).subscribe(res => {
    //        this.totalRecords = res;
    //    });
    //}

    reloadGrid() {

        this.getRowsCount();

        this.accountService.search(this.pageIndex, this.count, '', '').subscribe(res => {
            this.rowData = res;
        });

    }

    onEditComplete(event : any)
    {
        console.log(arguments);


        if (arguments.length > 0)
        {
            var acc = arguments[0].data;
            this.accountService.editAccount(acc)
                .subscribe(response => {

                    
                    this.toastrService.success(this.updateMsg, '', { positionClass: 'toast-top-left' } );
                    this.reloadGrid();
                });
        }
        
    }

    //LoadAccount

    getFilters(event: LazyLoadEvent) : Filter[]
   {
       let filters : Filter[] = [];

       if (event.filters) {
           if (Object.keys(event.filters).length > 0) {
               Object.keys(event.filters).forEach(routeKey => {
                   if (event.filters)
                       filters.push(new Filter(routeKey, event.filters[routeKey].value))
               });

           }
       }

        return filters;
    }
    
    loadAccountLazy(event: LazyLoadEvent) {
        
        var filter : any = null;
        var order : string = "";

         var sortAscDesc: string = "";

         if(event.sortOrder === -1){
          sortAscDesc = "DESC";
         }
         else if(event.sortOrder === 1)
            sortAscDesc = "ASC";
          

         
        if (event.filters && Object.keys(event.filters).length > 0)
                filter = this.getFilters(event);

        if(event.sortField)
            order = event.sortField + ' ' + sortAscDesc;

        this.currentFilter = filter;
        this.currentOrder = order;

        this.pageIndex = event.first;
        this.count = event.rows;

        this.getRowsCount();

         this.accountService.search(event.first,event.rows,order,filter).subscribe(res => {
             this.rowData = res;
             
             this.showloadingMessage = !(res.length == 0);

            });        
    }  

    //LoadAccount

    //Edit Account

    cancel() {
        this.account = new AccountInfo();
        this.displayDialog = false;
        this.newAccount = false;
    }

    save()
    {
        if (!this.newAccount)
        {
            this.accountService.editAccount(this.account)
                .subscribe(response => {                    
                    this.toastrService.success(this.updateMsg, '', { positionClass: 'toast-top-left' });
                    this.reloadGrid();
                });

            this.displayDialog = false;
        }
        else
        {
            this.accountService.insertAccount(this.account)
                .subscribe(response => {
                    this.toastrService.success(this.insertMsg, '', { positionClass: 'toast-top-left' });
                    this.reloadGrid();
                });

            this.newAccount = false;
            this.displayDialog = false;
        }
    }

    
    showDialogToEdit(acc: Account) {
        this.newAccount = false;
        this.account = new AccountInfo();
        this.account.accountId = acc.accountId;
        this.account.code = acc.code;
        this.account.name = acc.name;
        this.account.description = acc.description;
        this.account.fiscalPeriodId = acc.fiscalPeriodId;
        this.account.branchId = acc.branchId;

        this.displayDialog = true;
    }

    //Edit Account


    //Delete Account 

    showDialogToDelete(account: Account) {

        this.translateService.get("Messages.DeleteConfirm").subscribe((msg: string) => {
            this.deleteConfirm = String.Format(msg,account.name);
        });

        
        this.deleteAccountId = account.accountId;
        this.displayDeleteDialog = true;
    }

    deleteAccount(confirm : boolean)
    {
        if (confirm)
        {
            this.accountService.delete(this.deleteAccountId).subscribe(response => {
                this.deleteAccountId = 0;
                this.toastrService.info(this.deleteMsg, '', { positionClass: 'toast-top-left' });
                this.reloadGrid();
            });
        }

        //hide confirm dialog
        this.displayDeleteDialog = false;
    }

    //Delete Account

    
    //Add Account

    showDialogToAdd() {
        this.newAccount = true;
        this.displayDialog = true;
        this.account = new AccountInfo();

        
    }


    //Add Account

}


