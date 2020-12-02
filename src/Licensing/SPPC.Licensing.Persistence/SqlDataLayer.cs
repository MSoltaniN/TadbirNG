using System;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using BabakSoft.Platform.Common;

namespace BabakSoft.Platform.Data
{
    /// <summary>
    /// Implements database access functions for SQL Server .NET data provider.
    /// All function implementations are provider-specific.
    /// </summary>
    public class SqlDataLayer : DataLayerBase
    {
        /// <summary>
        /// Initializes this instance using an arbitrary connection string
        /// and a valid provider type.
        /// </summary>
        /// <param name="connection">A text value that can be interpreted by
        /// SQL Server .NET provider as a valid connection string</param>
        /// <param name="provider">A ProviderType enumerated value which specifies
        /// which provider to use for this instance</param>
        public SqlDataLayer(string connection)
            : base(connection)
        {
        }

        /// <summary>
        /// Reads information about parameters of an existing stored procedure
        /// from SQL Server catalogs.
        /// </summary>
        /// <param name="dbCommand">An instance of SQL Server .NET command object
        /// (SqlCommand) whose parameter information should be derived.
        /// </param>
        protected override void DeriveParameters(IDbCommand dbCommand)
        {
            CheckState();
            Verify.ArgumentNotNull(dbCommand, "dbCommand");

            try
            {
                SqlCommand cmd = (SqlCommand)dbCommand;
                SqlCommandBuilder.DeriveParameters(cmd);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "SqlDataLayer.DeriveParameters", e.Message);
                throw;
            }
        }

        /// <summary>
        /// Creates and returns an instance of a connection object that
        /// corresponds to the data provider already set for this object.
        /// </summary>
        /// <returns>A valid IDbConnection object that corresponds to
        /// the data provider already set for this object. The connection
        /// is not automatically opened.</returns>
        protected override IDbConnection CreateConnection()
        {
            return new SqlConnection(_connection);
        }

        /// <summary>
        /// Creates and returns an instance of a command object that
        /// corresponds to the data provider already set for this object.
        /// </summary>
        /// <param name="command">SQL command to set in the output command object.</param>
        /// <param name="dbConnection">A valid IDbConnection object that
        /// corresponds to the data provider already set for this object.
        /// The connection need not be opened.</param>
        /// <param name="type">A System.Data.CommandType value that specifies
        /// how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>A valid IDbCommand object that corresponds to
        /// the data provider already set for this object.</returns>
        protected override IDbCommand CreateCommand(string command, IDbConnection dbConnection, CommandType type, int timeout)
        {
            IDbCommand dbCmd = new SqlCommand(command, dbConnection as SqlConnection)
            {
                CommandType = type,
                CommandTimeout = timeout
            };
            return dbCmd;
        }

        /// <summary>
        /// Creates and returns an instance of a data adapter object that
        /// corresponds to the data provider already set for this object.
        /// </summary>
        /// <returns>A valid IDbDataAdapter object that corresponds to
        /// the data provider already set for this object.</returns>
        protected override IDbDataAdapter CreateAdapter()
        {
            return new SqlDataAdapter();
        }
    }
}
