using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;

namespace PubsData.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherService _service;
        private readonly ITitleService _titleService;
        public PublishersController(IPublisherService service, ITitleService titleService)
        {
            _service = service;
            _titleService = titleService;
        }

        public async Task<IActionResult> Index(string? q)
        {
            ViewBag.Query = q;
            var publishers = await _service.ListAsync(q);
            return View(publishers);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Publisher publisher)
        {
            if (!ModelState.IsValid)
                return View(publisher);

            var existing = await _service.GetAsync(publisher.PubId?.Trim());
            if (existing != null)
            {
                ModelState.AddModelError("PubId", "This PubId already exists. Please use a different one.");
                return View(publisher);
            }

            try
            {
                await _service.CreateAsync(publisher);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unable to create publisher: {ex.Message}");
                return View(publisher);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var publisher = await _service.GetAsync(id?.Trim());
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Publisher publisher)
        {
            if (!ModelState.IsValid) return View(publisher);

            try
            {
                await _service.UpdateAsync(publisher);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx)
            {
                ModelState.AddModelError("", "Invalid data format or constraint violation. Please check your input.");
                return View(publisher);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var publisher = await _service.GetAsync(id?.Trim());
            if (publisher == null) return NotFound();
            return View(publisher);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id, bool cascade = false)
        {
            var pubId = id?.Trim();
            if (string.IsNullOrWhiteSpace(pubId))
                return NotFound();

            try
            {
                // Cascade delete titles and related sales if requested
                if (cascade)
                {
                    var allTitles = await _titleService.ListAsync(pubId);
                    var titlesForPublisher = allTitles
                        .Where(t => string.Equals((t.PubId ?? string.Empty).Trim(), pubId, StringComparison.OrdinalIgnoreCase));

                    foreach (var title in titlesForPublisher)
                    {
                        if (!string.IsNullOrWhiteSpace(title.TitleId))
                        {
                            await _titleService.DeleteAsync(title.TitleId);
                        }
                    }
                }

                // Delete the publisher itself
                await _service.DeleteAsync(pubId);
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlEx && sqlEx.Number == 547)
            {
                // FK violation (employee exists)
                ModelState.AddModelError(string.Empty,
                    "Cannot delete this publisher because employees exist for this publisher.");
                var publisher = await _service.GetAsync(pubId);
                return View("Delete", publisher);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty,
                    $"Unable to delete publisher. {(cascade ? "Cascade delete attempted. " : string.Empty)}{ex.Message}");
                var publisher = await _service.GetAsync(pubId);
                return View("Delete", publisher);
            }
        }
        public async Task<IActionResult> Details(string id)
        {
            var publisher = await _service.GetAsync(id?.Trim());
            if (publisher == null) return NotFound();
            return View(publisher);
        }
    }
}