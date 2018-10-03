﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

            var threads = new List<Thread>();
            for (int i = 0; i < 4; ++i)
            {
                var thread = new Thread(doSmth);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
                thread.Join();

            tracer.StopTrace();

            PrintTraceResult(tracer.GetTraceResult());
        }

        static void doSmth()
        {
            tracer.StartTrace();

            var foo = new Foo(tracer);
            foo.MyMethod();

            tracer.StopTrace();
        }

        private static void PrintTraceResult(TraceResult traceResult)
        {
            var stream = new MemoryStream();
            ITraceResultSerializer traceResultSerializer;
            traceResultSerializer = new XMLTraceResultSerializer();
            traceResultSerializer.Serialize(stream, traceResult);
            var traceResultWriter = new ConsoleTraceResultWriter();

            try
            {
                traceResultWriter.Write(stream);
            }
            catch (Exception e)
            {
                Console.Write(e.Data);
            }
            finally
            {
                stream.Close();
                Console.ReadLine();
            }
        }
    }

    public class Foo
    {
        private Bar _bar;
        private ITracer _tracer;

        internal Foo(ITracer tracer)
        {
            _tracer = tracer;
            _bar = new Bar(_tracer);
        }

        public void MyMethod()
        {
            _tracer.StartTrace();

            Thread.Sleep(300);
            Recursion(3);

            _tracer.StopTrace();
        }

        public void Recursion(int num)
        {
            _tracer.StartTrace();

            if (num > 0)
            {
                Thread.Sleep(100);
                _bar.InnerMethod();
                Recursion(num - 1);
            }

            _tracer.StopTrace();
        }
    }

    public class Bar
    {
        private ITracer _tracer;

        internal Bar(ITracer tracer)
        {
            _tracer = tracer;
        }

        public void InnerMethod()
        {
            _tracer.StartTrace();
            Thread.Sleep(50);
            _tracer.StopTrace();
        }
    }
}
