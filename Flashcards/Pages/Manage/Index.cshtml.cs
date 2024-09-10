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
    public class IndexModel : PageModel
    {
        private readonly Flashcards.ApplicationDbContext _context;

        public IndexModel(Flashcards.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FlashCard> FlashCard { get;set; } = default!;

        public async Task OnGetAsync()
        {
            FlashCard = await _context.Flashcards.ToListAsync();
        }
    }
}
