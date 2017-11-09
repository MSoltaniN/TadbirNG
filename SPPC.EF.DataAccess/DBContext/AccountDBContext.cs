﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;

namespace SPPC.Tadbir.DataAccess
{
    public partial class AccountDBContext : DbContext
    {
        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ActivityInstanceEvent> ActivityInstanceEvent { get; set; }
        public virtual DbSet<BookmarkResumptionEvent> BookmarkResumptionEvent { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<BusinessPartner> BusinessPartner { get; set; }
        public virtual DbSet<BusinessUnit> BusinessUnit { get; set; }
        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<CostCenter> CostCenter { get; set; }
        public virtual DbSet<Currency> Currency { get; set; }
        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<CustomTrackingEvent> CustomTrackingEvent { get; set; }
        public virtual DbSet<DetailAccount> DetailAccount { get; set; }
        public virtual DbSet<Document> Document { get; set; }
        public virtual DbSet<DocumentAction> DocumentAction { get; set; }
        public virtual DbSet<DocumentStatus> DocumentStatus { get; set; }
        public virtual DbSet<DocumentType> DocumentType { get; set; }
        public virtual DbSet<ExtendedActivityEvent> ExtendedActivityEvent { get; set; }
        public virtual DbSet<FiscalPeriod> FiscalPeriod { get; set; }
        public virtual DbSet<FullAccount> FullAccount { get; set; }
        public virtual DbSet<FullDetail> FullDetail { get; set; }
        public virtual DbSet<FullDetailType> FullDetailType { get; set; }
        public virtual DbSet<Invoice> Invoice { get; set; }
        public virtual DbSet<InvoiceLine> InvoiceLine { get; set; }
        public virtual DbSet<IssueReceiptVoucher> IssueReceiptVoucher { get; set; }
        public virtual DbSet<IssueReceiptVoucherLine> IssueReceiptVoucherLine { get; set; }
        public virtual DbSet<IssueReceiptVoucherType> IssueReceiptVoucherType { get; set; }
        public virtual DbSet<Permission> Permission { get; set; }
        public virtual DbSet<PermissionGroup> PermissionGroup { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductCategory> ProductCategory { get; set; }
        public virtual DbSet<ProductInventory> ProductInventory { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<RequisitionVoucher> RequisitionVoucher { get; set; }
        public virtual DbSet<RequisitionVoucherLine> RequisitionVoucherLine { get; set; }
        public virtual DbSet<RequisitionVoucherType> RequisitionVoucherType { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoleBranch> RoleBranch { get; set; }
        public virtual DbSet<RolePermission> RolePermission { get; set; }
        public virtual DbSet<ServiceJob> ServiceJob { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<TransactionLine> TransactionLine { get; set; }
        public virtual DbSet<Uom> Uom { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserRole> UserRole { get; set; }
        public virtual DbSet<Warehouse> Warehouse { get; set; }
        public virtual DbSet<WorkflowInstanceEvent> WorkflowInstanceEvent { get; set; }
        public virtual DbSet<WorkItem> WorkItem { get; set; }
        public virtual DbSet<WorkItemDocument> WorkItemDocument { get; set; }
        public virtual DbSet<WorkItemHistory> WorkItemHistory { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["TadbirConnectionString"].ConnectionString;

                    optionsBuilder.UseSqlServer(connectionString);
                }
                catch
                {
                    ////TODO: Log error exception
                }
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account", "Finance");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(512);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_Account_Corporate_Branch");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_Account_Finance_FiscalPeriod");
            });

            modelBuilder.Entity<ActivityInstanceEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("ActivityInstanceEvent", "WFTracking");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ActivityId).HasMaxLength(256);

                entity.Property(e => e.ActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.ActivityName).HasMaxLength(1024);

                entity.Property(e => e.ActivityRecordType)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ActivityType).HasMaxLength(2048);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.State).HasMaxLength(128);

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<BookmarkResumptionEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("BookmarkResumptionEvent", "WFTracking");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.BookmarkName).HasMaxLength(1024);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OwnerActivityId).HasMaxLength(256);

                entity.Property(e => e.OwnerActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.OwnerActivityName).HasMaxLength(1024);

                entity.Property(e => e.OwnerActivityType).HasMaxLength(2048);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch", "Corporate");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Branch)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Corporate_Branch_Corporate_Company");
            });

            modelBuilder.Entity<BusinessPartner>(entity =>
            {
                entity.HasKey(e => e.PartnerId);

                entity.ToTable("BusinessPartner", "Contact");

                entity.Property(e => e.PartnerId).HasColumnName("PartnerID");

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.CommerceCode).HasMaxLength(64);

                entity.Property(e => e.Email).HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<BusinessUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId);

                entity.ToTable("BusinessUnit", "Corporate");

                entity.Property(e => e.UnitId).HasColumnName("UnitID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company", "Corporate");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<CostCenter>(entity =>
            {
                entity.ToTable("CostCenter", "Finance");

                entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.FullCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Finance_CostCenter_Finance_Parent");
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency", "Finance");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer", "Contact");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Address).HasMaxLength(256);

                entity.Property(e => e.CommerceCode).HasMaxLength(64);

                entity.Property(e => e.Email).HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Phone).HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<CustomTrackingEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("CustomTrackingEvent", "WFTracking");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ActivityId).HasMaxLength(256);

                entity.Property(e => e.ActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.ActivityName).HasMaxLength(1024);

                entity.Property(e => e.ActivityType).HasMaxLength(2048);

                entity.Property(e => e.CustomRecordName).HasMaxLength(2048);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<DetailAccount>(entity =>
            {
                entity.HasKey(e => e.DetailId);

                entity.ToTable("DetailAccount", "Finance");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.FullCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Finance_DetailAccount_Finance_Parent");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document", "Core");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.EntityNo)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.OperationalStatus)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Core_Document_Core_DocumentStatus");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Document)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Core_Document_Core_DocumentType");
            });

            modelBuilder.Entity<DocumentAction>(entity =>
            {
                entity.HasKey(e => e.ActionId);

                entity.ToTable("DocumentAction", "Core");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.ApprovedById).HasColumnName("ApprovedByID");

                entity.Property(e => e.ApprovedDate).HasColumnType("datetime");

                entity.Property(e => e.ConfirmedById).HasColumnName("ConfirmedByID");

                entity.Property(e => e.ConfirmedDate).HasColumnType("datetime");

                entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.ModifiedById).HasColumnName("ModifiedByID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ApprovedBy)
                    .WithMany(p => p.DocumentActionApprovedBy)
                    .HasForeignKey(d => d.ApprovedById)
                    .HasConstraintName("FK_Core_DocumentAction_Auth_ApprovedBy");

                entity.HasOne(d => d.ConfirmedBy)
                    .WithMany(p => p.DocumentActionConfirmedBy)
                    .HasForeignKey(d => d.ConfirmedById)
                    .HasConstraintName("FK_Core_DocumentAction_Auth_ConfirmedBy");

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.DocumentActionCreatedBy)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Core_DocumentAction_Auth_CreatedBy");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.DocumentAction)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Core_DocumentAction_Core_Document");

                entity.HasOne(d => d.ModifiedBy)
                    .WithMany(p => p.DocumentActionModifiedBy)
                    .HasForeignKey(d => d.ModifiedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Core_DocumentAction_Auth_ModifiedBy");
            });

            modelBuilder.Entity<DocumentStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("DocumentStatus", "Core");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("DocumentType", "Core");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<ExtendedActivityEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("ExtendedActivityEvent", "WFTracking");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ActivityId).HasMaxLength(256);

                entity.Property(e => e.ActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.ActivityName).HasMaxLength(1024);

                entity.Property(e => e.ActivityRecordType)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ActivityType).HasMaxLength(2048);

                entity.Property(e => e.ChildActivityId).HasMaxLength(256);

                entity.Property(e => e.ChildActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.ChildActivityName).HasMaxLength(1024);

                entity.Property(e => e.ChildActivityType).HasMaxLength(2048);

                entity.Property(e => e.FaultHandlerActivityId).HasMaxLength(256);

                entity.Property(e => e.FaultHandlerActivityInstanceId).HasMaxLength(256);

                entity.Property(e => e.FaultHandlerActivityName).HasMaxLength(1024);

                entity.Property(e => e.FaultHandlerActivityType).HasMaxLength(2048);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<FiscalPeriod>(entity =>
            {
                entity.ToTable("FiscalPeriod", "Finance");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.CompanyId).HasColumnName("CompanyID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.FiscalPeriod)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_FiscalPeriod_Corporate_Company");
            });

            modelBuilder.Entity<FullAccount>(entity =>
            {
                entity.ToTable("FullAccount", "Finance");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.CostCenterId).HasColumnName("CostCenterID");

                entity.Property(e => e.DetailId).HasColumnName("DetailID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.FullAccount)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_FullAccount_Finance_Account");

                entity.HasOne(d => d.CostCenter)
                    .WithMany(p => p.FullAccount)
                    .HasForeignKey(d => d.CostCenterId)
                    .HasConstraintName("FK_Finance_FullAccount_Finance_CostCenter");

                entity.HasOne(d => d.Detail)
                    .WithMany(p => p.FullAccount)
                    .HasForeignKey(d => d.DetailId)
                    .HasConstraintName("FK_Finance_FullAccount_Finance_Detail");

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.FullAccount)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_Finance_FullAccount_Finance_Project");
            });

            modelBuilder.Entity<FullDetail>(entity =>
            {
                entity.ToTable("FullDetail", "Finance");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.Detail2Id).HasColumnName("Detail2ID");

                entity.Property(e => e.Detail3Id).HasColumnName("Detail3ID");

                entity.Property(e => e.Detail4Id).HasColumnName("Detail4ID");

                entity.Property(e => e.Detail5Id).HasColumnName("Detail5ID");

                entity.Property(e => e.Detail6Id).HasColumnName("Detail6ID");

                entity.Property(e => e.Detail7Id).HasColumnName("Detail7ID");

                entity.Property(e => e.Detail8Id).HasColumnName("Detail8ID");

                entity.Property(e => e.Detail9Id).HasColumnName("Detail9ID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.HasOne(d => d.Detail2)
                    .WithMany(p => p.FullDetailDetail2)
                    .HasForeignKey(d => d.Detail2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail2");

                entity.HasOne(d => d.Detail3)
                    .WithMany(p => p.FullDetailDetail3)
                    .HasForeignKey(d => d.Detail3Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail3");

                entity.HasOne(d => d.Detail4)
                    .WithMany(p => p.FullDetailDetail4)
                    .HasForeignKey(d => d.Detail4Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail4");

                entity.HasOne(d => d.Detail5)
                    .WithMany(p => p.FullDetailDetail5)
                    .HasForeignKey(d => d.Detail5Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail5");

                entity.HasOne(d => d.Detail6)
                    .WithMany(p => p.FullDetailDetail6)
                    .HasForeignKey(d => d.Detail6Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail6");

                entity.HasOne(d => d.Detail7)
                    .WithMany(p => p.FullDetailDetail7)
                    .HasForeignKey(d => d.Detail7Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail7");

                entity.HasOne(d => d.Detail8)
                    .WithMany(p => p.FullDetailDetail8)
                    .HasForeignKey(d => d.Detail8Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail8");

                entity.HasOne(d => d.Detail9)
                    .WithMany(p => p.FullDetailDetail9)
                    .HasForeignKey(d => d.Detail9Id)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_Detail9");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FullDetail)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK_Finance_FullDetail_Finance_FullDetailType");
            });

            modelBuilder.Entity<FullDetailType>(entity =>
            {
                entity.HasKey(e => e.TypeId);

                entity.ToTable("FullDetailType", "Finance");

                entity.Property(e => e.TypeId).HasColumnName("TypeID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice", "Sales");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.ContractNo).HasMaxLength(64);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.IssueReceiptVoucherId).HasColumnName("IssueReceiptVoucherID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PartnerFullAccountId).HasColumnName("PartnerFullAccountID");

                entity.Property(e => e.PartnerFullDetailId).HasColumnName("PartnerFullDetailID");

                entity.Property(e => e.PartnerId).HasColumnName("PartnerID");

                entity.Property(e => e.Reference).HasMaxLength(64);

                entity.Property(e => e.ReferenceInvoiceId).HasColumnName("ReferenceInvoiceID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ShipmentNo).HasMaxLength(64);

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Corporate_Branch");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Contact_Customer");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Core_Document");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Finance_FiscalPeriod");

                entity.HasOne(d => d.FullAccount)
                    .WithMany(p => p.InvoiceFullAccount)
                    .HasForeignKey(d => d.FullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Finance_FullAccount");

                entity.HasOne(d => d.FullDetail)
                    .WithMany(p => p.InvoiceFullDetail)
                    .HasForeignKey(d => d.FullDetailId)
                    .HasConstraintName("FK_Sales_Invoice_Finance_FullDetail");

                entity.HasOne(d => d.IssueReceiptVoucher)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.IssueReceiptVoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Warehousing_IssueReceiptVoucher");

                entity.HasOne(d => d.PartnerFullAccount)
                    .WithMany(p => p.InvoicePartnerFullAccount)
                    .HasForeignKey(d => d.PartnerFullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Finance_PartnerFullAccount");

                entity.HasOne(d => d.PartnerFullDetail)
                    .WithMany(p => p.InvoicePartnerFullDetail)
                    .HasForeignKey(d => d.PartnerFullDetailId)
                    .HasConstraintName("FK_Sales_Invoice_Finance_PartnerFullDetail");

                entity.HasOne(d => d.Partner)
                    .WithMany(p => p.Invoice)
                    .HasForeignKey(d => d.PartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Contact_BusinessPartner");

                entity.HasOne(d => d.ReferenceInvoice)
                    .WithMany(p => p.InverseReferenceInvoice)
                    .HasForeignKey(d => d.ReferenceInvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_Invoice_Sales_ReferenceInvoice");
            });

            modelBuilder.Entity<InvoiceLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.ToTable("InvoiceLine", "Sales");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RequisitionVoucherId).HasColumnName("RequisitionVoucherID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UomId).HasColumnName("UomID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Corporate_Branch");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Finance_Currency");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Finance_FiscalPeriod");

                entity.HasOne(d => d.FullAccount)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.FullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Finance_FullAccount");

                entity.HasOne(d => d.FullDetail)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.FullDetailId)
                    .HasConstraintName("FK_Sales_InvoiceLine_Finance_FullDetail");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.InvoiceId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Sales_Invoice");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Product");

                entity.HasOne(d => d.RequisitionVoucher)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.RequisitionVoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Procurement_RequisitionVoucher");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.UomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Uom");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.InvoiceLine)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Sales_InvoiceLine_Inventory_Warehouse");
            });

            modelBuilder.Entity<IssueReceiptVoucher>(entity =>
            {
                entity.HasKey(e => e.VoucherId);

                entity.ToTable("IssueReceiptVoucher", "Warehousing");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.Property(e => e.ActingPartnerId).HasColumnName("ActingPartnerID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.PartnerFullAccountId).HasColumnName("PartnerFullAccountID");

                entity.Property(e => e.PartnerFullDetailId).HasColumnName("PartnerFullDetailID");

                entity.Property(e => e.PricedVoucherId).HasColumnName("PricedVoucherID");

                entity.Property(e => e.Reference).HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.ActingPartner)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.ActingPartnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Contact_ActingPartner");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Corporate_Branch");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Core_Document");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_FiscalPeriod");

                entity.HasOne(d => d.PartnerFullAccount)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.PartnerFullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullAccount");

                entity.HasOne(d => d.PartnerFullDetail)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.PartnerFullDetailId)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Finance_PartnerFullDetail");

                entity.HasOne(d => d.PricedVoucher)
                    .WithMany(p => p.InversePricedVoucher)
                    .HasForeignKey(d => d.PricedVoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Warehousing_PricedVoucher");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.IssueReceiptVoucher)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucher_Inventory_Warehouse");
            });

            modelBuilder.Entity<IssueReceiptVoucherLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.ToTable("IssueReceiptVoucherLine", "Warehousing");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.RequisitionVoucherId).HasColumnName("RequisitionVoucherID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UomId).HasColumnName("UomID");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Corporate_Branch");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Finance_Currency");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Finance_FiscalPeriod");

                entity.HasOne(d => d.FullAccount)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.FullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Finance_FullAccount");

                entity.HasOne(d => d.FullDetail)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.FullDetailId)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Finance_FullDetail");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Inventory_Product");

                entity.HasOne(d => d.RequisitionVoucher)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.RequisitionVoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Procurement_RequisitionVoucher");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.UomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Inventory_UOM");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Warehousing_Voucher");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.IssueReceiptVoucherLine)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Warehousing_IssueReceiptVoucherLine_Inventory_Warehouse");
            });

            modelBuilder.Entity<IssueReceiptVoucherType>(entity =>
            {
                entity.HasKey(e => e.VoucherTypeId);

                entity.ToTable("IssueReceiptVoucherType", "Warehousing");

                entity.Property(e => e.VoucherTypeId).HasColumnName("VoucherTypeID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission", "Auth");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.Flag).HasDefaultValueSql("((0))");

                entity.Property(e => e.GroupId).HasColumnName("GroupID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.Permission)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_Permission_Auth_PermissionGroup");
            });

            modelBuilder.Entity<PermissionGroup>(entity =>
            {
                entity.ToTable("PermissionGroup", "Auth");

                entity.Property(e => e.PermissionGroupId).HasColumnName("PermissionGroupID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.EntityName).HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("Person", "Contact");

                entity.Property(e => e.PersonId).HasColumnName("PersonID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Contact_Person_Auth_User");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product", "Inventory");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_Product_Inventory_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("ProductCategory", "Inventory");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.FullCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Inventory_ProductCategory_Inventory_Parent");
            });

            modelBuilder.Entity<ProductInventory>(entity =>
            {
                entity.ToTable("ProductInventory", "Inventory");

                entity.Property(e => e.ProductInventoryId).HasColumnName("ProductInventoryID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UomId).HasColumnName("UomID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.ProductInventory)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ProductInventory_Corporate_Branch");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.ProductInventory)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ProductInventory_Finance_FiscalPeriod");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductInventory)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ProductInventory_Inventory_Product");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.ProductInventory)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Inventory_ProductInventory_Inventory_Warehouse");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project", "Finance");

                entity.Property(e => e.ProjectId).HasColumnName("ProjectID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.FullCode)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.Level).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Finance_Project_Finance_Parent");
            });

            modelBuilder.Entity<RequisitionVoucher>(entity =>
            {
                entity.HasKey(e => e.VoucherId);

                entity.ToTable("RequisitionVoucher", "Procurement");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.OrderedDate).HasColumnType("datetime");

                entity.Property(e => e.PromisedDate).HasColumnType("datetime");

                entity.Property(e => e.Reason).HasMaxLength(256);

                entity.Property(e => e.ReceiverId).HasColumnName("ReceiverID");

                entity.Property(e => e.ReceiverUnitId).HasColumnName("ReceiverUnitID");

                entity.Property(e => e.Reference).HasMaxLength(64);

                entity.Property(e => e.RequesterId).HasColumnName("RequesterID");

                entity.Property(e => e.RequesterUnitId).HasColumnName("RequesterUnitID");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.ServiceJobId).HasColumnName("ServiceJobID");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.VoucherTypeId).HasColumnName("VoucherTypeID");

                entity.Property(e => e.WarehouseComment).HasMaxLength(256);

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_Branch");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Core_Document");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FiscalPeriod");

                entity.HasOne(d => d.FullAccount)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.FullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FullAccount");

                entity.HasOne(d => d.FullDetail)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.FullDetailId)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Finance_FullDetail");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.RequisitionVoucherReceiver)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Contact_Receiver");

                entity.HasOne(d => d.ReceiverUnit)
                    .WithMany(p => p.RequisitionVoucherReceiverUnit)
                    .HasForeignKey(d => d.ReceiverUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_ReceiverUnit");

                entity.HasOne(d => d.Requester)
                    .WithMany(p => p.RequisitionVoucherRequester)
                    .HasForeignKey(d => d.RequesterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Contact_Requester");

                entity.HasOne(d => d.RequesterUnit)
                    .WithMany(p => p.RequisitionVoucherRequesterUnit)
                    .HasForeignKey(d => d.RequesterUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Corporate_RequesterUnit");

                entity.HasOne(d => d.ServiceJob)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.ServiceJobId)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Core_ServiceJob");

                entity.HasOne(d => d.VoucherType)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.VoucherTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Procurement_RequisitionVoucherType");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.RequisitionVoucher)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucher_Inventory_Warehouse");
            });

            modelBuilder.Entity<RequisitionVoucherLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.ToTable("RequisitionVoucherLine", "Procurement");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.ActionId).HasColumnName("ActionID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.DeliveredDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.FullAccountId).HasColumnName("FullAccountID");

                entity.Property(e => e.FullDetailId).HasColumnName("FullDetailID");

                entity.Property(e => e.IsActive).HasDefaultValueSql("((0))");

                entity.Property(e => e.LastOrderedDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.PromisedDate).HasColumnType("datetime");

                entity.Property(e => e.RequiredDate).HasColumnType("datetime");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Timestamp)
                    .IsRequired()
                    .IsRowVersion();

                entity.Property(e => e.UomId).HasColumnName("UomID");

                entity.Property(e => e.VoucherId).HasColumnName("VoucherID");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.HasOne(d => d.Action)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.ActionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Core_DocumentAction");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Corporate_Branch");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FiscalPeriod");

                entity.HasOne(d => d.FullAccount)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.FullAccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FullAccount");

                entity.HasOne(d => d.FullDetail)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.FullDetailId)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Finance_FullDetail");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Product");

                entity.HasOne(d => d.Uom)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.UomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Uom");

                entity.HasOne(d => d.Voucher)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.VoucherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Procurement_RequisitionVoucher");

                entity.HasOne(d => d.Warehouse)
                    .WithMany(p => p.RequisitionVoucherLine)
                    .HasForeignKey(d => d.WarehouseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Procurement_RequisitionVoucherLine_Inventory_Warehouse");
            });

            modelBuilder.Entity<RequisitionVoucherType>(entity =>
            {
                entity.HasKey(e => e.VoucherTypeId);

                entity.ToTable("RequisitionVoucherType", "Procurement");

                entity.Property(e => e.VoucherTypeId).HasColumnName("VoucherTypeID");

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role", "Auth");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<RoleBranch>(entity =>
            {
                entity.ToTable("RoleBranch", "Auth");

                entity.Property(e => e.RoleBranchId).HasColumnName("RoleBranchID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.RoleBranch)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_RoleBranch_Corporate_Branch");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleBranch)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_RoleBranch_Auth_Role");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.ToTable("RolePermission", "Auth");

                entity.Property(e => e.RolePermissionId).HasColumnName("RolePermissionID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PermissionId).HasColumnName("PermissionID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Permission)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.PermissionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_RolePermission_Auth_Permission");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RolePermission)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_RolePermission_Auth_Role");
            });

            modelBuilder.Entity<ServiceJob>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.ToTable("ServiceJob", "Core");

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction", "Finance");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.No)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_Transaction_Corporate_Branch");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_Transaction_Core_Document");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_Transaction_Finance_FiscalPeriod");
            });

            modelBuilder.Entity<TransactionLine>(entity =>
            {
                entity.HasKey(e => e.LineId);

                entity.ToTable("TransactionLine", "Finance");

                entity.Property(e => e.LineId).HasColumnName("LineID");

                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.BranchId).HasColumnName("BranchID");

                entity.Property(e => e.Credit).HasColumnType("money");

                entity.Property(e => e.CurrencyId).HasColumnName("CurrencyID");

                entity.Property(e => e.Debit).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(512);

                entity.Property(e => e.FiscalPeriodId).HasColumnName("FiscalPeriodID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TransactionId).HasColumnName("TransactionID");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.TransactionLine)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_TransactionLine_Finance_Account");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.TransactionLine)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_TransactionLine_Corporate_Branch");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.TransactionLine)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_TransactionLine_Finance_Currency");

                entity.HasOne(d => d.FiscalPeriod)
                    .WithMany(p => p.TransactionLine)
                    .HasForeignKey(d => d.FiscalPeriodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_TransactionLine_Finance_FiscalPeriod");

                entity.HasOne(d => d.Transaction)
                    .WithMany(p => p.TransactionLine)
                    .HasForeignKey(d => d.TransactionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Finance_TransactionLine_Finance_Transaction");
            });

            modelBuilder.Entity<Uom>(entity =>
            {
                entity.ToTable("UOM", "Inventory");

                entity.Property(e => e.UomId).HasColumnName("UomID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "Auth");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.LastLoginDate).HasColumnType("datetime");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(256)
                    .IsUnicode(false);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole", "Auth");

                entity.Property(e => e.UserRoleId).HasColumnName("UserRoleID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_UserRole_Auth_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRole)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Auth_UserRole_Auth_User");
            });

            modelBuilder.Entity<Warehouse>(entity =>
            {
                entity.ToTable("Warehouse", "Inventory");

                entity.Property(e => e.WarehouseId).HasColumnName("WarehouseID");

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<WorkflowInstanceEvent>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.ToTable("WorkflowInstanceEvent", "WFTracking");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ActivityDefinition).HasMaxLength(256);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reason).HasMaxLength(2048);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.State).HasMaxLength(128);

                entity.Property(e => e.TimeCreated).HasColumnType("datetime");
            });

            modelBuilder.Entity<WorkItem>(entity =>
            {
                entity.ToTable("WorkItem", "Workflow");

                entity.Property(e => e.WorkItemId).HasColumnName("WorkItemID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.CreatedById).HasColumnName("CreatedByID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Remarks).HasMaxLength(1024);

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TargetId).HasColumnName("TargetID");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.HasOne(d => d.CreatedBy)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.CreatedById)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workflow_WorkItem_Auth_User");

                entity.HasOne(d => d.Target)
                    .WithMany(p => p.WorkItem)
                    .HasForeignKey(d => d.TargetId)
                    .HasConstraintName("FK_Workflow_WorkItem_Auth_Role");
            });

            modelBuilder.Entity<WorkItemDocument>(entity =>
            {
                entity.HasKey(e => e.DocumentItemId);

                entity.ToTable("WorkItemDocument", "Workflow");

                entity.Property(e => e.DocumentItemId).HasColumnName("DocumentItemID");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.DocumentType)
                    .IsRequired()
                    .HasMaxLength(128)
                    .IsUnicode(false);

                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.WorkItemId).HasColumnName("WorkItemID");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.WorkItemDocument)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workflow_WorkItemDocument_Core_Document");

                entity.HasOne(d => d.WorkItem)
                    .WithMany(p => p.WorkItemDocument)
                    .HasForeignKey(d => d.WorkItemId)
                    .HasConstraintName("FK_Workflow_WorkItemDocument_Workflow_WorkItem");
            });

            modelBuilder.Entity<WorkItemHistory>(entity =>
            {
                entity.HasKey(e => e.HistoryItemId);

                entity.ToTable("WorkItemHistory", "Workflow");

                entity.Property(e => e.HistoryItemId).HasColumnName("HistoryItemID");

                entity.Property(e => e.Action)
                    .IsRequired()
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DocumentId).HasColumnName("DocumentID");

                entity.Property(e => e.EntityId).HasColumnName("EntityID");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasMaxLength(16);

                entity.Property(e => e.Remarks).HasMaxLength(1024);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.Rowguid)
                    .HasColumnName("rowguid")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Document)
                    .WithMany(p => p.WorkItemHistory)
                    .HasForeignKey(d => d.DocumentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workflow_WorkItemHistory_Core_Document");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.WorkItemHistory)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workflow_WorkItemHistory_Auth_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.WorkItemHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Workflow_WorkItemHistory_Auth_User");
            });
        }
    }
}
