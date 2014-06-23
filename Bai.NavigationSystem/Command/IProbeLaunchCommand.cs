using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public interface IProbeLaunchCommand : ICommand
    {
        void SetReceiver(IMissionControlProxy missionControl, IProbe  probe, ISearchArea searchArea);
    }
}