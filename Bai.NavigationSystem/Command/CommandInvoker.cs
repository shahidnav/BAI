using System;
using System.Collections.Generic;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public class CommandInvoker : ICommandInvoker
    {
        private readonly IDictionary<CommandType, Action<ICommand>> _setReceiversMethodDictionary;
        private IEnumerable<ICommand> _commandList;
        private IMissionControlProxy _missionControl;
        private IProbe _probe;
        private ISearchArea _searchArea;

        public CommandInvoker()
        {
            _setReceiversMethodDictionary = new Dictionary<CommandType, Action<ICommand>>
                {
                    {CommandType.SearchAreaSetSizeCommand, SetReceiversOnSearchAreaSetSizeCommand},
                    {CommandType.ProbeNavigateCommand, SetReceiversOnProbeNavigateCommand},
                    {CommandType.ProbeLaunchCommand, SetReceiversOnProbeLaunchCommand}
                };
        }

        public void SetSearchArea(ISearchArea aSearchArea)
        {
            _searchArea = aSearchArea;
        }

        public void SetProbe(IProbe aProbe)
        {
            _probe = aProbe;
        }

        public void SetMissionControl(IMissionControlProxy aMissionControl)
        {
            _missionControl = aMissionControl;
        }

        public void Assign(IEnumerable<ICommand> aCommandList)
        {
            _commandList = aCommandList;
        }

        public void InvokeAll()
        {
            foreach (ICommand command in _commandList)
            {
                SetReceivers(command);
                command.Execute();
            }
        }

        private void SetReceivers(ICommand command)
        {
            _setReceiversMethodDictionary[command.GetCommandType()]
                .Invoke(command);
        }

        private void SetReceiversOnSearchAreaSetSizeCommand(ICommand command)
        {
            var searchAreaSetSizeCommand = (ISearchAreaSetSizeCommand) command;
            searchAreaSetSizeCommand.SetReceiver(_searchArea);
        }

        private void SetReceiversOnProbeNavigateCommand(ICommand command)
        {
            var probeNavigateCommand = (IProbeNavigateCommand) command;
            probeNavigateCommand.SetReceiver(_missionControl, _probe);
        }

        private void SetReceiversOnProbeLaunchCommand(ICommand command)
        {
            var probeLaunchCommand = (IProbeLaunchCommand) command;
            probeLaunchCommand.SetReceiver(_missionControl, _probe, _searchArea);
        }
    }
}