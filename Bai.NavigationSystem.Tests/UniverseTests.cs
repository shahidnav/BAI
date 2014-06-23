using Bai.NavigationSystem.SearchArea;
using NUnit.Framework;

namespace Bai.NavigationSystem.Tests
{
    public class UniverseTests
    {
        [TestFixture]
        public class Universe_GetSize
        {
            [TestCase(10, 10)]
            [TestCase(0, 0)]
            [TestCase(9, 9)]
            public void Returns_the_size_that_has_been_set(int expectedWidth, int expectedLength)
            {
                var expectedSize = new Size(expectedWidth, expectedLength);
                var universe = new Universe();
                universe.SetSize(expectedSize);
                var actualSize = universe.GetSize();

                Assert.AreEqual(expectedSize, actualSize);
            }
        }

        [TestFixture]
        public class Universe_IsValidPoint
        {
            [TestCase(10, 10, 3, 8, true)]
            [TestCase(0, 0, 3, 8, false)]
            [TestCase(6, 6, 6, 6, false)]
            [TestCase(10, 10, 9, 9, true)]
            public void Returns_expected_result_for_given_point(int aWidth, int aLength, int positionX, int positionY, bool expectedResult)
            {
                var size = new Size(aWidth, aLength);
                var position = new Point(positionX, positionY);
                var universe = new Universe();
                universe.SetSize(size);

                var actualResult = universe.IsValidPoint(position);
                Assert.AreEqual(expectedResult, actualResult);
            }
        }
    }
}
