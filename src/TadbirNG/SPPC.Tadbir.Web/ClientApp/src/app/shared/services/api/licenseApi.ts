// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.772
//     Template Version: 1.0
//     Generation Date: 2021-09-02 9:47:12 PM
//  
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class LicenseApi {

  public static UserLicenseUrl = environment.LicenseServerUrl + "/license/users/{0}";

  public static OnlineUserLicenseUrl = environment.LicenseServerUrl + "/license/users/{0}/online";

  public static ActivateLicenseUrl = environment.LicenseServerUrl + "/license/activate";

}
