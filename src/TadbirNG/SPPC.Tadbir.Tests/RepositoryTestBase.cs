using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using SPPC.Framework.Cryptography;
using SPPC.Tadbir.Domain;
using SPPC.Tadbir.Mapper;
using SPPC.Tadbir.Persistence.Utility;
using SPPC.Tadbir.Resources;
using SPPC.Tadbir.Service;
using SPPC.Tadbir.ViewModel.Auth;

namespace SPPC.Tadbir.Persistence.Tests
{
    public abstract class RepositoryTestBase
    {
        protected static ISystemRepository GetSystemRepository(IRepositoryContext context)
        {
            var sysContext = GetRepositoryContext();
            var utility = new ReportDirectUtility(context);
            var logger = new OperationLogRepository(context, new LogConfigRepository(context), utility);
            var config = new ConfigRepository(context, logger);
            return new SystemRepository(
                new SecureRepository(context), new MetadataRepository(sysContext, config), config, logger);
        }

        protected static IRepositoryContext GetRepositoryContext()
        {
            var unitOfWork = GetUnitOfWork();
            var mapper = new DomainMapper(new CryptoService());
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

        private static IStringLocalizer<AppStrings> GetStringLocalizer()
        {
            var options = new LocalizationOptions()
            {
                ResourcesPath = @"..\..\..\src\Framework\SPPC.Tadbir.Resources"
            };
            var factory = new ResourceManagerStringLocalizerFactory(
                new OptionsWrapper<LocalizationOptions>(options), new NullLoggerFactory());
            return new StringLocalizer<AppStrings>(factory);
        }

        private const string _connection = @"Server=BE-LAPTOP;Database=NGTadbir;
User ID=NgTadbirUser;Password=Demo1234;Trusted_Connection=False";
        private const string _sysConnection = @"Server=BE-LAPTOP;Database=NGTadbirSys;
User ID=NgTadbirUser;Password=Demo1234;Trusted_Connection=False";
    }
}
