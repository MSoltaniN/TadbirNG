import { Component, OnInit, Renderer2 } from '@angular/core';
import { BrowserStorageService, MetaDataService } from '@sppc/shared/services';
import { Command } from '@sppc/shared/models'
import { UserService } from '@sppc/admin/service';
import { DefaultComponent } from '@sppc/shared/class';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthenticationService } from '@sppc/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { SettingService } from '@sppc/config/service';
import { DialogService } from '@progress/kendo-angular-dialog';


@Component({
  selector: 'app-appheader',
  templateUrl: './appheader.component.html',
  styleUrls: ['./appheader.component.css']
})
export class AppheaderComponent extends DefaultComponent implements OnInit {

  public companyName: string;
  public branchName: string;
  public fiscalPeriodName: string;
  public userName: string;

  public profileItems: Array<Command>; 
  public icons: { [id: string]: string; } = {};

  constructor(public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public userService: UserService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    public dialogService: DialogService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
  }

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
    if (profileMenus == null) {
      this.userService.getDefaultUserCommands(this.Ticket).subscribe((res: Array<Command>) => {        
        this.bStorageService.setProfile(res);
        this.prepareProfileMenus(res);
      });
    }
    else {
      this.prepareProfileMenus(JSON.parse(profileMenus));
    }
  }


  prepareProfileMenus(profileMenus) {   
    this.profileItems = new Array<Command>();
    for (let item of profileMenus) {
      this.profileItems.push(item);
    }
  }
}
