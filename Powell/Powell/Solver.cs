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
        /// Golden Ratio minimization in direction algorithm implementation.
        /// algorithm source: Stachurski, A., "Podstawy optymalizacji"
        /// </summary>
        /// <param name="point"></param>
        /// <param name="direction"></param>
        /// <param name="argumentDiffMin"></param>
        /// <param name="argumentDiffExceeded"></param>
        /// <returns></returns>
        private float[] MinimizeInDirection(float[] point, float[] direction, Restrictions restrictions, out bool argumentDiffExceeded)
        {
            // TODO: argumentDiffMin -> check the maximum difference between points

            Console.WriteLine("direction: {0}", GeneratePointString(direction));

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

            // initial range [a, b]
            float[] leftBound = new float[dim];
            float[] rightBound = new float[dim];

            for (int i = 0; i < dim; i++)
            {
                leftBound[i] = point[i];
                rightBound[i] = point[i];
                if (directionFlag[i])
                {
                    leftBound[i] -= 10.0f;
                    rightBound[i] += 10.0f;
                }
            }

            if (Debug.IsDebugOn)
            {
                ShowPointInfo(leftBound, "init leftBound");
                ShowPointInfo(rightBound, "init rightBound");
            }

            // golden ratio coefficient
            float tau = 0.618f;

            // inner points, within the [a,b] range
            // boundary points are assigned to include non-modified elements of the array
            float[] leftInner = new float[dim];
            float[] rightInner = new float[dim];

            leftBound.CopyTo(leftInner, 0);
            rightBound.CopyTo(rightInner, 0);

            // ai = a + (1 - tau)(b - a)
            // bi = a + tau(b - a)
            for (int i = 0; i < dim; i++)
            {
                if (directionFlag[i])
                {
                    leftInner[i] = leftBound[i] + (1 - tau) * (rightBound[i] - leftBound[i]);
                    rightInner[i] = leftBound[i] + tau * (rightBound[i] - leftBound[i]);
                }
            }

            if (Debug.IsDebugOn)
            {
                ShowPointInfo(leftBound, "leftBound");
                ShowPointInfo(rightBound, "rightBound");
                ShowPointInfo(leftInner, "leftInner");
                ShowPointInfo(rightInner, "rightInner");
            }

            float leftInnerValue = Expression.Evaluate(leftInner);
            float rightInnerValue = Expression.Evaluate(rightInner);
            int loopIndex = 0;

            while (loopIndex < restrictions.NumberOfIterations  )
                //&& Math.Abs(rightInnerValue - leftInnerValue) > restrictions.FunctionValueDifferenceError)
            {
                if (Debug.IsDebugOn)
                    Console.WriteLine(loopIndex.ToString());
                if (leftInnerValue > rightInnerValue)
                {
                    leftInner.CopyTo(leftBound, 0);
                    rightInner.CopyTo(leftInner, 0);

                    for (int i = 0; i < dim; i++)
                    {
                        if (directionFlag[i])
                        {
                            rightInner[i] = leftBound[i] + tau * (rightBound[i] - leftBound[i]);
                        }
                    }
                    rightInnerValue = Expression.Evaluate(rightInner);
                }
                else
                {
                    rightInner.CopyTo(rightBound, 0);
                    leftInner.CopyTo(rightInner, 0);

                    for (int i = 0; i < dim; i++)
                    {
                        if (directionFlag[i])
                        {
                            leftInner[i] = leftBound[i] + (1 - tau) * (rightBound[i] - leftBound[i]);
                        }
                    }
                    leftInnerValue = Expression.Evaluate(leftInner);
                }
                loopIndex++;
            }
            for (int i = 0; i < dim; i++)
            {
                if (directionFlag[i])
                {
                    targetPoint[i] = (rightBound[i] + leftBound[i]) / 2;
                }
            }

            argumentDiffExceeded = false;
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
                ShowPointInfo(point, "punkt poczatkowy");

            while (Steps.Count <= restrictions.NumberOfIterations)
            {
                float currentPointValue = -1f;
                float previousPointValue = Expression.Evaluate(Steps.Last());
                float previousValueDifference = 0f;
                float[] newPoint = new float[startingPoint.Count()];
                int directionIndex = -1;


                // find the direction with maximum function difference
                for (int i = 0; i < DirectionBase.Count; i++)
                {

                    Steps.Add(MinimizeInDirection(Steps.Last(), DirectionBase[i], restrictions, out bool argDiffExceeded));

                    // if the argument difference is negligible, skip the point
                    if (argDiffExceeded)
                        continue;

                    float temporaryPointValue = Expression.Evaluate(Steps.Last());
                    float temporaryValueDifference = Math.Abs(temporaryPointValue - previousPointValue);

                    currentPointValue = temporaryPointValue;
                    previousValueDifference = temporaryValueDifference;

                    // if the difference between last two function values is less than the restriction, end the algorithm
                    if (Math.Abs(currentPointValue - previousPointValue) < restrictions.FunctionValueDifferenceError)
                    {
                        Console.WriteLine("the difference between last two function values is less than the restriction");
                        return true;
                    }
                }


                //// find the direction with maximum function difference
                //for (int i = 0; i < DirectionBase.Count; i++)
                //{
                //    float[] temporaryPoint = MinimizeInDirection(point, DirectionBase[i], restrictions, out bool argDiffExceeded);
                //    Console.WriteLine(string.Format("point: {0}, temp: {1}", GeneratePointString(point), GeneratePointString(temporaryPoint)));
                //    if (Debug.IsDebugOn)
                //        ShowPointInfo(temporaryPoint, "punkt " + i.ToString());

                //    // if the argument difference is negligible, skip the point
                //    if (argDiffExceeded)
                //        continue;

                //    float temporaryPointValue = Expression.Evaluate(temporaryPoint);
                //    float temporaryValueDifference = Math.Abs(temporaryPointValue - previousPointValue);

                //    Console.WriteLine(string.Format("i={0}, temp val: {1}, prev val: {2}, steps: {3}, temp: {4}", i, temporaryPointValue, previousPointValue, Steps.Count, GeneratePointString(temporaryPoint)));

                //    // check for the largest value difference
                //    if (temporaryValueDifference > previousValueDifference)
                //    {
                //        currentPointValue = temporaryPointValue;
                //        previousValueDifference = temporaryValueDifference;
                //        directionIndex = i;
                //        temporaryPoint.CopyTo(newPoint, 0);
                //    }
                //}

                //// if no direction can provide a significant value change, end the algorithm
                //if (directionIndex == -1)
                //{
                //    Console.WriteLine("no direction can provide a significant value change");
                //    return true;
                //}
                //else
                //{
                //    // get found point
                //    newPoint.CopyTo(point, 0);

                //    // add the point
                //    steps.Add(point);

                //    // if the difference between last two function values is less than the restriction, end the algorithm
                //    if (Math.Abs(currentPointValue - previousPointValue) < restrictions.FunctionValueDifferenceError)
                //    {
                //        Console.WriteLine("the difference between last two function values is less than the restriction");
                //        return true;
                //    }
                //}  
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

        private string GeneratePointString(float[] point)
        {
            return string.Format("[{0}]", string.Join(", ", point));
        }

        private void ShowPointInfo(float[] point, string additionalInfo = "")
        {
            MessageBox.Show(additionalInfo + "\r\n" + GeneratePointString(point), "Point info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
