using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using News.Data.Services;
using News.Data.Services.AbstractServices;

namespace News.Data.Services.ConcreateServices
{
    public class ImageService : IImageService
    {
        
        public bool DeleteOldPhoto(string FileName)
        {
            
            var oldImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", FileName);
            if (File.Exists(oldImagePath))
                {
                    File.Delete(oldImagePath);
                    return true;
                }
            return false;
        }

        public async Task<string> UploadImageAsync(IFormFile ImageFile)
        {
            var extension = Path.GetExtension(ImageFile.FileName);
                var randomFileName = string.Format($"{Guid.NewGuid().ToString()}{extension}");
                var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/img",randomFileName);
                using (var stream = new FileStream(path,FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }
            return randomFileName;
        }
    }
}