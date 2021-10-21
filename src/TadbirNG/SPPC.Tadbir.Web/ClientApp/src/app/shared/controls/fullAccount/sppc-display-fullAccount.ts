import { Component, OnInit, Input } from '@angular/core';
import { VoucherLine } from '@sppc/finance/models';



@Component({
  selector: 'sppc-display-fullAccount',
  template: `
    <div class="row article-account">
      <div class="col-xs-12">
        <label class="control-label" style="position: absolute;">حساب</label>
        <label class="control-label" style="text-align: center; direction: ltr; display:block;">
          {{ model?.fullAccount?.account.fullCode }} -
          {{ model?.fullAccount?.detailAccount.fullCode }} -
          {{ model?.fullAccount?.costCenter.fullCode }} -
          {{ model?.fullAccount?.project.fullCode }}
        </label>
</div>
<div class="col-xs-12">
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.account.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.detailAccount.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.costCenter.name" readonly />
        <input type="text" class="k-textbox sppc-float" [ngModel]="model?.fullAccount?.project.name" readonly />
      </div>
    </div>

`  ,
  styles: [`
.article-account input[type=text] { width:50% } .article-account { margin-top: 10px; }
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

