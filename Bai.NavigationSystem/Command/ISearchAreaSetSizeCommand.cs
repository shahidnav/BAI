using Bai.NavigationSystem.SearchArea;

namespace Bai.NavigationSystem.Command
{
    public interface ISearchAreaSetSizeCommand : ICommand
    {
        Size Size { get; }
        void SetReceiver(ISearchArea aSearchArea);
    }
}