


import { ToastrService } from "toastr-ng2/toastr";
import { MessageType } from "../enviroment";




export class BaseComponent {

    

    constructor(public toastrService: ToastrService)
    {

    }

    public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '', position: string = 'toast-top-left' )
    {
        switch (type)
        {
            case MessageType.Info:
                this.toastrService.info(text, '', { positionClass: position });
                break;
            case MessageType.Warning:
                this.toastrService.warning(text, '', { positionClass: position });
                break;
            case MessageType.Succes:
                this.toastrService.success(text, '', { positionClass: position });
                break;
        }

    }



}