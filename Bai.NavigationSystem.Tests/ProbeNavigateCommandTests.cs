using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.Probe;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class ProbeNavigateCommandTests
    {
        [Test]
        public void Returns_the_correct_command_type()
        {
            var command = new ProbeNavigateCommand();
            Assert.AreEqual(CommandType.ProbeNavigateCommand, command.GetCommandType());
        }

        [TestCase("FORWARD", Movement.Forward)]
        [TestCase("LEFT", Movement.Left)]
        public void Execute_calls_missioncontrol_for_directions_and_then_calls_move_on_the_receiving_probe(string expectedDirection, Movement expectedMovement)
        {
            var mockMissionControl = new Mock<IMissionControlProxy>();
            mockMissionControl.Setup(m => m.GetDirections()).Returns(new List<string> { expectedDirection });
            var mockProbe = new Mock<IProbe>();
            var command = new ProbeNavigateCommand();

            command.SetReceiver(mockMissionControl.Object, mockProbe.Object);
            command.Execute();

            mockProbe.Verify(m => m.Move(new List<Movement> { expectedMovement }), Times.Once());
        }

        [TestCase("DoTheSplits", null)]
        public void Execute_calls_missioncontrol_for_directions_and_throws_exception(string expectedDirection, Movement expectedMovement)
        {
            var mockMissionControl = new Mock<IMissionControlProxy>();
            mockMissionControl.Setup(m => m.GetDirections()).Returns(new List<string> { expectedDirection });
            var mockProbe = new Mock<IProbe>();
            var command = new ProbeNavigateCommand();
            command.SetReceiver(mockMissionControl.Object, mockProbe.Object);

            Assert.Throws<ApplicationException>(command.Execute);
            mockProbe.Verify(m => m.Move(It.IsAny<List<Movement>>()), Times.Never());
        }
    }
}
