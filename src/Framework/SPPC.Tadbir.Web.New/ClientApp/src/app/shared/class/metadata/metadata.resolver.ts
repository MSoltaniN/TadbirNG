import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { Injectable } from "@angular/core";
import { String } from '../source';
import { MetaDataService, BrowserStorageService, SessionKeys } from "@sppc/shared";

@Injectable()
export class MetaDataResolver implements Resolve<any> {
  constructor(public bStorageService: BrowserStorageService, private metadataService: MetaDataService) { }

  async resolve(route: ActivatedRouteSnapshot,state: RouterStateSnapshot)
  {
    var viewId = route.data.viewId;
    var lang = this.bStorageService.getLanguage();

    var metadataKey = String.Format(SessionKeys.MetadataKey, viewId ? viewId.toString() : '', lang ? lang : "fa");
    var metadata = this.bStorageService.getMetadata(metadataKey);
    if (metadata == null) {
      //this.metadataService.getMetaDataById(viewId).subscribe((res: any) => {        
      //  this.bStorageService.setMetadata(metadataKey, res.columns);
      //  return
      //});
      const response = await this.metadataService.getMetaDataById(viewId).toPromise();
      this.bStorageService.setMetadata(metadataKey, (<any>response).columns);
    }
  }  

}
