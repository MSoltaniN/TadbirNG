import { environment } from "@sppc/env/environment";

export class UserValueApi {

    // user-values/categories
    public static Categories = environment.BaseUrl + "/user-values/categories";

    // user-values/categories/{categoryId:min(1)}/values
    public static CategoryValues = environment.BaseUrl + "/user-values/categories/{0}/values";

    // user-values/values
    public static UserValues = environment.BaseUrl + "/user-values/values";

    // user-values/values/{valueId:min(1)}
    public static UserValue = environment.BaseUrl + "/user-values/values/{0}";
}
