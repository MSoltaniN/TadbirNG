
import { NgModule, NO_ERRORS_SCHEMA, forwardRef, ErrorHandler } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { Headers, RequestOptions, BaseRequestOptions, Http, HttpModule } from '@angular/http';
import { APP_BASE_HREF, CommonModule, Location, LocationStrategy, HashLocationStrategy, DatePipe } from '@angular/common';
// third party module to display toast 
import { ToastrModule } from 'ngx-toastr';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { ReactiveFormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { PopupModule } from '@progress/kendo-angular-popup';
import { ContextMenuModule } from '@progress/kendo-angular-menu';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { DpDatePickerModule } from 'ng2-jalali-date-picker';

import { Layout } from '../environments/environment';

import { BrowserModule } from "@angular/platform-browser";
//import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

import { GridModule } from '@progress/kendo-angular-grid';
import { RTL, MessageService } from '@progress/kendo-angular-l10n';

import { DialogModule } from '@progress/kendo-angular-dialog';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { InputsModule } from '@progress/kendo-angular-inputs';
import { CalendarModule } from '@progress/kendo-angular-dateinputs';

import { HotkeyModule } from 'angular2-hotkeys';



import { HttpClientModule, HttpClient } from '@angular/common/http';


import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { NgProgressRouterModule } from '@ngx-progressbar/router';


import { LayoutModule } from '@progress/kendo-angular-layout';


//import compress package
import { LZStringModule, LZStringService } from 'ng-lz-string';

import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { FinanceModule } from './finance/finance.module';
import { GeneralErrorHandler } from './shared/class/error.handler';
import { EnviromentComponent } from './shared/class/enviroment.component';



export function HttpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http);
}

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CoreModule,
    SharedModule,
    FinanceModule,
    AppRoutingModule,


    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ButtonsModule,
    CommonModule,
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    PopupModule,
    ContextMenuModule,
    ToastrModule.forRoot({ preventDuplicates: true }),
    HotkeyModule.forRoot(),
    DialogModule, DropDownsModule, GridModule, InputsModule, CalendarModule,
    DpDatePickerModule,
    TextMaskModule,
    TreeViewModule,
    HttpClientModule,
    LZStringModule,
    NgProgressModule.forRoot({
      direction: 'ltr+',
      spinnerPosition: 'left',
      color: 'white',
      thick: true,
      meteor: false
    }),
    NgProgressRouterModule,
    NgProgressHttpModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient]
      }
    }),
    LayoutModule
  ],
  providers: [ 
 
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: ErrorHandler, useClass: GeneralErrorHandler },
    //{
    //    provide: RTL,
    //    useFactory: function () {
    //        var lang = localStorage.getItem('lang');
    //        if (lang == "en") {
    //            return false;
    //        } 
    //        else 
    //            return true;
    //    }            
    //},
    DatePipe,
    Layout,
    EnviromentComponent,
    LZStringService,
    
  ],
  schemas: [NO_ERRORS_SCHEMA],
  //entryComponents: [
  //  SppcGridDatepicker,    
  //  TabComponent,   SelectFormComponent,   //],
  bootstrap: [AppComponent]
})
export class AppModule { }
