using System.Collections.Generic;

namespace SimpleReport.Template.Core.Repository
{

    public class RepositoryCommand : IRepositoryCommand
    {
        public RepositoryCommand()
        {
            Parameters = new Dictionary<string, object>();
        }
        public string CommandName { get; set; }         
        public IDictionary<string, object> Parameters { get; set; }
    }
}