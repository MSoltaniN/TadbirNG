import { MessageService } from "@progress/kendo-angular-l10n";
import { Error, ErrorType } from "@sppc/shared/models";
import { ToastrService } from "ngx-toastr";
import { Injectable } from "@angular/core";
import { TranslateService } from "@ngx-translate/core";
import { MessageBoxService } from "./message.service";
import { MessageType } from "@sppc/env/environment";

@Injectable()
export class ErrorHandlingService {
  globalErrorMessage: string;
  accessDeniedMsg: string;

  constructor(public messageBoxService: MessageBoxService, public translateService: TranslateService) {
    
    this.translateService.get('Messages.GlobalErrorMessage').subscribe((msg: string) => {
      this.globalErrorMessage = msg;
    });

    this.translateService.get('App.AccessDenied').subscribe((msg: string) => {
      this.accessDeniedMsg = msg;
    });

    
  }

  public handleError(error: Error | any) {
    if (error.statusCode == 400) {
      if (error.type) {
        switch (error.type) {
          case ErrorType.RuntimeException:
            this.messageBoxService.showMessage(error.messages[0], MessageType.Warning);
            break;
          case ErrorType.ValidationError:
            return error.messages;                         
        }
        return;
      }
    }

    if (error.statusCode == 500) {      
      this.messageBoxService.showMessage(error.Message, MessageType.Error);      
      return;
    }

    if (error.status == 401) {      
      return this.accessDeniedMsg;
    }

    if (error && error.type == 'error') {
      return this.globalErrorMessage;      
    }

    return undefined;
  } 

}
