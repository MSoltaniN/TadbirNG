import { DefaultComponent } from "../../class/default.component";
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from "ng2-translate";
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from '../../service/login/index';
import { Component } from "@angular/core";


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