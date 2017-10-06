import { Component, OnInit } from '@angular/core';

import { AccountService } from '../../service/index';
import { InputTextModule, DataTableModule, ButtonModule, DialogModule } from 'primeng/primeng'; /** add components for test DataGrid functionality */
import { ToastrService } from 'toastr-ng2'; /** add this component for message in client side */
import {LazyLoadEvent,FilterMetadata} from 'primeng/primeng';
import {Filter} from '../../class/filter';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";


@Component({
    selector: 'account',
    templateUrl: './account.component.html'
})


export class AccountComponent  implements OnInit {

    private rowData: string;

    public totalRecords: number;

    ngOnInit()
    {
        this.loadData();
    }   

    constructor(private accountService : AccountService,private toastrService: ToastrService){}



    loadData() {

        this.accountService.getTotalCount().subscribe(res => {
                this.totalRecords = res.result;
            });           

        //this.accountService.search(0,10,"","").subscribe(res => {
        //        this.rowData = res.result;
        //    });           
    }
    
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
  
}


