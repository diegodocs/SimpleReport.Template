using SimpleReport.Template.Application.Contracts.Service;
using System.Data;
using System.Text;

namespace SimpleReport.Template.Application.Service
{
    public class DataTableExportToHtmlService : IDataTableExportToHtmlService
    {
        protected readonly IFileHandler fileHandler;

        public DataTableExportToHtmlService(IFileHandler fileHandler)
        {
            this.fileHandler = fileHandler;
        }

        public void Save(DataTable table, string path)
        {
            var builder = new StringBuilder();
            builder.Append("<html >");
            builder.Append("<head>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<table border='1px' cellpadding='1' cellspacing='1' bgcolor='lightyellow' style='font-   family:Garamond; font-size:smaller'>");
            builder.Append("<tr >");
            foreach (DataColumn myColumn in table.Columns)
            {
                builder.Append("<td >");
                builder.Append(myColumn.ColumnName);
                builder.Append("</td>");
            }
            builder.Append("</tr>");
            foreach (DataRow myRow in table.Rows)
            {
                builder.Append("<tr >");
                foreach (DataColumn myColumn in table.Columns)
                {
                    builder.Append("<td >");
                    builder.Append(myRow[myColumn.ColumnName].ToString());
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            //Close tags.  
            builder.Append("</table>");
            builder.Append("</body>");
            builder.Append("</html>");

            fileHandler.Save(path, builder.ToString());
        }
    }
}