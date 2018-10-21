using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.Xml;
using System.IO;

namespace TracerLib
{
    public class JsonTraceResultSerializer : ITraceResultSerializer
    {
        protected readonly DataContractJsonSerializer jsonSerializer;

        public void Serialize(Stream outStream, TraceResult traceResult)
        {
            using (Stream stream = outStream)
            {
                jsonSerializer.WriteObject(stream, traceResult);
            }
        }

        public JsonTraceResultSerializer()
        {
            jsonSerializer = new DataContractJsonSerializer(typeof(TraceResult));
        }
    }
}
