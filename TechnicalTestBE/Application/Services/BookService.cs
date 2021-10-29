using Application.IServices;
using Application.Mappers;
using Application.Responses;

using Domain.Dtos;

using Persistence.Entities;
using Persistence.Factory;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryFactory _repository;

        public BookService(IRepositoryFactory repository)
        {
            _repository = repository;
        }

        public ResponseDto<BookDto> CreateBook(BookDto bookDto)
        {
            try
            {
                var numberOfBooksAllowed = _repository.Constants.GetFirstOrDefault(x => x.Key == "RegistrationLimit");
                if(numberOfBooksAllowed != null)
                {
                    var totalBooks = _repository.Books.GetAll().Count();

                    if (totalBooks + 1 > numberOfBooksAllowed.Value)
                        throw new Exception("No es posible registrar el libro, se alcanzó el máximo permitido");

                }

                var author = _repository.Authors.GetFirstOrDefault(x => x.Name == bookDto.Autor);
                if (author is null)
                    throw new Exception("El autor no está registrado");

                var exists = _repository.Books.GetFirstOrDefault(x => x.Title == bookDto.Titulo);
                if(exists != null)
                    throw new Exception("El libro ya ha sido registrado");

                
                var book = BookMapper.Mapper().CreateMapper().Map<BookDto, Book>(bookDto);
                book.AuthorId = author.AuthorId;
            
                var bookInserted = _repository.Books.Insert(book);
                _repository.Commit();

                var newBook = BookMapper.Mapper().CreateMapper().Map<Book, BookDto>(bookInserted);
                newBook.Autor = author.Name;
                return ServiceResponse<BookDto>.Correct(newBook);
            }
            catch(Exception ex)
            {
                return ServiceResponse<BookDto>.Conflict(ex.Message, null);
            }
        }

        public ResponseDto<bool> DeleteBookById(int id)
        {
            try
            {
                _repository.Books.DeleteById(id);
                _repository.Commit();
                return ServiceResponse<bool>.Correct(true);
            }
            catch
            {
                return ServiceResponse<bool>.NoContent("Error al intentar borrar el Libro", false);
            }
        }

        public ResponseDto<BookDto> GetBookById(int id)
        {
            try
            {
                var book = _repository.Books.GetById(id);
                var bookDto = BookMapper.Mapper().CreateMapper().Map<Book, BookDto>(book);

                var author = _repository.Authors.GetById(book.AuthorId);
                bookDto.Autor = author.Name;
                return ServiceResponse<BookDto>.Correct(bookDto);
            }
            catch
            {
                return ServiceResponse<BookDto>.NoContent("No se encontró el libro", null);
            }
        }

        public ResponseDto<BookDto> GetBookByTitle(string title)
        {
            var book = _repository.Books.GetFirstOrDefault(x => x.Title == title);
            var bookDto = BookMapper.Mapper().CreateMapper().Map<Book, BookDto>(book);
            
            var author = _repository.Authors.GetById(book.AuthorId);
            bookDto.Autor = author.Name;

            return ServiceResponse<BookDto>.Correct(bookDto);
        }

        public ResponseDto<IEnumerable<BookDto>> GetBooks()
        {
            var books = _repository.Books.GetAll();
            var authors = _repository.Authors.GetAll();

            var bookDtos = from b in books
                           join a in authors on b.AuthorId equals a.AuthorId
                           select new BookDto
                           {
                               Id = b.BookId,
                               Titulo = b.Title,
                               Anio = b.Year,
                               Genero = b.Genre,
                               NoPaginas = b.Pages,
                               Autor = a.Name
                           };

            return ServiceResponse<BookDto>.Correct(bookDtos);
        }

        public ResponseDto<IEnumerable<BookDto>> GetBooksByAuthor(string name)
        {
            var author = _repository.Authors.GetFirstOrDefault(x => x.Name == name);
            if(author is null)
                return ServiceResponse<BookDto>.Correct(new List<BookDto>());

            var books = _repository.Books.GetAll(x => x.AuthorId == author.AuthorId);

            var bookDtos = from b in books
                           select new BookDto
                           {
                               Id = b.BookId,
                               Titulo = b.Title,
                               Anio = b.Year,
                               Genero = b.Genre,
                               NoPaginas = b.Pages,
                               Autor = name
                           };

            return ServiceResponse<BookDto>.Correct(bookDtos);
        }

        public ResponseDto<int> GetConstant(string constant)
        {
            try
            {
                var constantBD = _repository.Constants.GetFirstOrDefault(x => x.Key == constant);
                if (constantBD is null)
                    throw new Exception("No hay valor establecido");

                return ServiceResponse<int>.Correct(constantBD.Value);

            }
            catch (Exception ex)
            {
                return ServiceResponse<int>.NoContent(ex.Message, -1);
            }
            
        }

        public ResponseDto<ConstantDto> SetConstant(ConstantDto constantDto)
        {
            var constantDB = _repository.Constants.GetFirstOrDefault(x => x.Key == constantDto.Nombre);
            if (constantDB is null)
            {
                var constant = ConstantMapper.Mapper().CreateMapper().Map<ConstantDto, Constant>(constantDto);
                var constantInserted = _repository.Constants.Insert(constant);
                _repository.Commit();

                var newConstant = ConstantMapper.Mapper().CreateMapper().Map<Constant, ConstantDto>(constantInserted);
                return ServiceResponse<ConstantDto>.Correct(newConstant);
            }

            constantDB.Value = constantDto.Valor;
            _repository.Constants.Update(constantDB);
            var updatedConstant = ConstantMapper.Mapper().CreateMapper().Map<Constant, ConstantDto>(constantDB);
            return ServiceResponse<ConstantDto>.Correct(updatedConstant);

        }

        public ResponseDto<bool> UpdateBook(BookDto bookDto)
        {
            try
            {
                var book = BookMapper.Mapper().CreateMapper().Map<BookDto, Book>(bookDto);
                var author = _repository.Authors.GetFirstOrDefault(x => x.Name == bookDto.Autor);
                if (author is null)
                    throw new Exception("El author no está registrado");
                
                book.AuthorId = author.AuthorId;
                _repository.Books.Update(book);
                return ServiceResponse<bool>.Correct(true);
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.ServerError(ex.Message, false);
            }
        }
    }
}
