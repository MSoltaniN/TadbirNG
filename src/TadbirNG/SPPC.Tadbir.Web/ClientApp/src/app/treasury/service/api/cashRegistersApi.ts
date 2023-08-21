import { environment } from "@sppc/env/environment";

export class CashRegisterApi
{
    // Active cash register items
    public static CashRegisters = environment.BaseUrl + "/cash-registers";

    // Inactive cash register items
    public static InactiveCashRegisters = environment.BaseUrl + "/cash-registers/inactive";

    // All cash register items
    public static AllCashRegisters = environment.BaseUrl + "/cash-registers/all";

    // API client URL for a cash register item specified by unique identifier
    public static CashRegister = environment.BaseUrl + "/cash-registers/{0}";

    // API client URL for all users assigned to the cash register item specified by unique identifier
    public static CashRegisterUsers = environment.BaseUrl + "/cash-registers/{0}/users";

    // cash-registers/{cashRegisterId:min(1)}/deactivate
    public static DeactivateCashRegister = environment.BaseUrl + "/cash-registers/{0}/deactivate";

    // cash-registers/{cashRegisterId:min(1)}/reactivate
    public static ReactivateCashRegister = environment.BaseUrl + "/cash-registers/{0}/reactivate";
}
