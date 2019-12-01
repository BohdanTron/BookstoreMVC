using System.Threading.Tasks;
using BookStore.DAL.Interfaces;
using BookStore.DAL.Interfaces.Repositories;
using BookStore.DAL.Repositories;

namespace BookStore.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private IBooksRepository _booksRepository;
        private ISagesRepository _sagesRepository;
        private IBookSagesRepository _bookSagesRepository;
        private IUsersRepository _usersRepository;
        private IOrdersRepository _ordersRepository;

        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IBooksRepository BooksRepository => _booksRepository ?? (_booksRepository = new BooksRepository(_dbContext));
        public ISagesRepository SagesRepository => _sagesRepository ?? (_sagesRepository = new SagesRepository(_dbContext));
        public IBookSagesRepository BookSagesRepository => 
            _bookSagesRepository ?? (_bookSagesRepository = new BookSagesRepository(_dbContext));

        public IUsersRepository UsersRepository => _usersRepository ?? (_usersRepository = new UsersRepository(_dbContext));
        public IOrdersRepository OrdersRepository => _ordersRepository ?? (_ordersRepository = new OrdersRepository(_dbContext));

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
