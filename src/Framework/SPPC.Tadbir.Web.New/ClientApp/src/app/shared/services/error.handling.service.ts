import { MessageService } from "@progress/kendo-angular-l10n";
import { Error, ErrorType } from "@sppc/shared/models";
import { ToastrService } from "ngx-toastr";
import { Injectable } from "@angular/core";

@Injectable()
export class ErrorHandlingService  {

  constructor(public toastrService: ToastrService) { }

  public handleError(error: Error | any) {
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
    }

    return undefined;
  } 

}
