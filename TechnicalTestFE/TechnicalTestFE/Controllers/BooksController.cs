using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnicalTestFE.Models;
using TechnicalTestFE.Services;

namespace TechnicalTestFE.Controllers
{
    public class BooksController : Controller
    {
        private readonly IService _service;
        
        public BooksController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var books = await _service.GetBooks();
            return Json(new { data = books });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var book = new Book();
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.RegisterBook(book);
                if (response != null)
                {
                    if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                    {
                        TempData["Message"] = null;
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Message"] = response.Message;
                    return View(book);
                }
            }

            TempData["Message"] = "Verifica tus datos";
            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _service.GetBookById(id);
            if (book is null)
                return NotFound();

            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateBook(book);
                if (response != null)
                {
                    if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                    {
                        TempData["Message"] = null;
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Message"] = response.Message;
                    return View(book);
                }
            }

            TempData["Message"] = "Verifica tus datos";
            return View(book);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteBook(id);
            if (!isDeleted)
                return Json(new { success = false, message = "Error borrando Libro" });

            return Json(new { success = true, message = "Libro eliminado con éxito" });
        }
    }
}
