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
        private ExpressionExt OptimizedExpression { get; }

        /// <summary>
        /// A list of consecutive steps represented as points
        /// </summary>
        private List<float[]> consecutivePointSeries = new List<float[]>();

        /// <summary>
        /// A list of consecutive steps represented as points
        /// </summary>
        public List<float[]> ConsecutivePointSeries { get => consecutivePointSeries; }

        /// <summary>
        /// Dimension of the considered problem.
        /// </summary>
        public int DimensionOfProblem { get; }

        /// <summary>
        /// A list of directions used in the algorithm.
        /// </summary>
        private List<float[]> DirectionSeries { get; set; } = new List<float[]>();

        /// <summary>
        /// Number of times the dimension base was changed.
        /// </summary>
        private int NumberOfSwappedDirections { get; set; }

        /// <summary>
        /// A container for global algorithm restrictions.
        /// </summary>
        private Restrictions AlgorithmRestrictions { get; }

        /// <summary>
        /// Class defining restrictions for the algorithm.
        /// </summary>
        public class Restrictions
        {
            /// <summary>
            /// The number of iterations.
            /// </summary>
            public int NumberOfAcceptableIterations { get; }

            /// <summary>
            /// The least permitted argument difference.
            /// </summary>
            public float MinimalArgumentDifference { get; }

            /// <summary>
            /// The least permitted function value difference.
            /// </summary>
            public float MinimalFunctionValueDifference { get; }

            /// <summary>
            /// The least step size permitted.
            /// </summary>
            public float MinimalStepSize { get; }

            /// <summary>
            /// The widest range at which the algorithm seeks the minimum.
            /// </summary>
            public float MaximalRangeWidth { get; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="numberOfAcceptableIterations">The number of iterations.</param>
            /// <param name="minimalArgumentDifference">The least permitted argument difference.</param>
            /// <param name="minimalFunctionValueDifference">The least permitted function value difference.</param>
            /// <param name="minimalStepSize">The least step size permitted.</param>
            /// <param name="maximalRangeWidth">The widest range at which the algorithm seeks the minimum.</param>
            public Restrictions(int numberOfAcceptableIterations, float minimalArgumentDifference, float minimalFunctionValueDifference, float minimalStepSize, float maximalRangeWidth)
            {
                // number of iterations must be at least 1.
                if (numberOfAcceptableIterations < 1)
                {
                    throw new ArgumentOutOfRangeException("numberOfAcceptableIterations", numberOfAcceptableIterations, "Number of iterations must be larger than 0.");
                }

                if (Math.Abs(minimalStepSize) < Math.Abs(minimalArgumentDifference))
                {
                    throw new ArgumentOutOfRangeException("MinimalStepSize", Math.Abs(minimalStepSize), "Minimal step size must be larger than minimal argument difference.");
                }

                if (Math.Abs(maximalRangeWidth) < Math.Abs(minimalStepSize))
                {
                    throw new ArgumentOutOfRangeException("MaximalRangeWidth", Math.Abs(maximalRangeWidth), "Maximal range width must be larger than minimal step size.");
                }

                NumberOfAcceptableIterations = numberOfAcceptableIterations;
                MinimalArgumentDifference = Math.Abs(minimalArgumentDifference);
                MinimalFunctionValueDifference = Math.Abs(minimalFunctionValueDifference);
                MinimalStepSize = Math.Abs(minimalStepSize);
                MaximalRangeWidth = Math.Abs(maximalRangeWidth);
            }
        }

        /// <summary>
        /// Enum defining the direction, towards which the minimum could be possibly found.
        /// </summary>
        private enum MinimumDirection
        {
            /// <summary>
            /// Minimum lies along the direction
            /// </summary>
            MinimumInDirection = 1,
            /// <summary>
            /// Minimum lies in opposite direction
            /// </summary>
            MinimumInOppositeDirection = -1,
            /// <summary>
            /// Minimum could not be found
            /// </summary>
            MinimumDirectionNotFound = 0
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dimensionOfProblem">Dimension of the considered problem</param>
        /// <param name="optimizedFunctionString">Analyzed function</param>
        /// <param name="restrictions">Algorithm restrictions</param>
        public Solver(int dimensionOfProblem, string optimizedFunctionString, Restrictions restrictions)
        {
            if (dimensionOfProblem < 1 || dimensionOfProblem > 10)
                throw new ArgumentOutOfRangeException("dimension", dimensionOfProblem, "Dimension value is out of range [1,10]");

            DimensionOfProblem = dimensionOfProblem;
            OptimizedExpression = new ExpressionExt(optimizedFunctionString, dimensionOfProblem);
            AlgorithmRestrictions = restrictions;
            NumberOfSwappedDirections = 0;

            FillDirectionSeriesWithCartesianCoordinateSystemBase();
        }

        /// <summary>
        /// Golden section search minimization in direction algorithm implementation.
        /// algorithm source: Stachurski, A., "Podstawy optymalizacji"
        /// </summary>
        /// <param name="leftOuterBound"></param>
        /// <param name="rightOuterBound"></param>
        /// <param name="startingPoint"></param>
        /// <param name="indexOfMinimizedPointCoordinate"></param>
        /// <returns></returns>
        private float FindMinimumInOneDirection(float leftOuterBound, float rightOuterBound, float[] startingPoint, int indexOfMinimizedPointCoordinate)
        {
            float goldenRationCoefficient = ((float)Math.Sqrt(5) - 1) / 2;

            float leftInnerBound = rightOuterBound - goldenRationCoefficient * (rightOuterBound - leftOuterBound);
            float rightInnerBound = leftOuterBound + goldenRationCoefficient * (rightOuterBound - leftOuterBound);

            // do until argument difference restriction reached
            while ((rightOuterBound - leftOuterBound) > AlgorithmRestrictions.MinimalArgumentDifference)
            {
                // calculate function values at inner points
                startingPoint[indexOfMinimizedPointCoordinate] = leftInnerBound;
                float leftInnerBoundFunctionValue = OptimizedExpression.Evaluate(startingPoint);
                startingPoint[indexOfMinimizedPointCoordinate] = rightInnerBound;
                float rightInnerBoundFunctionValue = OptimizedExpression.Evaluate(startingPoint);

                // compare function values at left and right inner point
                if (leftInnerBoundFunctionValue > rightInnerBoundFunctionValue)
                {
                    // minimum is within [left inner point, right bound]
                    leftOuterBound = leftInnerBound;
                    leftInnerBound = rightInnerBound;
                    rightInnerBound = leftOuterBound + goldenRationCoefficient * (rightOuterBound - leftOuterBound);
                }
                else
                {
                    // minimum is within [left bound, right inner point]
                    rightOuterBound = rightInnerBound;
                    rightInnerBound = leftInnerBound;
                    leftInnerBound = rightOuterBound - goldenRationCoefficient * (rightOuterBound - leftOuterBound);
                }
            }
            // estimated minimum is between left and right bound
            return (leftOuterBound + rightOuterBound) / 2;
        }

        /// <summary>
        /// Finds the minimal point value between two points.
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="lastPoint"></param>
        /// <returns></returns>
        private float[] FindMinimalValuePointBetweenTwoPoints(float[] firstPoint, float[] lastPoint)
        {
            float[] targetMinimalPoint = new float[DimensionOfProblem];
            for (int i = 0; i < DimensionOfProblem; i++)
            {
                if (Math.Abs(lastPoint[i] - firstPoint[i]) > AlgorithmRestrictions.MinimalArgumentDifference)
                {
                    targetMinimalPoint[i] = FindMinimumInOneDirection(Math.Min(firstPoint[i], lastPoint[i]), Math.Max(firstPoint[i], lastPoint[i]), firstPoint, i);
                }
                else
                {
                    targetMinimalPoint[i] = firstPoint[i];
                }
            }
            return targetMinimalPoint;
        }

        /// <summary>
        /// Checks if the minimal value point lies in given direction.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool IsMinimumInDirection(float[] startingPoint, float[] direction)
        {
            float[] temporaryStartingPoint = new float[DimensionOfProblem];
            startingPoint.CopyTo(temporaryStartingPoint, 0);

            for (int i = 0; i < DimensionOfProblem; i++)
            {
                if (Math.Abs(direction[i]) > AlgorithmRestrictions.MinimalFunctionValueDifference)
                {
                    temporaryStartingPoint[i] += AlgorithmRestrictions.MinimalStepSize * direction[i];
                }
            }

            return OptimizedExpression.Evaluate(startingPoint) > OptimizedExpression.Evaluate(temporaryStartingPoint);
        }

        /// <summary>
        /// Checks if the minimal value point lies in direction opposite to given.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="direction"></param>
        /// <param name="stepSize"></param>
        /// <returns></returns>
        private bool IsMinimumInOppositeDirection(float[] startingPoint, float[] direction)
        {
            float[] temporaryStartingPoint = new float[DimensionOfProblem];
            startingPoint.CopyTo(temporaryStartingPoint, 0);

            for (int i = 0; i < DimensionOfProblem; i++)
            {
                if (Math.Abs(direction[i]) > AlgorithmRestrictions.MinimalFunctionValueDifference)
                {
                    temporaryStartingPoint[i] -= AlgorithmRestrictions.MinimalStepSize * direction[i];
                }
            }

            return OptimizedExpression.Evaluate(startingPoint) > OptimizedExpression.Evaluate(temporaryStartingPoint);
        }

        /// <summary>
        /// Finds direction along provided direction vector, towards which the minimum lies.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="direction"></param>
        /// <returns>1 if in direction, -1 if in opposite direction, 0 otherwise</returns>
        private MinimumDirection FindDirectionTowardsMinimum(float[] startingPoint, float[] direction)
        {
            if (IsMinimumInDirection(startingPoint, direction))
            {
                return MinimumDirection.MinimumInDirection;
            }
            else if (IsMinimumInOppositeDirection(startingPoint, direction))
            {
                return MinimumDirection.MinimumInOppositeDirection;
            }
            else
            {
                return MinimumDirection.MinimumDirectionNotFound;
            }
        }

        /// <summary>
        /// Create an array of boolean values indicating whether a coordinate will be considered in further calculations.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        private bool[] FindConsideredCoordinatesArrayInDirectionArray(float[] direction)
        {
            // create a flag array, which determines which elements from direction array will be used
            bool[] consideredCoordinatesArray = new bool[DimensionOfProblem];
            for (int i = 0; i < consideredCoordinatesArray.Count(); i++)
            {
                if (Math.Abs(direction[i]) > AlgorithmRestrictions.MinimalFunctionValueDifference)
                {
                    consideredCoordinatesArray[i] = true;
                }
            }
            return consideredCoordinatesArray;
        }

        /// <summary>
        /// Finds the second boundary point of a unimodal range along direction.
        /// </summary>
        /// <param name="firstPoint"></param>
        /// <param name="direction"></param>
        /// <param name="directionTowardsMinimum"></param>
        /// <returns></returns>
        private float[] FindSecondPointOfUnimodalRangeAlongDirection(float[] firstPoint, float[] direction, int directionTowardsMinimum)
        {
            float[] secondPoint = new float[DimensionOfProblem];
            firstPoint.CopyTo(secondPoint, 0);

            // create a flag array, which determines which elements from direction array will be used
            bool[] consideredCoordinates = FindConsideredCoordinatesArrayInDirectionArray(direction);
            float differenceBetweenConsecutivePointsValue = 0;
            float previousPointValue = OptimizedExpression.Evaluate(firstPoint);
            bool maximumOffsetValueReached = false;

            do
            {
                previousPointValue = OptimizedExpression.Evaluate(secondPoint);

                for (int i = 0; i < DimensionOfProblem; i++)
                {
                    if (consideredCoordinates[i])
                    {
                        secondPoint[i] += directionTowardsMinimum * direction[i] * AlgorithmRestrictions.MinimalStepSize;

                        if (Math.Abs(secondPoint[i] - firstPoint[i]) > AlgorithmRestrictions.MaximalRangeWidth)
                        {
                            maximumOffsetValueReached = true;
                            break;
                        }
                    }
                }

                differenceBetweenConsecutivePointsValue = OptimizedExpression.Evaluate(secondPoint) - previousPointValue;

            } while (!maximumOffsetValueReached && !(differenceBetweenConsecutivePointsValue > 0 && Math.Abs(differenceBetweenConsecutivePointsValue) > AlgorithmRestrictions.MinimalFunctionValueDifference));

            return secondPoint;
        }

        /// <summary>
        /// Minimization in direction using the golden section search method.
        /// </summary>
        /// <param name="startingPoint"></param>
        /// <param name="directionOfOptimization"></param>
        /// <returns></returns>
        private float[] FindMinimalValuePointAlongDirection(float[] startingPoint, float[] directionOfOptimization)
        {
            MinimumDirection directionTowardsMinimum = FindDirectionTowardsMinimum(startingPoint, directionOfOptimization);

            // if direction towards minimum is 0, starting point is the minimum
            if (directionTowardsMinimum == MinimumDirection.MinimumDirectionNotFound)
            {
                return startingPoint;
            }

            float[] secondPoint = FindSecondPointOfUnimodalRangeAlongDirection(startingPoint, directionOfOptimization, (int)directionTowardsMinimum);
            return FindMinimalValuePointBetweenTwoPoints(startingPoint, secondPoint);
        }

        /// <summary>
        /// Generates the direction base as the cartesian coordinate system base.
        /// </summary>
        private void FillDirectionSeriesWithCartesianCoordinateSystemBase()
        {
            DirectionSeries.Clear();
            for (int i = 0; i < DimensionOfProblem; i++)
            {
                float[] direction = new float[DimensionOfProblem];   // by default, it is an array of 0s
                direction[i] = 1.0f;
                DirectionSeries.Add(direction);
            }
        }

        /// <summary>
        /// Changes the direction base.
        /// </summary>
        private void ChangeDirectionSeries()
        {
            if (NumberOfSwappedDirections == DimensionOfProblem)
            {
                FillDirectionSeriesWithCartesianCoordinateSystemBase();
                NumberOfSwappedDirections = 0;
            }
            else
            {
                // changing direction base
                float[] newDirection = new float[DimensionOfProblem];
                float[] pointFirst = ConsecutivePointSeries[ConsecutivePointSeries.Count - DimensionOfProblem - 1];
                float[] pointLast = ConsecutivePointSeries.Last();

                // determining the distance between the first and the last point
                float distanceBetweenFirstAndLastPoints = 0f;

                for (int j = 0; j < DimensionOfProblem; j++)
                {
                    distanceBetweenFirstAndLastPoints += (float)Math.Pow(pointLast[j] - pointFirst[j], 2.0);
                }
                distanceBetweenFirstAndLastPoints = (float)Math.Sqrt(distanceBetweenFirstAndLastPoints);

                // preparing new direction
                for (int j = 0; j < DimensionOfProblem; j++)
                {
                    newDirection[j] = (pointLast[j] - pointFirst[j]) / distanceBetweenFirstAndLastPoints;
                }

                // removing first direction and adding new one
                DirectionSeries.RemoveAt(0);
                DirectionSeries.Add(newDirection);
                NumberOfSwappedDirections++;
            }
        }

        /// <summary>
        /// Checks last two point changes for significant point distance change (above defined argument difference minimum).
        /// </summary>
        /// <returns></returns>
        private bool CheckLastThreePointsForSignificantDistanceChange()
        {
            int overallNumberOfPoints = ConsecutivePointSeries.Count;

            return !(overallNumberOfPoints > 2
                && FindDistanceBetweenTwoPoints(overallNumberOfPoints - 1, overallNumberOfPoints - 2) < AlgorithmRestrictions.MinimalArgumentDifference
                && FindDistanceBetweenTwoPoints(overallNumberOfPoints - 2, overallNumberOfPoints - 3) < AlgorithmRestrictions.MinimalArgumentDifference);
        }

        /// <summary>
        /// Checks last two point changes for significant value change (above defined value difference minimum).
        /// </summary>
        /// <returns></returns>
        private bool CheckLastThreePointsForSignificantValueChange()
        {
            int overallNumberOfPoints = ConsecutivePointSeries.Count;

            return !(overallNumberOfPoints > 2
                && FindValueDifferenceBetweenTwoPoints(overallNumberOfPoints - 1, overallNumberOfPoints - 2) < AlgorithmRestrictions.MinimalArgumentDifference
                && FindValueDifferenceBetweenTwoPoints(overallNumberOfPoints - 2, overallNumberOfPoints - 3) < AlgorithmRestrictions.MinimalArgumentDifference);
        }

        /// <summary>
        /// Checks if in last two point changes significant changes were made (to value or to coordinates).
        /// </summary>
        /// <returns></returns>
        private bool CheckLastTwoIterationsForSignificantChange()
        {
            return CheckLastThreePointsForSignificantDistanceChange() || CheckLastThreePointsForSignificantValueChange();
        }

        /// <summary>
        /// Finds the the distance between two most recent points.
        /// </summary>
        /// <returns></returns>
        private float FindDistanceBetweenTwoPoints(int indexOfFirstPoint, int indexOfSecondPoint)
        {
            float[] lastPointInSeries = ConsecutivePointSeries[indexOfFirstPoint];
            float[] lastButOnePointInSeries = ConsecutivePointSeries[indexOfSecondPoint];

            // determining the distance between the last two points
            double distanceBetweenMostRecentPoints = 0f;

            for (int j = 0; j < DimensionOfProblem; j++)
            {
                distanceBetweenMostRecentPoints += Math.Pow(lastPointInSeries[j] - lastButOnePointInSeries[j], 2.0);
            }
            return (float)Math.Sqrt(distanceBetweenMostRecentPoints);
        }

        /// <summary>
        /// Finds the the value difference between two most recent points.
        /// </summary>
        /// <returns></returns>
        private float FindValueDifferenceBetweenTwoPoints(int indexOfFirstPoint, int indexOfSecondPoint)
        {
            float[] lastPointInSeries = ConsecutivePointSeries[indexOfFirstPoint];
            float[] lastButOnePointInSeries = ConsecutivePointSeries[indexOfSecondPoint];

            return Math.Abs(OptimizedExpression.Evaluate(lastPointInSeries) - OptimizedExpression.Evaluate(lastButOnePointInSeries));
        }

        /// <summary>
        /// Finds the optimal point (the minimum) of the expression using Powell method, given the starting point.
        /// </summary>
        /// <param name="startingPoint">the starting point</param>
        /// <returns>true when optimum found, false otherwise</returns>
        public bool FindOptimalPoint(float[] startingPoint)
        {
            // in case the method is recalled, the list must be empty initially
            consecutivePointSeries.Clear();

            // first point of the algorithm
            consecutivePointSeries.Add(startingPoint);

            // current point in the algorithm iteration
            float[] currentPoint = new float[startingPoint.Count()];
            startingPoint.CopyTo(currentPoint, 0);

            if (Debug.IsDebugOn)
                PointHelper.ShowPointInfo(ConsecutivePointSeries.Last(), OptimizedExpression, "punkt poczatkowy");

            int iterationNumber = 1;

            while (iterationNumber < AlgorithmRestrictions.NumberOfAcceptableIterations)
            {
                float currentPointValue = -1f;
                float previousPointValue = OptimizedExpression.Evaluate(ConsecutivePointSeries.Last());
                float[] newPoint = new float[startingPoint.Count()];

                for (int i = 0; i < DimensionOfProblem && iterationNumber < AlgorithmRestrictions.NumberOfAcceptableIterations; i++)
                {
                    ConsecutivePointSeries.Last().CopyTo(newPoint, 0);
                    ConsecutivePointSeries.Add(FindMinimalValuePointAlongDirection(newPoint, DirectionSeries[i]));
                    iterationNumber++;

                    if (Debug.IsDebugOn)
                        PointHelper.ShowPointInfo(ConsecutivePointSeries.Last(), OptimizedExpression, "punkt");

                    float newCurrentPointValue = OptimizedExpression.Evaluate(ConsecutivePointSeries.Last());
                    float temporaryValueDifference = Math.Abs(newCurrentPointValue - previousPointValue);

                    currentPointValue = newCurrentPointValue;

                    if (!CheckLastTwoIterationsForSignificantChange())
                    {
                        return true;
                    }
                }
                ChangeDirectionSeries();
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
            if (DimensionOfProblem == 2)
            {
                // create an instance of graph renderer
                GraphRenderer renderer = new GraphRenderer(target, OptimizedExpression)
                {
                    RangeHorizontal = rangeHorizontal,
                    RangeVertical = rangeVertical,
                    IsolineCount = 20
                };

                // render the graph with isolines
                renderer.Render(true);

                // transform List<float[]> into PointF[] for 2D problem
                renderer.DrawPoints(ConsecutivePointSeries.Select(p => new PointF(p[0],p[1])).ToArray());
            }
        }
    }
}
