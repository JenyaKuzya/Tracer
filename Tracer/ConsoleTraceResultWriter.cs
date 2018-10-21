using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TracerLib
{
    public class ConsoleTraceResultWriter : ITraceResultWriter
    {
        public void WriteResult(TraceResult traceResult, ITraceResultSerializer serializer)
        {
            using (Stream consoleOutputStream = Console.OpenStandardOutput())
            {
                serializer.Serialize(consoleOutputStream, traceResult);
            }
        }
    }
}
