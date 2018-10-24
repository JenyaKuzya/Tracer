using System;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using System.Xml.Serialization;

namespace TracerLib
{
    [DataContract(Name = "result")]
    public class TraceResult
    {
        private ConcurrentDictionary<int, TracedThread> tracedThreads;

        public TraceResult()
        {
            tracedThreads = new ConcurrentDictionary<int, TracedThread>();
        }

        [DataMember(Name = "threads")]
        [XmlElement(ElementName = "threads")]
        public List<TracedThread> TracedThreads
        {
            get { return new List<TracedThread>(new SortedDictionary<int, TracedThread>(tracedThreads).Values); }
            private set { } 
        }

        public TracedThread AddThread(int id)
        {
            TracedThread tracedThread = new TracedThread(id);
            return tracedThreads.GetOrAdd(id, tracedThread);
        }

        public TracedThread GetThread(int id)
        {
            return tracedThreads[id];
        }

        public int GetCountOfThreads()
        {
            return tracedThreads.Count();
        }
    }
}
