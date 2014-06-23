using System.Collections.Generic;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.BureauApi
{
    public interface IMissionControlProxy
    {
        List<string> GetDirections();
        string LaunchProbe(Point aPoint);
    }
}