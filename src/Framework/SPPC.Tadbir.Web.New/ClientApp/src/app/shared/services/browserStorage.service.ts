import { Injectable } from '@angular/core';
import { String } from '@sppc/shared/class/source';
import { LZStringService } from 'ng-lz-string';
import { ContextInfo } from '@sppc/core';
import { CurrencyBookDefault } from '@sppc/finance/models';

export const SessionKeys = {
  CurrentContext: 'CurrentContext',
  FiscalPeriod: 'FiscalPeriod',
  AppVersion: 'AppVersion',
  Menu: 'menu',
  Profile: 'profile',
  Setting: 'setting_{0}',
  LastUserBranch: 'lastUserBranch_{0}_{1}',
  LastUserFpId: 'lastUserFpId_{0}_{1}',
  CurrentRoute: 'currentRoute',
  PreviousRoute:'PreviousRoute',
  CurrentSkin: 'currentSkin',
  NumberConfig: 'numberConfig',
  TestBalanceConfig: 'testBalanceConfig',
  DateRangeConfig: 'DateRangeConfig',
  SelectedDateRange: 'SelectedDateRange',
  MetadataKey: 'metadata_view_{0}_{1}',
  QuickSearchConfig: 'QuickSearchConfig_{0}_{1}',
  SelectForm: 'SelectForm',
  Lang: 'lang',
  ViewTreeConfig: 'viewTreeConfig',
  CurrencyBookDefault: 'CurrencyBookDefault',
  TestBalance: 'testBalance',
  SelectedBranch: 'SelectedBranch',
  SelectedFiscalPeriod: 'SelectedFiscalPeriod',
  ItemBalance: 'itemBalance',
  OperationLog: 'OperationLog',
  BalanceSheet: 'BalanceSheet',
  Shortcut: 'shortcut',
  License: 'license',
  LicenseInfo: 'license-info'
}


@Injectable()
export class BrowserStorageService {

  constructor(private lz: LZStringService) { }

  setContext(currentUser: ContextInfo, rememberMe: boolean) {
    if (rememberMe)
      localStorage.setItem(SessionKeys.CurrentContext, JSON.stringify(currentUser));
    else
      sessionStorage.setItem(SessionKeys.CurrentContext, JSON.stringify(currentUser));
  }

  setCurrentContext(currentUser: ContextInfo) {
    if (this.isRememberMe())
      localStorage.setItem(SessionKeys.CurrentContext, JSON.stringify(currentUser));
    else
      sessionStorage.setItem(SessionKeys.CurrentContext, JSON.stringify(currentUser));
  }

  removeCurrentContext() {
    if (localStorage.getItem(SessionKeys.CurrentContext))
      localStorage.removeItem(SessionKeys.CurrentContext);

    if (sessionStorage.getItem(SessionKeys.CurrentContext))
      sessionStorage.removeItem(SessionKeys.CurrentContext);
  }

  setLastUserBranchAndFpId(userId: number, companyId: string, branchId: string, fpId: string) {
    localStorage.setItem(String.Format(SessionKeys.LastUserBranch, userId, companyId), branchId);
    localStorage.setItem(String.Format(SessionKeys.LastUserFpId, userId, companyId), fpId);
  }

  setFiscalPeriod(item: any) {
    if (this.isRememberMe())
      localStorage.setItem(SessionKeys.FiscalPeriod, JSON.stringify(item));
    else
      sessionStorage.setItem(SessionKeys.FiscalPeriod, JSON.stringify(item));
  }

  setSession(key: any, value: any) {
    sessionStorage.setItem(key, JSON.stringify(value));
  }

  getSession(key: any) {
    var value = sessionStorage.getItem(key);
    if (value)
      return JSON.parse(value);
    return null;
  }

  getLicense():string {
    if (localStorage.getItem(SessionKeys.License))
      return localStorage.getItem(SessionKeys.License);

    return null;
  }

  setLicense(value:string) {
    localStorage.setItem(SessionKeys.License, value);    
  }

  setLicenseInfo(linceseInfo: any) {
    var licenseInfoString = JSON.stringify(linceseInfo);
    sessionStorage.setItem(SessionKeys.LicenseInfo, licenseInfoString);
  }

  getLicenseInfo(): any {
    if (localStorage.getItem(SessionKeys.LicenseInfo)) {
      return JSON.parse(sessionStorage.getItem(SessionKeys.LicenseInfo));
    }
    return null;
  }

  getFiscalPeriod(): string {
    if (this.isRememberMe())
      return localStorage.getItem(SessionKeys.FiscalPeriod);
    else
      return sessionStorage.getItem(SessionKeys.FiscalPeriod);
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
    else
      sessionStorage.setItem(SessionKeys.Menu, JSON.stringify(item));
  }

  setShortcut(item: any) {    
      localStorage.setItem(SessionKeys.Shortcut, JSON.stringify(item));    
  }

  getShortcut():string {
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
    if (this.isRememberMe())
      return localStorage.getItem(SessionKeys.Profile);
    else
      return sessionStorage.getItem(SessionKeys.Profile);
  }

  setProfile(item: any) {
    if (this.isRememberMe())
      localStorage.setItem(SessionKeys.Profile, JSON.stringify(item));
    else
      sessionStorage.setItem(SessionKeys.Profile, JSON.stringify(item));
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
    localStorage.setItem(String.Format(SessionKeys.Setting, userId), JSON.stringify(settings));
  }

  getUserSettings(userId: number): string {
    return localStorage.getItem(String.Format(SessionKeys.Setting, userId));
  }

  getLastUserBranch(userId: number, companyId: string): string | undefined {
    var branchId = localStorage.getItem(String.Format(SessionKeys.LastUserBranch, userId, companyId));
    return branchId != "undefined" ? branchId : undefined;
  }

  getLastUserFpId(userId: number, companyId: string): string | undefined {
    var fpId = localStorage.getItem(String.Format(SessionKeys.LastUserFpId, userId, companyId));
    return fpId != "undefined" ? fpId : undefined;
  }

  getCurrentUser(): ContextInfo | null {
    var currentUser: ContextInfo;
    var item: string | null = '';
    if (localStorage.getItem(SessionKeys.CurrentContext)) {
      item = localStorage.getItem(SessionKeys.CurrentContext);
    }
    else if (sessionStorage.getItem(SessionKeys.CurrentContext)) {
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
    if (this.isRememberMe())
      return localStorage.getItem(SessionKeys.Menu);
    else
      return sessionStorage.getItem(SessionKeys.Menu);
  }

  islogin(): boolean {
    if (this.getCurrentUser())
      return true;
    return false;
  }

  isRememberMe(): boolean {
    if (localStorage.getItem(SessionKeys.CurrentContext)) {
      return true;
    }
    return false;
  }

  getMetadata(metadataKey: string): string {



    var compressedData = localStorage.getItem(metadataKey);
    if (compressedData) {
      var t0 = performance.now();
      var decompressed = this.lz.decompress(compressedData);
      var t1 = performance.now();
      //console.log("decompress metadata time : " + (t1 - t0) + " milliseconds.");

      return decompressed;
    }

    return null;
  }

  setMetadata(metadataKey: string, columns: any) {
    var jsonData = JSON.stringify(columns);
    if (jsonData) {
      var t0 = performance.now();
      var compressedJSON = this.lz.compress(jsonData);
      var t1 = performance.now();
      //console.log("compress metadata time : " + (t1 - t0) + " milliseconds.");

      localStorage.setItem(metadataKey, compressedJSON);
    }
  }

  getViewTreeConfig(): string {
    return localStorage.getItem(SessionKeys.ViewTreeConfig);
  }

  setViewTreeConfig(treeConfig: any[]) {
    localStorage.setItem(SessionKeys.ViewTreeConfig, JSON.stringify(treeConfig));
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

  setTestBalanceConfig(numConfig: any) {
    localStorage.setItem(SessionKeys.TestBalanceConfig, JSON.stringify(numConfig));
  }


  removeDateRangeConfig() {
    if (localStorage.getItem(SessionKeys.DateRangeConfig))
      localStorage.removeItem(SessionKeys.DateRangeConfig);
  }

  setDateRangeConfig(dateConfig: any) {
    localStorage.setItem(SessionKeys.DateRangeConfig, JSON.stringify(dateConfig));
  }

  getdateRangeConfig(): string {
    return localStorage.getItem(SessionKeys.DateRangeConfig);
  }

  getSelectedDateRange(): string {
    return sessionStorage.getItem(SessionKeys.SelectedDateRange);
  }

  setSelectedDaterange(dataItem: any) {
    sessionStorage.setItem(SessionKeys.SelectedDateRange, JSON.stringify(dataItem));
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
    var sessionKey = String.Format(SessionKeys.QuickSearchConfig, viewId.toString(), userId.toString());
    return localStorage.getItem(sessionKey);
  }

  setQuickSearchConfig(viewId: number, userId: number, value: any) {
    var sessionKey = String.Format(SessionKeys.QuickSearchConfig, viewId.toString(), userId.toString());
    localStorage.setItem(sessionKey, JSON.stringify(value));
  }

  setCurrencyBookDefault(model: CurrencyBookDefault) {
    sessionStorage.setItem(SessionKeys.CurrencyBookDefault, JSON.stringify(model));
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

  removeSessionStorage(key:string) {
    sessionStorage.removeItem(key);    
  }

  removeLocalStorage(key: string) {
    localStorage.removeItem(key);
  }
}
