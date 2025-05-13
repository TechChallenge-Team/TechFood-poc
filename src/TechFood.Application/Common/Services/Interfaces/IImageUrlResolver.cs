using Microsoft.AspNetCore.Http;

namespace TechFood.Application.Common.Services.Interfaces;

public interface IImageUrlResolver
{
    string BuildFilePath(HttpRequest request, string folderName, string imageFileName);

    string CreateImageFileName(string categoryName, string contentType);
}
