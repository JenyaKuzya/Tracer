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
    public class Tracer : ITracer
    {
        private TraceResult traceResult;

        public Tracer()
        {
            traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace(1);
            StackFrame stackFrame = stackTrace.GetFrame(0);
            TracedMethod tracedMethod = new TracedMethod(stackFrame.GetMethod());

            TracedThread tracedThread = traceResult.AddThread(Thread.CurrentThread.ManagedThreadId);
            tracedThread.AddMethod(tracedMethod);
            tracedMethod.StartTrace();
        }

        public void StopTrace()
        {
            TracedThread tracedThread;
            int threadId = Thread.CurrentThread.ManagedThreadId;

            tracedThread = traceResult.GetThread(threadId);

            TracedMethod tracedMethod = tracedThread.GetMethod();
            tracedMethod.StopTrace();
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}
