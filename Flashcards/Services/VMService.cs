using Flashcards.Models;
using Flashcards.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NuGet.Configuration;
using System.Diagnostics;
using System.Xml.Linq;

namespace Flashcards.Controllers
{
    public class VMService : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly VmSettings _settings;
        public VMService(IOptions<VmSettings> settings,ApplicationDbContext context)
        {
            _settings = settings.Value;
            _context = context;
        }
        public string RepoPath { get => _settings.ReposPath; }
        public async Task EnsureVmIsRunningAsync()
        {
            if (!IsVmRunning())
            {
                StartVm();
            }
        }
        private void StartVm()
        {
            var startVmCommand =" startvm "+_settings.VMName;
            ExecuteCommand(startVmCommand);
        }
        private bool IsVmRunning()
        {
            var checkVmCommand = $" showvminfo {_settings.VMName} --machinereadable";
            var result = ExecuteCommand(checkVmCommand);

            // Check if the VM state is "running"
            return result.Contains("VMState=\"running\"");
        }
        private string ExecuteCommand(string command)
        {
            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = _settings.VMmgrPath,
                Arguments = command,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();
            var output = process.StandardOutput.ReadToEnd();
            process.Dispose();

            return output;
        }
    }
}
