using Microsoft.AspNetCore.Http;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Interfaces
{
    public interface IDocumentRepo
    {
        Task<bool> AddDocument(IFormCollection formModel);

        Task<bool> DeleteDocument(int documentId);

        Task<bool> UpdateDocument(IFormCollection formModel);

        Task<List<DocumnetVM>> GetAllDocuments();

        Task<List<DocumnetVM>> GetAllDocumentsByTaskId(int taskId);

        Task<DocumnetVM> GetDocumentById(int documentId);
    }
}
