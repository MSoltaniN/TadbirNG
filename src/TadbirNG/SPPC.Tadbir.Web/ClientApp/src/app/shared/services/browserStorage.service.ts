import { Injectable } from "@angular/core";
import { ContextInfo } from "@sppc/core";
import { CurrencyBookDefault } from "@sppc/finance/models";
import { String } from "@sppc/shared/class/source";

export const SessionKeys = {
  CurrentContext: "CurrentContext",
  FiscalPeriod: "FiscalPeriod",
  AppVersion: "AppVersion",
  Menu: "menu",
  Profile: "profile_{0}",
  Setting: "setting_{0}",
  LastUserBranch: "lastUserBranch_{0}_{1}",
  LastUserFpId: "lastUserFpId_{0}_{1}",
  CurrentRoute: "currentRoute",
  PreviousRoute: "PreviousRoute",
  CurrentSkin: "currentSkin",
  NumberConfig: "numberConfig",
  SystemConfig: "SystemConfig",
  TestBalanceConfig: "testBalanceConfig",
  DateRangeConfig: "DateRangeConfig_{0}",
  SelectedDateRange: "SelectedDateRange",
  MetadataKey: "metadata_view_{0}_{1}",
  QuickSearchConfig: "QuickSearchConfig_{0}_{1}",
  SelectForm: "SelectForm",
  Lang: "lang",
  ViewTreeConfig: "viewTreeConfig",
  CurrencyBookDefault: "CurrencyBookDefault",
  TestBalance: "testBalance",
  SelectedBranch: "SelectedBranch",
  SelectedFiscalPeriod: "SelectedFiscalPeriod",
  ItemBalance: "itemBalance",
  OperationLog: "OperationLog",
  BalanceSheet: "BalanceSheet",
  Shortcut: "shortcut",
  License: "license",
  LicenseInfo: "license-info",
  QuickReportSetting: "quick-report-{0}-{1}-{2}",
  AccountColletion: "account_collection",
};

@Injectable()
export class BrowserStorageService {
  constructor() {}

  setContext(currentUser: ContextInfo, rememberMe: boolean) {
    if (rememberMe)
      localStorage.setItem(
        SessionKeys.CurrentContext,
        JSON.stringify(currentUser)
      );
    else
      sessionStorage.setItem(
        SessionKeys.CurrentContext,
        JSON.stringify(currentUser)
      );
  }

  setCurrentContext(currentUser: ContextInfo) {
    if (this.isRememberMe())
      localStorage.setItem(
        SessionKeys.CurrentContext,
        JSON.stringify(currentUser)
      );
    else
      sessionStorage.setItem(
        SessionKeys.CurrentContext,
        JSON.stringify(currentUser)
      );
  }

  removeCurrentContext() {
    if (localStorage.getItem(SessionKeys.CurrentContext))
      localStorage.removeItem(SessionKeys.CurrentContext);

    if (sessionStorage.getItem(SessionKeys.CurrentContext))
      sessionStorage.removeItem(SessionKeys.CurrentContext);

    this.removeSelectedDateRange();

    this.removeLicense();
    this.removeLicenseInfo();
  }

  setLastUserBranchAndFpId(
    userId: number,
    companyId: string,
    branchId: string,
    fpId: string
  ) {
    localStorage.setItem(
      String.Format(SessionKeys.LastUserBranch, userId, companyId),
      branchId
    );
    localStorage.setItem(
      String.Format(SessionKeys.LastUserFpId, userId, companyId),
      fpId
    );
  }

  setFiscalPeriod(item: any) {
    if (this.isRememberMe())
      localStorage.setItem(SessionKeys.FiscalPeriod, JSON.stringify(item));
    else sessionStorage.setItem(SessionKeys.FiscalPeriod, JSON.stringify(item));
  }

  setSession(key: any, value: any) {
    sessionStorage.setItem(key, JSON.stringify(value));
  }

  getSession(key: any) {
    var value = sessionStorage.getItem(key);
    if (value) return JSON.parse(value);
    return null;
  }

  getLicense(): string {
    if (sessionStorage.getItem(SessionKeys.License))
      return sessionStorage.getItem(SessionKeys.License);

    return null;
  }

  setLicense(value: string) {
    sessionStorage.setItem(SessionKeys.License, value);
  }

  removeLicense() {
    sessionStorage.removeItem(SessionKeys.License);
  }

  removeLicenseInfo() {
    sessionStorage.removeItem(SessionKeys.LicenseInfo);
  }

  setLicenseInfo(linceseInfo: any) {
    var licenseInfoString = JSON.stringify(linceseInfo);
    sessionStorage.setItem(SessionKeys.LicenseInfo, licenseInfoString);
  }

  getLicenseInfo(): any {
    if (sessionStorage.getItem(SessionKeys.LicenseInfo)) {
      return JSON.parse(sessionStorage.getItem(SessionKeys.LicenseInfo));
    }
    return null;
  }

  getFiscalPeriod(): string {
    if (this.isRememberMe())
      return localStorage.getItem(SessionKeys.FiscalPeriod);
    else return sessionStorage.getItem(SessionKeys.FiscalPeriod);
  }

  removeFiscalPeriod() {
    if (localStorage.getItem(SessionKeys.FiscalPeriod))
      localStorage.removeItem(SessionKeys.FiscalPeriod);

    if (sessionStorage.getItem(SessionKeys.FiscalPeriod))
      sessionStorage.removeItem(SessionKeys.FiscalPeriod);
  }

  setMenu(item: any) {
    if (this.isRememberMe())
      localStorage.setItem(SessionKeys.Menu, JSON.stringify(item));
    else sessionStorage.setItem(SessionKeys.Menu, JSON.stringify(item));
  }

  setShortcut(item: any) {
    localStorage.setItem(SessionKeys.Shortcut, JSON.stringify(item));
  }

  getShortcut(): string {
    return localStorage.getItem(SessionKeys.Shortcut);
  }

  setCurrentRoute(currentUrl: string) {
    sessionStorage.setItem(SessionKeys.CurrentRoute, currentUrl);
  }

  getCurrentRoute(): string {
    return sessionStorage.getItem(SessionKeys.CurrentRoute);
  }

  removeCurrentRoute() {
    sessionStorage.removeItem(SessionKeys.CurrentRoute);
  }

  setPreviousRoute(previousUrl: string) {
    sessionStorage.setItem(SessionKeys.PreviousRoute, previousUrl);
  }

  getPreviousRoute(): string {
    return sessionStorage.getItem(SessionKeys.PreviousRoute);
  }

  removePreviousRoute() {
    sessionStorage.removeItem(SessionKeys.PreviousRoute);
  }

  getProfile(): string {
    var key = String.Format(SessionKeys.Profile, this.getLanguage());
    if (this.isRememberMe()) return localStorage.getItem(key);
    else return sessionStorage.getItem(key);
  }

  setProfile(item: any) {
    var key = String.Format(SessionKeys.Profile, this.getLanguage());
    if (this.isRememberMe()) localStorage.setItem(key, JSON.stringify(item));
    else sessionStorage.setItem(key, JSON.stringify(item));
  }

  checkVersion(version: string, userId: number) {
    if (version != localStorage.getItem(SessionKeys.AppVersion)) {
      localStorage.removeItem(SessionKeys.Setting + userId);

      var n = localStorage.length;
      while (n--) {
        var key = localStorage.key(n);
        if (/metadata_view/.test(key)) {
          localStorage.removeItem(key);
        }
      }
      localStorage.setItem(SessionKeys.AppVersion, version);
    }
  }

  setUserSetting(settings: any, userId: number) {
    localStorage.setItem(
      String.Format(SessionKeys.Setting, userId),
      JSON.stringify(settings)
    );
  }

  getUserSettings(userId: number): string {
    return localStorage.getItem(String.Format(SessionKeys.Setting, userId));
  }

  deleteUserSettings(userId: number) {
    localStorage.removeItem(String.Format(SessionKeys.Setting, userId));
  }

  getLastUserBranch(userId: number, companyId: string): string | undefined {
    var branchId = localStorage.getItem(
      String.Format(SessionKeys.LastUserBranch, userId, companyId)
    );
    return branchId != "undefined" ? branchId : undefined;
  }

  getLastUserFpId(userId: number, companyId: string): string | undefined {
    var fpId = localStorage.getItem(
      String.Format(SessionKeys.LastUserFpId, userId, companyId)
    );
    return fpId != "undefined" ? fpId : undefined;
  }

  getCurrentUser(): ContextInfo | null {
    var currentUser: ContextInfo;
    var item: string | null = "";
    if (localStorage.getItem(SessionKeys.CurrentContext)) {
      item = localStorage.getItem(SessionKeys.CurrentContext);
    } else if (sessionStorage.getItem(SessionKeys.CurrentContext)) {
      item = sessionStorage.getItem(SessionKeys.CurrentContext);
    }

    if (item) {
      var currentUser: ContextInfo = item !== null ? JSON.parse(item) : null;
      return currentUser;
    }

    return null;
  }

  getLanguage(): string {
    return localStorage.getItem(SessionKeys.Lang);
  }

  setLanguage(lang: string) {
    localStorage.setItem(SessionKeys.Lang, lang);
  }

  getCurrentSkin(): string {
    return localStorage.getItem(SessionKeys.CurrentSkin);
  }

  setCurrentSkin(skin: string) {
    localStorage.setItem(SessionKeys.CurrentSkin, skin);
  }

  getMenu(): string {
    if (this.isRememberMe()) return localStorage.getItem(SessionKeys.Menu);
    else return sessionStorage.getItem(SessionKeys.Menu);
  }

  islogin(): boolean {
    if (this.getCurrentUser()) return true;
    return false;
  }

  isRememberMe(): boolean {
    if (localStorage.getItem(SessionKeys.CurrentContext)) {
      return true;
    }
    return false;
  }

  getMetadata(metadataKey: string): string {
    var metadata = localStorage.getItem(metadataKey);
    if (metadata) return metadata;

    return null;
  }

  setMetadata(metadataKey: string, columns: any) {
    var jsonData = JSON.stringify(columns);
    if (jsonData) {
      localStorage.setItem(metadataKey, jsonData);
    }
  }

  getViewTreeConfig(): string {
    return localStorage.getItem(SessionKeys.ViewTreeConfig);
  }

  setViewTreeConfig(treeConfig: any[]) {
    localStorage.setItem(
      SessionKeys.ViewTreeConfig,
      JSON.stringify(treeConfig)
    );
  }

  getNumberConfig(): string {
    return localStorage.getItem(SessionKeys.NumberConfig);
  }

  getTestBalanceConfig(): string {
    return localStorage.getItem(SessionKeys.NumberConfig);
  }

  removeNumberConfig() {
    if (localStorage.getItem(SessionKeys.NumberConfig))
      localStorage.removeItem(SessionKeys.NumberConfig);
  }

  setNumberConfig(numConfig: any) {
    localStorage.setItem(SessionKeys.NumberConfig, JSON.stringify(numConfig));
  }

  getSystemConfig(): string {
    return sessionStorage.getItem(SessionKeys.SystemConfig);
  }

  removeSystemConfig() {
    if (sessionStorage.getItem(SessionKeys.SystemConfig))
      sessionStorage.removeItem(SessionKeys.SystemConfig);
  }

  setSystemConfig(systemConfig: string) {
    sessionStorage.setItem(
      SessionKeys.SystemConfig,
      JSON.stringify(systemConfig)
    );
  }

  setTestBalanceConfig(numConfig: any) {
    localStorage.setItem(
      SessionKeys.TestBalanceConfig,
      JSON.stringify(numConfig)
    );
  }

  removeDateRangeConfig() {
    if (localStorage.getItem(SessionKeys.DateRangeConfig))
      localStorage.removeItem(SessionKeys.DateRangeConfig);
  }

  setDateRangeConfig(dateConfig: any, companyId: string) {
    const key = String.Format(SessionKeys.DateRangeConfig, companyId);

    localStorage.setItem(key, JSON.stringify(dateConfig));
  }

  getDateRangeConfig(companyId: string): string {
    const key = String.Format(SessionKeys.DateRangeConfig, companyId);
    return localStorage.getItem(key);
  }

  getSelectedDateRange(): string {
    return sessionStorage.getItem(SessionKeys.SelectedDateRange);
  }

  setSelectedDaterange(dataItem: any) {
    sessionStorage.setItem(
      SessionKeys.SelectedDateRange,
      JSON.stringify(dataItem)
    );
  }

  removeSelectedDateRange() {
    sessionStorage.removeItem(SessionKeys.SelectedDateRange);
  }

  getSelectForm(): string {
    return sessionStorage.getItem(SessionKeys.SelectForm);
  }

  setSelectForm(value: string) {
    sessionStorage.setItem(SessionKeys.SelectForm, value);
  }

  getQuickSearchConfig(viewId: number, userId: number): string {
    var sessionKey = String.Format(
      SessionKeys.QuickSearchConfig,
      viewId.toString(),
      userId.toString()
    );
    return localStorage.getItem(sessionKey);
  }

  setQuickSearchConfig(viewId: number, userId: number, value: any) {
    var sessionKey = String.Format(
      SessionKeys.QuickSearchConfig,
      viewId.toString(),
      userId.toString()
    );
    localStorage.setItem(sessionKey, JSON.stringify(value));
  }

  setCurrencyBookDefault(model: CurrencyBookDefault) {
    sessionStorage.setItem(
      SessionKeys.CurrencyBookDefault,
      JSON.stringify(model)
    );
  }

  getCurrencyBookDefault(): CurrencyBookDefault | null {
    var model = sessionStorage.getItem(SessionKeys.CurrencyBookDefault);

    return model ? JSON.parse(model) : null;
  }

  setSelectedBranchId(branchId: number) {
    sessionStorage.setItem(SessionKeys.SelectedBranch, branchId.toString());
  }

  getSelectedBranchId(): number {
    var branchId = sessionStorage.getItem(SessionKeys.SelectedBranch);
    return branchId ? parseInt(branchId) : 0;
  }

  setSelectedFiscalPeriodId(fpId: number) {
    sessionStorage.setItem(SessionKeys.SelectedFiscalPeriod, fpId.toString());
  }

  getSelectedFiscalPeriodId(): number {
    var fpId = sessionStorage.getItem(SessionKeys.SelectedFiscalPeriod);
    return fpId ? parseInt(fpId) : 0;
  }

  removeSelectedBranchAndFiscalPeriod() {
    sessionStorage.removeItem(SessionKeys.SelectedBranch);
    sessionStorage.removeItem(SessionKeys.SelectedFiscalPeriod);
  }

  removeSessionStorage(key: string) {
    sessionStorage.removeItem(key);
  }

  removeLocalStorage(key: string) {
    localStorage.removeItem(key);
  }

  setQuickReportSetting(viewId: string, userId: string, value: string) {
    var key = String.Format(
      SessionKeys.QuickReportSetting,
      viewId.toString(),
      userId.toString(),
      this.getLanguage()
    );
    localStorage.setItem(key, value);
  }

  getQuickReportSetting(viewId: string, userId: string) {
    var key = String.Format(
      SessionKeys.QuickReportSetting,
      viewId.toString(),
      userId.toString(),
      this.getLanguage()
    );
    var jsonString = localStorage.getItem(key);
    return jsonString;
  }

  removeQuickReportSetting(viewId: string, userId: string) {
    var key = String.Format(
      SessionKeys.QuickReportSetting,
      viewId.toString(),
      userId.toString(),
      this.getLanguage()
    );
    var jsonString = localStorage.removeItem(key);
    return jsonString;
  }

  saveDashboardLayout(options, userId: string, companyId: string) {
    localStorage.setItem(
      `dashboard-${userId}-${companyId}`,
      JSON.stringify(options)
    );
  }

  loadDashboardLayout(userId: string, companyId: string) {
    const key = `dashboard-${userId}-${companyId}`;
    if (localStorage.getItem(key)) return JSON.parse(localStorage.getItem(key));

    return null;
  }
}
