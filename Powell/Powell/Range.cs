using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powell
{
    /// <summary>
    /// Defines range.
    /// </summary>
    public class Range
    {
        /// <summary>
        /// Starting value of the range.
        /// </summary>
        public double Start { get; }

        /// <summary>
        /// Final value of the range.
        /// </summary>
        public double End { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Range(double start, double end)
        {
            Start = start;
            End = end;
        }
    }
}
