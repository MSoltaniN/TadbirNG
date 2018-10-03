import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs/Subscription';
import { SppcLoadingService } from './sppc-loading.service';

@Component({
    selector: 'app-sppc-loading',
    template: `<div class="sppc-spinner" *ngIf="showLoading">
  <div class="sk-cube-grid">
    <div class="sk-cube sk-cube1"></div>
    <div class="sk-cube sk-cube2"></div>
    <div class="sk-cube sk-cube3"></div>
    <div class="sk-cube sk-cube4"></div>
    <div class="sk-cube sk-cube5"></div>
    <div class="sk-cube sk-cube6"></div>
    <div class="sk-cube sk-cube7"></div>
    <div class="sk-cube sk-cube8"></div>
    <div class="sk-cube sk-cube9"></div>
    </div>
</div>`,
    styles: ['.sppc-spinner{ background-color:rgba(0, 0, 0, .6) ;position: fixed;top: 0; bottom: 0;right: 0;left: 0; z-index:100010']
})
export class SppcLoadingComponent implements OnInit {

    subscription: Subscription;
    public showLoading = false;


    constructor(private sppcLoading: SppcLoadingService) {

        this.createServiceSubscription();
    }

    ngOnInit() {
    }

    createServiceSubscription() {
        
        this.subscription =
            this.sppcLoading.getMessage().subscribe(show => {


                if (show) {
                    this.showLoading = true;
                } else {
                    this.showLoading = false;
            }

            });
    }


}
