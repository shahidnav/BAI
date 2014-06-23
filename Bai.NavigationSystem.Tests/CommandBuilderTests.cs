using System;
using System.Collections.Generic;
using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class CommandBuilderTests
    {
        [Test]
        public void Build_returns_expected_command_list()
        {
            var mockSearchAreaSetSizeCommand = new Mock<ISearchAreaSetSizeCommand>();
            var mockProbeLaunchCommand = new Mock<ProbeLaunchCommand>();
            var mockProbeNavigateCommand = new Mock<ProbeNavigateCommand>();

            Func<Size, ISearchAreaSetSizeCommand> searchAreaSetSizeCommandFactory = size =>
                {
                    mockSearchAreaSetSizeCommand.Setup(x => x.Size).Returns(size);
                    return mockSearchAreaSetSizeCommand.Object;
                };
            Func<IProbeNavigateCommand> probeNavigateCommandFactory = () => mockProbeNavigateCommand.Object;
            Func<ProbeLaunchCommand> probeLaunchCommandFactory = () => mockProbeLaunchCommand.Object;

            var expectedCommandList = new List<ICommand>
                {
                    mockSearchAreaSetSizeCommand.Object,
                    mockProbeNavigateCommand.Object,
                    mockProbeLaunchCommand.Object
                };

            var commandBuilder = new CommandBuilder(searchAreaSetSizeCommandFactory, probeNavigateCommandFactory,
                                                    probeLaunchCommandFactory);

            var actualCommandList = commandBuilder.Build();
            Assert.AreEqual(expectedCommandList, actualCommandList);
        }
    }
}
