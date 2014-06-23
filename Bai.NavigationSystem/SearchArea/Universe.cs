namespace Bai.NavigationSystem.SearchArea
{
    public class Universe : ISearchArea
    {
        private Size Size { get; set; }

        public void SetSize(Size aSize)
        {
            Size = aSize;
        }

        public Size GetSize()
        {
            return Size;
        }

        public bool IsValidPoint(Point aPoint)
        {
            var isValidX = aPoint.X >= 0 && aPoint.X < Size.Width;
            var isValidY = aPoint.Y >= 0 && aPoint.Y < Size.Length;
            return isValidX && isValidY;
        }
    }
}