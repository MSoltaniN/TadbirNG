import { environment } from "@sppc/env/environment";

export class CheckBookReportApi {
    /**
    * API client URL for all check book report items
    */
    public static CheckBooksReport = environment.BaseUrl + "/check-book-report";

    /**
    * API server route URL for all check book report items
    */
    public static CheckBooksReportUrl = environment.BaseUrl + "/check-book-report";
}