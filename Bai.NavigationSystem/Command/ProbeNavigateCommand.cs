using System;
using System.Collections.Generic;
using System.Linq;
using Bai.NavigationSystem.BureauApi;
using Bai.NavigationSystem.Probe;

namespace Bai.NavigationSystem.Command
{
    public class ProbeNavigateCommand : IProbeNavigateCommand
    {
        private readonly IDictionary<string, Movement> _movementDictionary;
        private IMissionControlProxy _missionControl;
        private IProbe _probe;

        public ProbeNavigateCommand()
        {
            _movementDictionary = new Dictionary<string, Movement>
                {
                    {"LEFT", Movement.Left},
                    {"RIGHT", Movement.Right},
                    {"FORWARD", Movement.Forward}
                };
        }

        public CommandType GetCommandType()
        {
            return CommandType.ProbeNavigateCommand;
        }

        public void Execute()
        {
            List<string> directions = _missionControl.GetDirections();
            if (directions != null && directions.Count > 0)
            {
                _probe.Move(ParseDirections(directions));
            }
        }

        public void SetReceiver(IMissionControlProxy aMissionControl, IProbe aProbe)
        {
            _missionControl = aMissionControl;
            _probe = aProbe;
        }

        private IEnumerable<Movement> ParseDirections(IEnumerable<string> directions)
        {
            var movements = new List<Movement>();
            foreach (var direction in directions)
            {
                Movement value;
                if (_movementDictionary.TryGetValue(direction, out value))
                {
                    movements.Add(value);
                }
                else
                {
                    throw new ApplicationException(string.Format("Encountered an invalid direction: {0}", direction));
                }
            }

            return movements;
        }
    }
}