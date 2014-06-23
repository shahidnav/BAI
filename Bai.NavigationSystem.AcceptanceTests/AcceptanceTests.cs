using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;

namespace Bai.NavigationSystem.AcceptanceTests
{
    [TestFixture]
    public class AcceptanceTests
    {
        private IContainer _container;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Assembly mainAppAssembly = Assembly.GetAssembly(typeof (Program));

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(mainAppAssembly)
                   .AsImplementedInterfaces();

            // We will override the mission control registration to use our mock object
            var directions = new List<string>
                {
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD",
                    "LEFT",
                    "RIGHT",
                    "LEFT",
                    "LEFT",
                    "LEFT",
                    "RIGHT",
                    "FORWARD",
                    "LEFT",
                    "FORWARD",
                    "FORWARD",
                    "FORWARD"
                };

            var mockMissionControl = new Mock<IMissionControlProxy>();
            mockMissionControl.Setup(m => m.GetDirections()).Returns(directions);
            mockMissionControl.Setup(m => m.LaunchProbe(new Point(3, 8))).Returns("Success");
            mockMissionControl.Setup(m => m.LaunchProbe(It.IsNotIn(new Point(3, 8)))).Returns("Failed");

            builder.RegisterInstance(mockMissionControl.Object);

            _container = builder.Build();
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            _container.Dispose();
        }

        private string GetExpectedReportString()
        {
            var report = new StringBuilder();
            report.AppendLine("Probe Report");
            report.AppendLine("------------------");
            report.AppendFormat("Final coordinates: x - {0}, y - {1}", 3, 8);
            report.AppendLine();
            report.AppendFormat("Launch result message: {0}", "Success");
            report.AppendLine();
            report.AppendLine("------------------");

            return report.ToString();
        }

        [Test]
        public void Given_mocked_Api_configured_with_extracted_directions_and_results_produces_expected_report()
        {
            string expectedReport = GetExpectedReportString();
            var controlCenter = _container.Resolve<IControlCenter>();
            controlCenter.Execute();
            string actualReport = controlCenter.ProbeReport();

            Assert.AreEqual(expectedReport, actualReport);
        }
    }
}