using DataAccessNew.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessNew.Repositories
{
    public interface IRepository<TEntity>
            where TEntity : Entity
    {
        public Task CreateAsync(TEntity entity);

        public Task<List<TEntity>> ListAsync(params Expression<Func<TEntity, object>>[] properties);

        public Task<TEntity?> GetAsync(int id);

        public Task UpdateAsync(TEntity entity);

        public Task DeleteAsync(int id);
        public Task<List<TEntity>> GetToNameAsync(string name, params Expression<Func<TEntity, object>>[] properties);
        public Task<List<TEntity>> GetByCategoryAsync(string category, params Expression<Func<TEntity, object>>[] properties);
        public Task<TEntity?> GetByUsernameAsync(string category);
        public Task<List<OrderLine>> GetByUserIdAsync(int userId, params Expression<Func<OrderLine, object>>[] properties);
    }
}
