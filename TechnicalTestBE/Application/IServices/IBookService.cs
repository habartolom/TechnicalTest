using Domain.Dtos;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IBookService
    {
        ResponseDto<IEnumerable<BookDto>> GetBooks();
        ResponseDto<IEnumerable<BookDto>> GetBooksByAuthor(string name);
        ResponseDto<BookDto> GetBookById(int id);
        ResponseDto<BookDto> GetBookByTitle(string title);
        ResponseDto<BookDto> CreateBook(BookDto book);
        ResponseDto<bool> UpdateBook(BookDto book);
        ResponseDto<bool> DeleteBookById(int id);
        ResponseDto<int> GetConstant(string constant);
        ResponseDto<ConstantDto> SetConstant(ConstantDto constant);

    }
}
