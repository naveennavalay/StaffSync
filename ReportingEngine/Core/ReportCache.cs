using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportingEngine.Core
{
    public class ReportCache
    {
        private readonly Dictionary<string, Document> _cache = new Dictionary<string, Document>();

        public void Add(string key, Document document)
        {
            _cache[key] = document;
        }

        public bool TryGet(string key, out Document document)
        {
            return _cache.TryGetValue(key, out document);
        }

        public void Clear()
        {
            _cache.Clear();
        }
    }
}
