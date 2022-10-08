import { Location } from "@angular/common";
import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnInit,
  Renderer2,
} from "@angular/core";
import { DOCUMENT } from "@angular/platform-browser";
import { ActivatedRoute, Router } from "@angular/router";
import { TranslateService } from "@ngx-translate/core";
import { SettingService } from "@sppc/config/service";
import { AuthenticationService, Context } from "@sppc/core";
import { DefaultComponent } from "@sppc/shared/class";
import { DashboardSummaries, TabWidget } from "@sppc/shared/models";
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
import { BehaviorSubject, Subject } from "rxjs";
import { AddWidgetComponent } from "./add-widget/add-widget.component";
import { Dashboard } from "@sppc/shared/models/dashboard";
import { FullAccount } from "@sppc/finance/models";
import { WidgetParameter } from "@sppc/shared/models/widgetParameter";
import { TabWidgetComponent } from "./tab-widget/tab-widget.component";
import { ManageWidgetsComponent } from "./manage-widgets/manage-widgets.component";

interface DashboardConfig extends GridsterConfig {
  draggable: Draggable;
  resizable: Resizable;
  pushDirections: PushDirections;
}

class WidgetTabSubject {
  widgets: Subject<GridsterItem[]>;
  tabId: number;
}

class TabWidgetInfo implements TabWidget {
  tabId: number;
  widgetId: number;
  widgetTitle: string;
  widgetFunctionId: number;
  widgetFunctionName: string;
  widgetTypeId: number;
  widgetTypeName: string;
  widgetDescription: string;
  widgetAccounts: FullAccount[];
  widgetParmeters: WidgetParameter[];
  id: number;
  settings: string;
  defaultSettings: string;
  rowNo: number;
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

  tabSubjects: Array<WidgetTabSubject> = [];

  chart1: Array<GridsterItem>;
  chart2: Array<GridsterItem>;

  isDashboardEditMode: boolean;
  dialogRef: DialogRef;
  dialogModel: any;
  selectedWidgets: Widget[];

  currentDashboard: Dashboard;
  currentDashboardTabIndex: number = 0;
  widgetData: { [id: string]: any } = {};
  widgetOptions: { [id: string]: DashboardConfig } = {};

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

    Chart.defaults.global.defaultFontFamily = "'SPPC'";

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

    values[0] = 5;
    values[1] = 11;
    values[2] = 7;
    values[3] = 4;
    values[4] = 12;

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
  }

  changedOptions(tabId): void {
    this.changeDashboardMode(tabId);
    if (this.widgetOptions[tabId].api)
      this.widgetOptions[tabId].api.optionsChanged();
  }

  changeDashboardMode(tabId) {
    if (this.isDashboardEditMode) {
      this.widgetOptions[tabId].resizable.enabled = true;
      this.widgetOptions[tabId].draggable.enabled = true;
      this.widgetOptions[tabId].displayGrid = DisplayGrid.Always;
    } else {
      this.widgetOptions[tabId].resizable.enabled = false;
      this.widgetOptions[tabId].draggable.enabled = false;
      this.widgetOptions[tabId].displayGrid = DisplayGrid.None;
    }
  }

  onSettingClick() {
    //const currentTab =
    //this.currentDashboard.tabs[this.currentDashboardTabIndex];
    this.goToEditMode();
  }

  goToEditMode() {
    this.isDashboardEditMode = !this.isDashboardEditMode;
    this.currentDashboard.tabs.forEach((tab) => {
      this.changedOptions(tab.id);
    });
  }

  onCancelClick() {
    this.cancelEditMode();
  }

  cancelEditMode() {
    this.isDashboardEditMode = false;
    this.currentDashboard.tabs.forEach((tab) => {
      this.changedOptions(tab.id);
    });
  }

  onOkClick() {
    this.saveDashboard();
    this.cancelEditMode();
  }

  saveDashboard() {
    // this.bStorageService.saveDashboardLayout(
    //   this.dashboard,
    //   this.UserId.toString(),
    //   this.CompanyId.toString()
    // );
    //this.dashboardSubject.next(this.dashboard.filter((w) => w.selected));
  }

  onCloseWidget(widgetId: number) {
    const currentTab =
      this.currentDashboard.tabs[this.currentDashboardTabIndex];
    this.removeWidget(currentTab.id, widgetId);
  }

  ngAfterViewInit() {}

  ngOnInit() {
    this.initDashboard();

    this.settingService.setTitle("Entity.Dashboard");

    this.dashboard = this.bStorageService.loadDashboardLayout(
      this.UserId.toString(),
      this.CompanyId.toString()
    );

    // if (!this.dashboard) {
    //   this.dashboard = [
    //     {
    //       cols: 20,
    //       rows: 20,
    //       y: 0,
    //       x: 0,
    //       id: 1,
    //       selected: false,
    //       name: "GrossSales",
    //       title: "فروش نا خالص",
    //     },
    //     {
    //       cols: 20,
    //       rows: 20,
    //       y: 0,
    //       x: 0,
    //       id: 2,
    //       selected: false,
    //       name: "NetSales",
    //       title: "فروش خالص",
    //     },
    //   ];
    // }

    // this.dashboardSubject.next(this.dashboard.filter((w) => w.selected));
  }

  onTabChange($event) {
    const currentTab =
      this.currentDashboard.tabs[this.currentDashboardTabIndex];
    if (this.widgetOptions[currentTab.id].api)
      this.widgetOptions[currentTab.id].api.optionsChanged();
  }

  onManageWidgetsClick() {
    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.ManageWidgets"),
      content: ManageWidgetsComponent,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add('manage-widgets');
    this.dialogModel = this.dialogRef.content.instance;

    this.dialogRef.dialog.onDestroy(() => {
      this.settingService.setTitle("Entity.Dashboard");
    })
    this.dialogRef.content.instance.close.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  onAddWidgetClick() {
    const currentTab =
      this.currentDashboard.tabs[this.currentDashboardTabIndex];

    if (!this.isDashboardEditMode) this.goToEditMode();

    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.AddWidget"),
      content: AddWidgetComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.selectedWidgets =
      this.currentDashboard.tabs[this.currentDashboardTabIndex].widgets;

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.addNewWidget(res.widget);
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

  onAddTabClick() {
    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.AddTab"),
      content: TabWidgetComponent,
    });

    this.dialogRef.content.instance.save.subscribe((tabName) => {
      debugger;
      let tab: any = {};
      tab.index = this.currentDashboard.tabs.length;
      tab.title = tabName;
      tab.id = this.currentDashboard.tabs.length + 1;
      tab.rowNo = tab.id;
      tab.widgets = [];

      //TODO:posts record to DashboardTab table
      this.currentDashboard.tabs.push(tab);
      this.dialogRef.close();
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe(
      (res) => {
        this.dialogRef.close();
      }
    );
  }

  removeWidget(tabId, widgetId) {
    const currentTab = this.currentDashboard.tabs.filter(
      (t) => t.id == tabId
    )[0];
    const tabWidget = currentTab.widgets.find((w) => w.widgetId == widgetId);

    this.dashboadService
      .removeTabWidget(tabId, tabWidget.widgetId)
      .subscribe(() => {
        const index = currentTab.widgets.findIndex(
          (w) => w.widgetId == widgetId
        );
        currentTab.widgets.splice(index, 1);
        const widgets = this.getWidgetList(currentTab.id);
        this.getWidgetsSubject(currentTab.id).widgets.next(widgets);
      });
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
      minCols: 50,
      maxCols: 100,
      minRows: 50,
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

    this.dashboadService
      .getCurrentDashboard()
      .subscribe((dashboard: Dashboard) => {
        this.currentDashboard = dashboard;
        this.fillDashboardSubjects();
      });
  }

  getWidgets(tabId) {
    if (this.tabSubjects.findIndex((f) => f.tabId == tabId) >= 0) {
      return this.tabSubjects
        .filter((f) => f.tabId == tabId)[0]
        .widgets.asObservable();
    }

    return null;
  }

  removeTab(tabId) {
    //TODO:posts record to DashboardTab table
    const index = this.currentDashboard.tabs.findIndex((t) => t.id == tabId);
    this.currentDashboard.tabs.splice(index, 1);
  }

  getWidgetsSubject(tabId) {
    return this.tabSubjects.filter((f) => f.tabId == tabId)[0];
  }

  addNewWidget(widget) {
    const currentTab =
      this.currentDashboard.tabs[this.currentDashboardTabIndex];

    // const newWidget = {
    //   cols: 20,
    //   rows: 20,
    //   y: 0,
    //   x: 0,
    //   id: widget.id,
    //   title: widget.title,
    //   typeId: widget.typeId,
    // };

    // const promiseWidget = new Promise((resolve) => {
    //this.addNewWidgetToList(currentTab.id);

    let tabWidgetInfo = new TabWidgetInfo();
    tabWidgetInfo.tabId = currentTab.id;
    tabWidgetInfo.widgetId = widget.id;
    const setting = { height: 20, width: 20, x: 0, y: 0 };
    tabWidgetInfo.defaultSettings = JSON.stringify(setting);
    tabWidgetInfo.settings = JSON.stringify(setting);

    this.dashboadService
      .addTabWidget(currentTab.id, tabWidgetInfo)
      .subscribe((newTabWidget: TabWidget) => {
        currentTab.widgets.push(newTabWidget);
        const widgets = this.getWidgetList(currentTab.id);
        this.getWidgetsSubject(currentTab.id).widgets.next(widgets);
      });

    //   resolve(true);
    // });

    // promiseWidget.then(() => {

    // });
  }

  getWidgetData(widgetId, tabId) {
    return this.dashboadService.getWidgetData(widgetId).subscribe((res) => {
      this.widgetData[widgetId + "-" + tabId] = res;
    });
  }

  widgetHasData(widgetId, tabId) {
    if (this.widgetData[widgetId + "-" + tabId]) return true;
    return false;
  }

  // addNewWidgetToList(tabId, newWidget = undefined) {
  //   if (newWidget) {

  //   }

  //   // if (newWidget) {
  //   //   //add post method here to save new widget into db

  //   //   widgets.push(newWidget);
  //   //   this.getWidgetData(newWidget.id, tabId);
  //   // }
  // }

  getWidgetList(tabId) {
    let widgets = [];

    this.currentDashboard.tabs
      .find((t) => t.id == tabId)
      .widgets.forEach((widget) => {
        const setting = JSON.parse(widget.settings);
        debugger;
        widgets.push({
          cols: setting.width,
          rows: setting.height,
          y: setting.y,
          x: setting.x,
          id: widget.widgetId,
          title: widget.widgetTitle,
          typeId: widget.widgetTypeId,
        });

        if (!this.widgetHasData(widget.widgetId, tabId))
          this.getWidgetData(widget.widgetId, tabId);
      });

    return widgets;
  }

  fillDashboardSubjects() {
    let widgets = [];
    if (this.currentDashboard) {
      this.currentDashboard.tabs.forEach((tab) => {
        widgets = this.getWidgetList(tab.id);

        this.widgetOptions[tab.id] = JSON.parse(JSON.stringify(this.options));

        let subject = new BehaviorSubject<Array<GridsterItem>>(widgets);
        let tabSubject = new WidgetTabSubject();
        tabSubject.tabId = tab.id;
        tabSubject.widgets = subject;

        this.tabSubjects.push(tabSubject);
      });
    }
  }
}
