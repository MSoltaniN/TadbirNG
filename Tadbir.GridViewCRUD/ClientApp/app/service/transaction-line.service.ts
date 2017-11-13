import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { TransactionLine } from '../model/index';
import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../class/source';
import { expect } from 'chai';

export class TransactionLineInfo implements TransactionLine {
    constructor(public id: number = 0, public debit: number = 0,
        public credit: number = 0, public description?: string)
    { }
}

@Injectable()
export class TransactionLineService {
    
    private getAccountArticlesUrl = "http://37.59.93.7:8080/accounts/{0}/articles";    

    constructor(private http: Http) {
        
    }


    getAccountArticles(accountId: number) {
        var headers = new Headers();
        
        headers.append("Content-Type", "application/json");
        
        headers.append("X-Tadbir-AuthTicket", "AAEAAAD/////AQAAAAAAAAAMAgAAAE9TUFBDLlRhZGJpci5JbnRlcmZhY2VzLCBWZXJzaW9uPTEuMC4xNjYuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsDAMAAABOU1BQQy5UYWRiaXIuVmlld01vZGVsLCBWZXJzaW9uPTEuMC4xNjYuMCwgQ3VsdHVyZT1uZXV0cmFsLCBQdWJsaWNLZXlUb2tlbj1udWxsBQEAAAAjU1BQQy5UYWRiaXIuU2VydmljZS5TZWN1cml0eUNvbnRleHQBAAAAFTxVc2VyPmtfX0JhY2tpbmdGaWVsZAQvU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguVXNlckNvbnRleHRWaWV3TW9kZWwDAAAAAgAAAAkEAAAABQQAAAAvU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguVXNlckNvbnRleHRWaWV3TW9kZWwGAAAAEzxJZD5rX19CYWNraW5nRmllbGQgPFBlcnNvbkZpcnN0TmFtZT5rX19CYWNraW5nRmllbGQfPFBlcnNvbkxhc3ROYW1lPmtfX0JhY2tpbmdGaWVsZBk8QnJhbmNoZXM+a19fQmFja2luZ0ZpZWxkFjxSb2xlcz5rX19CYWNraW5nRmllbGQcPFBlcm1pc3Npb25zPmtfX0JhY2tpbmdGaWVsZAABAQMDAwh+U3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTeXN0ZW0uSW50MzIsIG1zY29ybGliLCBWZXJzaW9uPTQuMC4wLjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49Yjc3YTVjNTYxOTM0ZTA4OV1dflN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbU3lzdGVtLkludDMyLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXagBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTUFBDLlRhZGJpci5WaWV3TW9kZWwuQXV0aC5QZXJtaXNzaW9uQnJpZWZWaWV3TW9kZWwsIFNQUEMuVGFkYmlyLlZpZXdNb2RlbCwgVmVyc2lvbj0xLjAuMTY2LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAAEAAAAKCgkFAAAACQYAAAAJBwAAAAQFAAAAflN5c3RlbS5Db2xsZWN0aW9ucy5HZW5lcmljLkxpc3RgMVtbU3lzdGVtLkludDMyLCBtc2NvcmxpYiwgVmVyc2lvbj00LjAuMC4wLCBDdWx0dXJlPW5ldXRyYWwsIFB1YmxpY0tleVRva2VuPWI3N2E1YzU2MTkzNGUwODldXQMAAAAGX2l0ZW1zBV9zaXplCF92ZXJzaW9uBwAACAgICQgAAAAAAAAAAAAAAAEGAAAABQAAAAkJAAAAAQAAAAEAAAAEBwAAAKgBU3lzdGVtLkNvbGxlY3Rpb25zLkdlbmVyaWMuTGlzdGAxW1tTUFBDLlRhZGJpci5WaWV3TW9kZWwuQXV0aC5QZXJtaXNzaW9uQnJpZWZWaWV3TW9kZWwsIFNQUEMuVGFkYmlyLlZpZXdNb2RlbCwgVmVyc2lvbj0xLjAuMTY2LjAsIEN1bHR1cmU9bmV1dHJhbCwgUHVibGljS2V5VG9rZW49bnVsbF1dAwAAAAZfaXRlbXMFX3NpemUIX3ZlcnNpb24EAAA1U1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguUGVybWlzc2lvbkJyaWVmVmlld01vZGVsW10DAAAACAgJCgAAAAgAAAAIAAAADwgAAAAAAAAACA8JAAAABAAAAAgBAAAAAAAAAAAAAAAAAAAABwoAAAAAAQAAAAgAAAAEM1NQUEMuVGFkYmlyLlZpZXdNb2RlbC5BdXRoLlBlcm1pc3Npb25CcmllZlZpZXdNb2RlbAMAAAAJCwAAAAkMAAAACQ0AAAAJDgAAAAkPAAAACRAAAAAJEQAAAAkSAAAABQsAAAAzU1BQQy5UYWRiaXIuVmlld01vZGVsLkF1dGguUGVybWlzc2lvbkJyaWVmVmlld01vZGVsAgAAABs8RW50aXR5TmFtZT5rX19CYWNraW5nRmllbGQWPEZsYWdzPmtfX0JhY2tpbmdGaWVsZAEACAMAAAAGEwAAAAdBY2NvdW50DwAAAAEMAAAACwAAAAYUAAAAC1RyYW5zYWN0aW9u/wMAAAENAAAACwAAAAYVAAAABFVzZXIHAAAAAQ4AAAALAAAABhYAAAAEUm9sZT8AAAABDwAAAAsAAAAGFwAAABJSZXF1aXNpdGlvblZvdWNoZXJ/AAAAARAAAAALAAAABhgAAAATSXNzdWVSZWNlaXB0Vm91Y2hlcj8AAAABEQAAAAsAAAAGGQAAAAxTYWxlc0ludm9pY2UfAAAAARIAAAALAAAABhoAAAAQUHJvZHVjdEludmVudG9yeQ8AAAAL");
        
        var url = String.Format(this.getAccountArticlesUrl, accountId.toString());

        return this.http.get(url, { headers: headers })
            .map(response => <any>(<Response>response).json());
    }


}