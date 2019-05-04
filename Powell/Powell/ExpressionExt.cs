using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using org.mariuszgromada.math.mxparser;

namespace Powell
{
    /// <summary>
    /// Extended version of mXparser Expression class.
    /// </summary>
    public class ExpressionExt
    {
        /// <summary>
        /// List of arguments, starting with x1.
        /// </summary>
        private Argument[] Arguments { get; set; }
        
        /// <summary>
        /// Algebraic expression.
        /// </summary>
        private Expression AlgebraicExpression { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="args"></param>
        public ExpressionExt(string function, int args)
        {
            // initialize arguments
            Arguments = new Argument[args];
            for (int i = 0; i < args; i++)
            {
                Arguments[i] = new Argument(string.Format("x{0}", i + 1));
            }
            
            // create expression
            AlgebraicExpression = new Expression(function, Arguments);
        }

        /// <summary>
        /// Evaluate expression, substituting provided values.
        /// The number of values equal to the number of arguments is used.
        /// When too few values is provided, the rest is substituted with 0.
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public float Evaluate(params float[] values)
        {
            for (int i = 0; i < Math.Min(values.Count(), Arguments.Count()); i++)
            {
                Arguments[i].setArgumentValue(values[i]);
            }
            // when there are less values than arguments, substitute with 0
            for (int i = 0; i < Arguments.Count() - values.Count(); i++)
            {
                Arguments[i].setArgumentValue(0.0);
            }
            return (float) AlgebraicExpression.calculate();
        }
    }
}
