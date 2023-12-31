import { Location } from "@angular/common";
import {
  ChangeDetectorRef,
  Component,
  Inject,
  OnDestroy,
  OnInit,
  Renderer2,
  ViewChild,
} from "@angular/core";
import { DOCUMENT } from "@angular/common";
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
  GridsterComponent,
  GridsterConfig,
  GridsterItem,
  GridType,
  PushDirections,
  Resizable,
} from "angular-gridster2";
import { BehaviorSubject, Subject, Subscription } from "rxjs";
import { AddWidgetComponent } from "./add-widget/add-widget.component";
import { Dashboard } from "@sppc/shared/models/dashboard";
import { FullAccount } from "@sppc/finance/models";
import { WidgetParameter } from "@sppc/shared/models/widgetParameter";
import { TabWidgetComponent } from "./tab-widget/tab-widget.component";
import { ManageWidgetsComponent } from "./manage-widgets/manage-widgets.component";
import { TabView } from "primeng/tabview";
import { SerieItem } from "@sppc/shared/models/serieItem";
import { WidgetSetting } from "@sppc/shared/models/widgetSetting";
import { ChartService } from "@sppc/shared/services/chart.service";
import { take } from "rxjs/operators";
import { MessageType } from "@sppc/shared/enum/metadata";
import * as echarts from "echarts";
import { DashboardPermissions } from "@sppc/shared/security";
import { WidgetService } from "./services/widget.service";

interface DashboardConfig extends GridsterConfig {
  draggable: Draggable;
  resizable: Resizable;
  pushDirections: PushDirections;
}

class WidgetTabSubject {
  widgets: Subject<GridsterItem[]>;
  tabId: number;
  hasData: boolean;
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
export class DashboardComponent
  extends DefaultComponent
  implements OnInit, OnDestroy
{
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
  private editInlineTabName: number = 0;

  options: DashboardConfig;

  dashboard: Array<GridsterItem> = [];
  dashboardSubject = new BehaviorSubject<Array<GridsterItem>>(this.dashboard);
  widgetList$ = this.dashboardSubject.asObservable();
  tabTitle: string;
  tabSubjects: Array<WidgetTabSubject> = [];

  isDashboardEditMode: boolean;
  dialogRef: DialogRef;
  dialogModel: any;
  selectedWidgets: Widget[];

  @ViewChild(TabView) tabContainer: TabView;
  currentDashboard: Dashboard;

  currentDashboardTabIndex: number = 0;

  get currentDashboardTab() {
    const tab = this.tabContainer.tabs.filter((t) => t.selected)[0];
    return this.currentDashboard.tabs.filter(
      (t) => t.id == tab.viewContainer.element.nativeElement.id
    )[0];
  }

  widgetStatus: { [id: string]: any } = {};
  widgetData: { [id: string]: any } = {};
  widgetOptions: { [id: string]: DashboardConfig } = {};
  widgetSettings: { [id: string]: WidgetSetting } = {};
  widgets: { [id: string]: GridsterItem[] } = {};

  @ViewChild(GridsterComponent) gridSter: GridsterComponent;

  grossChartData;
  netChartData;
  manageWidgets = DashboardPermissions.ManageWidgets;
  refreshWidgets: boolean;

  subscription: Subscription;

  basicOptions: any = {
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
    public dashboardService: DashboardService,
    private dialogService: DialogService,
    private chRef: ChangeDetectorRef,
    private chartService: ChartService,
    private widgetService:WidgetService
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

    this.subscription = this.chartService.widgetToRefresh$.subscribe((r) => {
      this.refreshDashboard();
    });

    if (this.currentContext.fpId > 0 && this.currentContext.branchId > 0) {
      this.dashboardService
        .getCurrentDashboard()
        .pipe(take(2))
        .subscribe((res: DashboardSummaries) => {
          if (res) {
            this.cashierBalance = res.cashierBalance;
            this.bankBalance = res.bankBalance;
            this.liquidRatio = res.liquidRatio;
            this.unbalancedVoucherCount = res.unbalancedVoucherCount;
            this.dashboardInfo = res;
          }
          // this.drawNetSalesChart();
          // this.drawGrossSalesChart();
        });
    }

    //#endregion

    //#region Event in Each Route

    router.events.pipe(take(2)).subscribe((val) => {
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
            fps.pipe(take(2)).subscribe((res) => {
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
            branchList.pipe(take(2)).subscribe((res) => {
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
            companiesList.pipe(take(2)).subscribe((res) => {
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

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  spinRefreshIcon(selector:string,toSpin:boolean) {
    let refreshIcon = document.querySelector(selector);
    if (toSpin){
      if (refreshIcon && !refreshIcon.classList.contains('toSpin')) {
        refreshIcon.classList.add('toSpin');
      }
    } else {
      if (refreshIcon && refreshIcon.classList.contains('toSpin'))
        refreshIcon.classList.remove('toSpin');
      
      if (this.refreshWidgets) {
        let dashboard = JSON.stringify(this.currentDashboard);
        this.currentDashboard = undefined;
        setTimeout(() => {
          this.currentDashboard = JSON.parse(dashboard);
          this.refreshWidgets = false;
        }, 50);
      }
    }
  }

  onRefreshDashboardClick() {
    this.refreshWidgets = true;

    this.spinRefreshIcon(`#refreshDashboard`,true);
    this.refreshDashboard()
  }

  onRefreshWidget(tab,widget) {
    this.refreshWidgets = true;

    this.spinRefreshIcon(`#refresh-${widget.id}`,true);
    this.widgetService.getWidgets(widget.id).subscribe((res:Widget) =>{
      this.getWidgetData(res.typeId,widget.id,tab.id,res.title,widget.series);
    });
  }

  onSettingChanged(option) {
    const id = option.widgetId + "-" + option.tabId;
    this.widgetSettings[id].series = option.setting.series;
    this.widgetSettings[id].title = option.setting.title;
    
    const data = this.chartService.applyChartSetting(
      this.widgetSettings[id],
      this.widgetData[id]
    );
    this.widgetData[id] = data;

    const element: any = document.getElementById(
      option.setting.chartId.toString()
    );
    let echartsObj = echarts.getInstanceByDom(element);
    const height = echartsObj.getHeight();
    const width = echartsObj.getWidth();
    echartsObj.resize({ width: width + 1, height: height + 1 });
    setTimeout(() => {
      echartsObj.resize({ width: width - 1, height: height - 1 });
    }, 10);
  }

  getOptions(typeId, widgetId, tabId) {
    const id = widgetId + "-" + tabId;
    const data = this.widgetData[id];
    const type = this.chartService.getAdjustedChartType(
      this.widgetSettings[id]
    );

    return this.chartService.getOptions(type, data);
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
    this.goToEditMode();
  }

  goToEditMode() {
    if (this.currentDashboard) {
      this.isDashboardEditMode = !this.isDashboardEditMode;
      this.currentDashboard.tabs.forEach((tab) => {
        this.changedOptions(tab.id);
      });
    }
  }

  onCancelClick() {    
    this.cancelEditMode();
    this.widgetSettings={};
    this.refreshDashboard();
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
    const dashboardId = this.currentDashboard.id;

    let widgetsToUpdate = [];

    const promise = new Promise((resolve) => {
      this.currentDashboard.tabs.forEach((tab) => {
        this.getWidgets(tab.id)
        .pipe(take(1))
        .subscribe((changedWidgets) => {
          if (changedWidgets) {
            changedWidgets.forEach((item, index) => {
              if (tab.widgets.length > 0) {
                const setting = JSON.parse(tab.widgets[index].settings);
                  const widgetSetting =
                    this.widgetSettings[item.id + "-" + tab.id];
                  setting.width = item.cols;
                  setting.height = item.rows;
                  setting.x = item.x;
                  setting.y = item.y;
                  
                  if (widgetSetting.series.length > 0)
                    setting.series = widgetSetting.series;
                  if (widgetSetting.title) setting.title = widgetSetting.title;

                  tab.dashboardId = dashboardId;
                  tab.widgets[index].settings = JSON.stringify(setting);
                  widgetsToUpdate.push(tab.widgets[index]);
                }
              });
            }
          });
        //TODO:change method
      });

      resolve(true);
    });

    promise.then(() => {
      this.dashboardService
        .saveDashboardWidgets(widgetsToUpdate)
        .pipe(take(2))
        .subscribe(() => {});

      this.dashboardService
        .updateDashboardTab(this.currentDashboard.tabs)
        .subscribe(() => {});
    });
  }

  onCloseWidget(widgetId: number) {
    const currentTab = this.currentDashboardTab;
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
  }

  refreshDashboard() {
    this.dashboardService
      .getCurrentDashboard()
      .pipe(take(2))
      .subscribe((dashboard: Dashboard) => {
        this.currentDashboard = dashboard;
        this.fillDashboardSubjects(true);
      });
  }

  onTabChange($event) {
    const currentTab = this.currentDashboardTab;
    if (this.widgetOptions[currentTab.id].api)
      this.widgetOptions[currentTab.id].api.optionsChanged();
  }

  onManageWidgetsClick() {
    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.ManageWidgets"),
      content: ManageWidgetsComponent,
    });

    this.dialogRef.dialog.location.nativeElement.classList.add(
      "manage-widgets"
    );
    this.dialogModel = this.dialogRef.content.instance;

    this.dialogRef.dialog.onDestroy(() => {
      this.settingService.setTitle("Entity.Dashboard");
    });
    this.dialogRef.content.instance.close.subscribe((res) => {
      this.dialogRef.close();
    });
  }

  onAddWidgetClick() {
    if (!this.isDashboardEditMode) this.goToEditMode();

    this.dialogRef = this.dialogService.open({
      title: this.getText("Dashboard.WidgetList"),
      content: AddWidgetComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;

    if (this.currentDashboard) {
      const tab = this.currentDashboard.tabs[this.currentDashboardTabIndex];
      this.dialogModel.selectedWidgets = tab.widgets;
    }

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.addNewWidget(res.widget);
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
      let tab: any = {};
      if (this.currentDashboard && this.currentDashboard.tabs)
        tab.index =
          Math.max(...this.currentDashboard.tabs.map((t) => t.index)) + 1;
      else tab.index = 1;

      tab.title = tabName;
      //tab.id = this.currentDashboard.tabs.length + 1;
      tab.widgets = [];
      tab.dashboardId = this.currentDashboard.id;

      //TODO:posts record to DashboardTab table
      this.dashboardService
        .addDashboardTab(tab)
        .pipe(take(2))
        .subscribe((res: any) => {
          tab.id = res.id;
          this.currentDashboard.tabs.push(tab);
          this.fillDashboardSubjects();
          this.dialogRef.close();
        });
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

    this.dashboardService
      .removeTabWidget(tabId, tabWidget.widgetId)
      .pipe(take(2))
      .subscribe(() => {
        const index = currentTab.widgets.findIndex(
          (w) => w.widgetId == widgetId
        );
        currentTab.widgets.splice(index, 1);

        const id = widgetId + "-" + tabId;
        delete this.widgetSettings[id];
        delete this.widgetData[id];

        const widgets = this.getWidgetList(currentTab.id);

        this.getWidgetsSubject(currentTab.id).widgets.next(widgets);
      });
  }

  tabTitleDblClick(tabId: number, title: string) {
    this.editInlineTabName = tabId;
    this.tabTitle = title;
  }

  onTitleEnterKey(tabId: number) {
    this.editInlineTabName = 0;
    this.saveTabTitle(tabId, this.tabTitle);
  }

  onBlurTabName(tabId: number) {
    this.editInlineTabName = 0;
    this.saveTabTitle(tabId, this.tabTitle);
  }

  saveTabTitle(tabId: number, title: string) {
    this.currentDashboard.tabs.find((t) => t.id === tabId).title = title;
  }

  updateTabName() {}

  initDashboard() {
    this.options = {
      gridType: GridType.ScrollVertical,
      compactType: CompactType.None,
      margin: 10,
      outerMargin: true,
      outerMarginTop: 5,
      outerMarginRight: 5,
      outerMarginBottom: 5,
      outerMarginLeft: 5,
      useTransformPositioning: true,
      mobileBreakpoint: 200,
      minCols: 35,
      maxCols: 100,
      minRows: 50,
      maxRows: 100,
      fixedRowHeight:100,
      fixedColWidth:100,
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
      scrollToNewItems: true,
    };

    this.dashboardService
      .getCurrentDashboard()
      .pipe(take(2))
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
    this.getWidgets(tabId).pipe(
      take(1)
    ).subscribe(async (res) => {
      if (res.length) {
        let msg = await this.translateService.get('Messages.FirstRemoveWidgets').toPromise();
        this.showMessage(msg,MessageType.Warning);
      } else {
        this.dashboardService.removeDashboardTab(tabId)
        .pipe(take(2))
        .subscribe(() => {
          const index = this.currentDashboard.tabs.findIndex((t) => t.id == tabId);
          this.currentDashboard.tabs.splice(index, 1);
        }, err => {
          this.showMessage(err.messages[0],MessageType.Warning)
        });
      }
    })
  }

  getWidgetsSubject(tabId) {
    return this.tabSubjects.filter((f) => f.tabId == tabId)[0];
  }

  addNewWidget(widget) {
    let tabWidgetInfo = new TabWidgetInfo();
    tabWidgetInfo.widgetId = widget.id;
    const setting = {
      height: 20,
      width: 20,
      x: 0,
      y: 0,
    };
    tabWidgetInfo.settings = JSON.stringify(setting);

    let currentTabId = 0;
    if (this.currentDashboard) {
      const tabId = this.currentDashboardTab.id;
      const tab = this.currentDashboard.tabs.filter((t) => t.id == tabId)[0];
      const currentTab =
        this.currentDashboard.tabs[this.currentDashboardTabIndex];
      currentTabId = currentTab.id;
      tabWidgetInfo.tabId = currentTabId;

      this.dashboardService
        .addTabWidget(currentTabId, tabWidgetInfo)
        .pipe(take(2))
        .subscribe((newTabWidget: TabWidget) => {
          tab.widgets.push(newTabWidget);
          const widgets = this.getWidgetList(newTabWidget.tabId);
          this.getWidgetsSubject(newTabWidget.tabId).widgets.next(widgets);
        });
    } else {
      tabWidgetInfo.tabId = 0;

      this.dashboardService
        .postPostNewDashboard(tabWidgetInfo)
        .pipe(take(2))
        .subscribe((dashboard: any) => {
          this.currentDashboard = dashboard;
          this.fillDashboardSubjects();
          this.goToEditMode();
        });
    }
  }

  getWidgetData(
    widgetType,
    widgetId,
    tabId,
    widgetTitle,
    settingSeries: any[]
  ) {
    const id = widgetId + "-" + tabId;
    this.widgetStatus[id] = "progress";
    return this.dashboardService
      .getWidgetData(widgetId)
      .pipe(take(2))
      .subscribe((res) => {
        this.spinRefreshIcon(`.toSpin`,false);
        this.widgetStatus[id] = "done";
        let init = false;
        const series = [];
        //const id = widgetId + "-" + tabId;
        if (this.widgetSettings[id].series.length == 0) {
          init = true;
        }

        if (res.datasets) {
          res.datasets.forEach((item, index) => {
            if (init) {
              item.name = item.label;
              if (settingSeries.length > 0 && settingSeries[index])
                widgetType = settingSeries[index].type;

              item.type = this.chartService.getChartTypeName(widgetType);
            }
            
            const seriesItem: SerieItem = {
              name: item.label,
              type: widgetType.toString(),
            };

            if (widgetType == 1 || widgetType == 2 || widgetType == 3) {
              seriesItem.backgroundColor = new WidgetSetting().Colors[index];
              seriesItem.borderWidth = "1";
            }

            if (widgetType == 4) {
              seriesItem.backgroundColor = new WidgetSetting().Colors;
            }

            series.push(seriesItem);
          });
        }

        if (widgetType == 10 || widgetType == 11 || widgetType == 12) {
          const seriesItem: SerieItem = {
            name: widgetTitle,
            type: widgetType.toString(),
          };
          this.widgetSettings[id].series = [seriesItem];
        }
        //gauge
        if (widgetType != 10) {
          // if (!init) {
          //   if (this.widgetSettings[id]) {
          //     res = this.chartService.applyChartSetting(
          //       this.widgetSettings[id],
          //       res
          //     );
          //   }
          // } else {
          this.widgetSettings[id].series = series;
          res = this.chartService.applyChartSetting(
            this.widgetSettings[id],
            res
          );
          //}
        }

        this.widgetData[widgetId + "-" + tabId] = res;
      });
  }

  widgetHasData(widgetId, tabId) {
    if (this.widgetData[widgetId + "-" + tabId]) return true;
    return false;
  }

  getChartType(typeId, widgetId, tabId) {
    const setting = this.widgetSettings[widgetId + "-" + tabId];
    let type = "";
    if (setting) {
      type = this.chartService.getAdjustedChartType(setting);
    }

    if (type == "" || type == undefined)
      type = this.chartService.getChartTypeName(typeId);

    return type;
  }

  initSeries(widgetType: string, series: any[]) {
    //this.widgetSettings[id].series = series;
    const newSeries: any[] = [];
    series.forEach((item, index) => {
      let serieItem: any = {};
      let label = item.label;
      let backGroundColor = new WidgetSetting().Colors[index];
      let borderWidth = 1;
      let type = widgetType.toString();

      if (series && series[index]) {
        label = series[index].name;
        backGroundColor = series[index].backgroundColor;
        borderWidth = series[index].borderWidth;
        type = series[index].type;
      }

      serieItem.name = label;
      serieItem.backgroundColor = backGroundColor;
      serieItem.borderWidth = borderWidth;
      serieItem.type = type; //this.chartService.getChartType(parseInt(type));

      newSeries.push(serieItem);
    });

    return newSeries;
  }

  getDataOptions(widgetType, widgetId, tabId, dataSets: any[]) {
    const widget = this.currentDashboard.tabs
      .find((t) => t.id == tabId)
      .widgets.filter((w) => w.widgetId == widgetId)[0];

    const id = widget.widgetId + "-" + tabId;
    let series = [];
    if (this.widgetSettings[id]) {
      series = this.widgetSettings[id].series;
    }

    const newDataSet: any[] = [];
    dataSets.forEach((item, index) => {
      let serieItem: any = {};
      let label = item.label;
      let backGroundColor = new WidgetSetting().Colors[index];
      let borderWidth = 1;
      let type = widgetType.toString();

      if (series && series[index]) {
        label = series[index].name;
        backGroundColor = series[index].backgroundColor;
        borderWidth = series[index].borderWidth;
        type = series[index].type;
      }

      item.name = label;
      item.backgroundColor = backGroundColor;
      item.borderWidth = borderWidth;
      item.type = type;

      newDataSet.push(item);
    });

    return newDataSet;
  }

  getWidgetList(tabId, forceRefresh: boolean = false) {
    let widgets = [];

    this.currentDashboard.tabs
      .find((t) => t.id == tabId)
      .widgets.forEach((widget) => {
        const setting = JSON.parse(widget.settings);
        setting.series = setting.series
          ? setting.series
          : new Array<SerieItem>();

        widgets.push({
          cols: setting.width,
          rows: setting.height,
          y: setting.y,
          x: setting.x,
          id: widget.widgetId,
          title: widget.widgetTitle ? widget.widgetTitle: setting.title,
          typeId: widget.widgetTypeId,
          series: setting.series,
        });

        const colors = [
          "#970272",
          "#978b02",
          "#029722",
          "#0d19fd",
          "#0dfdbd",
          "#fd610d",
          "#ba9ffe",
        ];

        const title = setting.title ? setting.title : widget.widgetTitle;

        const set: WidgetSetting = {
          series: [],
          title: title,
          Colors: colors,
        };

        if (
          !this.widgetSettings[widget.widgetId + "-" + tabId] ||
          this.widgetSettings[widget.widgetId + "-" + tabId].series.length == 0
        ) {
          // this.widgetSettings[widget.widgetId + "-" + tabId] = set;
          this.widgetSettings[widget.widgetId + "-" + tabId] = set;
        } else {
          //this.widgetSettings[widget.widgetId + "-" + tabId] = set;
        }

        if (!this.widgetHasData(widget.widgetId, tabId) || forceRefresh)
          this.getWidgetData(
            widget.widgetTypeId,
            widget.widgetId,
            tabId,
            title,
            setting.series
          );
      });

    return widgets;
  }

  getData(widgetId, tabId) {
    const data = this.widgetData[widgetId + "-" + tabId];
    return data;
  }

  fillDashboardSubjects(forceRefresh: boolean = false) {
    let widgets = [];
    if (this.currentDashboard) {
      if (forceRefresh) {
        this.tabSubjects = [];
      }
      this.currentDashboard.tabs.forEach((tab) => {
        widgets = this.getWidgetList(tab.id, forceRefresh);

        this.widgetOptions[tab.id] = JSON.parse(JSON.stringify(this.options));

        let subject = new BehaviorSubject<Array<GridsterItem>>(widgets);
        let tabSubject = new WidgetTabSubject();
        tabSubject.tabId = tab.id;
        tabSubject.widgets = subject;

        this.tabSubjects.push(tabSubject);

        //this.widgets[tab.id] = widgets;
      });
    }
  }
}
