import { environment } from "@sppc/env/environment";

export class CashRegisterApi
{
    // API client URL for all cash register items
    public static AllCashRegisters = environment.BaseUrl + "/cash-registers";

    // API client URL for a cash register item specified by unique identifier
    public static CashRegister = environment.BaseUrl + "/cash-registers/{0}";

    // API client URL for all users assigned to the cash register item specified by unique identifier
    public static CashRegisterUsers = environment.BaseUrl + "/cash-registers/{0}/users";
}
