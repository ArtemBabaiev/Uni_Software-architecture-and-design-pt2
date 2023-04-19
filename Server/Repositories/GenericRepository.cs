﻿using Dapper;
using Server.Repositories.Interfaces;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Text;

namespace Server.Repositories
{
    internal abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected SqlConnection _sqlConnection;
        protected IDbTransaction _dbTransaction;
        private readonly string _tableName;

        public GenericRepository(SqlConnection sqlConnection, IDbTransaction dbTransaction, string tableName)
        {
            _sqlConnection = sqlConnection;
            _dbTransaction = dbTransaction;
            _tableName = tableName;
        }

        public async Task<long> AddAsync(T t)
        {
            var insertQuery = GenerateInsertQuery();
            var newId = await _sqlConnection.ExecuteScalarAsync<long>(
                insertQuery,
                param: t,
                transaction: _dbTransaction
                );
            return newId;
        }

        public async Task<int> AddRangeAsync(IEnumerable<T> list)
        {
            var inserted = 0;
            var query = GenerateInsertQuery();
            inserted += await _sqlConnection.ExecuteAsync(query,
                param: list);
            return inserted;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var qResult = await _sqlConnection.ExecuteAsync(
                $"DELETE FROM {_tableName} WHERE Id=@Id",
                param: new { Id = id },
                transaction: _dbTransaction
                );
            return Convert.ToBoolean(qResult);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _sqlConnection.QueryAsync<T>(
                $"SELECT * FROM {_tableName}",
                transaction: _dbTransaction
                );
        }

        public async Task<T> GetAsync(long id)
        {
            var result = await _sqlConnection.QuerySingleOrDefaultAsync<T>(
                $"SELECT * FROM {_tableName} WHERE Id=@Id",
                param: new { Id = id },
                transaction: _dbTransaction
                );
            return result;
        }

        public async Task ReplaceAsync(T t)
        {
            var updateQuery = GenerateUpdateQuery();
            await _sqlConnection.ExecuteAsync(
                updateQuery,
                param: t,
                transaction: _dbTransaction
                );
        }


        #region work with properties
        private IEnumerable<PropertyInfo> GetProperties => typeof(T).GetProperties();

        private static List<string> GenerateListOfProperties(IEnumerable<PropertyInfo> listOfProperties)
        {
            return (from prop in listOfProperties
                    let attributes = prop.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    where attributes.Length <= 0 || (attributes[0] as DescriptionAttribute)?.Description != "ignore"
                    select prop.Name).ToList();
        }

        private string GenerateUpdateQuery()
        {
            var updateQuery = new StringBuilder($"UPDATE {_tableName} SET ");
            var properties = GenerateListOfProperties(GetProperties);
            properties.ForEach(property =>
            {
                if (!property.Equals("Id"))
                {
                    updateQuery.Append($"{property}=@{property},");
                }
            });
            updateQuery.Remove(updateQuery.Length - 1, 1);
            updateQuery.Append(" WHERE Id=@Id");
            return updateQuery.ToString();
        }

        private string GenerateInsertQuery()
        {
            var insertQuery = new StringBuilder($"INSERT INTO {_tableName} ");
            insertQuery.Append("(");
            var properties = GenerateListOfProperties(GetProperties);
            properties.Remove("Id");
            properties.ForEach(prop => { insertQuery.Append($"[{prop}],"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(") VALUES (");

            properties.ForEach(prop => { insertQuery.Append($"@{prop},"); });
            insertQuery
                .Remove(insertQuery.Length - 1, 1)
                .Append(")");
            insertQuery.Append("; SELECT SCOPE_IDENTITY()");
            return insertQuery.ToString();
        }
        #endregion
    }
}
