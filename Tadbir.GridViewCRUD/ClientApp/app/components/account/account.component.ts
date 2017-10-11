import { Component, OnInit } from '@angular/core';

import { AccountService, AccountInfo } from '../../service/index';

import { Account } from '../../model/index';

import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng'; /** add components for test DataGrid functionality */
import { ToastrService } from 'toastr-ng2'; /** add this component for message in client side */
import {LazyLoadEvent,FilterMetadata} from 'primeng/primeng';
import {Filter} from '../../class/filter';



import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";


declare var jquery: any;
declare var $: any;


@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})


export class AccountComponent  implements OnInit {

    private rowData: string;

    public totalRecords: number;

    //variable for visible delete confirm dialog
    displayDeleteDialog: boolean;
    deleteAccountId: number;

    //for add in delete messageText
    fullname: string;

    //variable for edit dialog
    displayEditDialog: boolean;


    newAccount : boolean;
    account : Account = new AccountInfo

    ngOnInit()
    {
        this.loadData();
    }   

    constructor(private accountService : AccountService,private toastrService: ToastrService){}
    

    loadData() {

        //evaluate total row counts for gird paging 
        this.accountService.getTotalCount().subscribe(res => {
                this.totalRecords = res.result;
        });

      
        
    }


    

    //LoadAccount

    getFilters(event: LazyLoadEvent) : Filter[]
   {
       let filters : Filter[] = [];

       if(Object.keys(event.filters).length > 0)
       {
                Object.keys(event.filters).forEach(routeKey => {                        
                     filters.push(new Filter(routeKey,  event.filters[routeKey].value))                    
                });
            
        }

        return filters;
    }
    
    loadAccountLazy(event: LazyLoadEvent) {
        
        var filter : any = "";
        var order : string = "";

         var sortAscDesc: string = "";

         if(event.sortOrder === -1){
          sortAscDesc = "DESC";
         }
          else if(event.sortOrder === 1){
            sortAscDesc = "ASC";
          }
        
        if(Object.keys(event.filters).length > 0)
            filter = this.getFilters(event);

        if(event.sortField)
            order = event.sortField + ' ' + sortAscDesc;

         this.accountService.search(event.first,event.rows,order,filter).subscribe(res => {
                this.rowData = res.result;
            });        
    }  

    //LoadAccount

    //Edit Account

    cancel() {
        this.account = new AccountInfo();
        this.displayEditDialog = false;
    }

    showDialogToEdit(acc: Account)
    {
        this.newAccount = false;
        this.account = new AccountInfo();
        this.account.id = acc.id;
        this.account.code = acc.code;
        this.account.name = acc.name;
        this.account.description = acc.description;
        this.account.fiscalPeriodId = acc.fiscalPeriodId;

        this.displayEditDialog = true; 
    }

    //Edit Account


    //Delete Account 

    showDialogToDelete(account: Account) {
        this.fullname = account.name;
        this.deleteAccountId = account.id;
        this.displayDeleteDialog = true;
    }

    deleteAccount(confirm : boolean)
    {
        if (confirm)
        {
            this.accountService.delete(this.deleteAccountId).subscribe(response => {
                this.deleteAccountId = 0;
                this.loadData();
            });
        }

        //hide confirm dialog
        this.displayDeleteDialog = false;
    }

    //Delete Account

    
    //Add Account


    //Add Account

}


