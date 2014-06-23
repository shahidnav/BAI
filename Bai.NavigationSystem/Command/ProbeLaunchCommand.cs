using System.Globalization;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public class ProbeLaunchCommand : IProbeLaunchCommand
    {
        private IMissionControlProxy _missionControl;
        private IProbe _probe;
        private ISearchArea _searchArea;

        public CommandType GetCommandType()
        {
            return CommandType.ProbeLaunchCommand;
        }

        public void Execute()
        {
            if (!_searchArea.IsValidPoint(_probe.Position))
            {
                _probe.LaunchResult =
                    string.Format(
                        "Launch aborted probe position is out of bounds: Probe position x-{0}, y-{1}, Search area width-{2}, length-{3}",
                        _probe.Position.X.ToString(CultureInfo.InvariantCulture),
                        _probe.Position.Y.ToString(CultureInfo.InvariantCulture),
                        _searchArea.GetSize().Width.ToString(CultureInfo.InvariantCulture),
                        _searchArea.GetSize().Length.ToString(CultureInfo.InvariantCulture));
                return;
            }

            string message = _missionControl.LaunchProbe(_probe.Position);
            _probe.LaunchResult = message;
        }

        public void SetReceiver(IMissionControlProxy aMissionControl, IProbe aProbe, ISearchArea aSearchArea)
        {
            _missionControl = aMissionControl;
            _probe = aProbe;
            _searchArea = aSearchArea;
        }
    }
}