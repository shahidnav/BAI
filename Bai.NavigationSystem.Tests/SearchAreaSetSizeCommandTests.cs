using Bai.NavigationSystem.Command;
using Bai.NavigationSystem.SearchArea;
using Moq;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    [TestFixture]
    public class SearchAreaSetSizeCommandTests
    {
        [Test]
        public void Returns_the_correct_command_type()
        {
            var command = new SearchAreaSetSizeCommand(new Size(3, 3));
            Assert.AreEqual(CommandType.SearchAreaSetSizeCommand, command.GetCommandType());
        }

        [Test]
        public void Calls_SearchArea_setsize_on_execute_for_the_correct_receiver()
        {
            var expectedSize = new Size(3, 3);
            var mockSearchArea = new Mock<ISearchArea>();
            var command = new SearchAreaSetSizeCommand(expectedSize);
            command.SetReceiver(mockSearchArea.Object);

            command.Execute();

            mockSearchArea.Verify(m => m.SetSize(expectedSize), Times.Once());
        }
    }
}
