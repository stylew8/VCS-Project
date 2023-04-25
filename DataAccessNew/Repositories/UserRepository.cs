using DataAccess.Models;
using DataAccessNew.Models;
using DataAccessNew.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(ShopDbContext context) : base(context)
        {
            
        }

        public override Task<List<User>> GetToNameAsync(string name, params Expression<Func<User, object>>[] properties)
        {
            throw new NotImplementedException();
        }
        public override async Task<List<User>> GetByCategoryAsync(string text, params Expression<Func<User, object>>[] properties)
        {
            throw new NotImplementedException();
        }
        public override async Task<User?> GetByUsernameAsync(string username)
        {
            return await Entities
            .FirstOrDefaultAsync(c => c.Username == username);
        }

        public override async Task<List<OrderLine>> GetByUserIdAsync(int userId, params Expression<Func<OrderLine, object>>[] properties)
        {
            throw new NotImplementedException();
        }
    }
}