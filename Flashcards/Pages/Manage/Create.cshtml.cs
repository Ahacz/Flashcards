using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Flashcards;
using Flashcards.Models;
using Flashcards.Controllers;
using Flashcards.Services;

namespace Flashcards.Pages.Manage
{
    public class CreateModel : PageModel
    {
        private readonly Flashcards.ApplicationDbContext _context;
        private readonly VMService _vmService;
        private readonly SshService _sshService;

        public CreateModel(Flashcards.ApplicationDbContext context, VMService vmService, SshService ssh)
        {
            _sshService = ssh;
            _context = context;
            _vmService = vmService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public FlashCard FlashCard { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var repoPath = System.IO.Path.Combine(_vmService.RepoPath, new Uri(FlashCard.RepoLink).Segments[^1].Trim('/'));
            repoPath = repoPath.Substring(0, repoPath.Length - 4);
            FlashCard.RepoPath = repoPath;
            ModelState.ClearValidationState(nameof(FlashCard));
            if (!TryValidateModel(FlashCard, nameof(FlashCard)))
            {
                return Page();
            }
            try
            {
                await _vmService.EnsureVmIsRunningAsync();
                await _sshService.DownloadRepoAsync(FlashCard.RepoLink, repoPath);
                _context.Flashcards.Add(FlashCard);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add a new flashcard. Error:\n{ex.Message}");
            }

        }
    }
}
