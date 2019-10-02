using NUnit.Framework;
using Triangle_inequality.Lib;

namespace Triangle_inequality.Tests
{
    [TestFixture]
    public class TriangleTest
    {
        [Test]
        public void EquilateralTriangle()
        {
            Assert.IsTrue(Triangle.IsTriangle(2.5, 2.5, 2.5));
        }


        [Test]
        public void IsoscelesTriangle()
        {
            Assert.IsTrue(Triangle.IsTriangle(5, 7, 5));
        }


        [Test]
        public void OneNegativeLengthSide()
        {
            Assert.IsFalse(Triangle.IsTriangle(-7, 27, 3.3));
        }


        [Test]
        public void SumEqualsSideLength()
        {
            Assert.IsFalse(Triangle.IsTriangle(3, 4, 7));
        }


        [Test]
        public void SumLessThanSideLength()
        {
            Assert.IsFalse(Triangle.IsTriangle(5, 20, 15));
        }

        [Test]
        public void SumMoreThanSideLength()
        {
            Assert.IsTrue(Triangle.IsTriangle(10, 20, 25));
        }


        [Test]
        public void ThreeNegativeLengthSides()
        {
            Assert.IsFalse(Triangle.IsTriangle(-1, -2, -8));
        }


        [Test]
        public void ThreeZeroLengthSides()
        {
            Assert.IsFalse(Triangle.IsTriangle(0, 0, 0));
        }


        [Test]
        public void TwoNegativeLengthSides()
        {
            Assert.IsFalse(Triangle.IsTriangle(-17, 14, -16));
        }


        [Test]
        public void ZeroLengthSide()
        {
            Assert.IsFalse(Triangle.IsTriangle(3, 0, 2));
        }
    }
}