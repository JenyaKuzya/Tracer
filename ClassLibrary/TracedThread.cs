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
        private Stack<TracedMethod> stack;

        public TracedThread()
        {
            tracedMethods = new List<TracedMethod>();
            stack = new Stack<TracedMethod>();
        }

        public void AddMethod(TracedMethod method)
        {
            tracedMethods.Add(method);

            if (stack.Count == 0)
            {
                tracedMethods.Add(method);
            }
            else
            {
                stack.Peek().AddNestedMethod(method);
            }

            stack.Push(method);
        }

        public TracedMethod GetMethod()
        {
            return stack.Pop();
        }

        public long GetExecutionTime()
        {
            return tracedMethods.Select(tracedMethod => tracedMethod.ExecutionTime).Sum();
        }

        internal IEnumerable<TracedMethod> TracedMethods => tracedMethods;
    }
}
