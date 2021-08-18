namespace TapMarket.Services.File
{
    using System;
    using System.IO;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;

    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment) 
            => this.webHostEnvironment = webHostEnvironment;

        public string UploadFile(IFormFile image)
        {
            string fileName = null;

            if (image != null)
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "Images");

                fileName = Guid.NewGuid().ToString() + "-" + image.FileName;

                string filePath = Path.Combine(uploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    image.CopyTo(fileStream);
                }
            }

            return fileName;
        }
    }
}
