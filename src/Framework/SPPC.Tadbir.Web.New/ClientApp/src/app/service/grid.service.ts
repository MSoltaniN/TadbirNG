import { Injectable } from '@angular/core';
import { BaseService } from '../class/base.service';
import { HttpClient } from '@angular/common/http';




@Injectable()
export class GridService extends BaseService {


    constructor(public http: HttpClient) {
        super(http);
    }


}
