using Microsoft.AspNetCore.Mvc;
using WebApplication1.DTO.Book;
using WebApplication1.Models;
using WebApplication1.Services.BookService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await _bookService.GetBooks();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBookById(Guid id)
        {
            return await _bookService.GetBook(id);
        }

        [HttpPost]
        public async Task<Book> CreateBook([FromBody] BookForUpdate bookForUpdate)
        {
            return await _bookService.CreateBook(bookForUpdate);
        }

        [HttpPut("{id}")]
        public async Task UpdateBook(Guid id, [FromBody] BookForUpdate bookForUpdate)
        {
            await _bookService.UpdateBook(id, bookForUpdate);
        }

        [HttpDelete("{id}")]
        public async Task DeleteBook(Guid id)
        {
            await _bookService.DeleteBook(id);
        }
    }
}
