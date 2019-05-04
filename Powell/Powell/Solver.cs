using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
        /// Class defining restrictions for the algorithm.
        /// </summary>
        public class Restrictions
        {
            /// <summary>
            /// The number of iterations.
            /// </summary>
            public int NumberOfIterations { get; }

            /// <summary>
            /// The least permitted argument difference.
            /// </summary>
            public float ArgumentDifferenceError { get; }

            /// <summary>
            /// The least permitted function value difference.
            /// </summary>
            public float FunctionValueDifferenceError { get; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="numberOfIters">The number of iterations.</param>
            /// <param name="argDiffError">The least permitted argument difference.</param>
            /// <param name="funValDiffError">The least permitted function value difference.</param>
            public Restrictions(int numberOfIters, float argDiffError, float funValDiffError)
            {
                // number of iterations must be at least 1.
                if (numberOfIters < 1)
                    throw new ArgumentOutOfRangeException("NumberOfIterations", numberOfIters, "Number of iterations must be larger than 0.");

                NumberOfIterations = numberOfIters;
                ArgumentDifferenceError = Math.Abs(argDiffError);
                FunctionValueDifferenceError = Math.Abs(funValDiffError);
            }
        }

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

        private float GoldenRatio(float[] _startingPoint,int _direction)
        {
            float[] Point = _startingPoint;
            int direction = _direction;
            float a = -1000;
            float b = 1000;
            float tau = 0.618f;
            float alpha1 = a + (1 - tau) * (b - a);
            float alpha2 = a + (tau * (b - a));
            Point[direction] = alpha1;
            var fAlpha1 = Expression.Evaluate(Point);
            Point[direction] = alpha2;
            var fAlpha2 = Expression.Evaluate(Point);

            while (!(b - a < 0.001f))
            {
                if (fAlpha1 > fAlpha2)
                {
                    a = alpha1;
                    alpha1 = alpha2;
                    alpha2 = a + tau * (b - a);
                    Point[direction] = alpha2;
                    fAlpha2 = Expression.Evaluate(Point);
                }
                else
                {
                    b = alpha2;
                    alpha2 = alpha1;
                    alpha1 = a + (1 - tau) * (b - a);
                    Point[direction] = alpha1;
                    fAlpha1 = Expression.Evaluate(Point);
                }
            }
            return (a + b) / 2;
        }
        
        /// <summary>
        /// Finds the optimal point (the minimum) of the expression using Powell method, given the starting point.
        /// </summary>
        /// <param name="startingPoint">the starting point</param>
        /// <param name="restrictions">restrictions for the algorithm</param>
        /// <returns>true when optimum found, false otherwise</returns>
        public bool FindOptimalPoint(float[] startingPoint, Restrictions restrictions)
        {
            // in case the method is recalled, the Steps list must be empty initially
            steps.Clear();

            // check the starting point correctness
            if (!IsPointPropertiesCorrect(startingPoint))
                throw new ArgumentOutOfRangeException("startingPoint.Count", startingPoint.Count(), "Starting point dimension is invalid.");

            // first point of the algorithm
            steps.Add(startingPoint);

            double ExpressionValue = Expression.Evaluate(startingPoint);

            // implementation goes here
            throw new NotImplementedException("The algorithm is not implemented yet!");

            float[] Point = startingPoint;
            while (!(ExpressionValue < 0.001))
            {

                for(int i = 0; i < Dimension; i++)
                {
                    Point[i] = GoldenRatio(steps.Last(), i);
                }
                ExpressionValue = Expression.Evaluate(Point);
                steps.Add(Point);
            }
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
            // the visualization is possible only for dim = 2
            if (Dimension == 2)
            {
                // create an instance of graph renderer
                GraphRenderer renderer = new GraphRenderer(target, Expression)
                {
                    RangeHorizontal = rangeHorizontal,
                    RangeVertical = rangeVertical,
                    IsolineCount = 10
                };

                // render the graph with isolines
                renderer.Render(true);

                // transform List<float[]> into PointF[] for 2D problem
                renderer.DrawPoints(Steps.Select(p => new PointF(p[0],p[1])).ToArray());
            }
            else
                MessageBox.Show("Wizualizacja jest możliwa jedynie dla problemów 2-wymiarowych.", "Błąd wizualizacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Verifies if the point is correct in terms of algorithm parameters.
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        private bool IsPointPropertiesCorrect(float[] point)
        {
            return point.Count() == Dimension;
        }
    }
}
