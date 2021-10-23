import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BaseService } from '@sppc/shared/class/base.service';
import { BrowserStorageService } from '@sppc/shared/services/browserStorage.service';


@Injectable()
export class CharacterService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }

  public replaceBadChars(value:string) : string {
    value = value.replace('ي', 'ی');
    value = value.replace('ك', 'ک');
    return value;
  }
}
