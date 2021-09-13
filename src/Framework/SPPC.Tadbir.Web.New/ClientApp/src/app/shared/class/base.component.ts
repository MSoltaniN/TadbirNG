import { ToastrService } from 'ngx-toastr';
import { EnviromentComponent } from '@sppc/shared/class/enviroment.component';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { MessageType, MessagePosition } from '@sppc/shared/enum/metadata';
import { MessageBoxService } from '@sppc/shared/services/message.service';
import { ServiceLocator } from '@sppc/service.locator';


export class BaseComponent extends EnviromentComponent {

  /** this message show after update command */
  public updateMsg: string;

  /** this message show after insert command */
  public insertMsg: string;

  /** this message show after delete command */
  public deleteMsg: string;

  /** this message show in confirm messagebox */
  public deleteConfirmMsg: string;

  private messageBoxService: MessageBoxService;

  constructor(public toastrService: ToastrService, public bStorageService: BrowserStorageService) {
    super(bStorageService);

    this.messageBoxService = ServiceLocator.injector.get(MessageBoxService);
    //this.messageBoxService = new MessageBoxService(toastrService);
  } 

  public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '', position: MessagePosition = MessagePosition.TopLeft) {
    this.messageBoxService.showMessage(text, type, title, position);
  }
 


  


}
