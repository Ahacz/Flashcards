using Flashcards.Models;
using Microsoft.Extensions.Internal;
using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace Flashcards.Services
{
    public class SshService
    {
        private SshClient _sshClient;
        private readonly SshSettings _settings;
        public SshService(IOptions<SshSettings> settings)
        {
            _settings = settings.Value;
            _sshClient = new SshClient(_settings.Host, _settings.Port, _settings.Username, _settings.Pwd);
        }

        public async Task<string> DownloadRepoAsync(string repoLink, string localPath)
        {
            _sshClient.Connect();
            // Ensure the localPath directory exists
            var ensureDirectoryCommand = $"mkdir {localPath}";
            var ensureDirectoryResult = await ExecuteCommandAsync(ensureDirectoryCommand);
            if (!ensureDirectoryResult.IsSuccess)
            {
                throw new Exception($"Failed to create directory: {ensureDirectoryResult.Error}");
            }

            // Clone the repository
            var cloneCommand = $"git clone {repoLink} {localPath}";
            var cloneResult = await ExecuteCommandAsync(cloneCommand);
            if (!cloneResult.IsSuccess)
            {
                throw new Exception($"Failed to clone repository: {cloneResult.Error}");
            }

            // Restore NuGet packages
            var restoreCommand = $"cd {localPath} && dotnet restore";
            var restoreResult = await ExecuteCommandAsync(restoreCommand);
            if (!restoreResult.IsSuccess)
            {
                throw new Exception($"Failed to restore NuGet packages: {restoreResult.Error}");
            }
            _sshClient.Disconnect();
            return localPath;
        }

        public async Task RunRepoInVSAsync(string localPath)
        {
            _sshClient.Connect();

            // Find the solution file
            var findSolutionCommand = $"cd {localPath} && dir /b *.sln";
            var findSolutionResult = await ExecuteCommandAsync(findSolutionCommand);

            if (!findSolutionResult.IsSuccess)
            {
                throw new Exception($"Failed to find solution file: {findSolutionResult.Error}");
            }

            var solutionFileName = findSolutionResult.Output.Trim();
            if (string.IsNullOrEmpty(solutionFileName))
            {
                throw new Exception("Solution file not found.");
            }

            // Ensure the latest commit is checked out
            var resetCommand = $"cd {localPath} && git reset --hard HEAD";
            var resetResult = await ExecuteCommandAsync(resetCommand);
            if (!resetResult.IsSuccess)
            {
                throw new Exception($"Failed to reset to the latest commit: {resetResult.Error}");
            }

            // Check if Visual Studio is running
            var checkVSCommand = $"tasklist /FI \"IMAGENAME eq devenv.exe\"";
            var checkVSResult = await ExecuteCommandAsync(checkVSCommand);
            bool isVSRunning = checkVSResult.Output.Contains("devenv.exe");

            if (isVSRunning)
            {
                // Attach to the running instance of Visual Studio and open the solution
                var openSolutionCommand = $@"
                $vs = [Runtime.InteropServices.Marshal]::GetActiveObject('VisualStudio.DTE');
                $vs.Solution.Open('{Path.Combine(localPath, solutionFileName)}');";
                var openSolutionResult = await ExecuteCommandAsync(openSolutionCommand);
                if (!openSolutionResult.IsSuccess)
                {
                    throw new Exception($"Failed to open Visual Studio solution: {openSolutionResult.Error}");
                }
            }
            else
            {
                // Open Visual Studio with the solution file
                var openVSCommand = $"start devenv {Path.Combine(localPath, solutionFileName)}";
                var openVSResult = await ExecuteCommandAsync(openVSCommand);

                if (!openVSResult.IsSuccess)
                {
                    throw new Exception($"Failed to open Visual Studio: {openVSResult.Error}");
                }
            }

            _sshClient.Disconnect();
        }
        public async Task CheckConnectionAsync()
        {
            int attempts = 0;
            const int maxAttempts = 20;
            const int delayMs = 3000; // 3 seconds delay between attempts

            while (attempts < maxAttempts)
            {
                try
                {
                    _sshClient.Connect();
                    _sshClient.Disconnect();
                    return; // Connection successful, return from method
                }
                catch (Exception)
                {
                    await Task.Delay(delayMs);
                    attempts++;
                }
            }

            throw new TimeoutException("Failed to establish SSH connection to VM within the timeout period.");
        }


        private async Task<SshCmdResult> ExecuteCommandAsync(string commandText)
        {
            var command = _sshClient.CreateCommand(commandText);
            var result = await Task.Run(() => command.Execute());

            if (command.ExitStatus != 0)
            {
                _sshClient.Disconnect();
                return new SshCmdResult(false, result, command.Error);
            }
            return new SshCmdResult(true, result);
        }
    }
}
