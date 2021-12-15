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

  selector:string;

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

  //execute click of an element within internal element
  internalExecuteClickEvent(className)
  { 
    var activeElement = document.querySelector(this.selector);
    var elements = activeElement.getElementsByClassName(className);    
    if(elements.length > 0)
    {
      var element = <any> elements[0];      
      if(element.hasAttribute('hidden') || element.hasAttribute('disabled'))
        return;

      element.click();
    }
  }

  //execute click of an element within document
  globalExecuteClickEvent(className)
  {           
    var elements = document.getElementsByClassName(className);    
    if(elements.length > 0)
    {
      var element = <any> elements[0];      
      if(element.hasAttribute('hidden') || element.hasAttribute('disabled'))
        return;

      element.click();
    }
  }

  /** این متد برای اجرا توسط شورت کات های عمومی برنامه نوشته شده است - از جدول ShortcutCommand استفاده میشود */
  sh_exportToExcel()
  {
    this.internalExecuteClickEvent('sh-export-excel');    
  }

  sh_openAdvanceFilter()
  {
    this.internalExecuteClickEvent('sh-advance-filter');    
  }

  sh_openReportSetting()
  {
    this.internalExecuteClickEvent('sh-report-setting');    
  }

  sh_print()
  {
    this.internalExecuteClickEvent('sh-print');    
  }

  sh_openNewDialog()
  {
    this.internalExecuteClickEvent('sh-sh-add-button');    
  }

  sh_openEditDialog()
  {
    this.internalExecuteClickEvent('sh-edit-button');    
  }

  sh_delete()
  {
    this.internalExecuteClickEvent('sh-remove-button');    
  }

  sh_executeFilter()
  {
    this.internalExecuteClickEvent('sh-execute-filter');    
  }

  sh_removeFilter()
  {
    this.internalExecuteClickEvent('sh-remove-filter');    
  }

  sh_newVoucher()
  {
    this.internalExecuteClickEvent('sh-new-voucher');    
  }

  sh_removeVoucher()
  {
    this.internalExecuteClickEvent('sh-remove-voucher');    
  }  

  sh_checkVoucher()
  {
    this.internalExecuteClickEvent('sh-check-voucher');    
  }

  sh_unCheckVoucher()
  {
    this.internalExecuteClickEvent('sh-uncheck-voucher');    
  }

  /** open report management form */
  sh_openReportManager()
  {
    this.globalExecuteClickEvent('sh-38');    
  }

  ////#endregion
 



}
