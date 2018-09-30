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
            tracedThreads.GetOrAdd(id, addedThread);
        }
    }
}
