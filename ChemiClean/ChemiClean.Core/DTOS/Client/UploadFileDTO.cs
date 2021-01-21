using Microsoft.AspNetCore.Http;

namespace ChemiClean.Core.DTOs
{
    public class UploadFileDTO
    {
        public IFormFileCollection IFormFileCollection { get; set; }
        public string Path { get; set; }
    }
}