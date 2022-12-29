using Bok.Data;
using Bok.Data.Services;
using Bok.Data.Static;
using Bok.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace Bok.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _service;

        public BooksController(IBooksService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allBooks = await _service.GetAllAsync(n => n.Publishers);
            return View(allBooks);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allBooks = await _service.GetAllAsync(n => n.Publishers);

            if (!string.IsNullOrEmpty(searchString))
            {
                

                var filteredResultNew = allBooks.Where(n => string.Equals(n.Name, searchString, StringComparison.CurrentCultureIgnoreCase) || string.Equals(n.Description, searchString, StringComparison.CurrentCultureIgnoreCase)).ToList();

                return View("Index", filteredResultNew);
            }

            return View("Index", allBooks);
        }

        //GET: Books/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var bookDetail = await _service.GetBookByIdAsync(id);
            return View(bookDetail);
        }

        //GET: Books/Create
        public async Task<IActionResult> Create()
        {
            var bookDropdownsData = await _service.GetNewBookDropdownsValues();

            ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "Name");
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewBookVM book)
        {
            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Cinemas = new SelectList(bookDropdownsData.Publishers, "Id", "Name");
               
                ViewBag.Actors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

                return View(book);
            }

            await _service.AddNewBookAsync(book);
            return RedirectToAction(nameof(Index));
        }


        //GET: Books/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var bookDetails = await _service.GetBookByIdAsync(id);
            if (bookDetails == null) return View("NotFound");

            var response = new NewBookVM()
            {
                Id = bookDetails.Id,
                Name = bookDetails.Name,
                Description = bookDetails.Description,
                Price = bookDetails.Price,
              
                ImageURL = bookDetails.ImageURL,
                BookCategory = bookDetails.BookCategory,
                PublisherId = bookDetails.PublisherId,
                
                AuthorIds = bookDetails.Authors_Books.Select(n => n.AuthorId).ToList(),
            };

            var bookDropdownsData = await _service.GetNewBookDropdownsValues();
            ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "Name");
            ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewBookVM book)
        {
            if (id != book.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var bookDropdownsData = await _service.GetNewBookDropdownsValues();

                ViewBag.Publishers = new SelectList(bookDropdownsData.Publishers, "Id", "Name");
                ViewBag.Authors = new SelectList(bookDropdownsData.Authors, "Id", "FullName");

                return View(book);
            }

            await _service.UpdateBookAsync(book);
            return RedirectToAction(nameof(Index));
        }
    }
}