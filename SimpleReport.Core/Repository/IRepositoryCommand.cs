using System.Collections.Generic;

namespace SimpleReport.Template.Core.Repository
{

    public interface IRepositoryCommand
    {
        string CommandName { get; set; }

        IDictionary<string,object> Parameters { get; set; }
    }
}