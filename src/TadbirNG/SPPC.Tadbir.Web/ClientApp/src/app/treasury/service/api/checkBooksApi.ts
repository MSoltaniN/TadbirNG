import { environment } from "@sppc/env/environment";

export class CheckBooksApi
{
    /**
     * API client URL for all check book items
     * API client URL for create new checkBook
    */
    public static CheckBooks = environment.BaseUrl + "/check-books";

    /**
     * API client URL for a check book item specified by unique identifier
    */
    public static CheckBook = environment.BaseUrl + "/check-books/{0}";

    /**
     * API server route URL for a check book pages item specified by unique identifier
     * /check-book/{checkBookId:min(1)}
     */
    public static CheckBookPages = environment.BaseUrl + "/check-books/{0}/pages";

    /**
     * API client URL for search check book item specified by number
     */
    public static CheckBookByNo = environment.BaseUrl + "/check-books/by-no/{0}";

    /**
     * API server route URL for undoing a cancelled page specified by identifier
     */
    public static UndoCancelPage = environment.BaseUrl + "/check-books/pages/{0}/cancel/undo";

    /**
     * API server route URL for cancelling a single page specified by identifier
     */
    public static CancelPage = environment.BaseUrl + "check-books/pages/{0}/cancel";

    /**
     * API client URL for delete Pages of check book
     */
    public static DeletePages = environment.BaseUrl + "/check-book/{0}/pages";

    /**
    * API client URL for the first check book in current environment
    */
    public static FirstCheckBook = environment.BaseUrl + "/check-books/first";

    /**
    * API client URL for previous check book in current environment
    */
    public static PreviousCheckBook = environment.BaseUrl + "/check-books/{0}/previous";

    /**
    * API server route URL for previous check book in current environment
    */
    public static PreviousCheckBookUrl = environment.BaseUrl + "/check-books/{issueDate:DateTime}/previous";

    /**
    * API client URL for next check book in current environment
    */
    public static NextCheckBook = environment.BaseUrl + "/check-books/{0}/next";

    /**
    * API server route URL for next check book in current environment
    */
    public static NextCheckBookUrl = environment.BaseUrl + "/check-books/{issueDate:DateTime}/next";

    /**
    * API client URL for the last check book in current environment
    */
    public static LastCheckBook = environment.BaseUrl + "/check-books/last";

}