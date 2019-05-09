using System;
using System.Linq;
using System.Windows.Forms;

namespace Powell
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Algorithm starting point.
        /// </summary>
        private float[] StartingPoint { get; set; }

        /// <summary>
        /// Main form constructor.
        /// </summary>
        public FormMain()
        {   
            InitializeComponent();

            // adding sample 2D expressions
            comboBoxInputExpression.Items.AddRange(new string[]
            {
                "x1*exp(-x1^2-x2^2)",
                "(sin(x1)^2 + cos(x2)^2) / (5 + x1^2 + x2^2)",
                "x1^4 + x2^4 - 2*x1^2*x2 - 4*x1 + 3",
                "(x1-2)^2 + (x2-2)^2",
                "(x1^2+x2-11)^2+(x1+x2^2-7)^2-200",
                "x1^4+x2^4-0.62*x1^2-0.62*x2^2",
            });

            Debug.DebugOn();

            // set initial starting point to [0;0]
            ChangeStartingPoint(new float[decimal.ToInt32(numericUpDownDimension.Value)]);
        }

        /// <summary>
        /// Start the algorithm; if the problem is 2-dimensional, visualize the results.
        /// </summary>
        /// <param name="dimension"></param>
        /// <param name="startingPoint"></param>
        /// <param name="restrictions"></param>
        /// <param name="expression"></param>
        /// <param name="horizontalRange"></param>
        /// <param name="verticalRange"></param>
        private void FindOptimum(int dimension, float[] startingPoint, Solver.Restrictions restrictions, string expression, Range horizontalRange, Range verticalRange)
        {
            // solver instance
            Solver solver = new Solver(dimension, expression);

            // find optimal point
            if (solver.FindOptimalPoint(startingPoint, restrictions))
            {
                // visualize results if dim = 2
                solver.VisualizeResults(
                    pictureBoxGraph,
                    horizontalRange,
                    verticalRange);

                // print optimum in messagebox
                string optimum = string.Format("[{0}]", string.Join(", ", solver.Steps.Last()));
                MessageBox.Show("Punkt optymalny:\r\n" + optimum, "Znaleziono punkt optymalny", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Nie znaleziono punktu optymalnego", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Change the algorithm starting point and show it in a label.
        /// </summary>
        /// <param name="point"></param>
        private void ChangeStartingPoint(float[] point)
        {
            StartingPoint = point;
            textBoxStartingPointValue.Text = PointHelper.GeneratePointString(StartingPoint);
        }

        /// <summary>
        /// Button to start the algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFindOptimum_Click(object sender, System.EventArgs e)
        {
            // problem dimension
            int dimension = decimal.ToInt32(numericUpDownDimension.Value);

            // algorithm restrictions
            Solver.Restrictions restrictions = new Solver.Restrictions(
                decimal.ToInt32(numericUpDownNumberOfIters.Value), 
                (float)numericUpDownArgDiff.Value,
                (float)numericUpDownFunValDiff.Value
                );

            // expression string
            string expression = comboBoxInputExpression.Text;

            // start algorithm
            FindOptimum(
                dimension,
                StartingPoint,
                restrictions,
                expression,
                new Range(-6f, 6f),
                new Range(-6f, 6f)
                );
        }

        /// <summary>
        /// Change the problem dimension.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownDimension_ValueChanged(object sender, EventArgs e)
        {
            ChangeStartingPoint(new float[decimal.ToInt32(numericUpDownDimension.Value)]);
        }

        /// <summary>
        /// Button to change the starting point.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonChangeStartingPoint_Click(object sender, EventArgs e)
        {
            // create an instance of a dialog to generate new point
            FormPoint formPoint = new FormPoint(decimal.ToInt32(numericUpDownDimension.Value));

            // generate new point or null (if canceled)
            float[] newPoint = formPoint.CreateNewPoint();

            // if the generated point is not null, swap it with current starting point
            if (newPoint != null)
                ChangeStartingPoint(newPoint);
        }
    }
}
