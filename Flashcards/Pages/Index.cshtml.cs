using Flashcards.Controllers;
using Flashcards.Models;
using Flashcards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Flashcards.Pages
{
    public class IndexModel : PageModel
    {
        private readonly VMService _vmService;
        private readonly SshService _sshService;
        private readonly ApplicationDbContext _context;
        public List<FlashCard> Flashcards { get; set; }

        public IndexModel(ApplicationDbContext context, VMService vMService, SshService ssh)
        {
            _vmService= vMService;
            _context = context;
            _sshService = ssh;
            Flashcards = _context.Flashcards.ToList();
        }
        public async Task<IActionResult> OnPostRunRepoAsync(string flashcardId)
        {
            if(string.IsNullOrEmpty(flashcardId))
            {
                return BadRequest("FlashcardId is null.");
            }
            FlashCard f = await _context.Flashcards.FindAsync(Guid.Parse(flashcardId));
            try
            {
                await _vmService.EnsureVmIsRunningAsync();
                await _sshService.CheckConnectionAsync();
                await _sshService.RunRepoInVSAsync(f.RepoPath);

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        public void OnGet()
        {
            Flashcards = _context.Flashcards.ToList();
        }
    }
}

