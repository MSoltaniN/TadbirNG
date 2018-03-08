import { Component, OnInit, Renderer2 } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import {  AuthenticationService } from '../../service/login/index';
import { ToastrService } from "toastr-ng2/toastr";
import { DefaultComponent } from "../../class/default.component";
import { TranslateService } from "ng2-translate";
import { MetaDataService } from '../../service/metadata/metadata.service';


@Component({
    selector: 'login-container',
    templateUrl: 'login.container.component.html',
    styleUrls: ['./login.container.component.css']

})


export class LoginContainerComponent extends DefaultComponent implements OnInit {
    
    public step1: boolean = true;
    public step2: boolean = false;

    constructor(
        private route: ActivatedRoute,
        private router: Router,
        private authenticationService: AuthenticationService,
        public toastrService: ToastrService,
        public translate: TranslateService,
        public renderer: Renderer2,
        public metadata: MetaDataService) 
    {
        super(toastrService, translate, renderer, metadata,'');

    }

    ngOnInit() {
        
    }

    
}
