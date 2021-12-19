using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Cryptography;
using SPPC.Framework.Persistence;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Security;
using SPPC.Tadbir.Tests;
using SPPC.Tadbir.ViewModel.Auth;
using SPPC.Tools.Utility;

namespace SPPC.Tadbir.Persistence.Tests
{
    public abstract class RepositoryTestBase : TestBase
    {
        static RepositoryTestBase()
        {
            _connection = DbConnections.CompanyConnection;
            _sysConnection = DbConnections.SystemConnection;
        }

        protected static ISystemRepository GetSystemRepository(IRepositoryContext context)
        {
            var sysContext = GetRepositoryContext();
            var utility = new ReportDirectUtility(context);
            var logger = new OperationLogRepository(context, new LogConfigRepository(context), utility);
            var config = new ConfigRepository(context, logger, null);
            return new SystemRepository(
                new SecureRepository(context), new MetadataRepository(sysContext, config), config, logger);
        }

        protected static IRepositoryContext GetRepositoryContext()
        {
            var unitOfWork = GetUnitOfWork();
            var mapper = new DomainMapper(new CryptoService(new CertificateManager()));
            var dbConsole = new SqlServerConsole();
            var secContext = new SecurityContext(GetTestUserContext());
            var localizer = GetStringLocalizer();
            return new RepositoryContext(unitOfWork, mapper, secContext, dbConsole, localizer, null);
        }

        private static IAppUnitOfWork GetUnitOfWork()
        {
            var context = new TadbirContext() { ConnectionString = _connection };
            var builder = new DbContextOptionsBuilder<SystemContext>()
                .UseSqlServer(_sysConnection);
            var sysContext = new SystemContext(builder.Options);
            return new AppUnitOfWork(new DbContextAccessor(context, sysContext));
        }

        private static UserContextViewModel GetTestUserContext()
        {
            var userContext = new UserContextViewModel()
            {
                BranchId = 1,
                CompanyId = 1,
                Connection = _connection,
                FiscalPeriodId = 2,
                Id = 1,
                InventoryMode = (int)InventoryMode.Perpetual,
                Language = "fa",
                UserName = "admin"
            };
            userContext.Roles.Add(1);
            return userContext;
        }

        private static readonly string _connection;
        private static readonly string _sysConnection;
    }
}
