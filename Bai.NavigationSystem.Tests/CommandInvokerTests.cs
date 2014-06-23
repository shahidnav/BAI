using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class CommandInvokerTests
    {
        [Test]
        public void InvokeAll_on_ProbeLaunchCommand_sets_probe_missioncontrol_searcharea_as_receivers()
        {
            var mockProbe = new Mock<IProbe>();
            var mockMissionControl = new Mock<IMissionControlProxy>();
            var mockSearchArea = new Mock<ISearchArea>();
            var mockProbeLaunchCommand = new Mock<IProbeLaunchCommand>();
            mockProbeLaunchCommand.Setup(m => m.GetCommandType()).Returns(CommandType.ProbeLaunchCommand);

            var commandInvoker = new CommandInvoker();
            commandInvoker.Assign(new List<ICommand>() {mockProbeLaunchCommand.Object});
            commandInvoker.SetProbe(mockProbe.Object);
            commandInvoker.SetMissionControl(mockMissionControl.Object);
            commandInvoker.SetSearchArea(mockSearchArea.Object);

            commandInvoker.InvokeAll();

            mockProbeLaunchCommand.Verify(
                m => m.SetReceiver(mockMissionControl.Object, mockProbe.Object, mockSearchArea.Object), Times.Once());
        }
        
        [Test]
        public void InvokeAll_on_ProbeNavigateCommand_sets_probe_missioncontrol_as_receivers()
        {
            var mockProbe = new Mock<IProbe>();
            var mockMissionControl = new Mock<IMissionControlProxy>();
            var mockProbeNavigateCommand = new Mock<IProbeNavigateCommand>();
            mockProbeNavigateCommand.Setup(m => m.GetCommandType()).Returns(CommandType.ProbeNavigateCommand);

            var commandInvoker = new CommandInvoker();
            commandInvoker.Assign(new List<ICommand>() { mockProbeNavigateCommand.Object });
            commandInvoker.SetProbe(mockProbe.Object);
            commandInvoker.SetMissionControl(mockMissionControl.Object);

            commandInvoker.InvokeAll();

            mockProbeNavigateCommand.Verify(
                m => m.SetReceiver(mockMissionControl.Object, mockProbe.Object), Times.Once());
        }

        [Test]
        public void InvokeAll_on_SearchAreaSetSizeCommand_sets_SearchArea_as_receiver()
        {
            var mockSearchArea = new Mock<ISearchArea>();
            var mockSearchAreaSetSizeCommand = new Mock<ISearchAreaSetSizeCommand>();
            mockSearchAreaSetSizeCommand.Setup(m => m.GetCommandType()).Returns(CommandType.SearchAreaSetSizeCommand);

            var commandInvoker = new CommandInvoker();
            commandInvoker.Assign(new List<ICommand>() { mockSearchAreaSetSizeCommand.Object });
            commandInvoker.SetSearchArea(mockSearchArea.Object);

            commandInvoker.InvokeAll();

            mockSearchAreaSetSizeCommand.Verify(
                m => m.SetReceiver(mockSearchArea.Object), Times.Once());
        }

        [Test]
        public void InvokeAll_calls_execute_for_all_commands()
        {
            var mockProbeLaunchCommand = new Mock<IProbeLaunchCommand>();
            mockProbeLaunchCommand.Setup(m => m.GetCommandType()).Returns(CommandType.ProbeLaunchCommand);
            var mockProbeNavigateCommand = new Mock<IProbeNavigateCommand>();
            mockProbeNavigateCommand.Setup(m => m.GetCommandType()).Returns(CommandType.ProbeNavigateCommand);
            var mockSearchAreaSetSizeCommand = new Mock<ISearchAreaSetSizeCommand>();
            mockSearchAreaSetSizeCommand.Setup(m => m.GetCommandType()).Returns(CommandType.SearchAreaSetSizeCommand);

            var commandInvoker = new CommandInvoker();
            commandInvoker.Assign(new List<ICommand>() { mockProbeLaunchCommand.Object, mockProbeNavigateCommand.Object, mockSearchAreaSetSizeCommand.Object });

            commandInvoker.InvokeAll();

            mockProbeLaunchCommand.Verify(m => m.Execute(), Times.Once());
            mockProbeNavigateCommand.Verify(m => m.Execute(), Times.Once());
            mockSearchAreaSetSizeCommand.Verify(m => m.Execute(), Times.Once());
        }
    }
}
