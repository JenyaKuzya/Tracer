using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Collections.Generic;
using TracerLib;

namespace TracerUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        Tracer tracer = new Tracer();
        private int waitTime = 100;

        private void AnyMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
        }

        [TestMethod]
        public void TimeTest()
        {         
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
            long actualTime = tracer.GetTraceResult().GetThread(Thread.CurrentThread.ManagedThreadId).GetExecutionTime();
            Assert.IsTrue(actualTime >= waitTime);
        }

        [TestMethod]
        public void NameTest()
        {
            tracer.StartTrace();
            Thread.Sleep(waitTime);
            tracer.StopTrace();
            string actualClassName = tracer.GetTraceResult().GetThread(Thread.CurrentThread.ManagedThreadId).InnerMethods[0].ClassName;
            string actualMethodName = tracer.GetTraceResult().GetThread(Thread.CurrentThread.ManagedThreadId).InnerMethods[0].Name;
            Assert.AreEqual(actualMethodName, "NameTest");
            Assert.AreEqual(actualClassName, "UnitTest1");
        }

        [TestMethod]
        public void ThreadCountTest()
        {
            tracer.StartTrace();

            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 4; ++i)
            {
                var thread = new Thread(AnyMethod);
                threads.Add(thread);
                thread.Start();
            }

            foreach (Thread thread in threads)
            {
                thread.Join();
            }

            tracer.StopTrace();

            int actualCountOfThreads = tracer.GetTraceResult().GetCountOfThreads();
            Assert.AreEqual(actualCountOfThreads, 5);
        }

        [TestMethod]
        public void MethodCountTest()
        {
            tracer.StartTrace();

            AnyMethod();
            AnyMethod();
            AnyMethod();
            AnyMethod();

            tracer.StopTrace();

            int actualCountOfMethods = tracer.GetTraceResult().GetThread(Thread.CurrentThread.ManagedThreadId).InnerMethods[0].InnerMethods.Count;
            Assert.AreEqual(actualCountOfMethods, 4);
        }

        [TestMethod]
        public void CodeTest()
        {
            tracer.StartTrace();

            AnyMethod();

            tracer.StopTrace();

            Assert.IsTrue(ThereNoEqual(tracer.GetTraceResult().GetThread(Thread.CurrentThread.ManagedThreadId).InnerMethods));
        }

        private bool ThereNoEqual(List<TracedMethod> Methods)
        {
            for (int i = 0; i < Methods.Count - 1; i++)
            {
                for (int j = i; j < Methods.Count; j++)
                {
                    if (Methods[i] == Methods[j])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
