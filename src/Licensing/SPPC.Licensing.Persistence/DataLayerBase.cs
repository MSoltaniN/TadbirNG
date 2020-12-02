using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using BabakSoft.Platform.Common;

namespace BabakSoft.Platform.Data
{
    /// <summary>
    /// Implements common database access functions. This is the base class for provider-specific classes.
    /// </summary>
    public abstract class DataLayerBase
    {
        /// <summary>
        /// Initializes this instance using an arbitrary connection string
        /// and a valid provider type.
        /// This constructor is meant to be called only by derived classes.
        /// </summary>
        /// <param name="connection">A text value that can be interpreted as a
        /// connection string</param>
        protected DataLayerBase(string connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Gets the connection string for this object. This should be
        /// a valid connection string for this object's data provider.
        /// </summary>
        public string ConnectionString
        {
            get { return _connection; }
        }

        /// <summary>
        /// Executes a database query that returns a single value.
        /// </summary>
        /// <param name="command">SQL Command to execute.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>The single value returned by the database query.</returns>
        /// <remarks>If the database query returns multiple values in a
        /// record, the first value is returned. If the database query
        /// returns multiple rows, the first value of the first row is
        /// returned.</remarks>
        public object QueryScalar(string command, CommandType type = CommandType.Text, int timeout = DefaultTimeout)
        {
            if (type == CommandType.TableDirect)
                throw ExceptionBuilder.NewArgumentException();

            IDbCommand cmdScalar = null;
            object objScalar = null;
            try
            {
                cmdScalar = GetNonParametericCommand(command, type, timeout);
                NormalizeCommand(cmdScalar);
                objScalar = cmdScalar.ExecuteScalar();
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.QueryScalar", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdScalar);
            }

            return objScalar;
        }

        /// <summary>
        /// Executes a database query that returns a single value.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This should be the name of an existing stored procedure.
        /// </param>
        /// <param name="paramList">An object array that contains parameters
        /// required by the given stored procedure.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>The single value returned by the database query.</returns>
        /// <remarks>If the database query returns multiple values in a
        /// record, the first value is returned. If the database query
        /// returns multiple rows, the first value of the first row is
        /// returned.</remarks>
        public object QueryScalar(string command, object[] paramList, int timeout = DefaultTimeout)
        {
            if (paramList == null || paramList.Length == 0)
                return QueryScalar(command, CommandType.StoredProcedure, timeout);

            IDbCommand cmdScalar = null;
            object objScalar = null;
            try
            {
                cmdScalar = GetParametericCommand(command, paramList, timeout);
                NormalizeCommand(cmdScalar);
                objScalar = cmdScalar.ExecuteScalar();
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.QueryScalar", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdScalar);
            }

            return objScalar;
        }

        /// <summary>
        /// Executes a database query and returns query result as a
        /// DataTable object.
        /// </summary>
        /// <param name="command">SQL command to execute.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.</param>
        /// <returns>A DataTable object containing all rows returned by
        /// database query.</returns>
        public DataTable Query(string command, CommandType type = CommandType.Text, int timeout = DefaultTimeout)
        {
            IDbCommand cmdTable = null;
            DataTable table = null;
            try
            {
                cmdTable = GetNonParametericCommand(command, type, timeout);
                NormalizeCommand(cmdTable);
                table = GetQueryResult(cmdTable);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.Query", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdTable);
            }

            return table;
        }

        /// <summary>
        /// Executes a stored procedure and returns query result as a
        /// DataTable object.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This should be the name of an existing stored procedure.
        /// </param>
        /// <param name="paramList">An object array that contains parameters
        /// required by the given stored procedure.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>A DataTable object containing all rows returned by
        /// stored procedure.</returns>
        public DataTable Query(string command, object[] paramList, int timeout = DefaultTimeout)
        {
            if (paramList == null || paramList.Length == 0)
                return Query(command, CommandType.StoredProcedure, timeout);

            IDbCommand cmdTable = null;
            DataTable table = null;
            try
            {
                cmdTable = GetParametericCommand(command, paramList, timeout);
                NormalizeCommand(cmdTable);
                table = GetQueryResult(cmdTable);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.Query", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdTable);
            }

            return table;
        }

        /// <summary>
        /// Executes a database query that doesn't return any value.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This overload uses command parameter as a SQL statement.</param>
        /// <returns>The number of rows affected by executing the query.
        /// </returns>
        public int ExecuteNonQuery(string command)
        {
            return ExecuteNonQuery(command, CommandType.Text, DefaultTimeout);
        }

        /// <summary>
        /// Executes a database query that doesn't return any value.
        /// </summary>
        /// <param name="command">SQL Command to execute.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <returns>The number of rows affected by executing the query.
        /// </returns>
        public int ExecuteNonQuery(string command, CommandType type)
        {
            return (ExecuteNonQuery(command, type, DefaultTimeout));
        }

        /// <summary>
        /// Executes a database query that doesn't return any value.
        /// </summary>
        /// <param name="command">SQL Command to execute.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>The number of rows affected by executing the query.
        /// </returns>
        public int ExecuteNonQuery(string command, CommandType type, int timeout)
        {
            if (type == CommandType.TableDirect)
                ExceptionBuilder.NewArgumentException();

            int affected = -1;
            IDbCommand cmdAction = null;
            try
            {
                cmdAction = GetNonParametericCommand(command, type, timeout);
                NormalizeCommand(cmdAction);
                affected = ExecuteCommand(cmdAction);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.ExecuteNonQuery", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdAction);
            }

            return affected;
        }

        /// <summary>
        /// Executes a database query that doesn't return any value.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This should be the name of an existing stored procedure.
        /// </param>
        /// <param name="paramList">An object array that contains parameters
        /// required by the given stored procedure.</param>
        /// <returns>The number of rows affected by executing the query.
        /// </returns>
        public int ExecuteNonQuery(string command, object[] paramList)
        {
            return (ExecuteNonQuery(command, paramList, DefaultTimeout));
        }

        /// <summary>
        /// Executes a database query that doesn't return any value.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This should be the name of an existing stored procedure.
        /// </param>
        /// <param name="paramList">An object array that contains parameters
        /// required by the given stored procedure.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>The number of rows affected by executing the query.
        /// </returns>
        public int ExecuteNonQuery(string command, object[] paramList, int timeout)
        {
            if (paramList == null || paramList.Length == 0)
                return ExecuteNonQuery(command, CommandType.StoredProcedure, timeout);

            int affected = -1;
            IDbCommand cmdAction = null;
            try
            {
                cmdAction = GetParametericCommand(command, paramList, timeout);
                NormalizeCommand(cmdAction);
                affected = ExecuteCommand(cmdAction);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.ExecuteNonQuery", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdAction);
            }

            return affected;
        }

        /// <summary>
        /// Reads information about parameters of an existing stored procedure
        /// from database schema catalogs. Derived classes should implement
        /// this method, since the base class version does nothing.
        /// </summary>
        /// <param name="dbCommand">An instance of provider-specific command object
        /// </param>
        protected virtual void DeriveParameters(IDbCommand dbCommand)
        {
        }

        #region Factory Methods

        /// <summary>
        /// Creates and returns an instance of a connection object that
        /// corresponds to the data provider already set for this object.
        /// This method is for internal use by the base class.
        /// </summary>
        /// <returns>A valid IDbConnection object that corresponds to
        /// the data provider already set for this object. The connection
        /// is not automatically opened.</returns>
        protected abstract IDbConnection CreateConnection();

        /// <summary>
        /// Creates and returns an instance of a command object that
        /// corresponds to the data provider already set for this object.
        /// This method is for internal use by the base class.
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
        protected abstract IDbCommand CreateCommand(
            string command, IDbConnection dbConnection, CommandType type, int timeout);

        /// <summary>
        /// Creates and returns an instance of a data adapter object that
        /// corresponds to the data provider already set for this object.
        /// This method is for internal use by the base class.
        /// </summary>
        /// <returns>A valid IDbDataAdapter object that corresponds to
        /// the data provider already set for this object.</returns>
        protected abstract IDbDataAdapter CreateAdapter();

        #endregion

        #region Helper Methods

        /// <summary>
        /// Creates and returns an instance of provider-specific command object.
        /// The SQL statement for this command should not have any parameters.
        /// This method is for internal use.
        /// </summary>
        /// <param name="command">SQL Command to execute.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>An instance of provider-specific command object that is prepared to execute the given SQL command.
        /// </returns>
        protected IDbCommand GetNonParametericCommand(string command, CommandType type, int timeout)
        {
            CheckState();
            Verify.ArgumentNotNullOrEmptyString(command, "command");
            Verify.EnumValueIsDefined(typeof(CommandType), "type", (int)type);
            Verify.ArgumentNotLessThan(timeout, 0, "timeout");

            IDbConnection cnn = CreateConnection();
            IDbCommand cmd = CreateCommand(command, cnn, type, timeout);
            cnn.Open();
            return cmd;
        }

        /// <summary>
        /// Creates and returns an instance of provider-specific command object.
        /// The SQL statement for this command should have one or more parameters.
        /// This method is for internal use.
        /// </summary>
        /// <param name="command">SQL Command to execute.
        /// This should be the name of an existing stored procedure.
        /// </param>
        /// <param name="paramList">An object array that contains parameters
        /// required by the given stored procedure.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>An instance of provider-specific command object that is prepared to execute the given SQL command.
        /// </returns>
        protected IDbCommand GetParametericCommand(string command, object[] paramList, int timeout)
        {
            CheckState();
            Verify.ArgumentNotNullOrEmptyString(command, "command");
            Verify.ArgumentNotNull(paramList, "paramList");
            Verify.ArgumentNotLessThan(timeout, 0, "timeout");

            IDbConnection cnn = CreateConnection();
            IDbCommand cmd = CreateCommand(command, cnn, CommandType.StoredProcedure, timeout);
            cnn.Open();
            DeriveParameters(cmd);

            // Exclude @RETURN_VALUE
            if (cmd.Parameters.Count - 1 > paramList.Length)
            {
                cnn.Close();
                throw (ExceptionBuilder.NewArgumentException("Insufficient count of arguments.", "paramList"));
            }

            BindParameters(cmd, paramList);
            return cmd;
        }

        /// <summary>
        /// No summary information available.
        /// </summary>
        /// <param name="dbCommand">No documentation.</param>
        protected virtual void NormalizeCommand(IDbCommand dbCommand)
        {
        }

        /// <summary>
        /// Executes the database command already set in the given command
        /// object. If there's a pending transaction, given command will be
        /// executed in the context of pending transaction.
        /// </summary>
        /// <param name="dbCommand">The command object to be executed</param>
        /// <returns>The number of rows affected by executing the query.</returns>
        /// <remarks>The database command already set in the command object
        /// should not return any rows.</remarks>
        protected int ExecuteCommand(IDbCommand dbCommand)
        {
            CheckState();
            Verify.ArgumentNotNull(dbCommand, "dbCommand");

            int affected = dbCommand.ExecuteNonQuery();
            return affected;
        }

        /// <summary>
        /// Executes a database query and returns query result as a
        /// DataTable object. This method is for internal use.
        /// </summary>
        /// <param name="dbCommand">An instance of provider-specific command object
        /// </param>
        /// <returns>A DataTable object containing all rows returned by
        /// database query.</returns>
        protected DataTable GetQueryResult(IDbCommand dbCommand)
        {
            CheckState();
            Verify.ArgumentNotNull(dbCommand, "dbCommand");

            DataTable result = null;
            IDbDataAdapter adapter = CreateAdapter();
            adapter.SelectCommand = dbCommand;
            using (DataSet dset = new DataSet())
            {
                dset.Locale = CultureInfo.InvariantCulture;   // FxCop suggestion
                adapter.Fill(dset);
                result = dset.Tables[0];
            }
            return result;
        }

        /// <summary>
        /// If no active transaction is currently pending, closes the connection
        /// associated with the given command, otherwise leaves the connection open.
        /// </summary>
        /// <param name="dbCommand">An instance of provider-specific command object
        /// </param>
        protected void DisposeCommand(IDbCommand dbCommand)
        {
            if (dbCommand != null && dbCommand.Connection != null)
                dbCommand.Connection.Close();
        }

        /// <summary>
        /// Given a command object and an array of parameter values,
        /// binds parameters to command object's Parameters collection.
        /// </summary>
        /// <param name="dbCommand">An instance of provider-specific command object
        /// </param>
        /// <param name="paramList">An object array that contains values to be
        /// bound to the given command object.</param>
        protected virtual void BindParameters(IDbCommand dbCommand, object[] paramList)
        {
            CheckState();
            Verify.ArgumentNotNull(dbCommand, "dbCommand");

            try
            {
                for (int i = 1; i < dbCommand.Parameters.Count; i++)
                {
                    IDataParameter parameter = dbCommand.Parameters[i] as IDataParameter;
                    parameter.Value = paramList[i - 1];
                }
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.Bindparameters", e.Message);
                throw;
            }
        }

        #endregion

        /// <summary>
        /// Checks internal state of this object. Throws InvalidOperationException
        /// if connection text is null or empty.
        /// </summary>
        protected void CheckState()
        {
            // Object State Check...
            if (String.IsNullOrEmpty(_connection))
                throw ExceptionBuilder.NewInvalidOperationException();
        }

        /// <summary>
        /// Executes a database query and returns all values in a single column
        /// as an object array.
        /// </summary>
        /// <param name="command">A SQL command that returns one or more
        /// columns of data.</param>
        /// <param name="index">Zero-based index of the required output
        /// column in the given SQL command.</param>
        /// <param name="type">A System.Data.CommandType value that
        /// specifies how command parameter should be interpreted.</param>
        /// <param name="timeout">An integer that specifies the time in
        /// seconds that command should wait for completion before terminating.
        /// </param>
        /// <returns>Values of the required column as an object array.</returns>
        public object[] QueryColumn(string command, int index = 0, CommandType type = CommandType.Text,
            int timeout = DefaultTimeout)
        {
            if (type == CommandType.TableDirect)
                throw ExceptionBuilder.NewArgumentException();

            IDbCommand cmdValues = null;
            object[] values = null;
            try
            {
                cmdValues = GetNonParametericCommand(command, type, timeout);
                NormalizeCommand(cmdValues);
                values = GetColumnValues(cmdValues, index);
            }
            catch (Exception e)
            {
                Debugger.Log(1, "DataLayerBase.QueryColumn", e.Message);
                throw;
            }
            finally
            {
                DisposeCommand(cmdValues);
            }

            return values;
        }

        /// <summary>
        /// Executes a database query and returns all values in a single column
        /// as an object array. This method is for internal use.
        /// </summary>
        /// <param name="dbCommand">An instance of provider-specific command object
        /// </param>
        /// <param name="index">Zero-based index of the required output
        /// column in the given SQL command.</param>
        /// <returns>Values of the required column as an object array.</returns>
        private object[] GetColumnValues(IDbCommand dbCommand, int index)
        {
            CheckState();
            Verify.ArgumentNotNull(dbCommand, "dbCommand");
            Verify.ArgumentNotLessThan(index, 0, "index");

            object[] values = null;
            IDataReader rdr = null;
            try
            {
                ArrayList list = new ArrayList();
                rdr = dbCommand.ExecuteReader(CommandBehavior.SingleResult);
                if (index >= rdr.FieldCount)
                {
                    rdr.Close();
                    dbCommand.Connection.Close();
                    throw (ExceptionBuilder.NewArgumentOutOfRangeException("index"));
                }

                while (rdr.Read())
                    list.Add(rdr.GetValue(index));

                values = new object[list.Count];
                list.CopyTo(values);
            }
            finally
            {
                if (rdr != null && !rdr.IsClosed)
                    rdr.Close();
            }

            return values;
        }

        /// <summary>
        /// Default timeout (in seconds) for database operations.
        /// </summary>
        protected const int DefaultTimeout = 30;

        protected readonly string _connection;             // Connection text
    }
}
