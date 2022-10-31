using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AjmeeraAssesmentAPI.Models;

namespace AjmeeraAssesmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly AjmeeraInfotechTestDbContext _context;

        public BooksController(AjmeeraInfotechTestDbContext context)
        {
            _context = context;
        }

        // GET the details of all books 
        [HttpGet]
        [Route("GetDetailsOfAllBooks")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            return await _context.Books.ToListAsync();
        }

        // GET the book with the specific ID entered by user
        //Id is a mandatory parameter here 
        [HttpGet]
        [Route("GetTheSpecificBookDetails")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }        

        // POST: Save new book details
        // User enters only the Book Name and Author Name, Id will be generated automatically. 
        [HttpPost]
        [Route("SaveData")]
        public async Task<ActionResult<Book>> PostBook(string bookName, string authorName)
        {
            if (string.IsNullOrWhiteSpace(bookName) || string.IsNullOrWhiteSpace(authorName))
            {
                return NotFound();
            }
            else
            {
                var book = new Book() { 
                    Name = bookName, 
                    AuthorName = authorName 
                };
                _context.Books.Add(book);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBook", new { id = book.Id }, book);
            }
        }      

        
    }
}
