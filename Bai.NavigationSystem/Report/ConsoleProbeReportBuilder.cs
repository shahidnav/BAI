using System.Text;
using Bai.NavigationSystem.Probe;

namespace Bai.NavigationSystem.Report
{
    public class ConsoleProbeReportBuilder : IProbeReportBuilder
    {
        public string Execute(IProbe aProbe)
        {
            var report = new StringBuilder();
            report.AppendLine("Probe Report");
            report.AppendLine("------------------");
            report.AppendFormat("Final coordinates: x - {0}, y - {1}", aProbe.Position.X, aProbe.Position.Y);
            report.AppendLine();
            report.AppendFormat("Launch result message: {0}", aProbe.LaunchResult);
            report.AppendLine();
            report.AppendLine("------------------");

            return report.ToString();
        }
    }
}