using System.Collections.Generic;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public interface ICommandInvoker
    {
        void SetSearchArea(ISearchArea aSearchArea);
        void SetProbe(IProbe aProbe);
        void SetMissionControl(IMissionControlProxy aMissionControl);
        void Assign(IEnumerable<ICommand> aCommandList);
        void InvokeAll();
    }
}