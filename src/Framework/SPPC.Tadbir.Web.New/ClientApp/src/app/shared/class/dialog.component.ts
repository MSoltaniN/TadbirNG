
import { EnviromentComponent } from "./enviroment.component"
import { Component } from '@angular/core';
import { RTL } from '@progress/kendo-angular-l10n';
import { Layout } from '@sppc/env/environment';
import { BrowserStorageService } from "..";



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
/deep/.dialog .k-window-content {
  padding: 0
}

/deep/.rtlDialog .k-dialog-wrapper {
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
