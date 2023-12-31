
import { Component, Input } from "@angular/core";
import { DefaultComponent } from "@sppc/shared/class";




@Component({
  selector: 'editor-form-title',
  template: `
    <div *ngIf="parentModel && viewTitle">
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

      if (config) {
        var viewConfig = config.levels.find(f => f != null && f.no == this.parentModel.level + 1);

        if (viewConfig)
          this.viewTitle = viewConfig.name;
      }
    }

  }

}
