using System;
using System.IO;
using LeadApp.Services.FileService.Interfaces;

namespace LeadApp.Services.FileService
{
    public class FileService : IFileService
    {
        public StreamReader StreamReader(string path)
        {
            return File.OpenText(path);
        }
    }
}
