using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace NDecision.Aspects
{
	public class HasSpecLocator
	{
		public List<IHasSpec<T>> Locate<T>()
		{
			return (from assembly in AppDomain.CurrentDomain.GetAssemblies()
			        from type in assembly.GetTypes()
			        where type.GetInterfaces().Any(x => x.Equals(typeof (IHasSpec<T>)))
			        select Activator.CreateInstance(type)).OfType<IHasSpec<T>>().ToList();
		}
	}
}
