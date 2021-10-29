using Application.IServices;

using Domain.Dtos;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Persistence.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("Autores")]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        [Route("ListarAutores")]
        public IActionResult GetAll()
        {
            var response = _authorService.GetAuthors();
            return Ok(response);
        }

        [HttpGet]
        [Route("ObtenerAutor")]
        public IActionResult GetAuthorById(int id)
        {
            var response = _authorService.GetAuthorById(id);
            return Ok(response);
        }


        [HttpGet]
        [Route("BuscarAutor")]
        public IActionResult GetAuthorByName(string nombreCompleto)
        {
            var response = _authorService.GetAuthorByName(nombreCompleto);
            return Ok(response);
        }

        [HttpPost]
        [Route("RegistrarAutor")]
        public IActionResult Create([FromBody] AuthorDto author)
        {
            var response = _authorService.CreateAuthor(author);
            return Ok(response);
        }

        [HttpPut]
        [Route("ActualizarAutor")]
        public IActionResult Update([FromBody] AuthorDto author)
        {
            var response = _authorService.UpdateAuthor(author);
            return Ok(response);
        }

        [HttpDelete]
        [Route("BorrarAutor")]
        public IActionResult Delete(int id)
        {
            var response = _authorService.DeleteAuthorById(id);
            return Ok(response);
        }
    }
}
