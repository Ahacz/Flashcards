namespace Flashcards.Models
{
    public class SshSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Pwd { get; set; }
        public string VSLauncherPath { get; set; }
    }
    public class VmSettings
    {
        public string VMName { get; set; }
        public string VMmgrPath { get; set; }
        public string ReposPath { get; set; }
    }
    public class SshCmdResult
    {
        public bool IsSuccess { get; }
        public string Output { get; }
        public string Error { get; }

        public SshCmdResult(bool isSuccess, string output, string error = null)
        {
            IsSuccess = isSuccess;
            Output = output;
            Error = error;
        }
    }
}
