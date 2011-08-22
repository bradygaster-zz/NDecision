using System;
using System.Collections.Generic;
using System.Linq;

namespace NDecision
{
    public class Spec<T>
    {
        //static object _lock = new object();

        //internal static Spec<T> Instance { get; set; }
        internal List<T> Targets { get; set; }
        /*
        static Spec()
        {
            lock (_lock)
            {
                Spec<T>.Instance = new Spec<T>();
            }
        }
        */
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
            Spec<T> ret = new Spec<T>();

            if (!ret.Instructions.Any(x => x.Given.Equals(expression)))
            {
                ret.Instructions.Add(
                    new When<T>
                    {
                        Given = expression,
                        Spec = ret
                    });
            }

            return ret.Instructions.First(x => x.Given.Equals(expression));
        }

        public Spec<T> Run(params T[] targets)
        {
            this.Targets.Clear();
            this.Targets.AddRange(targets);

            this.Instructions.ForEach(instruction =>
            {
                this.Targets
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