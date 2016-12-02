using System.Data;

namespace SimpleReport.Template.Application.Contracts.Service
{
    public interface IDataTableExportService
    {
        void Save(DataTable datatable, string path);        
    }
}