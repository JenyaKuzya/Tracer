using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace TracerLib
{
    [DataContract(Name = "thread")]
    public class TracedThread
    {
        private List<TracedMethod> tracedMethods;
        private Stack<TracedMethod> stackOfMethods;

        [DataMember(Name = "id", Order = 0)]
        [XmlElement(ElementName = "id")]
        public int ThreadID { get; set; }

        [DataMember(Name = "time", Order = 1)]
        [XmlElement(ElementName = "time")]
        public string TimeWithPostfix
        {
            get { return GetExecutionTime().ToString() + "ms"; }
            set { }
        }

        [DataMember(Name = "methods", Order = 2)]
        [XmlElement(ElementName = "methods")]
        public List<TracedMethod> InnerMethods
        {
            get { return tracedMethods; }
            set { }
        }

        public TracedThread(int id)
        {
            tracedMethods = new List<TracedMethod>();
            stackOfMethods = new Stack<TracedMethod>();
            ThreadID = id;
        }

        public TracedThread()
        {    }

        public void AddMethod(TracedMethod method)
        {
            tracedMethods.Add(method);

            if (stackOfMethods.Count == 0)
            {
                tracedMethods.Add(method);
            }
            else
            {
                stackOfMethods.Peek().AddNestedMethod(method);
            }

            stackOfMethods.Push(method);
        }

        public TracedMethod GetMethod()
        {
            return stackOfMethods.Pop();
        }

        public long GetExecutionTime()
        {
            return tracedMethods.Select(tracedMethod => tracedMethod.ExecutionTime).Sum();
        }
    }
}
