using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections;
using System.Globalization;

namespace NDecision.Aspects
{
	[Serializable]
	public class ApplySpecsAfterExecutionAttribute
		: PostSharp.Aspects.OnMethodBoundaryAspect
	{
		public override void OnExit(PostSharp.Aspects.MethodExecutionArgs args)
		{
			if (args.Arguments.Any())
			{
				args.Arguments
					.ToList().ForEach(
						argument =>
						{
							var locator = new HasSpecLocator();
							var specs = typeof(HasSpecLocator)
								.GetMethod("Locate")
								.MakeGenericMethod(argument.GetType())
								.Invoke(locator, BindingFlags.Public, null, null, null);

							foreach (var iHasSpec in ((IEnumerable)specs))
							{
								var spec = typeof(IHasSpec<>)
									.MakeGenericType(argument.GetType())
									.GetMethod("GetSpec")
									.Invoke(iHasSpec, new object[] { });

								var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(argument.GetType()));
								typeof(List<>).MakeGenericType(argument.GetType())
									.GetMethod("Add")
									.Invoke(list, new object[] { argument });

								var arr = typeof(List<>).MakeGenericType(argument.GetType())
									.GetMethod("ToArray")
									.Invoke(list, new object[] { });

								typeof(Spec<>)
									.MakeGenericType(argument.GetType())
									.GetMethod("Run")
									.Invoke(spec, new object[] { arr });
							}
						});
			}
			else
			{
				var locator = new HasSpecLocator();
				var specs = typeof(HasSpecLocator)
					.GetMethod("Locate")
					.MakeGenericMethod(args.Instance.GetType())
					.Invoke(locator, BindingFlags.Public, null, null, null);

				foreach (var iHasSpec in ((IEnumerable)specs))
				{
					var spec = typeof(IHasSpec<>)
						.MakeGenericType(args.Instance.GetType())
						.GetMethod("GetSpec")
						.Invoke(iHasSpec, new object[] { });

					var list = Activator.CreateInstance(typeof(List<>).MakeGenericType(args.Instance.GetType()));
					typeof(List<>).MakeGenericType(args.Instance.GetType())
						.GetMethod("Add")
						.Invoke(list, new object[] { args.Instance });

					var arr = typeof(List<>).MakeGenericType(args.Instance.GetType())
						.GetMethod("ToArray")
						.Invoke(list, new object[] { });

					typeof(Spec<>)
						.MakeGenericType(args.Instance.GetType())
						.GetMethod("Run")
						.Invoke(spec, new object[] { arr });
				}
			}
		}
	}
}
