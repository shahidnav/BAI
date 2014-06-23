using System.Collections.Generic;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Probe
{
    public interface IProbe
    {
        Point Position { get; set; }
        Orientation Orientation { get; set; }
        string LaunchResult { get; set; }
        void Move(IEnumerable<Movement> movements);
    }
}
