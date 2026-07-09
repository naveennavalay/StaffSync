using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class DataTableReportBuilder
    {
        public Table Build(Section section, DataTable tableData)
        {
            Table table = section.AddTable();

            foreach (DataColumn column in tableData.Columns)
                table.AddColumn(Unit.FromCentimeter(3));

            Row header = table.AddRow();
            header.HeadingFormat = true;
            header.Format.Font.Bold = true;

            for (int i = 0; i < tableData.Columns.Count; i++)
                header.Cells[i].AddParagraph(tableData.Columns[i].ColumnName);

            foreach (DataRow dr in tableData.Rows)
            {
                Row row = table.AddRow();
                for (int i = 0; i < tableData.Columns.Count; i++)
                    row.Cells[i].AddParagraph(dr[i]?.ToString());
            }

            return table;
        }
    }
}
