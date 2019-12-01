using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _unitOfWork.UsersRepository
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (user == null) 
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            await Authenticate(model.Email);
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _unitOfWork.UsersRepository
                .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

            if (user != null)
            {
                ModelState.AddModelError("", "Invalid username or password");
                return View(model);
            }

            _unitOfWork.UsersRepository.Add(new User { Email = model.Email, Password = model.Password });
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("Login");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, username)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}