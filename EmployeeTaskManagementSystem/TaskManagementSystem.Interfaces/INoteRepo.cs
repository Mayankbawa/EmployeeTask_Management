using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface INoteRepo
    {
        Task<bool> AddNotes(NoteVM model);

        Task<bool> UpdateNote(NoteVM model);

        Task<NoteVM> GetNotesById(int id);

        Task<List<NoteVM>> GetAllNotes();

        Task<List<NoteVM>> GetAllNotesByTaskId(int taskId);

        Task<bool> DeleteNotes(int noteId);
    }
}
