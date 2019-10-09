using NUnit.Framework;
using Triangle_inequality.Lib;

namespace Triangle_inequality.Tests
{
    [TestFixture]
    public class TriangleTest
    {
        [Test]
        public void EquilateralTriangleTest()
        {
            Assert.IsTrue(Triangle.IsTriangle(2.5, 2.5, 2.5));
        }

        [Test]
        public void IsoscelesTriangleTest()
        {
            Assert.IsTrue(Triangle.IsTriangle(5, 7, 5));
        }

        [Test]
        public void OneNegativeLengthSideTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(-7, 27, 3.3));
        }

        [Test]
        public void SumEqualsSideLengthTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(3, 4, 7));
        }

        [Test]
        public void SumLessThanSideLengthTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(5, 20, 15));
        }

        [Test]
        public void SumMoreThanSideLengthTest()
        {
            Assert.IsTrue(Triangle.IsTriangle(10, 20, 25));
        }

        [Test]
        public void ThreeNegativeLengthSidesTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(-1, -2, -8));
        }

        [Test]
        public void ThreeZeroLengthSidesTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(0, 0, 0));
        }

        [Test]
        public void TwoNegativeLengthSidesTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(-17, 14, -16));
        }

        [Test]
        public void ZeroLengthSideTest()
        {
            Assert.IsFalse(Triangle.IsTriangle(3, 0, 2));
        }
    }
}