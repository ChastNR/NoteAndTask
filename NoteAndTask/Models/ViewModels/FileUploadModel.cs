using Microsoft.AspNetCore.Http;

namespace NoteAndTask.Models.ViewModels
{
    public class FileUploadModel
    {
        public IFormFile Image { get; set; }
    }
}
