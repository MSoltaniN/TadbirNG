using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SPPC.Tadbir.Configuration.Models;
using SPPC.Tadbir.ViewModel.Reporting;
using Stimulsoft.Base.Drawing;
using Stimulsoft.Report;
using Stimulsoft.Report.Components;
using Stimulsoft.Report.Components.Table;

namespace SPPC.Tadbir.Web.Api
{
    internal class QuickReportBuilder
    {
        internal StiReport Build(QuickReportConfig qr, int unit)
        {
            var quickReport = new StiReport();
            var quickReportTemplate = new StiReport();
            bool fitToPage = qr.ReportPageSetting.ColumnFitPage;
            quickReport.ReportUnit = StiReportUnitType.Inches;
            var dataSourceName = "root";
            string reportTemplate = string.Empty;

            // load template for adding styles
            if (Template != null && !string.IsNullOrEmpty(Template.Template))
            {
                reportTemplate = Template.Template;
                quickReportTemplate.LoadFromJson(reportTemplate);
            }
            else
            {
                string templateName = "SPPC.Tadbir.Web.Api.Resources.Reporting.Template.QuickReport.A4.Rtl.mrt";
                if (Language != "fa")
                {
                    templateName = "SPPC.Tadbir.Web.Api.Resources.Reporting.Template.QuickReport.A4.Rtl.mrt";
                }

                using (var reader = new StreamReader(
                    typeof(Program).Assembly.GetManifestResourceStream(templateName)))
                {
                    reportTemplate = reader.ReadToEnd();
                }

                quickReportTemplate.LoadFromString(reportTemplate);
            }

            int inchValue = unit;
            quickReport.Styles.AddRange(quickReportTemplate.Styles);

            // load template for adding styles
            quickReport = SetPageSetting(quickReport, qr);
            quickReport = CreateReportFooterBand(quickReport, quickReportTemplate);
            quickReport = CreateReportHeaderBand(quickReport, quickReportTemplate);
            
            quickReport = CreatePageHeaderBand(quickReport, quickReportTemplate);
            quickReport = CreatePageFooterBand(quickReport, quickReportTemplate);
            if (qr.Parameters != null)
            {
                quickReport = CreateReportParametersBand(qr, quickReport, quickReportTemplate);
            }

            quickReport = CreateHeaderBand(quickReport, qr.Columns, inchValue, dataSourceName, quickReportTemplate, Language, fitToPage);
            quickReport = CreateDataBand(quickReport, qr, dataSourceName, quickReportTemplate, Language, inchValue, fitToPage);
            quickReport = FillLocalVariables(quickReport, qr.Title);

            return quickReport;
        }

        internal LocalReportViewModel Template { get; set; }

        internal string Language { get; set; }

        private enum ReportBandType
        {
            ReportHeader,
            ReportFooter,
            PageHeader,
            PageFooter
        }

        private static StiReport CreateReportHeaderBand(StiReport report, StiReport reportTemplate)
        {
            var reportHeader = new StiReportTitleBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiReportTitleBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiReportTitleBand));
                reportHeader = (StiReportTitleBand)component;
                reportHeader = (StiReportTitleBand)ChangeLeftPositions(report, reportHeader, ReportBandType.ReportHeader, reportTemplate);
            }

            report.Pages[0].Components.Add(reportHeader);

            return report;
        }

        private static StiReport FillLocalVariables(StiReport report, string header)
        {
            report.Dictionary.Variables.Add("vReportFirstTitle", string.Empty);
            report.Dictionary.Variables.Add("vReportSummaryTitle", string.Empty);
            report.Dictionary.Variables.Add("vReportTitle", header);

            return report;
        }

        private static StiComponent ChangeLeftPositions(StiReport stiReport, StiComponent component, ReportBandType reportBandType, StiReport reportTemplate)
        {
            var collection = new StiComponentsCollection();
            double diff = 0;

            switch (reportBandType)
            {
                case ReportBandType.PageFooter:
                    collection = ((StiPageFooterBand)component).Components;
                    break;
                case ReportBandType.ReportHeader:
                    collection = ((StiReportTitleBand)component).Components;
                    break;
                case ReportBandType.ReportFooter:
                    collection = ((StiReportSummaryBand)component).Components;
                    break;
                case ReportBandType.PageHeader:
                    collection = ((StiPageHeaderBand)component).Components;
                    break;
            }

            if (reportTemplate.Pages[0].Orientation == StiPageOrientation.Portrait && stiReport.Pages[0].Orientation == StiPageOrientation.Landscape)
            {
                diff = 1;
            }
            else if (reportTemplate.Pages[0].Orientation == StiPageOrientation.Landscape && stiReport.Pages[0].Orientation == StiPageOrientation.Portrait)
            {
                diff = -1;
            }

            if (diff != 0)
            {
                foreach (StiComponent stiComponent in collection)
                {
                    var newLeft = stiComponent.Left;
                    if (diff > 0)
                    {
                        newLeft = (LandscapeWidth * stiComponent.Left) / PortraitWidth;
                    }

                    if (diff < 0)
                    {
                        newLeft = (PortraitWidth * stiComponent.Left) / LandscapeWidth;
                    }

                    stiComponent.Left = newLeft;
                }
            }

            return component;
        }

        private static StiReport CreateReportFooterBand(StiReport report, StiReport reportTemplate)
        {
            var reportFooter = new StiReportSummaryBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiReportSummaryBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiReportSummaryBand));
                reportFooter = (StiReportSummaryBand)component;
                reportFooter = (StiReportSummaryBand)ChangeLeftPositions(report, reportFooter, ReportBandType.ReportFooter, reportTemplate);
            }

            report.Pages[0].Components.Add(reportFooter);
            return report;
        }

        private static StiReport SetPageSetting(StiReport report, QuickReportConfig quickReportViewModel)
        {
            switch (quickReportViewModel.ReportPageSetting.PageSize)
            {
                case "A4":
                    report.Pages[0].Width = 8.27;
                    report.Pages[0].Height = 11.69;
                    break;
            }

            switch (quickReportViewModel.ReportPageSetting.PageOrientation)
            {
                case "Portrait":
                    report.Pages[0].Orientation = StiPageOrientation.Portrait;
                    break;
                case "Landscape":
                    report.Pages[0].Orientation = StiPageOrientation.Landscape;
                    break;
            }

            report.Pages[0].Margins.Left = quickReportViewModel.ReportPageSetting.MarginLeft;
            report.Pages[0].Margins.Right = quickReportViewModel.ReportPageSetting.MarginRight;
            report.Pages[0].Margins.Top = quickReportViewModel.ReportPageSetting.MarginTop;
            report.Pages[0].Margins.Bottom = quickReportViewModel.ReportPageSetting.MarginBottom;

            return report;
        }

        private static StiReport CreateReportParametersBand(QuickReportConfig quickReportViewModel, StiReport report, StiReport reportTemplate)
        {
            // read tblParameter from reportTemplate
            var table = new StiTable();
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiTable)))
            {
                table = (StiTable)componentsList.First(p => p.GetType() == typeof(StiTable));
            }

            string titleStyle = string.Empty, valueStyle = string.Empty;
            if (!string.IsNullOrEmpty(table.Components[0].ComponentStyle))
            {
                titleStyle = table.Components[0].ComponentStyle;
            }

            if (!string.IsNullOrEmpty(table.Components[1].ComponentStyle))
            {
                titleStyle = table.Components[1].ComponentStyle;
            }

            StiTableCell titleCell = (StiTableCell)table.Components[0].Clone(true);
            StiTableCell valueCell = (StiTableCell)table.Components[1].Clone(true);

            table.RowCount = (int)Math.Ceiling(((double)quickReportViewModel.Parameters.Count * 2 / table.ColumnCount));
            var ph = (StiPageHeaderBand)report.Pages[0].Components.ToList().Last(p => p.GetType() == typeof(StiPageHeaderBand));
            ph.Components.Clear();

            var panel = new StiPanel
            {
                CanGrow = true,
                Width = report.Pages[0].Width,
                PrintOn = table.PrintOnAllPages ? StiPrintOnType.AllPages : StiPrintOnType.OnlyFirstPage
            };
            panel.Components.Add(table);
            ph.Components.Add(panel);
            table.CreateCell();
            int i = 0;
            foreach (var param in quickReportViewModel.Parameters)
            {
                table.Components[i] = (StiTableCell)titleCell.Clone(true);
                StiTableCell headerCell = table.Components[i] as StiTableCell;
                headerCell.Text.Value = param.CaptionKey;
                headerCell.Name = "cell" + i;
                headerCell.Linked = true;
                i++;
                table.Components[i] = (StiTableCell)valueCell.Clone(true);
                StiTableCell dataCell = table.Components[i] as StiTableCell;
                dataCell.Name = "cell" + i;
                dataCell.Text.Value = "{" + param.Name + "}";
                i++;
            }

            table.Linked = true;
            return report;
        }

        private static StiReport CreatePageHeaderBand(StiReport report, StiReport reportTemplate)
        {
            var pageHeader = new StiPageHeaderBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiPageHeaderBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiPageHeaderBand));
                pageHeader = (StiPageHeaderBand)component;
                pageHeader = (StiPageHeaderBand)ChangeLeftPositions(report, pageHeader, ReportBandType.PageHeader, reportTemplate);
            }

            report.Pages[0].Components.Add(pageHeader);

            StiPageHeaderBand headerParameter = (StiPageHeaderBand)pageHeader.Clone(true);
            headerParameter.Name = "hdrParams";
            headerParameter.Linked = true;
            headerParameter.Height = 0.9;
            headerParameter.CanGrow = true;
            headerParameter.CanShrink = true;
            headerParameter.Components.Clear();
            report.Pages[0].Components.Add(headerParameter);

            return report;
        }

        private static StiReport CreatePageFooterBand(StiReport report, StiReport reportTemplate)
        {
            var reportFooter = new StiPageFooterBand();

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiPageFooterBand)))
            {
                var component = componentsList.First(p => p.GetType() == typeof(StiPageFooterBand));
                reportFooter = (StiPageFooterBand)component;
                reportFooter = (StiPageFooterBand)ChangeLeftPositions(report, reportFooter, ReportBandType.PageFooter, reportTemplate);
            }

            report.Pages[0].Components.Add(reportFooter);
            return report;
        }

        private static StiReport CreateHeaderBand(StiReport report, IList<QuickReportColumnConfig> columns,
            int oneInchInPixels, string dataSourceName, StiReport reportTemplate, string lang, bool fitToPage)
        {
            int visibleColumnCount = columns.Count(c => c.Visible);
            if (visibleColumnCount == 0)
            {
                return null;
            }

            bool isGroupExist = columns.Any(c => !string.IsNullOrEmpty(c.GroupName));
            List<QuickReportColumnConfig> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in columns
                                 orderby c.DisplayIndex ascending
                                 where c.Visible
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in columns
                                 orderby c.DisplayIndex descending
                                 where c.Visible
                                 select c).ToList();
            }

            string name = "HeaderBand" + dataSourceName;
            StiColumnHeaderBand headerBand = null;
            StiText sampleText = null;
            StiText sampleNumber = null;
            StiComponent component = null;

            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            if (componentsList.Any(p => p.GetType() == typeof(StiColumnHeaderBand)))
            {
                component = componentsList.First(p => p.GetType() == typeof(StiColumnHeaderBand));
                headerBand = (StiColumnHeaderBand)component.Clone(true);
                if (isGroupExist)
                {
                    headerBand.Height *= 2;
                    headerBand.CanGrow = false;
                }
            }

            // copy text style
            if (((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnTextHeader"))
            {
                sampleText = (StiText)((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnTextHeader").Clone(true);
            }

            // copy number style
            if (((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnNumberHeader"))
            {
                sampleNumber = (StiText)((StiColumnHeaderBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnNumberHeader").Clone(true);
            }

            if (headerBand == null)
            {
                headerBand = new StiColumnHeaderBand
                {
                    Name = name
                };
            }
            else
            {
                headerBand.Components.Clear();
            }

            int dataCellIndex = visibleColumnCount;
            double pageWidth = report.Pages[0].Width;
            int gridWidth = columns.Where(p => p.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, oneInchInPixels);
            double left = 0;
            double top = 0;
            double maxHeight = headerBand.Height;

            string lastGroupName = string.Empty;
            double lastWidth = 0;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                QuickReportColumnConfig column = orderdColumns[i];
                StiText txtHeaderCell = null;
                string ctrlName = "txtTitle_";
                ctrlName += column.Name + column.DisplayIndex;
                double width = GetSizeInInch(column.Width, oneInchInPixels);

                if (txtHeaderCell == null)
                {
                    txtHeaderCell = (StiText)(sampleText.Clone(true));

                    if (orderdColumns[i].Type != null)
                    {
                        switch (orderdColumns[i].Type.ToLower())
                        {
                            case "number":
                            case "money":
                                txtHeaderCell = (StiText)(sampleNumber.Clone(true));
                                break;
                            case "gregorian":
                            case "jalali":
                                txtHeaderCell = (StiText)(sampleNumber.Clone(true));
                                break;
                            case "string":
                            default:
                                txtHeaderCell = (StiText)(sampleText.Clone(true));
                                break;
                        }
                    }
                    else
                    {
                        txtHeaderCell = (StiText)(sampleText.Clone(true));
                    }

                    txtHeaderCell.Name = ctrlName;
                    txtHeaderCell.Left = left;
                    txtHeaderCell.Width = width;
                    txtHeaderCell.Page = report.Pages[0];
                    txtHeaderCell.Parent = headerBand;

                    txtHeaderCell.CanGrow = true;
                    txtHeaderCell.WordWrap = true;

                    if (string.IsNullOrEmpty(orderdColumns[i].GroupName) && isGroupExist)
                    {
                        top = 0;
                        txtHeaderCell.CanGrow = true;
                        txtHeaderCell.Height *= 2;
                        txtHeaderCell.GrowToHeight = true;
                        txtHeaderCell.CanShrink = false;
                        lastWidth = 0;
                    }
                    else if (isGroupExist)
                    {
                        top = 0;
                        if (orderdColumns[i].GroupName == lastGroupName)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            var txtHeaderCellWidth = txtHeaderCell.Width;
                            if (fitToPage)
                            {
                                var widthPercent = txtHeaderCellWidth / tableWidth * 100;
                                txtHeaderCellWidth = (widthPercent * pageWidth) / 100;
                            }

                            txtGroupText.Width = txtHeaderCellWidth + lastWidth;
                            txtGroupText.Text.Value = lastGroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left - lastWidth, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);
                            lastWidth = 0;
                            lastGroupName = string.Empty;
                        }
                        else if (!string.IsNullOrEmpty(orderdColumns[i].GroupName) && orderdColumns.Count(cc => cc.GroupName == orderdColumns[i].GroupName) == 1)
                        {
                            StiText txtGroupText = (StiText)txtHeaderCell.Clone(true);
                            var txtHeaderCellWidth = txtHeaderCell.Width;
                            if (fitToPage)
                            {
                                var widthPercent = txtHeaderCellWidth / tableWidth * 100;
                                txtHeaderCellWidth = (widthPercent * pageWidth) / 100;
                            }

                            txtGroupText.Width = txtHeaderCellWidth;
                            txtGroupText.Text.Value = orderdColumns[i].GroupName;
                            txtGroupText.CanShrink = false;
                            txtGroupText.ClientRectangle = new RectangleD(left, top, txtGroupText.Width, txtGroupText.Height);
                            headerBand.Components.Add(txtGroupText);
                            lastWidth = 0;
                            lastGroupName = string.Empty;
                        }
                        else
                        {
                            lastWidth = txtHeaderCell.Width;
                            lastGroupName = orderdColumns[i].GroupName;
                        }

                        txtHeaderCell.CanShrink = false;
                        top = txtHeaderCell.Height;
                    }
                    else
                    {
                        top = 0;
                        lastWidth = 0;
                    }
                }

                txtHeaderCell.Text.Value = !string.IsNullOrEmpty(column.UserTitle) ? column.UserTitle : column.Title;
                if (isGroupExist && lastWidth > 0)
                {
                    width = lastWidth;
                }

                if (fitToPage)
                {
                    var widthPercent = width / tableWidth * 100;
                    width = (widthPercent * pageWidth) / 100;

                    lastWidth = width;
                    txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                    left = txtHeaderCell.Left + width;
                }
                else
                {
                    txtHeaderCell.ClientRectangle = new RectangleD(left, top, width, txtHeaderCell.Height);
                    left = txtHeaderCell.Left + width;
                    lastWidth = width;
                }

                txtHeaderCell.GrowToHeight = true;
                headerBand.Components.Add(txtHeaderCell);
            }

            report.Pages[0].Components.Add(headerBand);
            return report;
        }

        private static StiReport CreateDataBand(StiReport report, QuickReportConfig quickReportViewModel,
            string dataSourceName, StiReport reportTemplate, string lang, int inchValue, bool fitToPage)
        {
            string ctrlName = "dataBand" + dataSourceName;

            List<QuickReportColumnConfig> orderdColumns = null;
            if (lang == "fa")
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.DisplayIndex ascending
                                 where c.Visible
                                 select c).ToList();
            }
            else
            {
                orderdColumns = (from c in quickReportViewModel.Columns
                                 orderby c.DisplayIndex descending
                                 where c.Visible
                                 select c).ToList();
            }

            double pageWidth = report.Pages[0].Width;
            int gridWidth = quickReportViewModel.Columns.Where(c => c.Visible).Sum(c => c.Width);
            double tableWidth = GetSizeInInch(gridWidth, inchValue);
            double left = 0;
            double top = 0;

            var dataBand = new StiDataBand();
            StiText sampleText = null;
            StiText sampleNumber = null;
            StiText sampleDate = null;
            var componentsList = reportTemplate.Pages[0].GetComponents().ToList();
            StiComponent component = null;

            if (componentsList.Any(p => p.GetType() == typeof(StiDataBand)))
            {
                component = componentsList.First(p => p.GetType() == typeof(StiDataBand));
                dataBand = (StiDataBand)component.Clone(true);
            }

            // copy text style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnTextData"))
            {
                sampleText = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnTextData").Clone(true);
            }

            // copy date style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnNumberData"))
            {
                sampleNumber = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnNumberData").Clone(true);
            }

            // copy number style
            if (((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                .Any(c => c.ComponentStyle == "Tadbir_ColumnDateData"))
            {
                sampleDate = (StiText)((StiDataBand)component).Components.ToList().Where(p => p.GetType() == typeof(StiText))
                    .First(c => c.ComponentStyle == "Tadbir_ColumnDateData").Clone(true);
            }

            dataBand.Components.Clear();
            dataBand.Name = ctrlName;
            dataBand.DataSourceName = dataSourceName;

            for (int i = orderdColumns.Count - 1; i >= 0; i--)
            {
                double width = GetSizeInInch(orderdColumns[i].Width, inchValue);
                StiText txtDataCell = null;
                string name = "txtDataCell_" + orderdColumns[i].Name;

                if (txtDataCell == null)
                {
                    if (orderdColumns[i].Name.ToLower() != "rowno" && orderdColumns[i].Type != null)
                    {
                        switch (orderdColumns[i].Type.ToLower())
                        {
                            case "number":
                            case "money":
                                txtDataCell = (StiText)(sampleNumber.Clone(true));
                                break;
                            case "date":
                                txtDataCell = (StiText)(sampleDate.Clone(true));
                                break;
                            case "string":
                            default:
                                txtDataCell = (StiText)(sampleText.Clone(true));
                                break;
                        }
                    }
                    else
                    {
                        txtDataCell = (StiText)(sampleDate.Clone(true));
                    }

                    txtDataCell.Name = name;
                    txtDataCell.Left = left;
                    txtDataCell.Parent = dataBand;
                    txtDataCell.Page = report.Pages[0];
                }

                if (orderdColumns[i].Type != null && orderdColumns[i].Type.ToLower() == "money")
                {
                    txtDataCell.TextFormat = new Stimulsoft.Report.Components.TextFormats.StiNumberFormatService(1, ".", 0, ",", 3, true, false, " ");
                }

                if (fitToPage)
                {
                    var widthPercent = width / tableWidth * 100;
                    width = (widthPercent * pageWidth) / 100;
                }

                txtDataCell.Width = width;
                txtDataCell.Text.Value = GetColumnValue(orderdColumns[i], dataSourceName, string.Empty);
                txtDataCell.ClientRectangle = new RectangleD(left, top, width, txtDataCell.Height);
                left = txtDataCell.Left + width;

                if (quickReportViewModel.ReportViewSetting != null)
                {
                    if (quickReportViewModel.ReportViewSetting.HideHorizontalLine && quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.None, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }

                    if (quickReportViewModel.ReportViewSetting.HideHorizontalLine && !quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.Left | StiBorderSides.Right, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }

                    if (!quickReportViewModel.ReportViewSetting.HideHorizontalLine && quickReportViewModel.ReportViewSetting.HideVerticalLine)
                    {
                        txtDataCell.Border = new StiBorder(StiBorderSides.Bottom, txtDataCell.Border.Color, txtDataCell.Border.Size, txtDataCell.Border.Style);
                    }
                }

                dataBand.Components.Add(txtDataCell);
            }

            report.Pages[0].Components.Add(dataBand);
            return report;
        }

        private static string GetColumnValue(QuickReportColumnConfig column, string dataSourceName, string functionTamplate)
        {
            string fullColumnName = "{0}";
            if (!string.IsNullOrEmpty(functionTamplate))
            {
                fullColumnName = functionTamplate;
            }

            fullColumnName = "{" + string.Format(fullColumnName, dataSourceName + "." + Stimulsoft.Report.CodeDom.StiCodeDomSerializator.ReplaceSymbols(column.Name)) + "}";
            return fullColumnName;
        }

        private static double GetSizeInInch(int pixel, int oneInchInPixel)
        {
            if (oneInchInPixel == 0)
            {
                return 0;
            }

            double inchValue = (double)pixel / (double)oneInchInPixel;

            return inchValue;
        }

        private const double LandscapeWidth = 11.69;
        private const double PortraitWidth = 8.27;
    }
}
