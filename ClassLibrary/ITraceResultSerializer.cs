using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TracerLib
{
    public interface ITraceResultSerializer
    {
        MemoryStream Serialize(MemoryStream outStream, TraceResult traceResult);
    }
}
