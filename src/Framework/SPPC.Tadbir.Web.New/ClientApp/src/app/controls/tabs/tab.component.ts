/**
 * A single tab page. It renders the passed template
 * via the @Input properties by using the ngTemplateOutlet
 * and ngTemplateOutletContext directives.
 */

import { Component, Input, ViewChild } from '@angular/core';
import { ReportViewerComponent } from '../../components/reportViewer/reportViewer.component';


@Component({
  selector: 'my-tab',
  styles: [
    `
    .pane{
      padding: 1em;
    }
  `
  ],
  template: `
    <div [hidden]="!active" class="pane">
      <report-viewer #viewer [Id]="Id">                                                                
      </report-viewer>
      <div [id]="Id" *ngIf='isDesigner'></div>
    </div>
  `
})
export class TabComponent {
  @Input('tabTitle') title: string;
  @Input() active = false;
  @Input() isCloseable = false;
  @Input() template;
  @Input() dataContext;
  @Input() isViewer : boolean;
  @Input() isDesigner : boolean;
  @Input() Id:string;
  @Input() Manager:any;

  @ViewChild(ReportViewerComponent) reportViewer: ReportViewerComponent;  
    

  public callViewer()
  {      
      this.reportViewer.showReportViewer(this.template,this.dataContext)
  }
}
