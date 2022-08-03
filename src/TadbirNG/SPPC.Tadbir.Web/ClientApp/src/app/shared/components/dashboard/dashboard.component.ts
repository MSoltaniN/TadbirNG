import { Location } from "@angular/common";
import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  Renderer2,
} from "@angular/core";
import { DOCUMENT } from '@angular/common';;
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { SettingService } from "@sppc/config/service";
import { AuthenticationService, Context } from "@sppc/core";
import { DefaultComponent } from "@sppc/shared/class";
import { DashboardSummaries } from "@sppc/shared/models";
import {
  BrowserStorageService,
  DashboardService,
  MetaDataService,
} from "@sppc/shared/services";
import { Chart } from "chart.js";
import { ToastrService } from "ngx-toastr";

import { DialogRef, DialogService } from "@progress/kendo-angular-dialog";
import { Widget } from "@sppc/shared/models/widget";
import {
  CompactType,
  DisplayGrid,
  Draggable,
  GridsterConfig,
  GridsterItem,
  GridType,
  PushDirections,
  Resizable,
} from "angular-gridster2";
import { BehaviorSubject } from "rxjs";
import { AddWidgetComponent } from "./add-widget/add-widget.component";

interface DashboardConfig extends GridsterConfig {
  draggable: Draggable;
  resizable: Resizable;
  pushDirections: PushDirections;
}

@Component({
  selector: "app-dashboard",
  templateUrl: "./dashboard.component.html",
  styleUrls: ["./dashboard.component.css"],
})
export class DashboardComponent extends DefaultComponent implements OnInit {
  currentContext?: Context = undefined;

  public showNavbar: boolean = false;

  public isLogin: boolean = false;

  public isRtl: boolean;

  public lang: string = "";

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

  options: DashboardConfig;

  dashboard: Array<GridsterItem> = [];
  dashboardSubject = new BehaviorSubject<Array<GridsterItem>>(this.dashboard);
  widgetList$ = this.dashboardSubject.asObservable();

  chart1: Array<GridsterItem>;
  chart2: Array<GridsterItem>;

  isDashboardEditMode: boolean;
  dialogRef: DialogRef;
  dialogModel: any;
  selectedWidgets: Widget[];

  grossChartData;
  netChartData;

  basicOptions = {
    plugins: {
      legend: {
        labels: {
          color: "#ebedef",
        },
      },
    },
    scales: {
      x: {
        ticks: {
          color: "#ebedef",
        },
        grid: {
          color: "rgba(255,255,255,0.2)",
        },
      },
      y: {
        ticks: {
          color: "#ebedef",
        },
        grid: {
          color: "rgba(255,255,255,0.2)",
        },
      },
    },
  };

  constructor(
    public router: Router,
    location: Location,
    private route: ActivatedRoute,
    public authenticationService: AuthenticationService,
    public toastrService: ToastrService,
    public translate: TranslateService,
    public renderer: Renderer2,
    public metadata: MetaDataService,
    public settingService: SettingService,
    public bStorageService: BrowserStorageService,
    @Inject(DOCUMENT) public document,
    public dashboadService: DashboardService,
    private dialogService: DialogService,
    private chRef: ChangeDetectorRef
  ) {
    super(
      toastrService,
      translate,
      bStorageService,
      renderer,
      metadata,
      settingService,
      "",
      undefined
    );

    this.isDashboardEditMode = false;

    this.currentContext = this.bStorageService.getCurrentUser();

    var language = this.bStorageService.getLanguage();
    if (language) {
      this.lang = language;
    } else {
      this.lang = "fa";
    }

    //#region Hide navbar
    if (this.currentContext != undefined) {
      this.showNavbar = true;
    }

    Chart.defaults.font.family = "'SPPC'";

    if (this.currentContext.fpId > 0 && this.currentContext.branchId > 0) {
      this.dashboadService
        .getDashboardInfo()
        .subscribe((res: DashboardSummaries) => {
          this.cashierBalance = res.cashierBalance;
          this.bankBalance = res.bankBalance;
          this.liquidRatio = res.liquidRatio;
          this.unbalancedVoucherCount = res.unbalancedVoucherCount;
          this.dashboardInfo = res;

          this.drawNetSalesChart();
          this.drawGrossSalesChart();
        });
    }

    //#endregion

    //#region Event in Each Route

    router.events.subscribe((val) => {
      if (
        location.path().toLowerCase() == "/login" ||
        location.path().toString().indexOf("/login?returnUrl=") >= 0
      ) {
        this.showNavbar = false;

        this.isLogin = true;
      } else {
        //#region add class to element

        this.isLogin = false;
        this.showNavbar = true;

        var spacePad = this.document.getElementById("spacePad");
        var currentLang = this.bStorageService.getLanguage();
        if (currentLang == "fa" || currentLang == null) {
          if (spacePad) {
            spacePad.classList.add("pull-right");
            spacePad.classList.remove("pull-left");
          }
        } else {
          if (spacePad) {
            spacePad.classList.add("pull-left");
            spacePad.classList.remove("pull-right");
          }
        }

        //#endregion

        //#region init enviroment variables

        var branchId: number = 0;
        var companyId: number = 0;
        var fpId: number = 0;
        var ticket: string = "";

        //set current route to session
        var currentRoute = this.bStorageService.getCurrentRoute();
        var currentUrl = location.path();

        if (currentRoute && currentRoute != currentUrl)
          this.bStorageService.setPreviousRoute(currentRoute);

        if (currentUrl != "/logout" && currentUrl != "/login")
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

        if (contextIsEmpty) {
          var fps = this.authenticationService.getFiscalPeriod(
            companyId,
            ticket
          );
          if (fps != null) {
            fps.subscribe((res) => {
              //this.fiscalPeriods = res;
              this.fiscalPeriodName = res.filter(
                (p: any) => p.key == fpId
              )[0].value;
            });
          }

          var branchList = this.authenticationService.getBranches(
            companyId,
            ticket
          );
          if (branchList != null) {
            branchList.subscribe((res) => {
              this.branchName = res.filter(
                (p: any) => p.key == branchId
              )[0].value;
            });
          }

          var companiesList = this.authenticationService.getCompanies(
            this.userName,
            ticket
          );
          if (companiesList != null) {
            companiesList.subscribe((res) => {
              this.companyName = res.filter(
                (p: any) => p.key == companyId
              )[0].value;
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
    });

    this.dashboardInfo.netSales.points.forEach(function (value) {
      values.push(value.yValue);
    });

    this.netChartData = {
      labels: labels,
      datasets: [
        {
          label: this.dashboardInfo.netSales.title,
          backgroundColor: "#42A5F5",
          data: values,
        },
      ],
    };
  }

  drawGrossSalesChart() {
    var labels: Array<string> = [];
    var values: Array<number> = [];

    this.dashboardInfo.grossSales.points.forEach(function (value) {
      labels.push(value.xValue);
    });

    this.dashboardInfo.grossSales.points.forEach(function (value) {
      values.push(value.yValue);
    });

    this.grossChartData = {
      labels: labels,
      datasets: [
        {
          label: this.dashboardInfo.grossSales.title,
          backgroundColor: "#42A5F5",
          data: values,
        },
      ],
    };
  }

  changedOptions(): void {
    this.changeDashboardMode();
    this.options.api.optionsChanged();
  }

  changeDashboardMode() {
    if (this.isDashboardEditMode) {
      this.options.resizable.enabled = true;
      this.options.draggable.enabled = true;
      this.options.displayGrid = DisplayGrid.Always;
    } else {
      this.options.resizable.enabled = false;
      this.options.draggable.enabled = false;
      this.options.displayGrid = DisplayGrid.None;
    }
  }

  onSettingClick() {
    this.goToEditMode();
  }

  goToEditMode() {
    this.isDashboardEditMode = !this.isDashboardEditMode;
    this.changedOptions();
  }

  onCancelClick() {
    this.cancelEditMode();
  }

  cancelEditMode() {
    this.isDashboardEditMode = false;
    this.changedOptions();
  }

  onOkClick() {
    this.saveDashboard();
    this.cancelEditMode();
  }

  saveDashboard() {
    this.bStorageService.saveDashboardLayout(
      this.dashboard,
      this.UserId.toString(),
      this.CompanyId.toString()
    );

    this.dashboardSubject.next(this.dashboard.filter((w) => w.selected));
  }

  onCloseWidget(id: number) {
    debugger;
    if (this.dashboard.findIndex((w) => w.id === id) >= 0) {
      this.dashboard.filter((w) => w.id === id)[0].selected = false;
      this.saveDashboard();
    }
  }

  ngAfterViewInit() {}

  ngOnInit() {
    this.initDashboard();

    this.dashboard = this.bStorageService.loadDashboardLayout(
      this.UserId.toString(),
      this.CompanyId.toString()
    );

    if (!this.dashboard) {
      this.dashboard = [
        {
          cols: 20,
          rows: 20,
          y: 0,
          x: 0,
          id: 1,
          selected: false,
          name: "GrossSales",
          title: "فروش نا خالص",
        },
        {
          cols: 20,
          rows: 20,
          y: 0,
          x: 0,
          id: 2,
          selected: false,
          name: "NetSales",
          title: "فروش خالص",
        },
      ];
    }

    this.dashboardSubject.next(this.dashboard.filter((w) => w.selected));
  }

  onAddWidgetClick() {
    this.goToEditMode();

    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.AddWidget"),
      content: AddWidgetComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.selectedWidgets = this.dashboard;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.dashboard = res.widgetList;
      this.saveDashboard();

      this.dialogRef.close();
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
        this.cancelEditMode();
      }
    );
  }

  initDashboard() {
    this.options = {
      gridType: GridType.Fit,
      compactType: CompactType.None,
      margin: 10,
      outerMargin: true,
      outerMarginTop: 5,
      outerMarginRight: 5,
      outerMarginBottom: 5,
      outerMarginLeft: 5,
      useTransformPositioning: true,
      mobileBreakpoint: 200,
      minCols: 40,
      maxCols: 100,
      minRows: 40,
      maxRows: 100,
      maxItemCols: 100,
      minItemCols: 1,
      maxItemRows: 100,
      minItemRows: 1,
      maxItemArea: 500,
      minItemArea: 1,
      defaultItemCols: 1,
      defaultItemRows: 1,
      fixedColWidth: 100,
      fixedRowHeight: 100,
      keepFixedHeightInMobile: false,
      keepFixedWidthInMobile: false,
      scrollSensitivity: 10,
      scrollSpeed: 20,
      enableEmptyCellClick: false,
      enableEmptyCellContextMenu: false,
      enableEmptyCellDrop: false,
      enableEmptyCellDrag: false,
      emptyCellDragMaxCols: 50,
      emptyCellDragMaxRows: 50,
      ignoreMarginInRow: false,
      draggable: {
        enabled: false,
      },
      resizable: {
        enabled: false,
      },
      swap: true,
      pushItems: true,
      disablePushOnDrag: true,
      disablePushOnResize: false,
      pushDirections: { north: true, east: true, south: true, west: true },
      pushResizeItems: true,
      displayGrid: DisplayGrid.None,
      disableWindowResize: false,
      disableWarnings: false,
      scrollToNewItems: false,
    };
  }
}
