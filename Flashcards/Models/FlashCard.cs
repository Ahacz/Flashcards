using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flashcards.Models
{
    public class FlashCard
    {
        private Guid _id;
        private string  _title, _description, _repoLink;
        private string? _repoPath;
        private int _queuePriority;
        public FlashCard() { }
        public FlashCard(Guid id, string title, string description, string repolink)
        {
            Id = id;
            Title = title;
            RepoLink = repolink;
            RepoPath = "";
            Description = description;
            QueuePriority = 0;
        }

       
        public Guid Id
        {
            get => _id;
            set => _id = value;
        }

        public string Title
        {
            get => _title;
            set => _title = value;
        }

        public string RepoLink
        {
            get => _repoLink;
            set => _repoLink = value;
        }

        public string Description
        {
            get => _description;
            set => _description = value;
        }
        public int QueuePriority
        {
            get => _queuePriority;
            set => _queuePriority = value;
        }

        public string RepoPath
        {
            get => _repoPath;
            set => _repoPath = value;
        }

        public void SetResult ( bool passed)
        {
            if (passed) _queuePriority += 1;
            else if (_queuePriority != 0) _queuePriority -= 1;
        }
    }
}
