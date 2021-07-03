using System.IO;

namespace LeadApp.Services.FileService.Interfaces
{
    public interface IFileService
    {
        StreamReader StreamReader(string path);
    }
}