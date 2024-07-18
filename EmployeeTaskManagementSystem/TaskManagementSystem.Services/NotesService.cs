using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Services
{
    public class NotesService : INoteRepo
    {
        private readonly TaskManagementDBContext context;

        public NotesService(TaskManagementDBContext _context)
        {
            context = _context;
        }

        public async Task<bool> AddNotes(NoteVM model)
        {
            bool status = false;
            try
            {
                var data = new Note
                {
                    Content = model.Content,
                    CreatedAt = DateTime.Now,
                    TaskId = model.TaskId,
                };

                await context.AddAsync(data);

                status = await context.SaveChangesAsync() > 0;

                return status;

            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<bool> DeleteNotes(int noteId)
        {
            bool status = false;
            try
            {
                var data = await context.Notes.FirstOrDefaultAsync(a => a.NoteId == noteId);

                if (data != null)
                {
                    context.Notes.Remove(data);
                }

                status = await context.SaveChangesAsync() > 0;

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<List<NoteVM>> GetAllNotes()
        {
            var data = new List<NoteVM>();
            try
            {
                data = await context.Notes.Select(a =>
                new NoteVM
                {
                    NoteId = a.NoteId,
                    Content = a.Content,
                    TaskId = a.TaskId,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt

                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<List<NoteVM>> GetAllNotesByTaskId(int taskId)
        {
            var data = new List<NoteVM>();
            try
            {
                data = await context.Notes.Where(a => a.TaskId == taskId).Select(a =>

                new NoteVM
                {
                    NoteId = a.NoteId,
                    Content = a.Content,
                    TaskId = a.TaskId,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt

                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<NoteVM> GetNotesById(int id)
        {
            var data = new NoteVM();

            try
            {
                data = await context.Notes.Where(a => a.NoteId == id).Select(a =>

                new NoteVM
                {
                    NoteId = a.NoteId,
                    Content = a.Content,
                    TaskId = a.TaskId,
                    CreatedAt = a.CreatedAt,
                    ModifiedAt = a.ModifiedAt

                }).FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<bool> UpdateNote(NoteVM model)
        {
            bool status = false;
            try
            {
                var data = await context.Notes.FirstOrDefaultAsync(a => a.NoteId == model.NoteId);

                if (data != null)
                {
                    data.Content = model.Content;
                    data.ModifiedAt = DateTime.Now;
                    data.TaskId = model.TaskId;
                }

                status = await context.SaveChangesAsync() > 0;

                return status;

            }
            catch (Exception ex)
            {
                return status;
            }
        }
    }
}
