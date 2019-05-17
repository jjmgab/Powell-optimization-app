using System;
using System.Linq;

namespace Powell
{
    /// <summary>
    /// Helper class for providing point info and basic operations.
    /// </summary>
    public static class PointHelper
    {
        /// <summary>
        /// Delegate defining a sum of two elements.
        /// </summary>
        public static Func<float, float, float> Sum { get; } = (x, y) => x + y;

        /// <summary>
        /// Delegate defining a difference of two elements.
        /// </summary>
        public static Func<float, float, float> Difference { get; } = (x, y) => x - y;

        /// <summary>
        /// Delegate defining an absolute difference of two elements.
        /// </summary>
        public static Func<float, float, float> AbsoluteDifference { get; } = (x, y) => Math.Abs(x - y);

        /// <summary>
        /// Delegate defining a squared difference of two elements.
        /// </summary>
        public static Func<float, float, float> DifferenceSquared { get; } = (x, y) => (float)Math.Pow(x - y, 2);

        /// <summary>
        /// Delegate defining a middle value of two elements.
        /// </summary>
        public static Func<float, float, float> MiddleValue { get; } = (x, y) => (x + y) / 2;

        /// <summary>
        /// A method applying a function over two float arrays.
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="secondPoint"></param>
        /// <param name="function"></param>
        /// <returns></returns>
        public static float[] TwoPointFunction(float[] firstPoint, float[] secondPoint, Func<float,float,float> function)
        {
            return firstPoint.Zip(secondPoint, function).ToArray();
        }

        /// <summary>
        /// A method multiplying a float array by a scalar.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static float[] MultiplyPointByScalar(float[] point, float scalar)
        {
            return point.Select(x => scalar * x).ToArray();
        }

        /// <summary>
        /// Generates a string showing all point elements in format: [e1, e2, ..., en].
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string GeneratePointString(float[] point)
        {
            return string.Format("[{0}]", string.Join("; ", point));
        }

        /// <summary>
        /// Writes point info to the console, providing optional additional information.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="expression"></param>
        /// <param name="additionalInfo"></param>
        public static void ShowPointInfo(float[] point, ExpressionExt expression, string additionalInfo = "")
        {
            Console.WriteLine(additionalInfo + " " + GeneratePointString(point) + ", val=" + expression.Evaluate(point));
        }
    }
}
