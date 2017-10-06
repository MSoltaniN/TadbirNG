import { Component, OnInit } from '@angular/core';

import { AccountService } from '../../service/index';
import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng'; /** add components for test DataGrid functionality */
import { ToastrService } from 'toastr-ng2'; /** add this component for message in client side */
//import {LazyLoadEvent} from '../../../components/common/api';
//import {FilterMetadata} from '../../../components/common/api';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";


@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})

export class AccountComponent  implements OnInit {

    private rowData: string;

    totalRecords: number;

    ngOnInit()
    {
        this.loadData();
    }

    constructor(private accountService : AccountService,private toastrService: ToastrService){}



    loadData() {
        this.accountService.getAccounts().subscribe(res => {
                this.rowData = res.result;
            });        
    }
    
    
    //loadAccountLazy(event: LazyLoadEvent) {
    //    //in a real application, make a remote request to load data using state metadata from event
    //    //event.first = First row offset
    //    //event.rows = Number of rows per page
    //    //event.sortField = Field name to sort with
    //    //event.sortOrder = Sort order as number, 1 for asc and -1 for dec
    //    //filters: FilterMetadata object having field as key and filter value, filter matchMode as value
        
    //    //imitate db connection over a network
    //    setTimeout(() => {
    //        if(this.datasource) {
    //            this.cars = this.datasource.slice(event.first, (event.first + event.rows));
    //        }
    //    }, 250);
    //}
  
}


