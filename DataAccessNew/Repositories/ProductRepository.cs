using DataAccess.Models;
using DataAccessNew.Models;
using DataAccessNew.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class ProductRepository : BaseRepository<Product>
    {

        public ProductRepository(ShopDbContext context) : base(context)
        {
        }

        public override async Task<List<Product>> GetToNameAsync(string name, params Expression<Func<Product, object>>[] properties)
        {
            IQueryable<Product> query = Entities
                .Where(x => x.Name == name);

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
        public override async Task<Product?> GetByUsernameAsync(string name)
        {
            return await Entities
                .FirstOrDefaultAsync(c => c.Name == name);
        }

        public override async Task<List<Product>> GetByCategoryAsync(string category, params Expression<Func<Product, object>>[] properties)
        {
            IQueryable<Product> query = Entities
                .Where(x => x.Category == category);

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }

        public override async Task<List<OrderLine>> GetByUserIdAsync(int userId, params Expression<Func<OrderLine, object>>[] properties)
        {
            throw new NotImplementedException();
        }
    }
}