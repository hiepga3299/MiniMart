using Dapper;
using Microsoft.Data.SqlClient;
using MiniMart.Infatructure.Abstract;
using System.Data;

namespace MiniMart.Infatructure.Dapper
{
    public class SQLQueryHandler : ISQLQueryHandler
    {
        private readonly string _connectionString = string.Empty;

        public SQLQueryHandler(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        }

        public async Task ExecuteNoReturnAsync(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(query, param: parameters, dbTransaction);
            }
        }

        public async Task<T> ExecuteReturnSingleValueScalarAasync<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.ExecuteScalarAsync<T>(query, param: parameters, dbTransaction);
            }
        }

        public async Task<T> ExecuteReturnSingleRowAasync<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<T>(query, param: parameters, dbTransaction);
            }
        }

        public async Task<IEnumerable<T>> ExecuteStoreProdecureReturnList<T>(string nameStore, DynamicParameters parameters, IDbTransaction dbTransaction = null)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QueryAsync<T>(nameStore, param: parameters, dbTransaction, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
