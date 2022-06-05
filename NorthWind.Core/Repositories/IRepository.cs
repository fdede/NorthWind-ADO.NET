using NorthWind.Core.Models;
using System.Data.SqlClient;

namespace NorthWind.Core.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> ExecuteReaderAsync(SqlCommand cmd);
        Task<bool> ExecuteNonQueryAsync(SqlCommand cmd);
    }
}
