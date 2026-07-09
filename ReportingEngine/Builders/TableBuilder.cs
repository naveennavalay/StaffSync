using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Builders
{
    public class TableBuilder
    {
        public Table CreateTable(Section section, IEnumerable<string> headers)
        {
            Table table = section.AddTable();
            foreach (var h in headers)
                table.AddColumn(Unit.FromCentimeter(3));

            Row row = table.AddRow();
            int i = 0;
            foreach (var h in headers)
                row.Cells[i++].AddParagraph(h);

            return table;
        }
    }
}
