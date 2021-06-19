import { MessageService } from "@progress/kendo-angular-l10n";
import { Error, ErrorType } from "@sppc/shared/models";
import { ToastrService } from "ngx-toastr";
import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";

@Injectable()
export class ErrorHandlingService {
  globalErrorMessage: string;
  constructor(public toastrService: ToastrService, public translateService: TranslateService) {
    
    this.translateService.get('Messages.GlobalErrorMessage').subscribe((msg: string) => {
      this.globalErrorMessage = msg;
    });
  }

  public handleError(error: Error | any) {
    debugger;
    if (error.statusCode == 400) {
      if (error.type) {
        switch (error.type) {
          case ErrorType.RuntimeException:
            this.toastrService.warning(error.messages[0]);
            break;
          case ErrorType.ValidationError:
            return error.messages;                         
        }
        return;
      }
    }

    if (error.statusCode == 500) {
      this.toastrService.error(error.Message);
      return;
    }

    if (error && error.type == 'error') {
      return this.globalErrorMessage;      
    }

    return undefined;
  } 

}
