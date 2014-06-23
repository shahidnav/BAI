using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public class SearchAreaSetSizeCommand : ISearchAreaSetSizeCommand
    {
        private ISearchArea _searchArea;

        public SearchAreaSetSizeCommand(Size aSize)
        {
            Size = aSize;
        }

        public Size Size { get; private set; }

        public CommandType GetCommandType()
        {
            return CommandType.SearchAreaSetSizeCommand;
        }

        public void Execute()
        {
            _searchArea.SetSize(Size);
        }

        public void SetReceiver(ISearchArea aSearchArea)
        {
            _searchArea = aSearchArea;
        }
    }
}