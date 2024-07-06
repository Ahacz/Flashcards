using Microsoft.AspNetCore.Mvc;

namespace Flashcards.Controllers
{
    public class HomeController : Controller
    { 
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var flashcards = _context.Flashcards.ToList();
            return View(flashcards);
        }
    }
}
