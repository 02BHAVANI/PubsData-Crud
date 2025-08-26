using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PubsData.Application.Interfaces;
using PubsData.Domain.Entities;

namespace PubsData.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISalesService _service;
        public SalesController(ISalesService service) => _service = service;

        public async Task<IActionResult> Index(string q)
        {
            var sales = await _service.ListAsync(q);
            ViewBag.Query = q;
            return View(sales);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId");
            ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName");
            return View(new Sales());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Sales sale)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId", sale?.StorId);
                    ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName", sale?.TitleId);
                    return View(sale);
                }

                await _service.CreateAsync(sale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error creating sale: {ex.Message}");
                ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId", sale?.StorId);
                ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName", sale?.TitleId);
                return View(sale);
            }
        }

        public async Task<IActionResult> Edit(string storId, string ordNum, string titleId)
        {
            var sale = await _service.GetAsync(storId, ordNum, titleId);
            if (sale == null) return NotFound();

            ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId", sale.StorId);
            ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName", sale.TitleId);
            return View(sale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Sales sale)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId", sale?.StorId);
                    ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName", sale?.TitleId);
                    return View(sale);
                }

                await _service.UpdateAsync(sale);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error updating sale: {ex.Message}");
                ViewBag.StorId = new SelectList(await _service.GetStoresAsync(), "StorId", "StorId", sale?.StorId);
                ViewBag.TitleId = new SelectList(await _service.GetTitlesAsync(), "TitleId", "TitleName", sale?.TitleId);
                return View(sale);
            }
        }
        public async Task<IActionResult> Delete(string storId, string ordNum, string titleId)
        {
            var sale = await _service.GetAsync(storId, ordNum, titleId);
            if (sale == null) return NotFound();
            return View(sale);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string storId, string ordNum, string titleId)
        {
            try
            {
                await _service.DeleteAsync(storId, ordNum, titleId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                var sale = await _service.GetAsync(storId, ordNum, titleId);
                ModelState.AddModelError("", $"Error deleting sale: {ex.Message}");
                return View("Delete", sale);
            }
        }

        public async Task<IActionResult> Details(string storId, string ordNum, string titleId)
        {
            var sale = await _service.GetAsync(storId, ordNum, titleId);
            if (sale == null) return NotFound();
            return View(sale);
        }
    }
}
