
import { Headers, RequestOptions, BaseRequestOptions, Http, HttpModule } from '@angular/http';
import { APP_BASE_HREF, Location, LocationStrategy, HashLocationStrategy } from '@angular/common';
import { TextMaskModule } from 'angular2-text-mask';
import { Layout } from '@sppc/env/environment';
//import { TranslateModule, TranslateLoader, TranslateStaticLoader } from "ng2-translate";
import { HotkeyModule } from 'angular2-hotkeys';
//import compress package
import { LZStringModule, LZStringService } from 'ng-lz-string';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA, forwardRef, ErrorHandler, Injector } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS, HttpClient } from '@angular/common/http';
import { NgProgressModule } from '@ngx-progressbar/core';
import { NgProgressHttpModule } from '@ngx-progressbar/http';
import { NgProgressRouterModule } from '@ngx-progressbar/router';
import { AppComponent } from '@sppc/app.component';
import { AppRoutingModule } from '@sppc/app-routing.module';
import { CoreModule } from '@sppc/core/core.module';
import { SharedModule } from '@sppc/shared/shared.module';
import { FinanceModule } from '@sppc/finance/finance.module';
import { AdminModule } from '@sppc/admin/admin.module';
import { ConfigModule } from '@sppc/config/config.module';
import { OrganizationModule } from '@sppc/organization/organization.module';
import { GeneralErrorHandler } from '@sppc/shared/class/error.handler';
import { EnviromentComponent } from '@sppc/shared/class/enviroment.component';
import { ServiceLocator } from './service.locator';

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
    ServiceLocator    
  ],
  schemas: [NO_ERRORS_SCHEMA],
  bootstrap: [AppComponent]
})
export class AppModule {
  
  constructor(private injector: Injector) {    // Create global Service Injector.
    ServiceLocator.injector = this.injector;
  }
}
