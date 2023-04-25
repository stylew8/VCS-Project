using DataAccess.Models;
using DataAccessNew.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessNew.Repositories
{
    public class OrderLineRepository : BaseRepository<OrderLine>
    {
        public OrderLineRepository(ShopDbContext context) : base(context)
        {
        }
        public override Task<List<OrderLine>> GetToNameAsync(string name, params Expression<Func<OrderLine, object>>[] properties)
        {
            throw new NotImplementedException();
        }
        public override async Task<List<OrderLine>> GetByCategoryAsync(string category, params Expression<Func<OrderLine, object>>[] properties)
        {
            throw new NotImplementedException();
        } 
        public override Task<OrderLine?> GetByUsernameAsync(string category)
        {
            throw new NotImplementedException();
        }

        public override async Task<List<OrderLine>> GetByUserIdAsync(int userId, params Expression<Func<OrderLine, object>>[] properties)
        {
            IQueryable<OrderLine> query = Entities
                .Where(x => x.OrderId == userId);

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
