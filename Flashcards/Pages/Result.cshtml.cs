using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Flashcards;
using Flashcards.Models;

namespace Flashcards.Pages
{
    public class ResultModel : PageModel
    {
        private readonly Flashcards.ApplicationDbContext _context;

        public ResultModel(Flashcards.ApplicationDbContext context)
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

            var flashcard =  await _context.Flashcards.FirstOrDefaultAsync(m => m.Id == id);
            if (flashcard == null)
            {
                return NotFound();
            }
            FlashCard = flashcard;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(Guid flashCardId, string action)
        {
            var flashCard = await _context.Flashcards.FindAsync(flashCardId);

            if (flashCard == null)
            {
                return NotFound();
            }

            switch (action)
            {
                case "easy":
                    flashCard.QueuePriority += 1;
                    break;
                case "review":
                    // No change in queue priority
                    break;
                case "fail":
                    if (flashCard.QueuePriority > 0)
                    {
                        flashCard.QueuePriority -= 1;
                    }
                    break;
                default:
                    return BadRequest("Invalid action.");
            }

            _context.Flashcards.Update(flashCard);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        private bool FlashCardExists(Guid id)
        {
            return _context.Flashcards.Any(e => e.Id == id);
        }
    }
}
