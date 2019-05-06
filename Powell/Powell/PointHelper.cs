using System;

namespace Powell
{
    /// <summary>
    /// Helper class for providing point info.
    /// </summary>
    public static class PointHelper
    {
        /// <summary>
        /// Generates a string showing all point elements in format: [e1, e2, ..., en].
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public static string GeneratePointString(float[] point)
        {
            return string.Format("[{0}]", string.Join(", ", point));
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
