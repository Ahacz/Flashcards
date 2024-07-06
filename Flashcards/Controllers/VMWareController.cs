using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Xml.Linq;

namespace Flashcards.Controllers
{
    [ApiController]
    [Route("api/vm")]
    public class VMWareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private string vmXPath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("VMPath");
        private string vboxManagePath = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetValue<string>("VBoxManagePath");
        public VMWareController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("start-snapshot/{flashcardId}")]
        public IActionResult StartSnapshot(Guid flashcardId)
        {
            var flashcard = _context.Flashcards.Find(flashcardId);
            if (flashcard == null)
            {
                return NotFound(new { message = "Flashcard not found" });
            }

            string command = $"snapshot VM restore \"{flashcard.SnapshotPath}\"";

            // Execute the VBoxManage command
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = vboxManagePath,
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            command = $"startvm VM";
            ProcessStartInfo psi2 = new ProcessStartInfo
            {
                FileName = vboxManagePath,
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = new Process())
            {
                process.StartInfo = psi;
                process.Start();

                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();


                if (process.ExitCode == 0)
                {
                    process.Close();
                    process.StartInfo = psi2;
                    process.Start();

                    string error2 = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode == 0)
                    {
                        // Command executed successfully
                        return Ok("VM snapshot started successfully.");
                    }
                    else
                    {
                        // Command failed
                        return BadRequest($"Failed to start VM snapshot. Error: {error2}");
                    }
                }
                else
                {
                    // Command failed
                    return BadRequest($"Failed to start VM snapshot. Error: {error}");
                }
            }
        }
    }
}
