
import { EnviromentComponent } from "./enviroment.component"
import { Component } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout } from '@sppc/shared/enum/metadata';
import { BrowserStorageService } from "../services";



export function getLayoutModule(layout: Layout) {
  return layout.getLayout();
}



@Component({
  selector: 'sppc-dialog',
  template: `
        <div class="dialog" [ngClass]="{ 'rtlDialog': this.CurrentLanguage === 'fa' }">
          <div kendoDialogContainer></div>
        </div>
`,
  styles: [`
::ng-deep.dialog .k-window-content {
  padding: 0
}

::ng-deep.rtlDialog .k-dialog-wrapper {
  direction: rtl !important;
}
  `],
  providers: [{
    provide: RTL,
    useFactory: getLayoutModule,
    deps: [Layout]
  }]
})

export class DialogComponent extends EnviromentComponent {

  constructor(public bStorageService: BrowserStorageService) {
    super(bStorageService);
  }

}
