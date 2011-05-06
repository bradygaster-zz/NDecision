using System;
using System.Collections.Generic;
using System.Linq;

namespace NDecision
{
    public class Handle<T>
    {
        static object _lock = new object();

        internal static Handle<T> Instance { get; set; }
        internal List<T> Targets { get; set; }

        static Handle()
        {
            lock (_lock)
            {
                Handle<T>.Instance = new Handle<T>();
            }
        }

        internal List<Instruction<T>> Instructions { get; set; }

        internal Handle()
        {
            this.Instructions = new List<Instruction<T>>();
            this.Targets = new List<T>();
        }

        public Instruction<T> ElseIf(Func<T, bool> expression)
        {
            return If(expression);
        }

        public static Instruction<T> If(Func<T, bool> expression)
        {
            lock (_lock)
            {
                if (!Handle<T>.Instance.Instructions.Any(x => x.Predicate.Equals(expression)))
                {
                    Handle<T>.Instance.Instructions.Add(
                        new Instruction<T>
                        {
                            Predicate = expression,
                            Container = Handle<T>.Instance
                        });
                }

                return Handle<T>.Instance.Instructions.First(x => x.Predicate.Equals(expression));
            }
        }

        public Handle<T> WithTargets(params T[] targets)
        {
            Handle<T>.Instance.Targets.AddRange(targets);
            return this;
        }

        public void Go()
        {
            this.Instructions.ForEach(instruction =>
                {
                    Handle<T>.Instance.Targets
                        .Where(instruction.Predicate).ToList()
                            .ForEach(target =>
                                {
                                    instruction
                                        .Destination.ToList()
                                            .ForEach(destination =>
                                                {
                                                    destination(target);
                                                });
                                });
                });
        }
    }
}