using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces.Repositories;

namespace BookStore.DAL.Repositories
{
    public class BookSagesRepository : Repository<BookSage>, IBookSagesRepository
    {
        public BookSagesRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
