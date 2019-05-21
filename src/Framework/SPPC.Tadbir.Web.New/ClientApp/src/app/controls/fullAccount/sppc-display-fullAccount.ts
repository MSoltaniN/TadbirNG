import { Component, OnInit, Input } from '@angular/core';
import { VoucherLine } from '../../model/index';



@Component({
  selector: 'sppc-display-fullAccount',
  template: `
    <div class="row article-account">
      <div class="col-xs-12">
        <label class="control-label">حساب</label>
        <label class="control-label" style="text-align: center; direction: ltr;">
          {{ model?.fullAccount?.account.fullCode }} -
          {{ model?.fullAccount?.detailAccount.fullCode }} -
          {{ model?.fullAccount?.costCenter.fullCode }} -
          {{ model?.fullAccount?.project.fullCode }}
        </label>
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.account.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.detailAccount.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.costCenter.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.project.name" readonly />
      </div>
    </div>

    <div class="row article-description">
      <div class="col-xs-12">
        <label class="control-label">شرح آرتیکل</label>
        <input type="text" class="k-textbox" [ngModel]="model?.description" readonly />
      </div>
    </div>
`  ,
  styles: [`
.article-account input[type=text] { width:50% } .article-account label { display:block } .article-account ,.article-description { margin-top: 15px; }
.article-description input[type=text] { width:100% }
`]
})


export class SppcDisplayFullAccountComponent implements OnInit {

  model: VoucherLine;

  @Input() set articleModel(model: VoucherLine) {
    this.model = model;
  }

  constructor() { }

  ngOnInit(): void {
  
  }

}

