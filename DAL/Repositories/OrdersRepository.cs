using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories
{
    public class OrdersRepository : Repository<Order>, IOrdersRepository
    {
        public OrdersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Set<Order>()
                .Include(x => x.User)
                .Include(x => x.Book)
                .ToListAsync();
        }
    }
}
