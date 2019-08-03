import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Component } from "@angular/core";
import { AuthenticationService } from "@sppc/core";


@Component({
    selector: 'logout',
    template: '',
})


export class LogoutComponent {
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
        public translate: TranslateService) {
        
        this.authenticationService.logout();
        this.router.navigate(['/login']);

    }

}
