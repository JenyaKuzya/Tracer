using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.Reflection;

namespace TracerLib
{
    class Tracer : ITracer
    {
        private TraceResult traceResult;

        public Tracer()
        {
            traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace(2);
            StackFrame stackFrame = stackTrace.GetFrame(0);
            TracedMethod tracedMethod = new TracedMethod(stackFrame.GetMethod());

            TracedThread tracedThread = traceResult.AddThread(Thread.CurrentThread.ManagedThreadId, new TracedThread());
            tracedThread.AddMethod(tracedMethod);
            tracedMethod.StartTrace();
        }

        public void StopTrace()
        {

        }

        public TraceResult GetTraceResult()
        {

        }
    }
}
