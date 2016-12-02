namespace SimpleReport.Template.Application.Contracts.Service
{

    public interface IFileHandler
    {
        void Save(string path, string content);
    }
}