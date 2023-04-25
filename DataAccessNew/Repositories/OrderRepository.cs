using DataAccess;
using DataAccess.Models;
using DataAccessNew.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessNew.Repositories
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(ShopDbContext context) : base(context)
        {
        }
        public override Task<List<Order>> GetToNameAsync(string name, params Expression<Func<Order, object>>[] properties)
        {
            throw new NotImplementedException();
        }
        public override async Task<List<Order>> GetByCategoryAsync(string text, params Expression<Func<Order, object>>[] properties)
        {
            throw new NotImplementedException();
        }
        public override Task<Order?> GetByUsernameAsync(string text)
        {
            throw new NotImplementedException();
        }
        public override async Task<List<OrderLine>> GetByUserIdAsync(int userId, params Expression<Func<OrderLine, object>>[] properties)
        {
            throw new NotImplementedException();
        }
    }
}
