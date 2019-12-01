using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces.Repositories;

namespace BookStore.DAL.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
