
import { OnInit, OnDestroy, Component, Host, ElementRef, Input, EventEmitter, Output } from "@angular/core";
import { RTL } from "@progress/kendo-angular-l10n";
import { ToastrService } from "ngx-toastr";
import { GridComponent } from "@progress/kendo-angular-grid";
import { TranslateService } from '@ngx-translate/core';
import { DefaultComponent } from "../../class/default.component";



@Component({
  selector: 'editor-form-title',
  template: `
    <div *ngIf="parentModel">
      <div class="alert alert-info fade in alert-dismissible">
        <span class='accInfoTitle'><strong>{{ viewTitle }}</strong></span> : {{ parentModel.name }}
      </div>
    </div>
`,
  styles: [`
.accInfoTitle {
        padding-right: 0px;
        padding-left: 0px;}
`]
})

export class EditorFormTitleComponent {

  viewTitle: string = '';

  @Input() parentModel: any;
  @Input() entityType: number;

  constructor(private defultComponent: DefaultComponent) {
  }


  ngOnInit(): void {
    if (this.parentModel && this.parentModel.level >= 0 && this.entityType) {
      var config = this.defultComponent.getViewTreeSettings(this.entityType);

      var viewConfig = config.levels.find(f => f != null && f.no == this.parentModel.level + 1);

      this.viewTitle = viewConfig.name;
    }

  }

}
