import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Component } from "@angular/core";
import { AuthenticationService } from "@sppc/core";
import { LicenseService } from '@sppc/shared/services/license.service';
import { String } from '@sppc/shared/class/source';
import { LicenseApi } from '@sppc/shared/services/api/licenseApi';
import { BrowserStorageService } from '@sppc/shared/services';
import { BaseComponent } from '@sppc/shared/class';

@Component({
    selector: 'logout',
    template: '',
})


export class LogoutComponent extends BaseComponent {
    model: any = {};
    loading = false;
    returnUrl: string;
    firstStep: boolean = true;
    ticket: string = '';


    public lang: string = '';
    public stepOne: boolean = true;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService, public toastrService: ToastrService,
        public translate: TranslateService,private licenseService:LicenseService,public bStorageService: BrowserStorageService) {
        
        super(toastrService, bStorageService);
        
        this.bStorageService.deleteUserSettings(this.UserId);

        this.authenticationService.logout();
        this.router.navigate(['/login']);

        this.licenseService.DeleteCurrentSessionAsync(LicenseApi.CurrentSessionUrl).subscribe((res) => {});

        
    }

}
