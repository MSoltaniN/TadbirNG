import { Component, ElementRef, NgZone, Renderer2, ChangeDetectorRef } from '@angular/core';
import { GridComponent } from '@progress/kendo-angular-grid';
import { BrowserSupportService } from '@progress/kendo-angular-grid/dist/npm/layout/browser-support.service';
import { SelectionService, SelectionEvent } from '@progress/kendo-angular-grid/dist/npm/selection/selection.service';
import { GroupInfoService } from '@progress/kendo-angular-grid/dist/npm/grouping/group-info.service';
import { GroupsService } from '@progress/kendo-angular-grid/dist/npm/grouping/groups.service';
import { ChangeNotificationService } from '@progress/kendo-angular-grid/dist/npm/data/change-notification.service';
import { DetailsService } from '@progress/kendo-angular-grid/dist/npm/rendering/details/details.service';
import { EditService } from '@progress/kendo-angular-grid/dist/npm/editing/edit.service';
import { FilterService } from '@progress/kendo-angular-grid/dist/npm/filtering/filter.service';
import { PDFService } from '@progress/kendo-angular-grid/dist/npm/pdf/pdf.service';
import { ResponsiveService } from "@progress/kendo-angular-grid/dist/npm/layout/responsive.service";
import { ExcelService } from '@progress/kendo-angular-grid/dist/npm/excel/excel.service';
import { ScrollSyncService } from "@progress/kendo-angular-grid/dist/npm/scrolling/scroll-sync.service";
import { ScrollMode } from '@progress/kendo-angular-grid/dist/npm/scrolling/scrollmode';
import { DomEventsService } from '@progress/kendo-angular-grid/dist/npm/common/dom-events.service';
import { ColumnResizingService } from "@progress/kendo-angular-grid/dist/npm/column-resizing/column-resizing.service";


@Component({
    selector: 'sppc-grid',
    template: ` `
})

    
export class SppcGrid extends GridComponent {
    constructor(private browserSupportService: BrowserSupportService, private selectionService: SelectionService) {        
        super(browserSupportService, selectionService, ElementRef, GroupInfoService, GroupsService, ChangeNotificationService,
            DetailsService, EditService, FilterService, PDFService, ResponsiveService, Renderer2, ExcelService, NgZone, ScrollSyncService,DomEventsService, true,
            ColumnResizingService, ChangeDetectorRef)
        
    }
    //my new properties
}
