using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;

namespace PubsData.Controllers
{
    public class TitlesController : Controller
    {
        private readonly ITitleService _service;
        private readonly IPublisherService _publisherService;

        public TitlesController(ITitleService titleService, IPublisherService publisherService)
        {
            _service = titleService;
            _publisherService = publisherService;
        }

        public async Task<IActionResult> Index(string? q)
        {
            ViewBag.Query = q;
            var titles = await _service.ListAsync(q);
            return View(titles);
        }

        public async Task<IActionResult> Create()
        {
            var publishers = await _publisherService.ListAsync(null);
            ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Title title)
        {
            if (!ModelState.IsValid)
            {
                var publishers = await _publisherService.ListAsync(null);
                ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName");
                return View(title);
            }

            try
            {
                await _service.CreateAsync(title);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error creating the title.";
                var publishers = await _publisherService.ListAsync(null);
                ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName");
                return View(title);
            }
        }

        public async Task<IActionResult> Edit(string id)
        {
            var title = await _service.GetAsync(id);
            if (title == null) return NotFound();

            var publishers = await _publisherService.ListAsync(null);
            ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName", title.PubId);

            ViewData["IsEdit"] = true;
            return View(title);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Title title)
        {
            if (!ModelState.IsValid)
            {
                var publishers = await _publisherService.ListAsync(null);
                ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName", title.PubId);
                return View(title);
            }

            try
            {
                await _service.UpdateAsync(title);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error updating the title.";
                var publishers = await _publisherService.ListAsync(null);
                ViewBag.Publishers = new SelectList(publishers, "PubId", "PubName", title.PubId);
                return View(title);
            }
        }

        public async Task<IActionResult> Delete(string id)
        {
            var title = await _service.GetAsync(id);
            if (title == null) return NotFound();
            return View(title);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error deleting the title.";
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Details(string id)
        {
            var title = await _service.GetAsync(id);
            if (title == null) return NotFound();
            return View(title);
        }
    }
}
