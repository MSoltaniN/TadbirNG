import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { Headers, RequestOptions, BaseRequestOptions } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'toastr-ng2';
//PRIMENG - Third party module
import { InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule } from 'primeng/primeng';

import { AppComponent } from './components/app/app.component';
import { NavMenuComponent } from './components/navmenu/navmenu.component';
import { AccountComponent } from './components/account/account.component';

import {BrowserModule} from "@angular/platform-browser";

import { TranslateModule } from "ng2-translate";

import { AccountService,FullAccountService } from './service/index';

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        AccountComponent        
    ],
    providers: [AccountService,FullAccountService,
        { provide: LocationStrategy, useClass: HashLocationStrategy }],        
    imports: [
        CommonModule,         
        HttpModule,
        FormsModule,
        BrowserAnimationsModule,
        ToastrModule.forRoot(),
        InputTextModule, DataTableModule, ButtonModule, DialogModule, PanelModule,
        BrowserModule,
        TranslateModule.forRoot(),
        RouterModule.forRoot([
            { path: '', redirectTo: 'account', pathMatch: 'full' },
            { path: 'account', component: AccountComponent },
            { path: '**', redirectTo: 'account' }
        ])
    ]
})
export class AppModuleShared {
}
