using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TracerLib
{
    public class TracedThread
    {
        private List<TracedMethod> tracedMethods;

        public int ExecutionTime { get; }

        public TracedThread()
        {
            tracedMethods = new List<TracedMethod>();
        }

        public void AddMethod(TracedMethod method)
        {
            tracedMethods.Add(method);
        }
    }
}
