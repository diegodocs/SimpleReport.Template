using SimpleReport.Template.Application.Contracts.Service;
using System.Data;
using System.Text;

namespace SimpleReport.Template.Application.Service
{
    public class DataTableExportToCsvService : IDataTableExportToCsvService
    {
        protected readonly IFileHandler fileHandler;

        public DataTableExportToCsvService(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }
        public void Save(DataTable table, string path)
        {
            char seperator = ';';
            var builder = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                builder.Append(table.Columns[i]);
                if (i < table.Columns.Count - 1)
                {
                    builder.Append(seperator);
                }
            }

            builder.AppendLine();
            foreach (DataRow dr in table.Rows)
            {
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    builder.Append(dr[i].ToString());

                    if (i < table.Columns.Count - 1)
                    {
                        builder.Append(seperator);
                    }
                }

                builder.AppendLine();
            }

            fileHandler.Save(path, builder.ToString());
        }
    }
}