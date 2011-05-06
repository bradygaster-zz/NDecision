using System;

namespace NDecision
{
    public class Instruction<T>
    {
        internal Func<T, bool> Predicate { get; set; }
        internal Action<T>[] Destination { get; set; }
        internal Handle<T> Container { get; set; }

        public Handle<T> Do(params Action<T>[] destination)
        {
            this.Destination = destination;
            return this.Container;
        }
    }
}