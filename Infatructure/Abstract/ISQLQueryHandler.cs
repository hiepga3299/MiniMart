using Dapper;
using System.Data;

namespace MiniMart.Infatructure.Abstract
{
    public interface ISQLQueryHandler
    {
        Task ExecuteNoReturnAsync(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null);
        Task<T> ExecuteReturnSingleRowAasync<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null);
        Task<T> ExecuteReturnSingleValueScalarAasync<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null);
        Task<IEnumerable<T>> ExecuteStoreProdecureReturnList<T>(string query, DynamicParameters parameters, IDbTransaction dbTransaction = null);
    }
}