using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TicketsReselling.Business.Models
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
