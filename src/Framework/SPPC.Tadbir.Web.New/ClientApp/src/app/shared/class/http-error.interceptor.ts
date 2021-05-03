import { Injectable, Injector, Host, Renderer2, RendererFactory2, ElementRef, Inject, forwardRef } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HttpResponse } from "@angular/common/http";
//import { throwError } from 'rxjs/observable/throw';
import { retry, catchError, map } from 'rxjs/operators';
import { ErrorHandlingService } from "../services";
import { Observable } from "rxjs";
import { DefaultComponent } from "./default.component";
import { DetailComponent } from "./detail.component";
import { ToastrService } from "ngx-toastr";

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  component: any;

  constructor(public toastrService: ToastrService)
  {
    //this.component = hostElement.nativeElement.__component;
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
          
          return Observable.throw(error.error);
        })
      )
  }
}
