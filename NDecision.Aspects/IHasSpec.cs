using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NDecision.Aspects
{
    public interface IHasSpec<T>
    {
        Spec<T> GetSpec();
    }
}
