using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NDecision;
using NDecision.Aspects;

namespace Decision.Tests
{
	public class RobotAssemblyLine
	{
		[ApplySpecsAfterExecution]
		public void Attach(Robot robot)
		{
			// the attribute should do most of the relevant work
		}
	}

	public interface IRobotRecharger
	{
		void Recharge(Robot robot);
	}

	public class IHasRobotSpec : IHasSpec<Robot>
	{
		public Spec<Robot> GetSpec()
		{
			return Spec<Robot>
				.When(x => x.Charge <= 20)
				.Then((x) =>
				{
					Console.WriteLine("I need more power!");
					x.Recharger.Recharge(x);
				});
		}
	}

	public class Robot
	{
		public Robot(IRobotRecharger recharger)
		{
			this.Recharger = recharger;
		}

		public int Charge { get; set; }

		public IRobotRecharger Recharger { get; set; }

		[ApplySpecsBeforeExecution]
		public void Work()
		{
			this.Charge -= 5;
		}
	}
}
