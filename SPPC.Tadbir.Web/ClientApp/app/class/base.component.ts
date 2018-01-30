


import { ToastrService } from "toastr-ng2/toastr";
import { MessageType } from "../enviroment";




export class BaseComponent {

    

    constructor(public toastrService: ToastrService)
    {

    }

    public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '' )
    {
        switch (type)
        {
            case MessageType.Info:
                this.toastrService.info(text, '', { positionClass: 'toast-top-left' });
                break;
            case MessageType.Warning:
                this.toastrService.warning(text, '', { positionClass: 'toast-top-left' });
                break;
            case MessageType.Succes:
                this.toastrService.success(text, '', { positionClass: 'toast-top-left' });
                break;
        }

    }

}