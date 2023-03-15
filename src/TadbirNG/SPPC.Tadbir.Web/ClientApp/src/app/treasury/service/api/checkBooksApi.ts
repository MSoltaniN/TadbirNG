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
     * and to CreatePages
     * /check-book/{checkBookId:min(1)}
     */
    public static CheckBookPages = environment.BaseUrl + "/check-book/{0}/pages";

    /**
     * API client URL for search check book item specified by unique No
     */
    public static CheckBookByNo = environment.BaseUrl + "/check-book/by-no/{0}";

    /**
     * API client URL for Undo Cancel Page of check book
     */
    public static UndoCancelPage = environment.BaseUrl + "/check-book-Undo-cancel-page/{0}/page";

    /**
     * API client URL for Cancel Page of check book
     */
    public static CancelPage = environment.BaseUrl + "/check-book-cancel-page/{0}/page";

    /**
     * API client URL for delete Pages of check book
     */
    public static DeletePages = environment.BaseUrl + "/check-book/{0}/pages";
}