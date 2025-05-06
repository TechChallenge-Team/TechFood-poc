using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using TechFood.Application.Common.Services.Interfaces;

namespace TechFood.Application.Common.Services
{
    public class ImageUrlResolver : IImageUrlResolver
    {
        private readonly IConfiguration _appConfiguration;

        public ImageUrlResolver(IConfiguration appConfiguration)
        {
            _appConfiguration = appConfiguration;
        }

        public string BuildFilePath(HttpRequest request, string folderName, string imageFileName)
        {
            var scheme = request.Scheme;
            var host = request.Host.Value;
            var baseUrl = _appConfiguration["TechFoodStaticImagesUrl"]?.Trim('/');

            return $"{scheme}://{host}/{baseUrl}/{folderName}/{imageFileName}";
        }

        public string CreateImageFileName(string categoryName, string contentType)
        {
            return $"{Regex.Replace(categoryName.Trim(), @"\s+", "-")}-{DateTime.UtcNow:yyyyMMddHHmmss}.{contentType.Replace("image/", "")}";
        }
    }
}
