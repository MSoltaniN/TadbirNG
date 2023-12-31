import { HashLocationStrategy, LocationStrategy } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import {
  ErrorHandler,
  Injector,
  NgModule,
  NO_ERRORS_SCHEMA,
} from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { BrowserModule } from "@angular/platform-browser";
import { LoadingBarModule } from "@ngx-loading-bar/core";
import { LoadingBarHttpClientModule } from "@ngx-loading-bar/http-client";
import { LoadingBarRouterModule } from "@ngx-loading-bar/router";
import { AdminModule } from "@sppc/admin/admin.module";
import { AppRoutingModule } from "@sppc/app-routing.module";
import { AppComponent } from "@sppc/app.component";
import { ConfigModule } from "@sppc/config/config.module";
import { CoreModule } from "@sppc/core/core.module";
import { FinanceModule } from "@sppc/finance/finance.module";
import { OrganizationModule } from "@sppc/organization/organization.module";
import { EnviromentComponent } from "@sppc/shared/class/enviroment.component";
import { GeneralErrorHandler } from "@sppc/shared/class/error.handler";
import { HttpErrorInterceptor } from "@sppc/shared/class/http-error.interceptor";
import { Layout } from "@sppc/shared/enum/metadata";
import { ErrorHandlingService } from "@sppc/shared/services";
import { SharedModule } from "@sppc/shared/shared.module";
import { TextMaskModule } from "angular2-text-mask";
import { ServiceLocator } from "./service.locator";

import { ToastrModule } from "ngx-toastr";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { TreasuryModule } from "./treasury/treasury.module";

@NgModule({
  declarations: [AppComponent],
  imports: [
    CoreModule,
    SharedModule.forRoot(),
    FinanceModule,
    AdminModule,
    ConfigModule,
    OrganizationModule,
    TreasuryModule,
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    FormsModule,
    ReactiveFormsModule,
    TextMaskModule,
    HttpClientModule,
    LoadingBarModule,
    LoadingBarRouterModule,
    LoadingBarHttpClientModule,
    HttpClientModule,
    BrowserAnimationsModule,
    ToastrModule.forRoot(
      {
        preventDuplicates: true,
        toastClass:'toast toastr-rtl'
      })
  ],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy },
    { provide: ErrorHandler, useClass: GeneralErrorHandler },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
    Layout,
    EnviromentComponent,
    ServiceLocator,
    ErrorHandlingService,
  ],
  schemas: [NO_ERRORS_SCHEMA],
  bootstrap: [AppComponent],
})
export class AppModule {
  constructor(private injector: Injector) {
    // Create global Service Injector.
    ServiceLocator.injector = this.injector;
  }
}
