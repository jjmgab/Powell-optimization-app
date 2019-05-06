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
        /// A list of directions used in the algorithm.
        /// </summary>
        private List<float[]> DirectionBase { get; set; } = new List<float[]>();

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

            // create a number of directions equal to the value of the dimension
            // it is assumed that a direction is a versor of the space
            for (int i = 0; i < Dimension; i++)
            {
                float[] direction = new float[Dimension];   // by default, it is an array of 0s
                direction[i] = 1.0f;
                DirectionBase.Add(direction);
            }
        }

        /// <summary>
        /// Golden section search minimization in direction algorithm implementation.
        /// algorithm source: Stachurski, A., "Podstawy optymalizacji"
        /// </summary>
        /// <param name="leftBound"></param>
        /// <param name="rightBound"></param>
        /// <param name="point"></param>
        /// <param name="index"></param>
        /// <param name="restrictions"></param>
        /// <returns></returns>
        float GoldenSectionMethod(float leftBound, float rightBound, float[] point, int index, Restrictions restrictions)
        {
            // golden ratio coefficient
            float tau = ((float)Math.Sqrt(5) - 1) / 2;

            // left and right inner point
            float leftInner = rightBound - tau * (rightBound - leftBound);
            float rightInner = leftBound + tau * (rightBound - leftBound);

            // do until argument difference restriction reached
            while ((rightBound - leftBound) > restrictions.ArgumentDifferenceError)
            {
                // calculate function values at inner points
                point[index] = leftInner;
                float leftInnerFunctionValue = Expression.Evaluate(point);
                point[index] = rightInner;
                float rightInnerFunctionValue = Expression.Evaluate(point);

                // compare function values at left and right inner point
                if (leftInnerFunctionValue > rightInnerFunctionValue)
                {
                    // minimum is within [left inner point, right bound]
                    leftBound = leftInner;
                    leftInner = rightInner;
                    rightInner = leftBound + tau * (rightBound - leftBound);
                }
                else
                {
                    // minimum is within [left bound, right inner point]
                    rightBound = rightInner;
                    rightInner = leftInner;
                    leftInner = rightBound - tau * (rightBound - leftBound);
                }
            }

            // estimated minimum is between left and right bound
            return (leftBound + rightBound) / 2;
        }

        /// <summary>
        /// Minimization in direction using the golden section search method.
        /// </summary>
        /// <param name="point"></param>
        /// <param name="direction"></param>
        /// <param name="argumentDiffMin"></param>
        /// <param name="argumentDiffExceeded"></param>
        /// <returns></returns>
        private float[] MinimizeInDirection(float[] point, float[] direction, Restrictions restrictions)
        {
            // considered problem dimension
            int dim = point.Count();

            float[] targetPoint = new float[dim];
            point.CopyTo(targetPoint, 0);

            // create a flag array, which determines which elements from direction array will be used
            bool[] directionFlag = new bool[direction.Count()];
            for (int i = 0; i < directionFlag.Count(); i++)
            {
                if (Math.Abs(direction[i]) > restrictions.FunctionValueDifferenceError)
                {
                    directionFlag[i] = true;
                }
            }

            for (int i = 0; i < dim; i++)
            {
                if (directionFlag[i])
                {
                    // TODO: how to determine a sufficient range??
                    targetPoint[i] = GoldenSectionMethod(point[i] - 5f, point[i] + 5f, point, i, restrictions);
                }
            }
            return targetPoint;
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

            // current point in the algorithm iteration
            float[] point = new float[startingPoint.Count()];
            startingPoint.CopyTo(point, 0);

            if (Debug.IsDebugOn)
                PointHelper.ShowPointInfo(Steps.Last(), Expression, "punkt poczatkowy");

            while (Steps.Count <= restrictions.NumberOfIterations)
            {
                float currentPointValue = -1f;
                float previousPointValue = Expression.Evaluate(Steps.Last());
                float previousValueDifference = 0f;
                float[] newPoint = new float[startingPoint.Count()];

                // find the direction with maximum function difference
                for (int i = 0; i < DirectionBase.Count; i++)
                {
                    Steps.Last().CopyTo(newPoint, 0);
                    Steps.Add(MinimizeInDirection(newPoint, DirectionBase[i], restrictions));

                    if (Debug.IsDebugOn)
                        PointHelper.ShowPointInfo(Steps.Last(), Expression, "punkt");

                    float temporaryPointValue = Expression.Evaluate(Steps.Last());
                    float temporaryValueDifference = Math.Abs(temporaryPointValue - previousPointValue);

                    currentPointValue = temporaryPointValue;
                    previousValueDifference = temporaryValueDifference;

                    // TODO: check for argument difference here!

                    // if the difference between last two function values is less than the restriction, end the algorithm
                    if (Math.Abs(currentPointValue - previousPointValue) < restrictions.FunctionValueDifferenceError)
                    {
                        if (Debug.IsDebugOn)
                            Console.WriteLine("the difference between last two function values is less than the restriction");
                        return true;
                    }
                }  
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
