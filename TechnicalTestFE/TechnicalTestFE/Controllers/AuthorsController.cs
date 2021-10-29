using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TechnicalTestFE.Models;
using TechnicalTestFE.Services;

namespace TechnicalTestFE.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IService _service;
        
        public AuthorsController(IService service)
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
            var authors = await _service.GetAuthors();
            return Json(new { data = authors });
        }

        [HttpGet]
        public IActionResult Create()
        {
            var author = new Author() {
                BirthDate = DateTime.Today
            };
            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.RegisterAuthor(author);
                if(response != null)
                {
                    if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                    {
                        TempData["Message"] = null;
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Message"] = response.Message;
                    return View(author);
                }
            }
            
            TempData["Message"] = "Verifica tus datos";
            return View(author);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _service.GetAuthorById(id);
            if (author is null)
                return NotFound();

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                var response = await _service.UpdateAuthor(author);
                if (response != null)
                {
                    if (response.StatusCode.Equals(System.Net.HttpStatusCode.OK))
                    {
                        TempData["Message"] = null;
                        return RedirectToAction(nameof(Index));
                    }

                    TempData["Message"] = response.Message;
                    return View(author);
                }
            }

            TempData["Message"] = "Verifica tus datos";
            return View(author);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted = await _service.DeleteAuthor(id);
            if (!isDeleted)
                return Json(new { success = false, message = "Error borrando Author" });

            return Json(new { success = true, message = "Autor eliminado con éxito" });
        }
    }
}
