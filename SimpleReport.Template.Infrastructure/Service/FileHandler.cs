using SimpleReport.Template.Application.Contracts.Service;
using System.IO;

namespace SimpleReport.Template.Infrastructure.Service
{
    public class FileHandler : IFileHandler
    {
        public void Save(string path, string content)
        {
            File.AppendAllText(path, content);
        }
    }
}