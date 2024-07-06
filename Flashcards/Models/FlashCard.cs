using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flashcards.Models
{
    public class FlashCard
    {
        private Guid _id;
        private string  _title, _description, _snapshotPath;
        private int _queuePriority;
        public FlashCard() { }
        public FlashCard(Guid id, string title, string description, string snapshotpath)
        {
            Id = id;
            Title = title;
            SnapshotPath = snapshotpath;
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

        public string SnapshotPath
        {
            get => _snapshotPath;
            set => _snapshotPath = value;
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

        public void SetResult ( bool passed)
        {
            if (passed) _queuePriority += 1;
            else if (_queuePriority != 0) _queuePriority -= 1;
        }
    }
}
