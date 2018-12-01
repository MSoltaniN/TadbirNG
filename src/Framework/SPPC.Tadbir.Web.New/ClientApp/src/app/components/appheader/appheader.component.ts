import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from '../../service/login/authentication.service';
import { Command } from '../../model/command';
import { SessionKeys } from '../../../environments/environment.prod';

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

  constructor(public authenticationService: AuthenticationService) { 

    var branchId: number = 0;
    var companyId: number = 0;
    var fpId: number = 0;
    var ticket: string = "";

    if (localStorage.getItem('currentContext') != null) {
      var item: string | null;
      item = localStorage.getItem('currentContext');
      var currentContext = JSON.parse(item != null ? item.toString() : "");

      branchId = currentContext ? parseInt(currentContext.branchId) : 0;
      companyId = currentContext ? parseInt(currentContext.companyId) : 0;
      fpId = currentContext ? parseInt(currentContext.fpId) : 0;
      ticket = currentContext ? currentContext.ticket : "";
      this.userName = currentContext ? currentContext.userName.toString() : "";


      
    }
    else if (sessionStorage.getItem('currentContext') != null) {
      var item: string | null;
      item = sessionStorage.getItem('currentContext');
      var currentContext = JSON.parse(item != null ? item.toString() : "");

      branchId = currentContext ? parseInt(currentContext.branchId) : 0;
      companyId = currentContext ? parseInt(currentContext.companyId) : 0;
      fpId = currentContext ? parseInt(currentContext.fpId) : 0;
      this.userName = currentContext ? currentContext.userName.toString() : "";
      ticket = currentContext ? currentContext.ticket : "";

      
    }

    var fps = this.authenticationService.getFiscalPeriod(companyId, ticket);
    if (fps != null) {
      fps.subscribe(res => {
        //this.fiscalPeriods = res;
        this.fiscalPeriodName = res.filter((p: any) => p.key == fpId)[0].value;
      });
    }

    var branchList = this.authenticationService.getBranches(companyId, ticket);
    if (branchList != null) {
      branchList.subscribe(res => {
        this.branchName = res.filter((p: any) => p.key == branchId)[0].value;
      });
    }


    var companiesList = this.authenticationService.getCompanies(this.userName, ticket);
    if (companiesList != null) {
      companiesList.subscribe(res => {
        this.companyName = res.filter((p: any) => p.key == companyId)[0].value;;

      });
    }


    let profileMenus: any;
    if (this.authenticationService.isRememberMe())
        profileMenus = localStorage.getItem(SessionKeys.Profile);
    else
        profileMenus = sessionStorage.getItem(SessionKeys.Profile);

    
    if(profileMenus)
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
