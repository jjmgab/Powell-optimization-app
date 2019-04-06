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
        private Argument[] Arguments { get; set; }
        private Expression AlgebraicExpression { get; }

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

        public double Evaluate(params double[] values)
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
            return AlgebraicExpression.calculate();
        }
    }
}
