using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment environment;

    public FileService(IWebHostEnvironment environment)
    {
        this.environment = environment;
    }
    public string CreateFile(string folder, IFormFile file)
    {
        var path = Path.Combine(environment.WebRootPath, folder, file.FileName);
        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return path;
    }

    public bool DeleteFile(string folder, string fileName)
    {
        var path = Path.Combine(environment.WebRootPath, folder, fileName);
        if (File.Exists(path))
        {
            File.Delete(path);
            return true;
        }
        return false;
    }
}
