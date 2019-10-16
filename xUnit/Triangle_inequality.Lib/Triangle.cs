using System;

namespace Triangle_inequality.Lib
{
    public class Triangle
    {
        public static bool IsTriangle(double firstSide, double secondSide, double thirdSide)
        {
            if(firstSide < 0 || secondSide < 0 || thirdSide < 0)
            {
                throw new ArgumentException("One side of triangl has negative length.");
            }

            if(firstSide == 0 || secondSide == 0 || thirdSide == 0)
            {
                throw new ArgumentException("One side of triangl has zero length.");
            }

            if (Double.IsInfinity(firstSide) || Double.IsInfinity(secondSide) || Double.IsInfinity(thirdSide))
            {
                throw new OverflowException();
            }
            return (firstSide > 0 && secondSide > 0 && thirdSide > 0 && (firstSide + secondSide > thirdSide) && (firstSide + thirdSide > secondSide) && (secondSide + thirdSide > firstSide));
        }
    }
}
