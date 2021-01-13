import { QuickReportColumnConfig } from "./QuickReportColumnConfig";
import { QuickReportViewSetting } from "../components/reportManagement/QuickReportViewSetting";
import { ReportPageSetting } from "./reportPageSetting";

export interface QuickReportConfig {

  viewId: number;

  title: string;

  inchValue: number;

  columns: Array<QuickReportColumnConfig>;

  parameters: any;

  pageSetting: ReportPageSetting;
}


export class QuickReportConfigInfo implements QuickReportConfig {
    parameters: any;
    viewId: number;
    title: string;
    inchValue: number;
    columns: QuickReportColumnConfig[];
    reportViewSetting: QuickReportViewSetting;
    pageSetting: ReportPageSetting;
}
