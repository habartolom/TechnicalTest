using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnicalTestFE.Models;

namespace TechnicalTestFE.Services
{
    public interface IService
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<IEnumerable<Book>> GetBooksByAuthor(string name);
        Task<Book> GetBookById(int id);
        Task<Book> GetBookByTitle(string title);
        Task<Response<Book>> RegisterBook(Book book);
        Task<Response<bool>> UpdateBook(Book book);
        Task<bool> DeleteBook(int id);
        Task<int> GetRegistrationLimit();
        Task<Response<Constant>> SetRegistrationLimit(int limit);
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(int id);
        Task<Author> GetAuthorByName(string name);
        Task<Response<Author>> RegisterAuthor(Author author); 
        Task<Response<bool>> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(int id);
    }
}
