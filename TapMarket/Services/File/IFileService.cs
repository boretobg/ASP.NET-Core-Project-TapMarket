namespace TapMarket.Services
{
    using Microsoft.AspNetCore.Http;

    public interface IFileService
    {
        public string UploadFile(IFormFile image);
    }
}
