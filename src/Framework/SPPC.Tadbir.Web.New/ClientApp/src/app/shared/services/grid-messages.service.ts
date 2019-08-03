import { MessageService } from '@progress/kendo-angular-l10n';
import { TranslateService } from '@ngx-translate/core';
import { Injectable } from "@angular/core";
import { SppcMessageService } from '@sppc/shared';


    
@Injectable()
export class GridMessageService extends MessageService {

    private messageService: SppcMessageService

    constructor(private translate: TranslateService) {
        super()   
        this.messageService = new SppcMessageService(translate);
    }

    
    
    
    get(key: string): string | undefined {
        
        var result: string = "";
        
        result = this.messageService.getText(key.split('.')[2])
                  
        return result;
    }
   
}
