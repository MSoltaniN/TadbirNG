import { Component, Inject, OnInit, Renderer2 } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { DOCUMENT } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Chart } from "chart.js";
import { MetaDataService, BrowserStorageService, DashboardService } from '@sppc/shared/services';
import { Context, AuthenticationService } from '@sppc/core';
import { SettingService } from '@sppc/config/service';
import { DefaultComponent } from '@sppc/shared/class';
import { DashboardSummaries } from '@sppc/shared/models';




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

  public cashierBalance: any;
  public bankBalance: any;
  public liquidRatio: any;
  public unbalancedVoucherCount: any;
  // public netSales:any;
  // public grossSales:any;

  constructor(public router: Router, location: Location,
    private route: ActivatedRoute,
    public authenticationService: AuthenticationService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    @Inject(DOCUMENT) public document,
    public dashboadService: DashboardService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);

    this.currentContext = this.bStorageService.getCurrentUser();

    var language = this.bStorageService.getLanguage();
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

    Chart.defaults.global.defaultFontFamily = "'SPPC'";

    this.dashboadService.getDashboardInfo().subscribe((res: DashboardSummaries) => {

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
        var currentLang = this.bStorageService.getLanguage();
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
          this.bStorageService.setCurrentRoute(currentUrl);


        var contextIsEmpty: boolean = true;

        var currentContext = this.bStorageService.getCurrentUser();

        if (currentContext) {
          branchId = currentContext.branchId;
          companyId = currentContext.companyId;
          fpId = currentContext.fpId;
          ticket = currentContext.ticket;
          this.userName = currentContext.userName;
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

  drawNetSalesChart() {
    var labels: Array<string> = [];
    var values: Array<number> = [];

    this.dashboardInfo.netSales.points.forEach(function (value) {
      labels.push(value.xValue);
    })

    this.dashboardInfo.netSales.points.forEach(function (value) {
      values.push(value.yValue);
    })


    this.canvas = document.getElementById('netChart');
    this.ctx = this.canvas.getContext('2d');
    let myChart = new Chart(this.ctx, {
      type: 'line',

      data: {
        labels: labels,
        datasets: [{
          fill: false,
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
        responsive: true,
        scales: {
          yAxes: [
            {
              ticks: {
                callback: function (label, index, labels) {
                  return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
                },
              }
            }
          ]
        },
        tooltips: {
          mode: 'index',
          intersect: false,
          callbacks: {
            label: function (t, d) {
              return t.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
            }
          }
        }
      }
    });


  }

  drawGrossSalesChart() {
    var labels: Array<string> = [];
    var values: Array<number> = [];

    this.dashboardInfo.grossSales.points.forEach(function (value) {
      labels.push(value.xValue);
    })

    this.dashboardInfo.grossSales.points.forEach(function (value) {
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
          borderWidth: 1
        }]
      },
      options: {
        responsive: true,
        scales: {
          yAxes: [
            {
              ticks: {
                callback: function (label, index, labels) {
                  return label.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
                },
              }
            }
          ]
        },
        tooltips: {
          mode: 'index',
          intersect: false,
          callbacks: {
            label: function (t, d) {
              return t.yLabel.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");;
            }
          }
        }
      },

    });


  }

  ngAfterViewInit() {


  }

  ngOnInit() {



  }







}
