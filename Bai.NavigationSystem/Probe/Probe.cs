using System;
using System.Collections.Generic;
using System.Linq;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Probe
{
    public class Probe : IProbe
    {
        private readonly IDictionary<Orientation, Action> _forwardMoveDictionary;
        private readonly IDictionary<Orientation, Action> _leftMoveDictionary;
        private readonly IDictionary<Movement, Action> _movementMethodDictionary;
        private readonly IDictionary<Orientation, Action> _rightMoveDictionary;

        public Probe()
        {
            Position = new Point(0, 0);
            Orientation = Orientation.North;

            _movementMethodDictionary = new Dictionary<Movement, Action>
                {
                    {Movement.Left, () => _leftMoveDictionary[Orientation].Invoke()},
                    {Movement.Right, () => _rightMoveDictionary[Orientation].Invoke()},
                    {Movement.Forward, () => _forwardMoveDictionary[Orientation].Invoke()}
                };

            _leftMoveDictionary = new Dictionary<Orientation, Action>
                {
                    {Orientation.North, () => Orientation = Orientation.West},
                    {Orientation.East, () => Orientation = Orientation.North},
                    {Orientation.South, () => Orientation = Orientation.East},
                    {Orientation.West, () => Orientation = Orientation.South}
                };

            _rightMoveDictionary = new Dictionary<Orientation, Action>
                {
                    {Orientation.North, () => Orientation = Orientation.East},
                    {Orientation.East, () => Orientation = Orientation.South},
                    {Orientation.South, () => Orientation = Orientation.West},
                    {Orientation.West, () => Orientation = Orientation.North}
                };

            _forwardMoveDictionary = new Dictionary<Orientation, Action>
                {
                    {Orientation.North, () => { Position = new Point(Position.X, Position.Y + 1); }},
                    {Orientation.East, () => { Position = new Point(Position.X + 1, Position.Y); }},
                    {Orientation.South, () => { Position = new Point(Position.X, Position.Y - 1); }},
                    {Orientation.West, () => { Position = new Point(Position.X - 1, Position.Y); }}
                };
        }

        public Point Position { get; set; }
        public Orientation Orientation { get; set; }
        public string LaunchResult { get; set; }

        public void Move(IEnumerable<Movement> movements)
        {
            movements.ToList().ForEach(movement => _movementMethodDictionary[movement].Invoke());
        }
    }
}