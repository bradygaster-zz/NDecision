using System;
using System.Linq;
using NUnit.Framework;
using NDecision;
using NDecision.Aspects;
using Moq;

namespace Decision.Tests
{
	[TestFixture]
	public class RobotDomainTests
	{
		[Test]
		public void spec_locator_can_find_specs()
		{
			var specs = new HasSpecLocator().Locate<Robot>();
			Assert.True(specs.Any());
		}

		[Test]
		public void robot_recharger_recharges_robot()
		{
			var specs = new HasSpecLocator().Locate<Robot>();
			var mockRecharger = new Mock<IRobotRecharger>();
			var robot = new Robot(mockRecharger.Object) { Charge = 10 };
			specs.ForEach(s => s.GetSpec().Run(robot));
			mockRecharger.Verify(x => x.Recharge(robot));
		}

		[Test]
		public void robot_recharger_automatically_recharges_robot_when_needed_to_perform_work()
		{
			var recharger = new Mock<IRobotRecharger>();
			var robot = new Robot(recharger.Object);
			robot.Work();
			recharger.Verify(x => x.Recharge(robot));
		}

		[Test]
		public void robot_line_charges_robots_upon_attachment()
		{
			var recharger = new Mock<IRobotRecharger>();
			var robot = new Robot(recharger.Object);
			new RobotAssemblyLine().Attach(robot);
			recharger.Verify(x => x.Recharge(robot));
		}
	}
}
