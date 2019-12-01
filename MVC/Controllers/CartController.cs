using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.DAL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVC.Extensions;
using MVC.Models;

namespace MVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.Get<Cart>("Cart");
            return View(cart?.Items ?? new List<CartItem>());
        }

        [HttpPost]
        public async Task<RedirectToActionResult> AddToCart(int bookId)
        {
            var book = await _unitOfWork.BooksRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart");
                if (cart == null)
                {
                    cart = new Cart();
                }
                
                cart.Add(book);
                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }

        public async Task<RedirectToActionResult> RemoveFromCart(int bookId)
        {
            var book = await _unitOfWork.BooksRepository.GetByIdAsync(bookId);
            if (book != null)
            {
                var cart = HttpContext.Session.Get<Cart>("Cart");
                cart.Remove(book);

                HttpContext.Session.Set("Cart", cart);
            }

            return RedirectToAction("Index");
        }
    }
}