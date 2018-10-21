using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [DataContract(Name = "method")]
    public class TracedMethod
    {
        private Stopwatch stopwatch;
        private List<TracedMethod> nestedMethods;

        [DataMember(Name = "name", Order = 0)]
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }

        [DataMember(Name = "class", Order = 1)]
        [XmlElement(ElementName = "class")]
        public string ClassName { get; set; }

        [XmlIgnore]
        public long ExecutionTime { get; set; }

        [DataMember(Name = "time", Order = 2)]
        [XmlElement(ElementName = "time")]
        public string TimeWithPostfix
        {
            get { return ExecutionTime.ToString() + "ms"; }
            set { }
        }

        [DataMember(Name = "methods", Order = 3)]
        [XmlElement(ElementName = "methods")]
        public List<TracedMethod> InnerMethods
        {
            get { return new List<TracedMethod>(nestedMethods); }
            set { }
        }

        public TracedMethod(MethodBase method)
        {
            Name = method.Name;
            ClassName = method.DeclaringType.Name;
            nestedMethods = new List<TracedMethod>();
            stopwatch = new Stopwatch();
        }

        public TracedMethod()
        {    }

        public void StartTrace()
        {
            stopwatch.Start();
        }

        public void StopTrace()
        {
            stopwatch.Stop();
            ExecutionTime = stopwatch.ElapsedMilliseconds;
        }

        public void AddNestedMethod(TracedMethod tracedMethod)
        {
            nestedMethods.Add(tracedMethod);
        }
    }
}
