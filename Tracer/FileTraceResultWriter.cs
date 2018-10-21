using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TracerLib
{
    public class FileTraceResultWriter : ITraceResultWriter
    {
        public string Filename
        { set; get; }

        public void WriteResult(TraceResult traceResult, ITraceResultSerializer serializer)
        {
            using (FileStream fileStream = new FileStream(Filename, FileMode.Append))
            {
                serializer.Serialize(fileStream, traceResult);
            }
        }

        public FileTraceResultWriter(string fileName)
        {
            Filename = fileName;
        }
    }
}
