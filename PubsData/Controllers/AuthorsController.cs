using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;

namespace PubsData.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IAuthorService _service;
        public AuthorsController(IAuthorService service) => _service = service;

        public async Task<IActionResult> Index(string? q)
        {
            ViewBag.Query = q;
            var authors = await _service.ListAsync(q);
            return View(authors);
        }

        public IActionResult Create()
        {
            ViewData["IsEdit"] = false;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid) return View(author);

            try
            {
                await _service.CreateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                ModelState.AddModelError("", "Invalid data format or constraint violation. Please check your input.");
                return View(author);
            }
        }


        public async Task<IActionResult> Edit(string id)
        {
            var author = await _service.GetAsync(id);
            if (author == null) return NotFound();
            ViewData["IsEdit"] = true;
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Author author)
        {
            if (!ModelState.IsValid) return View(author);

            try
            {
                await _service.UpdateAsync(author);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                ModelState.AddModelError("", "Invalid data format or constraint violation. Please check your input.");
                return View(author);
            }
        }


        public async Task<IActionResult> Delete(string id)
        {
            var author = await _service.GetAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(string id)
        {
            var author = await _service.GetAsync(id);
            if (author == null) return NotFound();
            return View(author);
        }
    }
}