namespace Triangle_inequality.Lib
{
    public class Triangle
    {
        public static bool IsTriangle(double firstSide, double secondSide, double thirdSide) =>
            (firstSide > 0 && secondSide > 0 && thirdSide > 0 && (firstSide + secondSide > thirdSide) && (firstSide + thirdSide > secondSide) && (secondSide + thirdSide > firstSide));
    }
}
