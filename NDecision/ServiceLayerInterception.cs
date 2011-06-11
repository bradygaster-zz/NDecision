using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDecision
{
    public class ServiceLayerInterception
    {
        public static void Run<T>(T target)
        {
            Container.Current.Resolve<ILogger>().Log("Setting Value");
        }
    }
}
