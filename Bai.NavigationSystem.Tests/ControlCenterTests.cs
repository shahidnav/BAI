using System.Collections.Generic;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.Report;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    public class ControlCenterTests
    {
        [TestFixture]
        public class ControlCenter_Constructor
        {
            [Test]
            public void Sets_MissionControl_on_CommandInvoker()
            {
                var mockMissionControl = new Mock<IMissionControlProxy>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var controlCenter = new ControlCenter(mockMissionControl.Object, null, null, null,
                                                      mockCommandInvoker.Object, null);

                mockCommandInvoker.Verify(i => i.SetMissionControl(mockMissionControl.Object), Times.Once());
            }

            [Test]
            public void Sets_Probe_on_CommandInvoker()
            {
                var mockProbe = new Mock<IProbe>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var controlCenter = new ControlCenter(null, null, mockProbe.Object, null, mockCommandInvoker.Object,
                                                      null);

                mockCommandInvoker.Verify(i => i.SetProbe(mockProbe.Object), Times.Once());
            }

            [Test]
            public void Sets_SearchArea_on_CommandInvoker()
            {
                var mockSearchArea = new Mock<ISearchArea>();
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var controlCenter = new ControlCenter(null, mockSearchArea.Object, null, null, mockCommandInvoker.Object,
                                                      null);

                mockCommandInvoker.Verify(i => i.SetSearchArea(mockSearchArea.Object), Times.Once());
            }
        }

        [TestFixture]
        public class ControlCenter_Execute
        {
            [Test]
            public void Invokes_all_commands()
            {
                var expectedCommandsList = new List<ICommand>();
                var mockCommandBuilder = new Mock<ICommandBuilder>();
                mockCommandBuilder.Setup(b => b.Build()).Returns(expectedCommandsList);
                var mockCommandInvoker = new Mock<ICommandInvoker>();
                var controlCenter = new ControlCenter(null, null, null, mockCommandBuilder.Object,
                                                      mockCommandInvoker.Object, null);
                controlCenter.Execute();

                mockCommandInvoker.Verify(m => m.Assign(expectedCommandsList), Times.Once());
                mockCommandInvoker.Verify(m => m.InvokeAll(), Times.Once());
            }
        }

        [TestFixture]
        public class ControlCenter_ProbeReport
        {
            [Test]
            public void Calls_ProbeReportBuilder_Execute()
            {
                const string expectedReport = "Probe report";
                var mockProbeReportBuilder = new Mock<IProbeReportBuilder>();
                mockProbeReportBuilder.Setup(r => r.Execute(It.IsAny<IProbe>())).Returns(expectedReport);
                var mockCommandInvoker = new Mock<ICommandInvoker>();

                var controlCenter = new ControlCenter(null, null, null, null, mockCommandInvoker.Object,
                                                      mockProbeReportBuilder.Object);
                string actualReport = controlCenter.ProbeReport();

                Assert.AreEqual(expectedReport, actualReport);
            }
        }
    }
}