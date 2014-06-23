using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;

namespace Bai.NavigationSystem.Command
{
    public interface IProbeNavigateCommand : ICommand
    {
        void SetReceiver(IMissionControlProxy aMissionControl, IProbe aProbe);
    }
}