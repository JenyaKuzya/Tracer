using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TracerLib;
using System.IO;

namespace TracerProject
{
    class Program
    {
        private static ITracer tracer = new Tracer();
        static void Main(string[] args)
        {
            tracer.StartTrace();

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 4; ++i)
            {
                Thread thread = new Thread(doSmth);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
                thread.Join();

            tracer.StopTrace();

            PrintTraceResult(tracer.GetTraceResult());
        }

        private static void PrintTraceResult(TraceResult traceResult)
        {
            ITraceResultSerializer serializer = new XMLTraceResultSerializer();
            ITraceResultWriter resultWriter = new ConsoleTraceResultWriter();
            resultWriter.WriteResult(traceResult, serializer);
            Console.ReadKey();
            resultWriter = new FileTraceResultWriter("traceResult.txt");
            resultWriter.WriteResult(traceResult, serializer);
        }

        static void doSmth()
        {
            tracer.StartTrace();

            var foo = new Foo(tracer);
            foo.MyMethod();

            tracer.StopTrace();
        }
    }

    public class Foo
    {
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(300);

            _tracer.StopTrace();
        }  
    }
}
