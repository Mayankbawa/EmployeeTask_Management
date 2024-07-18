using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TaskManagementSystem.DataAccess.TaskManagementEntities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Models;
using TaskManagementSystem.Utilities;

namespace TaskManagementSystem.Services
{
    public class DocumentService : IDocumentRepo
    {
        private readonly TaskManagementDBContext context;

        private readonly FileUploadHandler fileUploadHandler;

        public DocumentService(TaskManagementDBContext _context, FileUploadHandler _fileUploadHandler)
        {
            context = _context;
            fileUploadHandler = _fileUploadHandler;
        }

        public async Task<bool> AddDocument(IFormCollection formModel)
        {
            bool status = false;
            try
            {
                var uploadModel = fileUploadHandler.UploadSingleFile(formModel);

                var additionalFormModel = formModel["formTextData"];
                var additionalData = additionalFormModel[0];
                var model = JsonConvert.DeserializeObject<DocumnetVM>(additionalData);

                var data = new Document()
                {
                    FileName = uploadModel.FileName + uploadModel.fileExtension,
                    FilePath = uploadModel.FilePath,
                    TaskId = model.TaskId,
                    UploadedAt = DateTime.Now,
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

        public async Task<bool> DeleteDocument(int documentId)
        {
            bool status = false;
            try
            {
                var data = await context.Documents.FirstOrDefaultAsync(a => a.DocumentId == documentId);

                if (data != null)
                {
                    context.Documents.Remove(data);
                }
                status = await context.SaveChangesAsync() > 0;

                fileUploadHandler.DeleteFile(data.FilePath);

                return status;
            }
            catch (Exception ex)
            {
                return status;
            }
        }

        public async Task<List<DocumnetVM>> GetAllDocuments()
        {
            List<DocumnetVM> data = new List<DocumnetVM>();
            try
            {
                data = await context.Documents.Select(a =>
                new DocumnetVM
                {
                    TaskId = a.TaskId,
                    DocumentId = a.DocumentId,
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    UploadedAt = a.UploadedAt

                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<List<DocumnetVM>> GetAllDocumentsByTaskId(int taskId)
        {
            List<DocumnetVM> data = new List<DocumnetVM>();
            try
            {
                data = await context.Documents.Where(a => a.TaskId == taskId).Select(a =>
                new DocumnetVM
                {
                    TaskId = a.TaskId,
                    DocumentId = a.DocumentId,
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    UploadedAt = a.UploadedAt

                }).ToListAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public async Task<DocumnetVM> GetDocumentById(int documentId)
        {
            DocumnetVM data = new DocumnetVM();
            try
            {
                data = await context.Documents.Where(a => a.DocumentId == documentId).Select(a =>
                new DocumnetVM
                {
                    TaskId = a.TaskId,
                    DocumentId = a.DocumentId,
                    FileName = a.FileName,
                    FilePath = a.FilePath,
                    UploadedAt = a.UploadedAt

                }).FirstOrDefaultAsync();

                return data;
            }
            catch (Exception ex)
            {
                return data;
            }
        }

        public Task<bool> UpdateDocument(IFormCollection formModel)
        {
            throw new NotImplementedException();
        }
    }
}
