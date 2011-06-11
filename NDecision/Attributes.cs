using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using PostSharp.Aspects;

namespace NDecision
{
    [Serializable]
    public class RunSpecsOnSetAttribute
        : LocationInterceptionAspect
    {
        public override void OnSetValue(LocationInterceptionArgs args)
        {
            var target = args.Instance;

            var genericMethod =
                typeof(ServiceLayerInterception)
                    .GetMethod("Run")
                    .MakeGenericMethod(target.GetType())
                    .Invoke(target, new object[] { target });

            base.OnSetValue(args);
        }
    }
}
