import { Injectable } from '@angular/core';
import { ContextInfo } from './login';
import { String } from '../class/source';
import { LZStringService } from 'ng-lz-string';

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
  CurrentSkin: 'currentSkin',
  NumberConfig: 'numberConfig',
  DateRangeConfig: 'DateRangeConfig',
  SelectedDateRange: 'SelectedDateRange',
  MetadataKey: 'metadata_view_{0}_{1}',
  QuickSearchConfig: 'QuickSearchConfig_{0}_{1}',
  SelectForm: 'SelectForm',
  Lang: 'lang',
  ViewTreeConfig: 'viewTreeConfig'
}


@Injectable()
export class BrowserStorageService {

  constructor(private lz: LZStringService) { }

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

  getCurrentRoute(): string {
    return sessionStorage.getItem(SessionKeys.CurrentRoute);
  }

  removeCurrentRoute() {
    sessionStorage.removeItem(SessionKeys.CurrentRoute);
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

  getLastUserBranch(userId: number, companyId: string): string {
    return localStorage.getItem(String.Format(SessionKeys.LastUserBranch, userId, companyId));
  }

  getLastUserFpId(userId: number, companyId: string): string {
    return localStorage.getItem(String.Format(SessionKeys.LastUserFpId, userId, companyId));
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

  setCurrentRoute(currentUrl: string) {
    sessionStorage.setItem(SessionKeys.CurrentRoute, currentUrl);
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
      console.log("decompress metadata time : " + (t1 - t0) + " milliseconds.");

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
      console.log("compress metadata time : " + (t1 - t0) + " milliseconds.");

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

  removeNumberConfig() {
    if (localStorage.getItem(SessionKeys.NumberConfig))
      localStorage.removeItem(SessionKeys.NumberConfig);
  }

  setNumberConfig(numConfig: any) {
    localStorage.setItem(SessionKeys.NumberConfig, JSON.stringify(numConfig));
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
}
