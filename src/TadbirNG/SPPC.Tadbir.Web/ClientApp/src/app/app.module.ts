import { HashLocationStrategy, LocationStrategy } from "@angular/common";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import {
  ErrorHandler,
  Injector,
  NgModule,
  NO_ERRORS_SCHEMA,
} from "@angular/core";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { BrowserModule } from "@angular/platform-browser";
import { NgProgressModule } from "@ngx-progressbar/core";
import { NgProgressHttpModule } from "@ngx-progressbar/http";
import { NgProgressRouterModule } from "@ngx-progressbar/router";
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

@NgModule({
  declarations: [AppComponent],
  imports: [
    CoreModule,
    SharedModule.forRoot(),
    FinanceModule,
    AdminModule,
    ConfigModule,
    OrganizationModule,
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: "ng-cli-universal" }),
    HttpModule,
    FormsModule,
    ReactiveFormsModule,
    TextMaskModule,
    HttpClientModule,
    NgProgressModule.forRoot({
      direction: "ltr+",
      spinnerPosition: "left",
      color: "white",
      thick: true,
      meteor: false,
    }),
    NgProgressRouterModule,
    NgProgressHttpModule,
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
