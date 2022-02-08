using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPPC.Framework.Common;
using SPPC.Framework.Persistence;
using SPPC.Licensing.Model;

namespace SPPC.Licensing.Persistence
{
    public class LicenseRepository : ILicenseRepository
    {
        public LicenseRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public int GetLicenseId(string customerKey, string licenseKey)
        {
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            int licenseId = repository
                .GetEntityQuery()
                .Where(lic => lic.Customer.CustomerKey == customerKey
                    && lic.LicenseKey == licenseKey)
                .Select(lic => lic.Id)
                .FirstOrDefault();
            return licenseId;
        }

        public LicenseModel GetLicense(string licenseKey, string customerKey)
        {
            var license = default(LicenseModel);
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            license = repository
                .GetByCriteria(lic => lic.LicenseKey == licenseKey)
                .SingleOrDefault();

            return license;
        }

        public async Task<LicenseFileModel> GetLicenseFileDataAsync(string licenseKey, string customerKey)
        {
            var licenseFile = default(LicenseFileModel);
            var repository = UnitOfWork.GetAsyncRepository<LicenseModel>();
            var license = await repository.GetFirstByCriteriaAsync(
                lic => lic.LicenseKey == licenseKey && lic.CustomerKey == customerKey,
                lic => lic.Customer);
            if (license != null)
            {
                licenseFile = new LicenseFileModel()
                {
                    CustomerName = license.Customer.CompanyName,
                    ContactName = String.Format(
                        "{0} {1}", license.Customer.ContactFirstName, license.Customer.ContactLastName),
                    CustomerKey = license.CustomerKey,
                    LicenseKey = license.LicenseKey,
                    HardwareKey = license.HardwareKey,
                    ClientKey = license.ClientKey,
                    Secret = license.Secret,
                    UserCount = license.UserCount,
                    Edition = license.Edition,
                    StartDate = license.StartDate,
                    EndDate = license.EndDate,
                    ActiveModules = license.ActiveModules,
                    IsActivated = license.IsActivated,
                    OfflineLimit = license.OfflineLimit
                };
            }

            return licenseFile;
        }

        public async Task<IList<LicenseModel>> GetLicensesAsync(int? customerId = null)
        {
            var repository = UnitOfWork.GetAsyncRepository<LicenseModel>();
            var query = repository.GetEntityQuery();
            if (customerId.HasValue)
            {
                query = query.Where(lic => lic.CustomerId == customerId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task SaveLicenseAsync(LicenseModel license)
        {
            Verify.ArgumentNotNull(license, nameof(license));
            var repository = UnitOfWork.GetAsyncRepository<LicenseModel>();
            if (license.Id == 0)
            {
                repository.Insert(license);
                await UnitOfWork.CommitAsync();
            }
            else
            {
                var existing = await repository.GetByIDAsync(license.Id);
                if (existing != null)
                {
                    UpdateValues(license, existing);
                    repository.Update(license);
                    await UnitOfWork.CommitAsync();
                }
            }
        }

        public bool? GetActivationStatus(string licenseKey)
        {
            var repository = UnitOfWork.GetRepository<LicenseModel>();
            bool? isActivated = repository
                .GetEntityQuery()
                .Where(lic => lic.LicenseKey == licenseKey)
                .Select(lic => lic.IsActivated)
                .FirstOrDefault();
            return isActivated;
        }

        private IUnitOfWork UnitOfWork { get; }

        private static void UpdateValues(LicenseModel license, LicenseModel existing)
        {
            existing.UserCount = license.UserCount;
            existing.Edition = license.Edition;
            existing.StartDate = license.StartDate;
            existing.EndDate = license.EndDate;
            existing.ActiveModules = license.ActiveModules;
        }
    }
}
