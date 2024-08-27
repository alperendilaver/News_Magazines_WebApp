using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace News.Data.Services.AbstractServices
{
    public interface IImageService
    {
        public Task<string> UploadImageAsync(IFormFile ImageFile);

        public bool DeleteOldPhoto(string FileName);
    }
}