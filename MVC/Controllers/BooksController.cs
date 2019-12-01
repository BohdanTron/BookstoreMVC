using System.Threading.Tasks;
using BookStore.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class BooksController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BooksController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _unitOfWork.BooksRepository.GetAllAsync();            
            return View(books);
        }
    }
}
