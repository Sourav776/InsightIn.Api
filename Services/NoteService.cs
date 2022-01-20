using InsightIn.Api.Interfaces;
using InsightIn.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Services
{
    public class NoteService : INoteService
    {
        private readonly List<Note> _noteList = new();
        private static long _noteId = 1;
        public bool AddNote(Note model)
        {
            model.NoteId = _noteId++;
            _noteList.Add(model);
            return true;
        }

        public bool DeleteNote(long noteId)
        {
            var note = _noteList.FirstOrDefault(x => x.NoteId == noteId);
            if (note is not null)
            {
                _noteList.Remove(note);
            }
            return true;
        }

        public List<Note> GetList(string userName)
        {
            return _noteList.Where(x=>x.UserName==userName).ToList();
        }
        public List<NoteType> GetNoteTypes()
        {
            return new List<NoteType>()
            {
                new NoteType()
                {
                    TypeId=1,
                    TypeName="Regular"
                },
                new NoteType()
                {
                    TypeId=2,
                    TypeName="Reminder"
                },
                new NoteType()
                {
                    TypeId=3,
                    TypeName="Todo"
                },
                new NoteType()
                {
                    TypeId=4,
                    TypeName="Bookmark"
                }
            };
        }

        public bool UpdateNote(long noteId, Note model)
        {
            var note = _noteList.FirstOrDefault(x => x.NoteId == noteId);
            if (note is not null)
            {
                note.NoteType = model.NoteType;
                note.NoteContent = model.NoteContent;
                note.IsComplete = model.IsComplete;
                note.ReminderTimeDate = model.ReminderTimeDate;
                note.DueTimeDate = model.DueTimeDate;
            }
            return true;
        }
    }
}
