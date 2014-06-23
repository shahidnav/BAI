namespace Bai.NavigationSystem.SearchArea
{
    public interface ISearchArea
    {
        void SetSize(Size aSize);
        Size GetSize();
        bool IsValidPoint(Point aPoint);
    }
}