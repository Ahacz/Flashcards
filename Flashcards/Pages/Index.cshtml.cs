using Flashcards.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flashcards.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<FlashCard> Flashcards { get; set; }

        public void OnGet()
        {
            Flashcards = _context.Flashcards.ToList();
        }
    }
}

