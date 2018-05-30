import { EnviromentComponent } from "./enviroment.component";
import { RequestOptions, Http, Response } from "@angular/http";
import { Headers } from '@angular/http';
import { Filter } from "./filter";
import { GridOrderBy } from "./grid.orderby";
import { String } from '../class/source';
import { Observable } from "rxjs/Observable";

export class BaseService extends EnviromentComponent {

    public headers: Headers | undefined | null;
    public options: RequestOptions | undefined;

    constructor(public http: Http) {
        super();

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });

        this.headers.append('X-Tadbir-AuthTicket', this.Ticket);

        if (this.CurrentLanguage == "fa")
            this.headers.append('Accept-Language', 'fa-IR,fa');

        if (this.CurrentLanguage == "en")
            this.headers.append('Accept-Language', 'en-US,en');

        this.options = new RequestOptions({ headers: this.headers });


    }

    /**
     * لیست رکوردها بر اساس فیلتر و مرتب سازی
     * @param apiUrl آدرس‌ کامل api
     * @param start شماره شروع رکورد
     * @param count تعداد رکورد
     * @param orderby مرتب سازی
     * @param filters فیلتر
     */
    public getAll(apiUrl: string, start?: number, count?: number, orderby?: string, filters?: Filter[]) {
        var gridPaging = { pageIndex: start, pageSize: count };
        var sort = new Array<GridOrderBy>();
        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response));
    }

    /**
     * گرفتن رکورد با استفاده از id رکورد
     * @param apiUrl آدرس کامل api
     * @param modelId شماره id رکورد
     */
    public getById(apiUrl: string) {
        var options = new RequestOptions({ headers: this.headers });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

    /**
     * ایجاد رکورد جدید
     * @param apiUrl آدرس کامل api
     * @param model رکورد جدید برای افزودن
     */
    public insert<T>(apiUrl: string, model: T): Observable<string> {
        var body = JSON.stringify(model);
        return this.http.post(apiUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    /**
     * ویرایش رکورد
     * @param apiUrl آدرس کامل api
     * @param model رکورد برای ویرایش
     * @param modelId شماره id مدل
     */
    public edit<T>(apiUrl: string, model: T): Observable<string> {
        var body = JSON.stringify(model);
        return this.http.put(apiUrl, body, this.options)
            .map(res => res)
            .catch(this.handleError);
    }

    /**
     * حذف رکورد جاری
     * @param apiUrl آدرس کامل api
     * @param modelId شماره id رکورد
     */
    public delete(apiUrl: string): Observable<string> {
        return this.http.delete(apiUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    /**
     * حذف گروهی
     * @param apiUrl آدرس api
     * @param models رکوردها
     */
    public groupDelete(apiUrl: string,models: string[]): Observable<string> {
        var modelId: string = '';
        let modelArray: Array<number> = Array();
        for (var i = 0; i < models.length; i++) {
            var modelId = models[i].split(' ')[0];
            modelArray.push(parseInt(modelId));
        }
        let body = JSON.stringify({ paraph: '', items: modelArray });
        return this.http.put(apiUrl, body, this.options)
            .map(response => response)
            .catch(this.handleError);
    }
    /**
     * تعداد رکورد بر اساس فیلتر و مرتب سازی
     * @param apiUrl آدرس کامل api
     * @param orderby مرتب سازی
     * @param filters فیلترها
     */
    public getCount(apiUrl: string, orderby?: string, filters?: any[]) {
        var headers = this.headers;
        var postItem = { filters: filters };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        if (searchHeaders)
            searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });
        return this.http.get(apiUrl, options)
            .map(response => <any>(<Response>response).json());
    }

    /**
     * تعداد کل رکوردها
     * @param apiUrl آدرس api
     */
    public getTotalCount(apiUrl: string) {
        var url = String.Format(apiUrl, this.FiscalPeriodId, this.BranchId);
        return this.http.get(url, this.options)
            .map(response => <any>(<Response>response).json());;
    }

    /**
     * 
     * @param error
     */
    public handleError(error: Response) {
        return Observable.throw(error.json());
    }
}