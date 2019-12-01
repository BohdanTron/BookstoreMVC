using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories
{
    public class SagesRepository : Repository<Sage>, ISagesRepository
    {
        public SagesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Sage> GetByIdAsync(int id)
        {
            return await _dbContext.Sages
                .Include(x => x.BookSages).ThenInclude(x => x.Book)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
