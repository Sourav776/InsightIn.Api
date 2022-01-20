using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Models
{
    public class Note
    {
        public long NoteId { get; set; }
        public long NoteType { get; set; }
        public string NoteContent { get; set; }
        public DateTime? NoteDate { get; set; } = DateTime.Now;
        public string UserName { get; set; }
        public DateTime? ReminderTimeDate { get; set; }
        public DateTime? DueTimeDate { get; set; }
        public bool IsComplete { get; set; } = false;
    }
}
