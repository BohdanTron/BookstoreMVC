using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DAL.Repositories
{
    public class BooksRepository : Repository<Book>, IBooksRepository
    {
        public BooksRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Book> GetByIdAsync(int id)
        {
            return await _dbContext.Books
                .Include(x => x.BookSages).ThenInclude(x => x.Sage)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
