import { Component, OnInit, Renderer2 } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from '@ngx-translate/core';
import { AuthenticationService } from '@sppc/core';
import { MetaDataService, BrowserStorageService } from '@sppc/shared';
import { SettingService } from '@sppc/config';




@Component({
  selector: 'login-container',
  templateUrl: 'login.container.component.html',
  styleUrls: ['./login.container.component.css']

})


export class LoginContainerComponent extends DefaultComponent implements OnInit {


  public step1: boolean = false;
  public step2: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
    this.step2 = false;
  }

  ngOnInit() {
    if (this.authenticationService.isRememberMe() || this.UserId != 0) {
      if (this.route.snapshot.queryParams['returnUrl'] == undefined) {
        this.step1 = false;
        this.step2 = true;
      }
      else {
        this.router.navigate(this.route.snapshot.queryParams['returnUrl']);
      }
    }
    else {
      this.step1 = true;
      this.step2 = false;
    }
  }


}
