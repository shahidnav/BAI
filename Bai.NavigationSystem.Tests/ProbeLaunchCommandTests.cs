using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class ProbeLaunchCommandTests
    {
        [Test]
        public void Returns_the_correct_command_type()
        {
            var command = new ProbeLaunchCommand();
            Assert.AreEqual(CommandType.ProbeLaunchCommand, command.GetCommandType());
        }

        [Test]
        public void Calls_missioncontrol_LaunchProbe_and_sets_launch_result_on_execute_for_the_correct_receivers()
        {
            var mockMissionControl = new Mock<IMissionControlProxy>();
            var probe = new Probe.Probe {Position = new Point(3, 3)};
            var mockSearchArea = new Mock<ISearchArea>();
            var command = new ProbeLaunchCommand();
            mockSearchArea.Setup(m => m.IsValidPoint(It.IsAny<Point>())).Returns(true);
            mockMissionControl.Setup(m => m.LaunchProbe(It.IsAny<Point>())).Returns("Success");

            command.SetReceiver(mockMissionControl.Object, probe, mockSearchArea.Object);
            command.Execute();

            Assert.AreEqual("Success", probe.LaunchResult);
        }
        [Test]
        public void Doesnt_call_missioncontrol_LaunchProbe_and_sets_launch_result_on_execute_for_the_correct_receivers()
        {
            const string expectedResult = "Launch aborted probe position is out of bounds: Probe position x-3, y-3, Search area width-2, length-2";
            var mockMissionControl = new Mock<IMissionControlProxy>();
            var probe = new Probe.Probe {Position = new Point(3, 3)};
            var mockSearchArea = new Mock<ISearchArea>();
            var command = new ProbeLaunchCommand();
            mockSearchArea.Setup(m => m.GetSize()).Returns(new Size(2,2));
            mockSearchArea.Setup(m => m.IsValidPoint(It.IsAny<Point>())).Returns(false);
            mockMissionControl.Setup(m => m.LaunchProbe(It.IsAny<Point>())).Returns("Success");

            command.SetReceiver(mockMissionControl.Object, probe, mockSearchArea.Object);
            command.Execute();

            mockMissionControl.Verify(m => m.LaunchProbe(It.IsAny<Point>()), Times.Never());
            Assert.AreEqual(expectedResult, probe.LaunchResult);
        }
    }
}
