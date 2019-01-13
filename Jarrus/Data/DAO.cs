using GeneralHux.CRUD;
using GeneralHux.ErrorHandling;
using GeneralHux.Extensions;
using GeneralHux.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Jarrus.Data
{
    public class DAO
    {
        private string _originalSql;
        private SqlConnection _connection;
        private SqlCommand _command;
        private SqlDataReader _reader;
        private SqlTransaction _transaction;

        private List<string> _transactionSqlStatements = new List<string>();
        private Server SqlServer = Server.JARRUS;

        public void OpenConnection(Server server, string storedProcedureOrSql)
        {
            SqlServer = server;
            _connection = new SqlConnection(SqlServer.ConnectionString);
            _originalSql = storedProcedureOrSql;

            OpenConnection();
            SetupSqlCommand();
        }

        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
            else if (_connection.State == ConnectionState.Open)
            {
            }
            else
            {
                throw new Exception("_connection state is not ready to be opened. Current state : " + _connection.State);
            }
        }

        public void OpenTransaction(Server server)
        {
            _connection = new SqlConnection(SqlServer.ConnectionString);
            OpenConnection();

            _transactionSqlStatements = new List<string>();
            _transaction = _connection.BeginTransaction();

            _command = new SqlCommand("", _connection, _transaction);
            _command.CommandType = CommandType.Text;
        }

        public void AddSqlStatementToTransaction(string sql)
        {
            _transactionSqlStatements.Add(sql);
        }

        public void SimpleExecute(Server server, string sql)
        {
            try
            {
                OpenConnection(server, sql);
                Execute();
            } catch(Exception ex)
            {
                throw ex;
            } finally
            {
                CloseConnection();
            }
        }

        public void ExecuteTransaction()
        {
            try
            {
                foreach(var sqlStatement in _transactionSqlStatements)
                {
                    _command.CommandText = sqlStatement;
                    int rowsAffected = _command.ExecuteNonQuery();
                }
                
                _transaction.Commit();
            }
            catch (Exception)
            {
                try
                {
                    _transaction.Rollback();
                }
                catch (Exception ex2)
                {
                    throw ex2;
                }
            }
        }

        public void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
            else if (_connection.State == ConnectionState.Closed)
            {
            }
            else
            {
                throw new Exception("_connection state is not ready to be closed. Current state : " + _connection.State);
            }
        }

        public void AddParameter(string parameter, object value)
        {
            if (parameter == null)
            {
                throw new Exception("A null parameter is not allowed.");
            }

            parameter = parameter.Replace("@", "");
            parameter = "@" + parameter;

            if (value == null)
            {
                _command.Parameters.AddWithValue(parameter, DBNull.Value);
            }
            else if (value.GetType().Name.Equals("DateTime"))
            {
                var dateValue = (DateTime)value;
                _command.Parameters.AddWithValue(parameter, dateValue.IsNull() ? DBNull.Value : value);
            }
            else
            {
                _command.Parameters.AddWithValue(parameter, value);
            }
        }

        private void SetupSqlCommand()
        {
            var sql = _originalSql;

            if (_originalSql.Length < 50)
            {
                sql = FileHelper.GetSqlFile(_originalSql);
            }

            _command = new SqlCommand(sql, _connection);
            _command.CommandType = CommandType.Text;
        }

        public void Execute()
        {
            _command.Parameters.Add("@NewId", SqlDbType.Int).Direction = ParameterDirection.Output;
            _reader = _command.ExecuteReader();
        }

        public int GetNewId()
        {
            try
            {
                return Convert.ToInt32(_command.Parameters["@NewId"].Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public DataTable GetSchemaTable()
        {
            _reader.Read();
            return _reader.GetSchemaTable();
        }

        public bool HasNextRow() { return _reader.Read(); }
        public string GetNameOfColumn(int columnNumber) { return _reader.GetName(columnNumber); }
        public ConnectionState GetConnectionStatus() { return _connection.State; }
        public bool GetBoolean(string columnName, bool defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetBoolean(_reader.GetOrdinal(columnName)) : defaultValue; }
        public object GetObject(string columnName, object defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetOrdinal(columnName) : defaultValue; }
        public byte GetByte(string columnName, byte defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetByte(_reader.GetOrdinal(columnName)) : defaultValue; }
        public decimal GetDecimal(string columnName, decimal defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetDecimal(_reader.GetOrdinal(columnName)) : defaultValue; }
        public double GetDouble(string columnName, double defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetDouble(_reader.GetOrdinal(columnName)) : defaultValue; }
        public float GetFloat(string columnName, float defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetFloat(_reader.GetOrdinal(columnName)) : defaultValue; }
        public short GetShort(string columnName, short defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetInt16(_reader.GetOrdinal(columnName)) : defaultValue; }
        public int GetInt(string columnName, int defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetInt32(_reader.GetOrdinal(columnName)) : defaultValue; }
        public long GetLong(string columnName, long defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetInt64(_reader.GetOrdinal(columnName)) : defaultValue; }
        public DateTime GetDateTime(string columnName, DateTime defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetDateTime(_reader.GetOrdinal(columnName)) : defaultValue; }
        public string GetString(string columnName, string defaultValue) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetString(_reader.GetOrdinal(columnName)) : defaultValue; }
        public string GetSqlGuid(string columnName, string defaultValue = null) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetSqlGuid(_reader.GetOrdinal(columnName)).ToString() : defaultValue; }
        public string GetGuid(string columnName, string defaultValue = null) { return !_reader.IsDBNull(_reader.GetOrdinal(columnName)) ? _reader.GetGuid(_reader.GetOrdinal(columnName)).ToString() : defaultValue; }

        public string GetString(string columnName)
        {
            var columnNumber = _reader.GetOrdinal(columnName);
            return _reader.IsDBNull(columnNumber) ? null : _reader.GetString(columnNumber);
        }

        public DateTime GetDateTime(string columnName)
        {
            var columnNumber = _reader.GetOrdinal(columnName);
            return _reader.IsDBNull(columnNumber) ? new DateTime() : _reader.GetDateTime(columnNumber);
        }

        public bool? GetNullableBoolean(string columnName)
        {
            var columnNumber = _reader.GetOrdinal(columnName);

            if (_reader.IsDBNull(columnNumber))
            {
                return null;
            }

            return _reader.GetBoolean(_reader.GetOrdinal(columnName));
        }

        public bool GetBoolean(string columnName) { return _reader.GetBoolean(_reader.GetOrdinal(columnName)); }
        public Object GetObject(String columnName) { return _reader.GetValue(_reader.GetOrdinal(columnName)); }
        public Byte GetByte(String columnName) { return _reader.GetByte(_reader.GetOrdinal(columnName)); }
        public Decimal GetDecimal(String columnName) { return _reader.GetDecimal(_reader.GetOrdinal(columnName)); }
        public Double GetDouble(String columnName) { return _reader.GetDouble(_reader.GetOrdinal(columnName)); }
        public float GetFloat(String columnName) { return _reader.GetFloat(_reader.GetOrdinal(columnName)); }
        public short GetShort(String columnName) { return _reader.GetInt16(_reader.GetOrdinal(columnName)); }
        public int GetInt(String columnName) { return _reader.GetInt32(_reader.GetOrdinal(columnName)); }
        public long GetLong(String columnName) { return _reader.GetInt64(_reader.GetOrdinal(columnName)); }
    }
}