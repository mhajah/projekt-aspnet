using Microsoft.AspNetCore.Mvc;
using ProjektASPNET.Data;
using ProjektASPNET.Models;

namespace ProjektASPNET.Controllers
{
    public class RecipesController : Controller
    {
        private readonly ApplicationDbContext _context; // Załóżmy, że ApplicationDbContext to klasa kontekstu bazy danych

        public RecipesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wyświetlenie formularza
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zapisywanie przepisu do bazy danych
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Title,Description,Image")] Recipe recipe, IFormFile upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await upload.CopyToAsync(memoryStream);
                        recipe.Photo = memoryStream.ToArray();
                    }
                }

                _context.Add(recipe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Przekierowanie do głównej listy przepisów
            }
            return View(recipe);
        }
    }

}
