using System;

namespace NDecision
{
    public class When<T>
    {
        internal Func<T, bool> Given { get; set; }
        internal Action<T>[] Destination { get; set; }
        internal Spec<T> Spec { get; set; }

        public Spec<T> Then(params Action<T>[] destination)
        {
            this.Destination = destination;
            return this.Spec;
        }
    }
}