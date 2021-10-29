using Application.IServices;

using Domain.Dtos;

using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("Libros")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("ListarLibros")]
        public IActionResult GetAll()
        {
            var response = _bookService.GetBooks();
            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLibrosPorAutor")]
        public IActionResult GetBooksByAuthor(string nombre)
        {
            var response = _bookService.GetBooksByAuthor(nombre);
            return Ok(response);
        }

        [HttpGet]
        [Route("ObtenerLibro")]
        public IActionResult GetBookById(int id)
        {
            var response = _bookService.GetBookById(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("BuscarLibro")]
        public IActionResult GetBookByTitle(string titulo)
        {
            var response = _bookService.GetBookByTitle(titulo);
            return Ok(response);
        }

        [HttpPost]
        [Route("RegistrarLibro")]
        public IActionResult Create([FromBody] BookDto book)
        {
            var response = _bookService.CreateBook(book);
            return Ok(response);
        }

        [HttpPut]
        [Route("ActualizarLibro")]
        public IActionResult Update([FromBody] BookDto book)
        {
            var response = _bookService.UpdateBook(book);
            return Ok(response);
        }

        [HttpDelete]
        [Route("BorrarLibro")]
        public IActionResult Delete(int id)
        {
            var response = _bookService.DeleteBookById(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("ConsultarLimite")]
        public IActionResult GetRegistrationLimit()
        {
            var response = _bookService.GetConstant("RegistrationLimit");
            return Ok(response);
        }

        [HttpPut]
        [Route("EstablecerLimite")]
        public IActionResult SetRegistrationLimit(int valor)
        {
            var response = _bookService.SetConstant(new ConstantDto() { Nombre = "RegistrationLimit", Valor = valor});
            return Ok(response);
        }

    }
}
