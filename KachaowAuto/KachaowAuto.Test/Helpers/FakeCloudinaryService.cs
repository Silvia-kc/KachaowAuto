using KachaowAuto.Core.Interfaces;
using KachaowAuto.Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KachaowAuto.Tests.Helpers
{
    public class FakeCloudinaryService : ICloudinaryService
    {
        public string? UploadUrl { get; set; } = "https://img.com/default.jpg";
        public string? UploadPublicId { get; set; } = "parts/default";
        public List<string> DeletedPublicIds { get; } = new();

        public Task<CloudinaryUploadResultModel?> UploadImageAsync(IFormFile file, string folder)
        {
            if (UploadUrl == null)
            {
                return Task.FromResult<CloudinaryUploadResultModel?>(null);
            }

            return Task.FromResult<CloudinaryUploadResultModel?>(new CloudinaryUploadResultModel
            {
                Url = UploadUrl,
                PublicId = UploadPublicId
            });
        }

        public Task<bool> DeleteImageAsync(string publicId)
        {
            DeletedPublicIds.Add(publicId);
            return Task.FromResult(true);
        }
    }
}
