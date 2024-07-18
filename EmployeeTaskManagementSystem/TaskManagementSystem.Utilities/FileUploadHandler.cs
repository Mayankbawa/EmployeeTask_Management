using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Utilities
{
    public class FileUploadHandler
    {
        private readonly IConfiguration config;

        private readonly string taskDocumentPath;

        public FileUploadHandler(IConfiguration _config)
        {
            config = _config;
            taskDocumentPath = config.GetSection("TaskManagementDocuments").GetSection("TaskDocuments").Value;
        }

        public UploadResponseVM UploadSingleFile(IFormCollection formModel)
        {
            CheckAndCreateDirectory();

            var result = ProcessSingleFile(formModel);

            return result;
        }

        public UploadResponseVM ProcessSingleFile(IFormCollection formModel)
        {
            string directoryPath = taskDocumentPath;
            string fileExt = string.Empty, fileName = string.Empty, uploadFilePath = string.Empty;

            fileExt = Path.GetExtension(formModel.Files[0].FileName);
            fileName = "Emp-Task-doc-" + DateTime.Now.ToString("MMddyy-HHmmssffffff") + fileExt;
            uploadFilePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(uploadFilePath, FileMode.Create))
            {
                formModel.Files[0].CopyTo(stream);
            }

            var result = new UploadResponseVM()
            {
                FileName = fileName,
                fileExtension = fileExt,
                FilePath = uploadFilePath,
            };

            return result;
        }

        public async void DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public void CheckAndCreateDirectory()
        {
            string directoryPath = taskDocumentPath;

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
