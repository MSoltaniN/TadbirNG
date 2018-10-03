import { Component, OnInit, Input, Injector, ErrorHandler, Injectable, Host, ViewContainerRef } from "@angular/core";
import { RTL } from "@progress/kendo-angular-l10n";


import { ToastrService } from "ngx-toastr";
import { TranslateService } from '@ngx-translate/core';

import { Router } from "@angular/router";
import { SppcLoadingService } from "../controls/sppcLoading";


    
@Injectable()
export class GeneralErrorHandler implements ErrorHandler, OnInit {
    
    @Input() public showError: boolean;
    @Input() public errorCode: string;
    @Input() public errorMessage: string;
    
    constructor(private injector: Injector) { }


    //#region Exception Handling

    handleError(error: any): void {
        const notificationService = this.injector.get(ToastrService);
        const translateService = this.injector.get(TranslateService);
        const sppcLoadingService = this.injector.get(SppcLoadingService);
        const router = this.injector.get(Router);
        

        var errTitle = '';
        translateService.get('Exception.ErrorTitle').subscribe((msg: string) => {
            errTitle = msg;
        });

        var posCss = 'toast-top-center'

        if (error._body) {
            var errorException = JSON.parse(error._body);
            var eCode = errorException.errorDetail.errorCode;
            var eMessage = errorException.message;

            var errCodeLabel = '';
            var errMsgLabel = '';

            translateService.get('Exception.ErrorCode').subscribe((msg: string) => {
                errCodeLabel = msg;
            });

            translateService.get('Exception.ErrorMsg').subscribe((msg: string) => {
                errMsgLabel = msg;
            });



            sppcLoadingService.hide();

            eMessage = eMessage + "<br>" + errCodeLabel + ":" + eCode;

            notificationService.error(eMessage, errTitle, { positionClass: posCss, disableTimeOut: true, enableHtml: true });

            //app.errCode = eCode;
            //app.errMessage = eMessage;
            //app.showErrorDialog = true;

            return;
        }
        // Log the error anyway
        console.error('It happens: ', error);

        //app.errMessage = error.message;

        //app.showErrorDialog = true;
    }

    //#endregion

    ngOnInit(): void {
        
    }
    
    
}
