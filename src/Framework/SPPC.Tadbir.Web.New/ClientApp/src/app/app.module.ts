
import { Headers, RequestOptions, BaseRequestOptions, Http, HttpModule } from '@angular/http';
import { APP_BASE_HREF, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { TextMaskModule } from 'angular2-text-mask';
import { Layout } from '../environments/environment';
//import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { HotkeyModule } from 'angular2-hotkeys';
//import compress package
import { LZStringModule, LZStringService } from 'ng-lz-string';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA, forwardRef, ErrorHandler } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { NgProgressRouterModule } from '@ngx-progressbar/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { FinanceModule } from './finance/finance.module';
import { AdminModule } from './admin/admin.module';
import { ConfigModule } from './config/config.module';
import { OrganizationModule } from './organization/organization.module';
import { GeneralErrorHandler } from './shared/class/error.handler';
import { EnviromentComponent } from './shared/class/enviroment.component';





@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    CoreModule,
    SharedModule.forRoot(),
    FinanceModule,
    AdminModule,
    ConfigModule,
    OrganizationModule,
    AppRoutingModule,


    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),


    
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    

    HotkeyModule.forRoot(),

    TextMaskModule,
    
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
    Layout,
    EnviromentComponent,
    LZStringService,
    
  ],
  schemas: [NO_ERRORS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule { }
