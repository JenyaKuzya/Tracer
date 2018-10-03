using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace TracerLib
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, TracedThread> tracedThreads;

        public TraceResult()
        {
            tracedThreads = new ConcurrentDictionary<int, TracedThread>();
        }

        public TracedThread AddThread(int id, TracedThread addedThread)
        {
            return tracedThreads.GetOrAdd(id, addedThread);
        }

        public TracedThread GetThread(int id)
        {
            return tracedThreads[id];
        }

        internal IEnumerable<KeyValuePair<int, TracedThread>> TracedThreads => tracedThreads;
    }
}
