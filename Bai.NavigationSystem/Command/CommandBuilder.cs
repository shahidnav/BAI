using System;
using System.Collections.Generic;
using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public class CommandBuilder : ICommandBuilder
    {
        private readonly Func<IProbeLaunchCommand> _probeLaunchCommandFactory;
        private readonly Func<IProbeNavigateCommand> _probeNavigateCommandFactory;
        private readonly Func<Size, ISearchAreaSetSizeCommand> _searchAreaSetSizeCommandFactory;

        public CommandBuilder(Func<Size, ISearchAreaSetSizeCommand> aSearchAreaSetSizeCommandFactory,
                              Func<IProbeNavigateCommand> probeNavigateCommandFactory,
                              Func<IProbeLaunchCommand> probeLaunchCommandFactory)
        {
            _searchAreaSetSizeCommandFactory = aSearchAreaSetSizeCommandFactory;
            _probeNavigateCommandFactory = probeNavigateCommandFactory;
            _probeLaunchCommandFactory = probeLaunchCommandFactory;
        }

        public IEnumerable<ICommand> Build()
        {
            var commandList = new List<ICommand>
                {
                    _searchAreaSetSizeCommandFactory(new Size(10, 10)),
                    _probeNavigateCommandFactory(),
                    _probeLaunchCommandFactory()
                };

            return commandList;
        }
    }
}