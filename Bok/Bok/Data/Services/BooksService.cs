using Bok.Data.Base;
using Bok.Data.ViewModels;
using Bok.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bok.Data.Services
{
    public class BooksService : EntityBaseRepository<Book>, IBooksService
    {
        private readonly AppDbContext _context;
        public BooksService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewBookAsync(NewBookVM data)
        {
            var newBook = new Book()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                PublisherId = data.PublisherId,

                BookCategory = data.BookCategory,
            };
            await _context.Books.AddAsync(newBook);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var authorId in data.AuthorIds)
            {
                var newAuthorBook = new Author_Book()
                {
                    BookId = newBook.Id,
                    AuthorId = authorId
                };
                await _context.Authors_Books.AddAsync(newAuthorBook);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Book> GetBookByIdAsync(int id)
        {
            var bookDetails = await _context.Books
                .Include(c => c.Publishers)
                .Include(am => am.Authors_Books).ThenInclude(a => a.Author)
                .FirstOrDefaultAsync(n => n.Id == id);

            return bookDetails;
        }

        public async Task<NewBookDropdownsVM> GetNewBookDropdownsValues()
        {
            var response = new NewBookDropdownsVM()
            {
                Authors = await _context.Authors.OrderBy(n => n.FullName).ToListAsync(),
                Publishers = await _context.Publishers.OrderBy(n => n.Name).ToListAsync(),
            };

            return response;
        }

        public async Task UpdateBookAsync(NewBookVM data)
        {
            var dbBook = await _context.Books.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbBook != null)
            {
                dbBook.Name = data.Name;
                dbBook.Description = data.Description;
                dbBook.Price = data.Price;
                dbBook.ImageURL = "";
                dbBook.PublisherId = data.PublisherId;

                dbBook.BookCategory = data.BookCategory;
                await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingAuthorsDb = _context.Authors_Books.Where(n => n.BookId == data.Id).ToList();
            _context.Authors_Books.RemoveRange(existingAuthorsDb);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach (var authorId in data.AuthorIds)
            {
                var newAuthorBook = new Author_Book()
                {
                    BookId = data.Id,
                    AuthorId = authorId
                };
                await _context.Authors_Books.AddAsync(newAuthorBook);
            }
            await _context.SaveChangesAsync();
        }

       
    }
}

