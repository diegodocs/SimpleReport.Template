using System.Data;
using SimpleReport.Template.Core.Repository;

namespace SimpleReport.Template.Core.Repository
{
    public interface IRepositoryForSql
    {        
        DataTable GetDataTable(IRepositoryCommand commandRepository);        
    }
}