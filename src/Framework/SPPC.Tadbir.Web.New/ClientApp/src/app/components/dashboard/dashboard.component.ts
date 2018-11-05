import { Component, Inject, OnInit, Renderer2,AfterViewInit } from '@angular/core';
import { Context } from '../../model/context';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DOCUMENT } from '@angular/platform-browser';
import { AuthenticationService } from '../../service/login/index';

import { ToastrService } from 'ngx-toastr';

import { TranslateService } from '@ngx-translate/core';

import { SessionKeys } from '../../../environments/environment.prod';
import { DefaultComponent } from '../../class/default.component';
import { MetaDataService } from '../../service/metadata/metadata.service';
import { SettingService,DashboardService } from '../../service/index';
import { Chart, ChartData, Point } from "chart.js";
import { DashboardSummaries } from 'src/app/model/dashboardSummaries';
import { tick } from '@angular/core/testing';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent extends DefaultComponent implements OnInit {

  currentContext?: Context = undefined;

  public showNavbar: boolean = false;

  public isLogin: boolean = false;

  public isRtl: boolean;

  public lang: string = '';

  public companyName: string;
  public branchName: string;
  public fiscalPeriodName: string;
  public userName: string;

  public compenies: any = {};

  public branches: any = {};

  public fiscalPeriods: any = {};
  
  public dashboardInfo: DashboardSummaries;

  public cashierBalance:any;
  public bankBalance:any;
  public liquidRatio:any;
  public unbalancedVoucherCount:any;
  // public netSales:any;
  // public grossSales:any;

  constructor(public router: Router,location: Location,
    private route: ActivatedRoute,
    public authenticationService: AuthenticationService,
    public toastrService: ToastrService,
    public translate: TranslateService,    
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    @Inject(DOCUMENT) public document,
    public dashboadService: DashboardService) {
    super(toastrService, translate, renderer, metadata, settingService,'','');


      

      if (localStorage.getItem('currentContext') != null) {
        var item: string | null;
        item = localStorage.getItem('currentContext');
        this.currentContext = JSON.parse(item != null ? item.toString() : "");
      }
  
      var language = localStorage.getItem('lang');
      if (language) {
        this.lang = language;
      }
      else {
        this.lang = "fa";
  
      }
  

    //#region Hide navbar
    if (this.currentContext != undefined) {
      this.showNavbar = true;
    }

    this.dashboadService.getDashboardInfo().subscribe((res : DashboardSummaries) => {
        
        this.cashierBalance = res.cashierBalance;
        this.bankBalance = res.bankBalance;
        this.liquidRatio = res.liquidRatio;
        this.unbalancedVoucherCount = res.unbalancedVoucherCount;
        // this.grossSales = res.grossSales;
        // this.netSales = res.netSales;
        this.dashboardInfo = res;

        this.drawNetSalesChart();

        this.drawGrossSalesChart();
        
    });

    //#endregion

    //#region Event in Each Route 

    router.events.subscribe((val) => {


      if (location.path().toLowerCase() == '/login' || location.path().toString().indexOf('/login?returnUrl=') >= 0) {
        this.showNavbar = false;

        this.isLogin = true;

      }
      else {

        //#region add class to element

        this.isLogin = false;
        this.showNavbar = true;

        var spacePad = this.document.getElementById('spacePad')
        var currentLang = localStorage.getItem('lang')
        if (currentLang == 'fa' || currentLang == null) {
          if (spacePad) {
            spacePad.classList.add('pull-right');
            spacePad.classList.remove('pull-left');
          }
        }
        else {
          if (spacePad) {
            spacePad.classList.add('pull-left');
            spacePad.classList.remove('pull-right');
          }
        }

        //#endregion

        //#region init enviroment variables

        var branchId: number = 0;
        var companyId: number = 0;
        var fpId: number = 0;
        var ticket: string = "";

        //set current route to session
        var currentUrl = location.path().toLowerCase();
        if (currentUrl != '/logout' && currentUrl != '/login')
          sessionStorage.setItem(SessionKeys.CurrentRoute, currentUrl);


        var contextIsEmpty: boolean = true;

        if (localStorage.getItem('currentContext') != null) {
          var item: string | null;
          item = localStorage.getItem('currentContext');
          var currentContext = JSON.parse(item != null ? item.toString() : "");

          branchId = currentContext ? parseInt(currentContext.branchId) : 0;
          companyId = currentContext ? parseInt(currentContext.companyId) : 0;
          fpId = currentContext ? parseInt(currentContext.fpId) : 0;
          ticket = currentContext ? currentContext.ticket : "";
          this.userName = currentContext ? currentContext.userName.toString() : "";


          contextIsEmpty = false;
        }
        else if (sessionStorage.getItem('currentContext') != null) {
          var item: string | null;
          item = sessionStorage.getItem('currentContext');
          var currentContext = JSON.parse(item != null ? item.toString() : "");

          branchId = currentContext ? parseInt(currentContext.branchId) : 0;
          companyId = currentContext ? parseInt(currentContext.companyId) : 0;
          fpId = currentContext ? parseInt(currentContext.fpId) : 0;
          ticket = currentContext ? currentContext.ticket.toString() : "";
          this.userName = currentContext ? currentContext.userName.toString() : "";


          contextIsEmpty = false;
        }

        if (!contextIsEmpty) {



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

        }

        

        //#endregion
      }
    });

    //#endregion


  }

  canvas: any;
  ctx: any;

  drawNetSalesChart()
  {
    var labels : Array<string> = [];
    var values : Array<number> = [];

     this.dashboardInfo.netSales.points.forEach(function(value){
       labels.push(value.xValue);
     })

     this.dashboardInfo.netSales.points.forEach(function(value){
       values.push(value.yValue);
    })
    
    this.canvas = document.getElementById('netChart');
    this.ctx = this.canvas.getContext('2d');
    let myChart = new Chart(this.ctx, {
      type: 'line',

      data: {
          labels: labels,          
          datasets: [{
              fill:false,
              label: this.dashboardInfo.netSales.title,              
              data: values,
               backgroundColor: [
                   'rgba(255, 99, 132, 1)',
                   'rgba(54, 162, 235, 1)',
                   'rgba(255, 206, 86, 1)'
               ],
              borderWidth: 3              
          }]
      },
      options: {
        responsive: true
      }
    });

  }

  drawGrossSalesChart()
  {
    var labels : Array<string> = [];
    var values : Array<number> = [];

     this.dashboardInfo.grossSales.points.forEach(function(value){
       labels.push(value.xValue);
     })

     this.dashboardInfo.grossSales.points.forEach(function(value){
       values.push(value.yValue);
    })
    
    this.canvas = document.getElementById('grossChart');
    this.ctx = this.canvas.getContext('2d');
    let myChart = new Chart(this.ctx, {
      type: 'bar',
      data: {
          labels: labels,          
          datasets: [{              
              label: this.dashboardInfo.grossSales.title,
              data: values,
              backgroundColor: [
                'rgba(212, 113, 34, 1)',
                'rgba(49, 113, 34, 1)',
                'rgba(30, 206, 86, 1)'
              ],
              borderWidth: 1
          }]
      },
      options: {
        responsive: true        
      }
    });
  }

  ngAfterViewInit() {
    
    
  }

  ngOnInit() {

    
     
  }


  




}
