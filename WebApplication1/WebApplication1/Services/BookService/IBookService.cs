using WebApplication1.DTO.Book;
using WebApplication1.Models;

namespace WebApplication1.Services.BookService
{
    public interface IBookService
    {
        Task<Book> GetBook(Guid id);
        Task<List<Book>> GetBooks();
        Task<Book> CreateBook(BookForUpdate bookForUpdate);
        Task UpdateBook(Guid id, BookForUpdate bookForUpdate);
        Task DeleteBook(Guid id);
    }
}
