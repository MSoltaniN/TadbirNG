// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version: 1.1.590
//     Template Version: 1.0
//     Generation Date: 03/01/1398 05:21:32 ب.ظ
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

import { environment } from "@sppc/env/environment";

export class MetadataApi {
  // metadata/views
  public static ViewsMetaData = environment.BaseUrl + "/metadata/views";

  // metadata/views/{viewName}
  public static ViewMetadata = environment.BaseUrl + "/metadata/views/{0}";

  // metadata/views/{viewId:min(1)}
  public static ViewMetadataById = environment.BaseUrl + "/metadata/views/{0}";

  // metadata/permissions
  public static PermissionMetadata =
    environment.BaseUrl + "/metadata/permissions";

  // acccollections-cashbank
  public static AccountCollectionsCashBankData =
    environment.BaseUrl + "/acccollections-cashbank";
}
