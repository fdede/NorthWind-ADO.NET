using NorthWind.Core.Models;
using NorthWind.Core.Repositories;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace NorthWind.Repository.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private Type GetType
        {
            get { return typeof(T); }
        }

        public async Task<bool> ExecuteNonQueryAsync(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = AppDbContext.Connection;

                if (AppDbContext.Connection.State == ConnectionState.Closed)
                    await AppDbContext.Connection.OpenAsync();

                var resultObject = await cmd.ExecuteNonQueryAsync();
                return resultObject > 0;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (AppDbContext.Connection.State == ConnectionState.Open)
                    AppDbContext.Connection.Close();
            }
        }

        public async Task<List<T>> ExecuteReaderAsync(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = AppDbContext.Connection;

                if (AppDbContext.Connection.State == ConnectionState.Closed)
                    await AppDbContext.Connection.OpenAsync();

                SqlDataReader dataReader = await cmd.ExecuteReaderAsync();
                List<T> result = new List<T>();

                if (dataReader != null)
                {
                    while (dataReader.Read())
                    {
                        T entity = Activator.CreateInstance<T>();
                        foreach (PropertyInfo property in GetType.GetProperties())
                        {
                            property.SetValue(entity, dataReader[property.Name], null);
                        }
                        result.Add(entity);
                    }
                }

                return result;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (AppDbContext.Connection.State == ConnectionState.Open)
                    AppDbContext.Connection.Close();
            }
        }
    }
}
