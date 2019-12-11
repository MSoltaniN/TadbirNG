import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SystemIssue } from '@sppc/finance/models';
import { BaseService } from '@sppc/shared/class';
import { BrowserStorageService } from '@sppc/shared/services';



export class SystemIssueInfo implements SystemIssue {
  id: number;
  parentId?: number;
  permissionId?: number;
  viewId?: number;
  children: SystemIssue[];
  title: string;
  apiUrl: string;
  deleteApiUrl: string;
}

@Injectable()
export class SystemIssueService extends BaseService {

  constructor(public http: HttpClient, public bStorageService: BrowserStorageService) {
    super(http, bStorageService);
  }
}
