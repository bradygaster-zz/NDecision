using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;

namespace NDecision.Tests
{
    [TestFixture]
    public class RobotTests
    {
        [Test]
        public void can_property_setter_attribute_be_fired()
        {
            var mock = new Mock<ILogger>();
            Container.Current.RegisterInstance<ILogger>(mock.Object);
            new Robot().Charge = 20;
            mock.Verify(m => m.Log("Setting Value"));
        }
    }
}
