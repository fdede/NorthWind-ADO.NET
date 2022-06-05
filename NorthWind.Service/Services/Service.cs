using NorthWind.Core.Services;
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using NorthWind.Repository.Repositories;
using NorthWind.Core.Models;

namespace NorthWind.Service.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly GenericRepository<T> _repository;

        public Service()
        {
            _repository = new GenericRepository<T>();
        }

        private Type GetType
        {
            get { return typeof(T); }
        }

        public async Task<T> AddAsync(T entity)
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = $"prc_{GetType.Name}_Insert"
            };

            foreach (PropertyInfo property in GetType.GetProperties())
            {
                cmd.Parameters.AddWithValue(property.Name, property.GetValue(entity));
            }

            List<T> result = await _repository.ExecuteReaderAsync(cmd);
            return result.FirstOrDefault();
        }

        public Task<bool> DeleteAsync(long ID)
        {
            throw new NotImplementedException();
        }

        public async Task<List<T>> GetAllAsync()
        {
            SqlCommand cmd = new SqlCommand()
            {
                CommandText = $"prc_{GetType.Name}_Select",
                CommandType = CommandType.StoredProcedure
            };

            return await _repository.ExecuteReaderAsync(cmd);
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> WhereAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }
    }
}
