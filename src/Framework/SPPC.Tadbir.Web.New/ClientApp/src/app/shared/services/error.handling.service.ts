import { MessageService } from "@progress/kendo-angular-l10n";
import { Error, ErrorType } from "@sppc/shared/models";
import { ToastrService } from "ngx-toastr";
import { Injectable } from "@angular/core";

@Injectable()
export class ErrorHandlingService  {

  constructor(public toastrService: ToastrService) { }

  public handleError(error: Error | any) {
    if (error.type) {
      switch (error.type) {
        case ErrorType.Info:
          this.toastrService.info(error.messages[0]);
          break;
        case ErrorType.Warning:
          this.toastrService.warning(error.messages[0]);
          break;
        case ErrorType.RuntimeException:
          this.toastrService.error(error.messages[0]);
          break;
        case ErrorType.ValidationError:
          return error.messages;
      }
      return;
    }

    if (error.ClassName) {
      this.toastrService.error(error.Message);
    }

    return undefined;
  } 

}
