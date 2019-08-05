import { Component, OnInit, Renderer2, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import "rxjs/Rx";
import { TranslateService } from '@ngx-translate/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { ActivatedRoute, Router } from '@angular/router';
import { DialogService } from '@progress/kendo-angular-dialog';
import { Layout } from '@sppc/env/environment';
import { DefaultComponent, MetaDataService, BrowserStorageService } from '@sppc/shared';
import { SettingService } from '@sppc/config/service';



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
  styles: [`
  input[type=text],textarea { width: 100%; }
    .home-img {
      position: absolute;
      top: calc(50% - 250px);
      right: calc(50% - 170px);
      opacity: .1;
    }
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})


export class HomeComponent extends DefaultComponent implements OnInit {

  @ViewChild('itemListRef') el: TemplateRef<any>;
  @ViewChild('dialogActions') actionBtn: TemplateRef<any>;

  private dialog;
  voucherNo: number;
  returnUrl: string;


  constructor(public toastrService: ToastrService, public translate: TranslateService, private activeRoute: ActivatedRoute, public router: Router, public bStorageService: BrowserStorageService,
    public renderer: Renderer2, public metadata: MetaDataService, public settingService: SettingService, private dialogService: DialogService) {
    super(toastrService, translate, bStorageService, renderer, metadata, settingService, '', undefined);
  }

  ngOnInit() {

    this.returnUrl = this.activeRoute.snapshot.queryParamMap.get('returnUrl');

    this.dialog = this.dialogService.open({
      title: 'شماره سند',
      content: this.el,
      actions: this.actionBtn
    });


  }

  passVoucherId() {
    this.close();
    this.router.navigate([this.returnUrl], { queryParams: { no: this.voucherNo } });
  }

  public close() {
    if (this.dialog) {
      this.dialog.close();
    }
  }
}
