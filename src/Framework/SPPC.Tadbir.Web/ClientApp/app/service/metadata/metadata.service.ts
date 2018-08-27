import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import "rxjs/Rx";
import { String } from '../../class/source';
import { expect } from 'chai';
import { Filter } from "../../class/filter";
import { GridOrderBy } from "../../class/grid.orderby";
import { HttpParams, HttpClient } from "@angular/common/http";
import { Environment } from "../../enviroment";
import { Context } from "../../model/context";
import { BaseService } from '../../class/base.service';
import { Property } from '../../class/metadata/property';
import { MetadataApi } from '../api/index';

@Injectable()
export class MetaDataService extends BaseService {

    headers: Headers;
    options: RequestOptions;

    constructor(public http: HttpClient) {
        super(http);
    }

    /**
     * return metadata from database for each entity
     * @param entityName is name of entity like 'account' , 'transaction' , ...
     */
    getMetaData(entityName: string) {

        

        //headers = new Headers({ 'Content-Type': 'application/json; charset=utf-8' });

        //headers.append('X-Tadbir-AuthTicket', this.Ticket);

        var options = { headers: this.httpHeaders };

        var url = String.Format(MetadataApi.EntityMetadata, entityName);
        return this.http.get(url, options)
            .map(response => (<Response>response));
    }


    getMetaDataById(entityId: number) {
        var options = { headers: this.httpHeaders };
        
        var options = { headers: this.httpHeaders };
        var url = String.Format(MetadataApi.EntityMetadataById, entityId);
        return this.http.get(url, options)
            .map(response => (<Response>response));
    }


}