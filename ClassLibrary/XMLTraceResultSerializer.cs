using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace TracerLib
{
    public class XMLTraceResultSerializer : ITraceResultSerializer
    {
        private XmlSerializer xmlSerializer;

        public void Serialize(Stream outStream, TraceResult traceResult)
        {
            xmlSerializer.Serialize(outStream, traceResult);         
        }

        public XMLTraceResultSerializer()
        {
            xmlSerializer = new XmlSerializer(typeof(TraceResult));
        }
    }
}
