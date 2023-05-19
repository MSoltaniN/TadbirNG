using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.Threading.Tasks;
using SPPC.Framework.Common;
using SPPC.Framework.Mapper;

namespace SPPC.Tadbir.Persistence
{
    /// <summary>
    /// عملیات مورد نیاز برای خواندن اطلاعات از بانک اطلاعاتی اکسس را پیاده سازی می کند
    /// </summary>
    public class AccessRepository : IAccessRepository
    {
        /// <summary>
        /// نمونه جدیدی از این کلاس می سازد
        /// </summary>
        /// <param name="mapper">امکان نگاشت کلاس های مختلف به یکدیگر را فراهم می کند</param>
        public AccessRepository(IDomainMapper mapper)
        {
            _mapper = mapper;
        }

        /// <summary>
        /// به روش آسنکرون، اطلاعات جدول مشخص شده را از بانک اطلاعاتی اکسس در مسیر داده شده خوانده و برمی گرداند
        /// </summary>
        /// <typeparam name="TModel">نوع مدل نمایشی مورد نظر برای دریافت اطلاعات جدول اکسس</typeparam>
        /// <param name="mdbPath">مسیر فایل بانک اطلاعاتی اکسس</param>
        /// <param name="tableName">نام جدولی که اطلاعات آن باید خوانده شود</param>
        /// <returns>اطلاعات جدول مورد نظر به شکل مدل نمایشی داده شده</returns>
        public async Task<IList<TModel>> GetAllAsync<TModel>(string mdbPath, string tableName)
        {
            Verify.ArgumentNotNullOrEmptyString(mdbPath, nameof(mdbPath));
            Verify.ArgumentNotNullOrEmptyString(tableName, nameof(tableName));
            if (!File.Exists(mdbPath))
            {
                throw ExceptionBuilder.NewGenericException<FileNotFoundException>();
            }

            var all = new List<TModel>();
            string connection = String.Format(_connectionTemplate, mdbPath);
            var dbCommand = new OdbcCommand(String.Format(_getAllTemplate, tableName));
            using (var dbConnection = new OdbcConnection(connection))
            {
                dbCommand.Connection = dbConnection;
                await dbConnection.OpenAsync();
                var dbReader = dbCommand.ExecuteReader();
                while (dbReader.Read())
                {
                    all.Add(_mapper.Map<TModel>(dbReader));
                }

                dbReader.Close();
            }

            return all;
        }

        private const string _connectionTemplate = "Driver={{Microsoft Access Driver (*.mdb, *.accdb)}};Dbq={0}";
        private const string _getAllTemplate = "SELECT * FROM {0}";
        private readonly IDomainMapper _mapper;
    }
}
