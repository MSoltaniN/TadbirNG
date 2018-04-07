


import { ToastrService } from "toastr-ng2/toastr";
import { MessageType, MessagePosition } from "../enviroment";
import { EnviromentComponent } from "./enviroment.component"



export class BaseComponent extends EnviromentComponent {

    

    constructor(public toastrService: ToastrService)
    {
        super();

    }

    /**
     * show message box on screen
     * @param text is the text of message
     * @param type is type of message like Info,Succes,Warning
     * @param title is title of message window
     * @param position is position of message window in screen
     */
    public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '', position : MessagePosition = MessagePosition.TopLeft)
    {
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

        switch (type)
        {
            case MessageType.Info:
                this.toastrService.info(text, title, { positionClass: posCss });
                break;
            case MessageType.Warning:
                this.toastrService.warning(text, title, { positionClass: posCss });
                break;
            case MessageType.Succes:
                this.toastrService.success(text, title, { positionClass: posCss });
                break;
        }

    }

    

}