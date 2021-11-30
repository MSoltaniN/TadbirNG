import { ToastrService } from 'ngx-toastr';
import { EnviromentComponent } from '@sppc/shared/class/enviroment.component';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';
import { MessageType, MessagePosition } from '@sppc/shared/enum/metadata';
import { MessageBoxService } from '@sppc/shared/services/message.service';
import { ServiceLocator } from '@sppc/service.locator';
import { Renderer2 } from '@angular/core';


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

  //private elref: ElementRef;

  constructor(public toastrService: ToastrService, public bStorageService: BrowserStorageService) {
    super(bStorageService);

    this.messageBoxService = ServiceLocator.injector.get(MessageBoxService);
    //this.elref = ServiceLocator.injector.get(ElementRef);


    //this.messageBoxService = new MessageBoxService(toastrService);
  } 

  public showMessage(text: string, type: MessageType = MessageType.Info, title: string = '', position: MessagePosition = MessagePosition.TopLeft) {
    this.messageBoxService.showMessage(text, type, title, position);
  }
 

  ////#region Shortcut methods 

  executeClickEvent(className)
  {    
    var activeElement = document.activeElement;
    var elements = activeElement.getElementsByClassName(className);
    if(elements.length > 0)
    {
      var element = <any> elements[0];      
      element.click();
    }
  }

  /** این متد برای اجرا توسط شورت کات های عمومی برنامه نوشته شده است - از جدول ShortcutCommand استفاده میشود */
  exportToExcel()
  {
    this.executeClickEvent('export-excel');    
  }

  openAdvanceFilter()
  {
    this.executeClickEvent('advance-filter');    
  }

  openReportSetting()
  {
    this.executeClickEvent('report-setting');    
  }

  print()
  {
    this.executeClickEvent('print');    
  }

  ////#endregion
 



}
