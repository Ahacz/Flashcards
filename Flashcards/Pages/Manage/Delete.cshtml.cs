using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Flashcards;
using Flashcards.Models;

namespace Flashcards.Pages.Manage
{
    public class DeleteModel : PageModel
    {
        private readonly Flashcards.ApplicationDbContext _context;

        public DeleteModel(Flashcards.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public FlashCard FlashCard { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flashcard = await _context.Flashcards.FirstOrDefaultAsync(m => m.Id == id);

            if (flashcard == null)
            {
                return NotFound();
            }
            else
            {
                FlashCard = flashcard;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flashcard = await _context.Flashcards.FindAsync(id);
            if (flashcard != null)
            {
                FlashCard = flashcard;
                _context.Flashcards.Remove(FlashCard);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
