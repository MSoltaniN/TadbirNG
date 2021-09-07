import { Component, OnInit } from '@angular/core';
import { BrowserStorageService } from '@sppc/shared/services';
import { Command } from '@sppc/shared/models'


@Component({
  selector: 'app-appheader',
  templateUrl: './appheader.component.html',
  styleUrls: ['./appheader.component.css']
})
export class AppheaderComponent implements OnInit {

  public companyName: string;
  public branchName: string;
  public fiscalPeriodName: string;
  public userName: string;

  public profileItems: Array<Command>;
  menuList: Array<Command> = new Array<Command>();
  public icons: { [id: string]: string; } = {};

  constructor(public bStorageService: BrowserStorageService) {}

  ngOnInit() {
    
    var currentContext = this.bStorageService.getCurrentUser();
    if (currentContext) {
      this.userName = currentContext && currentContext.userName ? currentContext.userName.toString() : "";
      this.fiscalPeriodName = currentContext && currentContext.fiscalPeriodName ? currentContext.fiscalPeriodName.toString() : "";
      this.branchName = currentContext && currentContext.branchName ? currentContext.branchName.toString() : "";
      this.companyName = currentContext && currentContext.companyName ? currentContext.companyName.toString() : "";
    }

    let profileMenus: any;
    profileMenus = this.bStorageService.getProfile();

    if (profileMenus)
      this.menuList = JSON.parse(profileMenus);
    this.profileItems = new Array<Command>();
    for (let item of this.menuList) {     
      this.profileItems.push(item);     
    }

  }

}
