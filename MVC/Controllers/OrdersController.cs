using System;
using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Extensions;
using MVC.Models;

namespace MVC.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.OrdersRepository.GetAllAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                ModelState.AddModelError("", "Email is required");
                return BadRequest(ModelState);
            }

            var user = _unitOfWork.UsersRepository.Add(new User { Email = email });
            
            var cart = HttpContext.Session.Get<Cart>("Cart");
            foreach (var item in cart.Items)
            {
                await CreateOrderFromCartItem(item, user);
            }

            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Books");
        }

        private async Task CreateOrderFromCartItem(CartItem cartItem, User user)
        {
            var order = _unitOfWork.OrdersRepository.Add(new Order
            {
                Date = DateTime.Now,
                Quantity = cartItem.Count,
                UserId = user.Id,
                User = user,
                BookId = cartItem.Book.Id,
                Book = await _unitOfWork.BooksRepository.GetByIdAsync(cartItem.Book.Id)
            });

            user.Orders.Add(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}