using System;
using System.Collections.Generic;
using System.Linq;

namespace NDecision
{
    public class Spec<T>
    {
        static object _lock = new object();

        internal static Spec<T> Instance { get; set; }
        internal List<T> Targets { get; set; }

        static Spec()
        {
            lock (_lock)
            {
                Spec<T>.Instance = new Spec<T>();
            }
        }

        internal List<When<T>> Instructions { get; set; }

        internal Spec()
        {
            this.Instructions = new List<When<T>>();
            this.Targets = new List<T>();
        }

        public When<T> OrWhen(Func<T, bool> expression)
        {
            return When(expression);
        }

        public static When<T> When(Func<T, bool> expression)
        {
            lock (_lock)
            {
                if (!Spec<T>.Instance.Instructions.Any(x => x.Given.Equals(expression)))
                {
                    Spec<T>.Instance.Instructions.Add(
                        new When<T>
                        {
                            Given = expression,
                            Spec = Spec<T>.Instance
                        });
                }

                return Spec<T>.Instance.Instructions.First(x => x.Given.Equals(expression));
            }
        }

        public Spec<T> Run(params T[] targets)
        {
            Spec<T>.Instance.Targets.Clear();
            Spec<T>.Instance.Targets.AddRange(targets);

            this.Instructions.ForEach(instruction =>
            {
                Spec<T>.Instance.Targets
                    .Where(instruction.Given).ToList()
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

            return this;
        }
    }
}