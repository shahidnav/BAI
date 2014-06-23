using System.Collections.Generic;

namespace Bai.NavigationSystem.Command
{
    public interface ICommandBuilder
    {
        IEnumerable<ICommand> Build();
    }
}