import { ToastrService } from "ngx-toastr";
import { MessagePosition, MessageType } from "@sppc/shared/enum/metadata";
import { Injectable } from "@angular/core";

@Injectable()
export class MessageBoxService {
     
  constructor(private toastrService: ToastrService) {
    
  }


  /**
  * show message box on screen
  * @param text is the text of message
  * @param type is type of message like Info,Succes,Warning
  * @param title is title of message window
  * @param position is position of message window in screen
  */
  public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '', position: MessagePosition = MessagePosition.TopLeft) {

    if (text == '' || text == undefined || text == null)
      return;

    var pos: MessagePosition = position;

    var lang = localStorage.getItem('lang');
    if (lang != 'fa')
      pos = MessagePosition.TopRight;

    var posCss = 'toast-top-left'
    switch (pos) {
      case MessagePosition.TopRight:
        posCss = 'toast-top-right';
        break;
      case MessagePosition.TopCenter:
        posCss = 'toast-top-center';
        break;
    }

    switch (type) {
      case MessageType.Info:
        this.toastrService.info(text, title, { positionClass: posCss, enableHtml: true });
        break;
      case MessageType.Warning:
        this.toastrService.warning(text, title, { positionClass: posCss, enableHtml: true });
        break;
      case MessageType.Succes:
        this.toastrService.success(text, title, { positionClass: posCss, enableHtml: true });
        break;
      case MessageType.Error:
        this.toastrService.error(text, title, { positionClass: posCss, enableHtml: true });
        break;
    }

  }  

}
