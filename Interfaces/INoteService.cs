using InsightIn.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsightIn.Api.Interfaces
{
    public interface INoteService
    {
        List<Note> GetList(string userName);
        List<NoteType> GetNoteTypes();
        bool AddNote(Note model);
        bool UpdateNote(long noteId, Note model);
        bool DeleteNote(long noteId);
    }
}
