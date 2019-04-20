using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Powell
{
    public class Solver
    {
        /// <summary>
        /// Analyzed function.
        /// </summary>
        private ExpressionExt Expression { get; }

        /// <summary>
        /// A list of consecutive steps represented as points
        /// </summary>
        private List<float[]> steps = new List<float[]>();

        /// <summary>
        /// A list of consecutive steps represented as points
        /// </summary>
        public List<float[]> Steps { get => steps; }

        /// <summary>
        /// Dimension of the considered problem.
        /// </summary>
        public int Dimension { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dimension">Dimension of the considered problem</param>
        /// <param name="function">Analyzed function</param>
        public Solver(int dimension, string function)
        {
            if (dimension < 1 || dimension > 10)
                throw new ArgumentOutOfRangeException("dimension", dimension, "Dimension value is out of range [1,10]");

            Dimension = dimension;
            Expression = new ExpressionExt(function, dimension);
        }

        public bool FindOptimalPoint(float[] startingPoint) // all parameters such as ending conditions should be stated here
        {
            // first point of the algorithm
            steps.Add(startingPoint);

            // implementation goes here

            return true;
        }

        /// <summary>
        /// Visualizes algorithm results.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="rangeHorizontal"></param>
        /// <param name="rangeVertical"></param>
        public void VisualizeResults(PictureBox target, Range rangeHorizontal, Range rangeVertical)
        {
            if (Dimension == 2)
            {
                GraphRenderer renderer = new GraphRenderer(target, Expression)
                {
                    RangeHorizontal = rangeHorizontal,
                    RangeVertical = rangeVertical,
                    IsolineCount = 10
                };
                renderer.Render(true);

                // transform List<float[]> into PointF[] for 2D problem
                renderer.DrawPoints(Steps.Select(p => new PointF(p[0],p[1])).ToArray());
            }
            else
                MessageBox.Show("Wizualizacja jest możliwa jedynie dla problemów 2-wymiarowych.", "Błąd wizualizacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
