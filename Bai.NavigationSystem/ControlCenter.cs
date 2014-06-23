using System.Collections.Generic;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.Report;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem
{
    public class ControlCenter : IControlCenter
    {
        private readonly ICommandBuilder _commandBuilder;
        private readonly ICommandInvoker _commandInvoker;
        private readonly IMissionControlProxy _missionControl;
        private readonly IProbe _probe;
        private readonly IProbeReportBuilder _probeReportBuilder;
        private readonly ISearchArea _searchArea;

        public ControlCenter(IMissionControlProxy aMissionControl, ISearchArea aSearchArea, IProbe aProbe,
                             ICommandBuilder aCommandBuilder,
                             ICommandInvoker aCommandInvoker, IProbeReportBuilder aProbeReportBuilder)
        {
            _missionControl = aMissionControl;
            _searchArea = aSearchArea;
            _probe = aProbe;
            _commandBuilder = aCommandBuilder;
            _commandInvoker = aCommandInvoker;
            _commandInvoker.SetMissionControl(_missionControl);
            _commandInvoker.SetSearchArea(_searchArea);
            _commandInvoker.SetProbe(_probe);
            _probeReportBuilder = aProbeReportBuilder;
        }

        public void Execute()
        {
            IEnumerable<ICommand> commandList = _commandBuilder.Build();
            _commandInvoker.Assign(commandList);
            _commandInvoker.InvokeAll();
        }

        public string ProbeReport()
        {
            return _probeReportBuilder.Execute(_probe);
        }
    }
}