import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { TranslateService } from '@ngx-translate/core';
import { Renderer2 } from '@angular/core';
import { MetaDataService, BrowserStorageService, SessionKeys } from '@sppc/shared/services';
import { SettingService } from '@sppc/config/service';
import { DefaultComponent, String } from '@sppc/shared/class';
import { DialogRef, DialogService, DialogCloseResult } from '@progress/kendo-angular-dialog';
import { CompanyFormComponent } from '../company/company-form.component';
import { CompanyDbInfo, BranchInfo, FiscalPeriodInfo, CompanyService, BranchService, FiscalPeriodService } from '@sppc/organization/service';
import { BranchFormComponent } from '../branch/branch-form.component';
import { FiscalPeriodFormComponent } from '../fiscalPeriod/fiscalPeriod-form.component';
import { CompanyDb, Branch, FiscalPeriod } from '@sppc/organization/models';
import { CompanyApi, FiscalPeriodApi, BranchApi } from '@sppc/organization/service/api';
import { MessageType } from '@sppc/shared/enum/metadata';
import { ViewName } from '@sppc/shared/security';




@Component({
  selector: 'initial-wizard',
  template: ''
})
export class InitialWizardComponent extends DefaultComponent implements OnInit {

  company: CompanyDb;
  branch: Branch;
  fiscalPeriod: FiscalPeriod;

  @Output() save: EventEmitter<any> = new EventEmitter();
  @Output() cancel: EventEmitter<any> = new EventEmitter();

  public dialogRef: DialogRef;
  public dialogModel: any;

  constructor(
    public toastrService: ToastrService, public bStorageService: BrowserStorageService, public companyService: CompanyService, public branchService: BranchService,
    public translate: TranslateService, public renderer: Renderer2, public dialogService: DialogService, public fiscalPeriodService: FiscalPeriodService,
    public metadata: MetaDataService, public settingService: SettingService
  ) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
  }

  async ngOnInit() {

    await this.metadataResolver(ViewName.Company);
    await this.metadataResolver(ViewName.Branch);
    await this.metadataResolver(ViewName.FiscalPeriod);

    this.openCompanyEditor(new CompanyDbInfo());
  }

  /**
   *عملیات مربوط به باز و بسته شدن فرم شرکت
   * */
  openCompanyEditor(dataitem: CompanyDbInfo) {
    this.dialogRef = this.dialogService.open({
      title: this.getText('Company.CreateCompany'),
      content: CompanyFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = dataitem;
    this.dialogModel.isNew = true;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isWizard = true;

    this.dialogRef.dialog.location.nativeElement.classList.add('dialog-wizard');

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.companyValidation(res);
    });

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.cancel.emit();
      }
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
      this.cancel.emit();
    });
  }

  /**
   *عملیات مربوط به باز و بسته شدن فرم شعبه
   * */
  openBranchEditor(dataItem: BranchInfo) {

    this.dialogRef = this.dialogService.open({
      title: this.getText('Branch.CreateBranch'),
      content: BranchFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = dataItem;
    this.dialogModel.isNew = true;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isWizard = true;

    this.dialogRef.dialog.location.nativeElement.classList.add('dialog-wizard');

    this.dialogRef.content.instance.save.subscribe((res) => {
      this.branch = res;
      this.dialogRef.close();
      this.openFiscalPeriodEditor();
    });

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.cancel.emit();
      }
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
      this.cancel.emit();
    });

    // مرحله قبل در فرم شعبه
    this.dialogRef.content.instance.previousStep.subscribe(res => {
      this.dialogRef.close();
      this.openCompanyEditor(this.company);
    })
  }

  /**
   *عملیات مربوط به باز و بسته شدن فرم دوره مالی
   * */
  openFiscalPeriodEditor() {
    this.dialogRef = this.dialogService.open({
      title: this.getText('FiscalPeriod.CreateFiscalPeriod'),
      content: FiscalPeriodFormComponent,
    });

    this.dialogModel = this.dialogRef.content.instance;
    this.dialogModel.model = new FiscalPeriodInfo();
    this.dialogModel.isNew = true;
    this.dialogModel.errorMessages = undefined;
    this.dialogModel.isWizard = true;

    this.dialogRef.dialog.location.nativeElement.classList.add('dialog-wizard');

    this.dialogRef.content.instance.save.subscribe((res) => {
      if (res)
        this.fiscalPeriodValidation(res);
      else
        this.insertCompany();
    });

    this.dialogRef.result.subscribe((result) => {
      if (result instanceof DialogCloseResult) {
        this.cancel.emit();
      }
    });

    const closeForm = this.dialogRef.content.instance.cancel.subscribe((res) => {
      this.dialogRef.close();
      this.cancel.emit();
    });

    // مرحله قبل در فرم دوره مالی
    this.dialogRef.content.instance.previousStep.subscribe(res => {
      this.dialogRef.close();
      this.openBranchEditor(this.branch);
    })
  }

  /**
   * صحت اطلاعات شرکت را چک میکند
   * @param dataItem اطلاعات شرکت
   */
  companyValidation(dataItem: CompanyDb) {
    //this.companyService.companyValidation(CompanyApi.CompanyValidation, dataItem).subscribe(res => {
    //  this.company = dataItem;
    //  this.dialogRef.close();
    //  this.openBranchEditor(new BranchInfo());
    //}, error => {
    //  this.dialogModel.errorMessages = error;
    //})
  }

  /**
   * صحت اطلاعات دوره مالی را چک میکند
   * @param dataItem اطلاعات دوره مالی
   */
  fiscalPeriodValidation(dataItem: FiscalPeriod) {
    //this.fiscalPeriodService.fiscalPeriodValidation(FiscalPeriodApi.FiscalPeriodValidation, dataItem).subscribe(res => {
    //  this.fiscalPeriod = dataItem;

    //  this.insertCompany();

    //}, error => {
    //  this.dialogModel.errorMessages = error;
    //})
  }

  /**
   * ذخیره شرکت
   * */
  insertCompany() {
    this.companyService.insert<CompanyDb>(CompanyApi.Companies, this.company).subscribe(res => {
      this.insertBranch(res);
    })
  }

  /**
   * ذخیره شعبه
   * */
  insertBranch(company: any) {
    //if (this.branch) {
    //  this.branch.companyId = company.id;
    //  this.branchService.insert<Branch>(BranchApi.BranchInitial, this.branch).subscribe(res => {
    //    this.insertFiscalPeriod(company, res);
    //  })
    //}
    //else {
    //  this.insertFiscalPeriod(company, undefined);
    //}
  }

  /**
   * ذخیره دوره مالی
   * */
  insertFiscalPeriod(company: any, branch: any) {
    //if (this.fiscalPeriod) {
    //  this.fiscalPeriod.companyId = company.id;
    //  this.fiscalPeriodService.insert<FiscalPeriod>(FiscalPeriodApi.FiscalPeriodInitial, this.fiscalPeriod).subscribe(res => {
    //    this.emitData(company, branch, res);
    //  })
    //}
    //else {
    //  this.emitData(company, branch, undefined);
    //}
  }

  /**
   * ارسال اطلاعات ذخیره شده از فرم ویزارد به فرم لاگین
   * */
  emitData(company: any, branch: any, fiscalPeriod: any) {
    this.showMessage(this.getText('Company.SuccessInitialCompany'), MessageType.Succes);
    this.save.emit({ company: company, branch: branch, fiscalPeriod: fiscalPeriod });
    this.dialogRef.close();
  }

}
