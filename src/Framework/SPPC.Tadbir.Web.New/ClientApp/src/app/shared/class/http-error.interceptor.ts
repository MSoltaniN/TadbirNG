import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from "@angular/common/http";
import { retry, catchError, map } from 'rxjs/operators';
import { Observable } from "rxjs";
import { ToastrService } from "ngx-toastr";
import { DialogService, DialogCloseResult } from "@progress/kendo-angular-dialog";
import { Router } from "@angular/router";
import { BrowserStorageService } from "../services";
import { TranslateService } from "@ngx-translate/core";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  component: any;
  okButtonTitle: string;
  formTitle: string;

  constructor(public toastrService: ToastrService, private dialogService: DialogService,
    private router: Router, private bStorageService: BrowserStorageService, private translate: TranslateService)
  {
    //this.component = hostElement.nativeElement.__component;
    this.translate.get("Buttons.Ok").subscribe((msg: string) => {
      this.okButtonTitle = msg;
    });

    this.translate.get("Exception.ErrorTitle").subscribe((msg: string) => {
      this.formTitle = msg;
    });
  }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request)
      .pipe(
        retry(0),
        catchError((error: HttpErrorResponse) => {
                    
          if (error.error && error.error.statusCode == 500) {
            // client-side error
            this.toastrService.error(error.error.messages[0]);
            return Observable.throw(undefined);
          }

          if (error.error && error.error.statusCode == 401) {            
            //logout current user
           
            this.bStorageService.removeCurrentContext(); //remove current user from session

            var message = error.error.messages[0];
            var dialogRef = this.dialogService.open({
              title: this.formTitle,
              content: message,
              actions: [{ text: this.okButtonTitle, primary: true }],
              width: 420,
              height: 145,
              minWidth: 250
            });

            dialogRef.result.subscribe(() => {
              this.router.navigate(['/login']);
            });

            return Observable.throw(undefined);
          }

          if (error.error == null && error.status == 401) {            
            return Observable.throw(error);
          }
          //else if (!error.error && error && !this.sharingDataService.supressShowError) {
          //  this.toastrService.error(error.message);
          //  return Observable.throw(error);
          //}
          
          return Observable.throw(error.error);
        })
      )
  }
}
