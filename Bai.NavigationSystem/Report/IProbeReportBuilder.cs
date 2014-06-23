using Bai.NavigationSystem.Probe;

namespace Bai.NavigationSystem.Report
{
    public interface IProbeReportBuilder
    {
        string Execute(IProbe aProbe);
    }
}
