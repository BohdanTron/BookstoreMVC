using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BookStore.DAL.Entities;
using BookStore.DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Sages

        public async Task<IActionResult> GetSages()
        {
            var sages = await _unitOfWork.SagesRepository.GetAllAsync();
            if (sages == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Sages/Get.cshtml", sages);
        }

        public async Task<IActionResult> CreateSage()
        {
            var books = await _unitOfWork.BooksRepository.GetAllAsync();
            return View("~/Views/Admin/Sages/Create.cshtml", books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSage(Sage sage, IEnumerable<int> selectedBookIds, IFormFile photo)
        {
            if (photo != null && photo.Length > 0)
            {
                sage.Photo = GetPhotoData(photo);
            }

            var newSage = _unitOfWork.SagesRepository.Add(sage);
            if (selectedBookIds.Any())
            {
                await AddBooksToSage(newSage, selectedBookIds);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("GetSages");
        }

        public async Task<IActionResult> EditSage(int id)
        {
            var sage = await _unitOfWork.SagesRepository.GetByIdAsync(id);
            if (sage == null)
            {
                return NotFound();
            }

            ViewBag.AllBooks = await _unitOfWork.BooksRepository.GetAllAsync();

            return View("~/Views/Admin/Sages/Edit.cshtml", sage);
        }

        [HttpPost]
        public async Task<IActionResult> EditSage(Sage sage, IEnumerable<int> selectedBookIds, IFormFile photo)
        {
            var existingSage = await _unitOfWork.SagesRepository.GetByIdAsync(sage.Id);
            if (existingSage == null)
            {
                return NotFound();
            }

            existingSage.Name = sage.Name;
            existingSage.Age = sage.Age;
            existingSage.City = sage.City;
            existingSage.Photo = photo != null && photo.Length > 0 ? GetPhotoData(photo) : null;

            existingSage.BookSages.Clear();
            if (selectedBookIds.Any())
            {
                await AddBooksToSage(existingSage, selectedBookIds);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("GetSages");
        }

        public async Task<IActionResult> DeleteSage(int id)
        {
            var sage = await _unitOfWork.SagesRepository.GetByIdAsync(id);
            if (sage == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Sages/Delete.cshtml", sage);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSage(Sage sage)
        {
            var existingSage = await _unitOfWork.SagesRepository.GetByIdAsync(sage.Id);
            if (existingSage == null)
            {
                return NotFound();
            }

            _unitOfWork.SagesRepository.Remove(existingSage);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToAction("GetSages");
        }

        private async Task AddBooksToSage(Sage sage, IEnumerable<int> bookIds)
        {
            foreach (var bookId in bookIds)
            {
                sage.BookSages.Add(new BookSage
                {
                    SageId = sage.Id,
                    Sage = sage,
                    BookId = bookId,
                    Book = await _unitOfWork.BooksRepository.GetByIdAsync(bookId)
                });
            }
        }

        private byte[] GetPhotoData(IFormFile image)
        {
            byte[] photoData = null;
            using (var binaryReader = new BinaryReader(image.OpenReadStream()))
            {
                photoData = binaryReader.ReadBytes((int)image.Length);
            }

            return photoData;
        }

        #endregion

        #region Books

        public async Task<IActionResult> Index()
        {
            var books = await _unitOfWork.BooksRepository.GetAllAsync();
            if(books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        public async Task<IActionResult> CreateBook()
        {
            var sages = await _unitOfWork.SagesRepository.GetAllAsync();
            return View("~/Views/Admin/Books/Create.cshtml", sages);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(Book book, IEnumerable<int> selectedSageIds)
        {
            var newBook = _unitOfWork.BooksRepository.Add(book);

            if (selectedSageIds.Any())
            {
                await AddSagesToBook(newBook, selectedSageIds);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            ViewBag.AllSages = await _unitOfWork.SagesRepository.GetAllAsync();

            return View("~/Views/Admin/Books/Edit.cshtml", book);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(Book book, IEnumerable<int> selectedSageIds)
        {
            var existingBook = await _unitOfWork.BooksRepository.GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                return NotFound();
            }
            
            existingBook.Name = book.Name;
            existingBook.Description = book.Description;

            existingBook.BookSages.Clear();
            if(selectedSageIds.Any())
            {
                await AddSagesToBook(existingBook, selectedSageIds);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _unitOfWork.BooksRepository.GetByIdAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            return View("~/Views/Admin/Books/Delete.cshtml", book);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(Book book)
        {
            var existingBook = await _unitOfWork.BooksRepository.GetByIdAsync(book.Id);
            if (existingBook == null)
            {
                return NotFound();
            }

            _unitOfWork.BooksRepository.Remove(existingBook);
            await _unitOfWork.SaveChangesAsync();
            
            return RedirectToAction("Index");
        }

        private async Task AddSagesToBook(Book book, IEnumerable<int> sageIds)
        {
            foreach (var sageId in sageIds)
            {
                book.BookSages.Add(new BookSage
                {
                    BookId = book.Id,
                    Book = book,
                    SageId = sageId,
                    Sage = await _unitOfWork.SagesRepository.GetByIdAsync(sageId)
                });
            }
        }

        #endregion
    }
}