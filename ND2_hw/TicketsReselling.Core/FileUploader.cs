using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace TicketsReselling.Core
{
    public static class FileUploader
    {
        public static async Task<string> UploadFile(IFormFile file)
        {

            var newFileName = Path.ChangeExtension(Path.GetRandomFileName(), Path.GetExtension(file.FileName));

            using (var stream = File.Create(Path.Combine("wwwroot/Public", newFileName)))
            {
                await file.CopyToAsync(stream);
            }

            return newFileName;
        }
    }
}
