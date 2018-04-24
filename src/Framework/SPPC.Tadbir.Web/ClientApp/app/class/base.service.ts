import { EnviromentComponent } from "./enviroment.component";
import { RequestOptions } from "@angular/http";
import { Headers } from '@angular/http';

export class BaseService extends EnviromentComponent {


    public headers: Headers | undefined | null;
    public options: RequestOptions | undefined;
    
    constructor()
    {
        super();

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });

        var ticket: string = '';
        if (localStorage.getItem('currentContext')) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.ticket != '') {
                ticket = currentContext.ticket;
            }
        }
        else if (sessionStorage.getItem('currentContext')) {
            var item: string | null;
            item = sessionStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");
            if (currentContext.userName != '') {
                ticket = currentContext.ticket;
            }
        }
        
        this.headers.append('X-Tadbir-AuthTicket', ticket);        

        if(this.CurrentLanguage == "fa")
            this.headers.append('Accept-Language', 'fa-IR,fa');

        if (this.CurrentLanguage == "en")
            this.headers.append('Accept-Language', 'en-US,en');

        this.options = new RequestOptions({ headers: this.headers}); 
        

    }


}