import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { TransactionLine } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';
import { Filter } from "../class/filter";
import { GridOrderBy } from "../class/grid.orderby";
import { HttpParams } from "@angular/common/http";
import { Environment, MessageType } from "../enviroment";
import { Context } from "../model/context";
import { FullAccount } from '../model/fullaccount';

export class TransactionLineInfo implements TransactionLine {
    constructor(public id: number = 0, public debit: number = 0, public credit: number = 0, public description?: string,
        public fiscalPeriodId: number = 0, public branchId: number = 0, public transactionId: number = 0, public currencyId: number = 0, public currencyName: string = "", public fullAccount: FullAccount = new FullAccount()) {
        //TODO
        //this section written in base class
        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            this.fiscalPeriodId = currentContext ? currentContext.fpId.toString() : '';
            this.branchId = currentContext ? currentContext.branchId.toString() : '';

        }
        //this section written in base class

        this.currencyId = 1;
        this.currencyName = "ریال";

    }
}

@Injectable()
export class TransactionLineService {

    private getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";
    getAccountArticles(accountId: number) {
        var headers = new Headers();

        headers.append("Content-Type", "application/json");

        headers.append("X-Tadbir-AuthTicket", "AAEAAAD/////AQAAAAAAAAAMAgAAAE9TUFBDLlRhZGJpci5JbnRlcmZhY2VzLCBWZXJzaW9uPTEuMC4xNjYuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsDAMAAABOU1BQQy5UYWRiaXIuVmlld01vZGVsLCBWZXJzaW9uPTEuMC4xNjYuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsBQEAAAAjU1BQQy5UYWRiaXIuU2VydmljZS5TZWN1cml0eUNvbnRleHQBAAAAFTxVc2VyPmtfX0JhY2tpbmdGaWVsZAQvU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguVXNlckNvbnRleHRWaWV3TW9kZWwDAAAAAgAAAAkEAAAABQQAAAAvU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguVXNlckNvbnRleHRWaWV3TW9kZWwGAAAAEzxJZD5rX19CYWNraW5nRmllbGQgPFBlcnNvbkZpcnN0TmFtZT5rX19CYWNraW5nRmllbGQfPFBlcnNvbkxhc3ROYW1lPmtfX0JhY2tpbmdGaWVsZBk8QnJhbmNoZXM+a19fQmFja2luZ0ZpZWxkFjxSb2xlcz5rX19CYWNraW5nRmllbGQcPFBlcm1pc3Npb25zPmtfX0JhY2tpbmdGaWVsZAABAQMDAwh+U3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTeXN0ZW0uSW50MzIsIG1zY29ybGliLCBWZXJzaW9uPTQuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjc3YTVjNTYxOTM0ZTA4OV1dflN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbU3lzdGVtLkludDMyLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXagBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTUFBDLlRhZGJpci5WaWV3TW9kZWwuQXV0aC5QZXJtaXNzaW9uQnJpZWZWaWV3TW9kZWwsIFNQUEMuVGFkYmlyLlZpZXdNb2RlbCwgVmVyc2lvbj0xLjAuMTY2LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAAEAAAAKCgkFAAAACQYAAAAJBwAAAAQFAAAAflN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbU3lzdGVtLkludDMyLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBwAACAgICQgAAAAAAAAAAAAAAAEGAAAABQAAAAkJAAAAAQAAAAEAAAAEBwAAAKgBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTUFBDLlRhZGJpci5WaWV3TW9kZWwuQXV0aC5QZXJtaXNzaW9uQnJpZWZWaWV3TW9kZWwsIFNQUEMuVGFkYmlyLlZpZXdNb2RlbCwgVmVyc2lvbj0xLjAuMTY2LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAAZfaXRlbXMFX3NpemUIX3ZlcnNpb24EAAA1U1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguUGVybWlzc2lvbkJyaWVmVmlld01vZGVsW10DAAAACAgJCgAAAAgAAAAIAAAADwgAAAAAAAAACA8JAAAABAAAAAgBAAAAAAAAAAAAAAAAAAAABwoAAAAAAQAAAAgAAAAEM1NQUEMuVGFkYmlyLlZpZXdNb2RlbC5BdXRoLlBlcm1pc3Npb25CcmllZlZpZXdNb2RlbAMAAAAJCwAAAAkMAAAACQ0AAAAJDgAAAAkPAAAACRAAAAAJEQAAAAkSAAAABQsAAAAzU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguUGVybWlzc2lvbkJyaWVmVmlld01vZGVsAgAAABs8RW50aXR5TmFtZT5rX19CYWNraW5nRmllbGQWPEZsYWdzPmtfX0JhY2tpbmdGaWVsZAEACAMAAAAGEwAAAAdBY2NvdW50DwAAAAEMAAAACwAAAAYUAAAAC1RyYW5zYWN0aW9u/wMAAAENAAAACwAAAAYVAAAABFVzZXIHAAAAAQ4AAAALAAAABhYAAAAEUm9sZT8AAAABDwAAAAsAAAAGFwAAABJSZXF1aXNpdGlvblZvdWNoZXJ/AAAAARAAAAALAAAABhgAAAATSXNzdWVSZWNlaXB0Vm91Y2hlcj8AAAABEQAAAAsAAAAGGQAAAAxTYWxlc0ludm9pY2UfAAAAARIAAAALAAAABhoAAAAQUHJvZHVjdEludmVudG9yeQ8AAAAL");

        var url = String.Format(this.getAccountArticlesUrl, accountId.toString());

        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }




    private _getTransactionLinesUrl = Environment.BaseUrl + "/transactions/{0}/details";//transactionId
    ////private _deleteMultiTransactionLinesUrl = Environment.BaseUrl + "/transactions";
    private _getCountUrl = Environment.BaseUrl + "/transactions/{0}/articles/count";
    private _deleteTransactionLineUrl = Environment.BaseUrl + "/transactions/articles/{0}";//articleId
    private _postNewTransactionLineUrl = Environment.BaseUrl + "/transactions/{0}/articles";//transactionId
    private _putModifiedTransactionLineUrl = Environment.BaseUrl + "/transactions/articles/{0}";//articleId

    headers: Headers;
    options: RequestOptions;


    constructor(private http: Http) {

        this.headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });

        //this section written in base class
        var ticket = '';
        if (localStorage.getItem('currentContext') != null) {
            var item: string | null;
            item = localStorage.getItem('currentContext');
            var currentContext = JSON.parse(item != null ? item.toString() : "");

            ticket = currentContext ? currentContext.ticket.toString() : '';
        }

        //this section written in base class

        this.headers.append('X-Tadbir-AuthTicket', ticket);
        this.options = new RequestOptions({ headers: this.headers });
    }



    ////get count of records base on Grid filters and order value
    getCount(transactionId: number, orderby?: string, filters?: any[]) {
        var headers = this.headers;
        var url = String.Format(this._getCountUrl, transactionId);
        var postItem = { filters: filters };
        var searchHeaders = this.headers;
        var postBody = JSON.stringify(postItem);
        var base64Body = btoa(encodeURIComponent(postBody));
        searchHeaders.set('X-Tadbir-GridOptions', base64Body);
        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());;

    }

    search(transactionId: number, start?: number, count?: number, orderby?: string, filters?: Filter[]) {
        var headers = this.headers;

        var gridPaging = { pageIndex: start, pageSize: count };

        var sort = new Array<GridOrderBy>();

        if (orderby) {
            var orderByParts = orderby.split(' ');
            var fieldName = orderByParts[0];
            if (orderByParts[1] != 'undefined')
                sort.push(new GridOrderBy(orderByParts[0], orderByParts[1].toUpperCase()));
        }
        var postItem = { Paging: gridPaging, filters: filters, sortColumns: sort };


        var url = String.Format(this._getTransactionLinesUrl, transactionId);

        var searchHeaders = this.headers;

        var postBody = JSON.stringify(postItem);

        var base64Body = btoa(encodeURIComponent(postBody));

        searchHeaders.set('X-Tadbir-GridOptions', base64Body);

        var options = new RequestOptions({ headers: searchHeaders });

        return this.http.get(url, options)
            .map(response => <any>(<Response>response).json());
    }

    editTransactionLine(transactionLine: TransactionLine): Observable<string> {
        var body = JSON.stringify(transactionLine);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._putModifiedTransactionLineUrl, transactionLine.id);

        return this.http.put(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    insertTransactionLine(transactionId: number, transactionLine: TransactionLine): Observable<string> {
        debugger;
        var body = JSON.stringify(transactionLine);
        var headers = this.headers;
        var options = new RequestOptions({ headers: headers });

        var url = String.Format(this._postNewTransactionLineUrl, transactionId);

        return this.http.post(url, body, options)
            .map(res => res)
            .catch(this.handleError);
    }

    delete(transactionLineId: number): Observable<string> {

        var deleteByIdUrl = String.Format(this._deleteTransactionLineUrl, transactionLineId);

        return this.http.delete(deleteByIdUrl, this.options)
            .map(response => response)
            .catch(this.handleError);
    }

    //// this method comment beacause method in controller not implemented

    //deleteTransactionLines(transactionLines: string[]): Observable<string> {

    //    let body = JSON.stringify(transactionLines);

    //    let headers = this.headers
    //    let options = new RequestOptions({ headers: headers, body: body });

    //    return this.http.delete(this._deleteMultiTransactionLinesUrl, this.options)
    //        .map(response => response.json().message)
    //        .catch(this.handleError);
    //}


    private handleError(error: Response) {
        return Observable.throw(error.json());
    }


}