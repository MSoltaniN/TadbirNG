import { Component, OnInit } from '@angular/core';
import { Command } from '../../model/command';
import { BrowserStorageService } from '../../service/browserStorage.service';

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

  constructor(public bStorageService: BrowserStorageService) {

    var branchId: number = 0;
    var companyId: number = 0;
    var fpId: number = 0;
    var ticket: string = "";

    var currentContext = this.bStorageService.getCurrentUser();
    if (currentContext) {
      branchId = currentContext ? currentContext.branchId : 0;
      companyId = currentContext ? currentContext.companyId : 0;
      fpId = currentContext ? currentContext.fpId : 0;
      ticket = currentContext ? currentContext.ticket : "";
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
      // if (parent.id == 15)
      // {

      //for (let item of parent.children) {
      this.profileItems.push(item);
      //}
      //}
    }

  }

  ngOnInit() {
  }

}
