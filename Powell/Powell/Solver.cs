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
            float[] targetPoint = new float[Dimension];
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

            for (int i = 0; i < Dimension; i++)
            {
                if (directionFlag[i])
                {
                    // determining range for the minimum determination
                    float leftOffset = 0f;
                    float rightOffset = 0f;
                    float offsetStep = 0.1f;
                    float offsetMax = 100f;

                    float startingPointValue = Expression.Evaluate(point);

                    float[] helperPoint = new float[Dimension];
                    point.CopyTo(helperPoint, 0);

                    // left offset
                    // first check if the direction is valid
                    helperPoint[i] -= offsetStep;
                    if (startingPointValue > Expression.Evaluate(helperPoint))
                    {
                        helperPoint[i] = point[i];
                        float previousValue;
                        do
                        {
                            previousValue = Expression.Evaluate(helperPoint);
                            helperPoint[i] -= offsetStep;
                            leftOffset += offsetStep;
                        }
                        while (leftOffset < offsetMax && previousValue > Expression.Evaluate(helperPoint));
                    }

                    // back to the starting point, so the other direction can be verified
                    helperPoint[i] = point[i];

                    // right offset
                    // first check if the direction is valid
                    helperPoint[i] += offsetStep;
                    if (startingPointValue > Expression.Evaluate(helperPoint))
                    {
                        helperPoint[i] -= offsetStep;
                        float previousValue;
                        do
                        {
                            previousValue = Expression.Evaluate(helperPoint);
                            helperPoint[i] += offsetStep;
                            rightOffset += offsetStep;
                        }
                        while (rightOffset < offsetMax && previousValue > Expression.Evaluate(helperPoint));
                    }

                    targetPoint[i] = GoldenSectionMethod(point[i] - leftOffset, point[i] + rightOffset, point, i, restrictions);
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

            int iterationNumber = 0;

            while (iterationNumber < restrictions.NumberOfIterations)
            {
                float currentPointValue = -1f;
                float previousPointValue = Expression.Evaluate(Steps.Last());
                float previousValueDifference = 0f;
                float[] newPoint = new float[startingPoint.Count()];

                // find the direction with maximum function difference
                for (int i = 0; i < Dimension && iterationNumber < restrictions.NumberOfIterations; i++)
                {
                    Steps.Last().CopyTo(newPoint, 0);
                    Steps.Add(MinimizeInDirection(newPoint, DirectionBase[i], restrictions));
                    iterationNumber++;

                    if (Debug.IsDebugOn)
                        PointHelper.ShowPointInfo(Steps.Last(), Expression, "punkt");

                    float temporaryPointValue = Expression.Evaluate(Steps.Last());
                    float temporaryValueDifference = Math.Abs(temporaryPointValue - previousPointValue);

                    currentPointValue = temporaryPointValue;
                    previousValueDifference = temporaryValueDifference;

                    // CONDITION: checking for argument difference
                    float[] pointLast = Steps.Last();
                    float[] pointBeforeLast = Steps[Steps.Count - 2];

                    // determining the distance between the last two points
                    float distanceBetweenMostRecentPoints = 0f;

                    for (int j = 0; j < Dimension; j++)
                    {
                        distanceBetweenMostRecentPoints += (float)Math.Pow(pointLast[j] - pointBeforeLast[j], 2.0);
                    }
                    distanceBetweenMostRecentPoints = (float)Math.Sqrt(distanceBetweenMostRecentPoints);

                    if (distanceBetweenMostRecentPoints < restrictions.ArgumentDifferenceError)
                    {
                        if (Debug.IsDebugOn)
                            Console.WriteLine("the distance between last two points is less than the restriction");
                        // remove last step, because when the condition is fulfilled, last two points are the same
                        Steps.RemoveAt(Steps.Count - 1);
                        return true;
                    }
                }

                // changing direction base
                float[] newDirection = new float[Dimension];
                float[] pointFirst = Steps[0];
                float[] pointLastt = Steps.Last();

                // determining the distance between the first and the last point
                float distanceBetweenFirstAndLastPoints = 0f;

                for (int j = 0; j < Dimension; j++)
                {
                    distanceBetweenFirstAndLastPoints += (float)Math.Pow(pointLastt[j] - pointFirst[j], 2.0);
                }
                distanceBetweenFirstAndLastPoints = (float)Math.Sqrt(distanceBetweenFirstAndLastPoints);

                // preparing new direction
                for (int j = 0; j < Dimension; j++)
                {
                    newDirection[j] = (pointLastt[j] - pointFirst[j]) / distanceBetweenFirstAndLastPoints;
                }

                // removing first direction and adding new one
                DirectionBase.RemoveAt(0);
                DirectionBase.Add(newDirection);
            }

            // if the number of iterations was exceeded we can assume that no optimum was found
            MessageBox.Show(string.Format("{0}\r\n{1}", Properties.Resources.ErrorNumberofItersExceeded, Properties.Resources.ErrorOptimumNotFound), Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
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
