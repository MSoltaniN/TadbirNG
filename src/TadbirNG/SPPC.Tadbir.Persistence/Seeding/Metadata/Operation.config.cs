using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SPPC.Tadbir.Model.Metadata;

namespace SPPC.Tadbir.Persistence.Seeding
{

    internal class OperationConfiguration : IEntityTypeConfiguration<Operation>
    {
        public void Configure(EntityTypeBuilder<Operation> builder)
        {

            builder.HasData(new Operation { Id = 1,  Name = "View", Description = "" });
            builder.HasData(new Operation { Id = 2,  Name = "Create", Description = "" });
            builder.HasData(new Operation { Id = 3,  Name = "Edit", Description = "" });
            builder.HasData(new Operation { Id = 4,  Name = "Delete", Description = "" });
            builder.HasData(new Operation { Id = 5,  Name = "Filter", Description = "" });
            builder.HasData(new Operation { Id = 6,  Name = "Print", Description = "" });
            builder.HasData(new Operation { Id = 7,  Name = "Save", Description = "" });
            builder.HasData(new Operation { Id = 8,  Name = "Archive", Description = "" });
            builder.HasData(new Operation { Id = 9,  Name = "SetDefault", Description = "" });
            builder.HasData(new Operation { Id = 10, Name = "Design", Description = "" });
            builder.HasData(new Operation { Id = 11, Name = "Check", Description = "" });
            builder.HasData(new Operation { Id = 12, Name = "UndoCheck", Description = "" });
            builder.HasData(new Operation { Id = 13, Name = "Confirm", Description = "" });
            builder.HasData(new Operation { Id = 14, Name = "UndoConfirm", Description = "" });
            builder.HasData(new Operation { Id = 15, Name = "Approve", Description = "" });
            builder.HasData(new Operation { Id = 16, Name = "UndoApprove", Description = "" });
            builder.HasData(new Operation { Id = 17, Name = "Finalize", Description = "" });
            builder.HasData(new Operation { Id = 18, Name = "UndoFinalize", Description = "" });
            builder.HasData(new Operation { Id = 19, Name = "Mark", Description = "" });
            builder.HasData(new Operation { Id = 20, Name = "QuickReportDesign", Description = "" });
            builder.HasData(new Operation { Id = 21, Name = "GroupDelete", Description = "" });
            builder.HasData(new Operation { Id = 30, Name = "ViewArchive", Description = "" });
            builder.HasData(new Operation { Id = 31, Name = "CalendarChange", Description = "" });
            builder.HasData(new Operation { Id = 32, Name = "CurrencyChange", Description = "" });
            builder.HasData(new Operation { Id = 33, Name = "DecimalCountChange", Description = "" });
            builder.HasData(new Operation { Id = 34, Name = "DefaultCodingChange", Description = "" });
            builder.HasData(new Operation { Id = 35, Name = "RoleAccess", Description = "" });
            builder.HasData(new Operation { Id = 36, Name = "CreateLine", Description = "" });
            builder.HasData(new Operation { Id = 37, Name = "EditLine", Description = "" });
            builder.HasData(new Operation { Id = 38, Name = "DeleteLine", Description = "" });
            builder.HasData(new Operation { Id = 39, Name = "GroupDeleteLines", Description = "" });
            builder.HasData(new Operation { Id = 40, Name = "CreateRate", Description = "" });
            builder.HasData(new Operation { Id = 41, Name = "EditRate", Description = "" });
            builder.HasData(new Operation { Id = 42, Name = "DeleteRate", Description = "" });
            builder.HasData(new Operation { Id = 43, Name = "PrintRates", Description = "" });
            builder.HasData(new Operation { Id = 44, Name = "GroupDeleteRates", Description = "" });
            builder.HasData(new Operation { Id = 45, Name = "ViewRates", Description = "" });
            builder.HasData(new Operation { Id = 46, Name = "GroupCheck", Description = "" });
            builder.HasData(new Operation { Id = 47, Name = "GroupUndoCheck", Description = "" });
            builder.HasData(new Operation { Id = 48, Name = "GroupFinalize", Description = "" });
            builder.HasData(new Operation { Id = 49, Name = "GroupUndoFinalize", Description = "" });
            builder.HasData(new Operation { Id = 50, Name = "GroupConfirm", Description = "" });
            builder.HasData(new Operation { Id = 51, Name = "GroupUndoConfirm", Description = "" });
            builder.HasData(new Operation { Id = 52, Name = "Normalize", Description = "" });
            builder.HasData(new Operation { Id = 53, Name = "GroupNormalize", Description = "" });
            builder.HasData(new Operation { Id = 54, Name = "Export", Description = "" });
            builder.HasData(new Operation { Id = 55, Name = "ExportRates", Description = "" });
            builder.HasData(new Operation { Id = 56, Name = "FilterRates", Description = "" });
            builder.HasData(new Operation { Id = 58, Name = "PrintPreview", Description = "" });
            builder.HasData(new Operation { Id = 60, Name = "CreatePages", Description = "" });
            builder.HasData(new Operation { Id = 61, Name = "DeletePages", Description = "" });
            builder.HasData(new Operation { Id = 62, Name = "CancelPage", Description = "" });
            builder.HasData(new Operation { Id = 63, Name = "UndoCancelPage", Description = "" });
            builder.HasData(new Operation { Id = 64, Name = "ConnectToCheck", Description = "" });
            builder.HasData(new Operation { Id = 65, Name = "DisconnectFromCheck", Description = "" });
            builder.HasData(new Operation { Id = 66, Name = "AssignCashRegisterUser", Description = "" });
            builder.HasData(new Operation { Id = 67, Name = "UndoArchive", Description = "" });
            builder.HasData(new Operation { Id = 68, Name = "Register", Description = "" });
            builder.HasData(new Operation { Id = 69, Name = "RemoveInvalidAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 70, Name = "AggregateAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 71, Name = "CreateAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 72, Name = "EditAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 73, Name = "DeleteAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 74, Name = "GroupDeleteAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 75, Name = "PrintAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 76, Name = "PrintPreviewAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 77, Name = "FilterAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 78, Name = "ExportAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 79, Name = "CreateCashAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 80, Name = "EditCashAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 81, Name = "DeleteCashAccountLine", Description = "" });
            builder.HasData(new Operation { Id = 82, Name = "GroupDeleteCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 83, Name = "PrintCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 84, Name = "PrintPreviewCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 85, Name = "FilterCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 86, Name = "ExportCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 87, Name = "RemoveInvalidCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 88, Name = "AggregateCashAccountLines", Description = "" });
            builder.HasData(new Operation { Id = 89, Name = "Deactivate", Description = "" });
            builder.HasData(new Operation { Id = 90, Name = "Reactivate", Description = "" });
            builder.HasData(new Operation { Id = 91, Name = "PrintForm", Description = "" });
            builder.HasData(new Operation { Id = 92, Name = "PrintPreviewForm", Description = "" });
            builder.HasData(new Operation { Id = 93, Name = "UndoRegister", Description = "" });

        }

    }
}