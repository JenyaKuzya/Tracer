﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace TracerLib
{
    public class TracedMethod
    {
        private Stopwatch stopwatch;
        private List<TracedMethod> tracedMethods;

        public string Name { get; }
        public string ClassName { get; }
        public int ExecutionTime { get; }

        public TracedMethod(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.Name;
            tracedMethods = new List<TracedMethod>();
            stopwatch = new Stopwatch();
        }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
        }
    }
}