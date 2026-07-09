using MigraDoc.DocumentObjectModel.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Interfaces
{
    public interface ITableBuilder
    {
        Table CreateTable(IEnumerable<string> headers);
    }
}
