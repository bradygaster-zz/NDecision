using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace NDecision
{
    public class Container
    {
        public static Container Current { get; set; }

        static Container()
        {
            Container.Current = new Container();
        }

        UnityContainer UnityContainer { get; set; }

        public Container()
        {
            this.UnityContainer = new UnityContainer();
        }

        public void RegisterInstance<I>(I target)
        {
            this.UnityContainer.RegisterInstance<I>(target);
        }

        public T Resolve<T>()
        {
            return this.UnityContainer.Resolve<T>();
        }
    }
}