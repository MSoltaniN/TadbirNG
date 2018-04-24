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

        this.headers.append('X-Tadbir-AuthTicket', this.Ticket);        

        if(this.CurrentLanguage == "fa")
            this.headers.append('Accept-Language', 'fa-IR,fa');

        if (this.CurrentLanguage == "en")
            this.headers.append('Accept-Language', 'en-US,en');

        this.options = new RequestOptions({ headers: this.headers}); 
        

    }


}