using System.Collections.Generic;
using Bai.NavigationSystem.Probe;
using Bai.NavigationSystem.SearchArea;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class ProbeTests
    {
        [TestCase(Movement.Forward, Movement.Forward, Movement.Forward, 0, 3)]
        [TestCase(Movement.Right, Movement.Forward, Movement.Forward, 2, 0)]
        [TestCase(Movement.Right, Movement.Left, Movement.Forward, 0, 1)]
        public void Given_movements_probe_moves_to_correct_position(Movement firstMove, Movement secondMove,
                                                                    Movement thirdMove, int expectedX, int expectedY)
        {
            var expectedPosition = new Point(expectedX, expectedY);
            var route = new List<Movement> {firstMove, secondMove, thirdMove};
            var probe = new Probe.Probe();

            probe.Move(route);

            Assert.AreEqual(expectedPosition, probe.Position);
        }
    }
}