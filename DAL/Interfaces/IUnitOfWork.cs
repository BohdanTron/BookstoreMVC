using BookStore.DAL.Interfaces.Repositories;
using System;
using System.Threading.Tasks;

namespace BookStore.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IBooksRepository BooksRepository { get; }
        ISagesRepository SagesRepository { get; }
        IBookSagesRepository BookSagesRepository { get; }
        IUsersRepository UsersRepository { get; }
        IOrdersRepository OrdersRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
