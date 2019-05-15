using System;
using System.Collections.Generic;
using System.Drawing;
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
        /// Consecutive algorithm steps.
        /// </summary>
        private List<float[]> Steps { get; set; }

        /// <summary>
        /// Minimized expression.
        /// </summary>
        private string Expression { get; set; }

        /// <summary>
        /// Main form constructor.
        /// </summary>
        public FormMain()
        {   
            InitializeComponent();

            // set the icon
            Icon = Properties.Resources.MainIcon;

            // set tooltips
            toolTipHints.SetToolTip(labelMinStepSize, Properties.Resources.TooltipMinStep);
            toolTipHints.SetToolTip(labelArgDiff, Properties.Resources.TooltipMinArgDiff);
            toolTipHints.SetToolTip(labelFunValDiff, Properties.Resources.TooltipMinFunValDiff);
            toolTipHints.SetToolTip(labelNumberOfIters, Properties.Resources.TooltipMaxIter);
            toolTipHints.SetToolTip(labelRangeWidth, Properties.Resources.TooltipRangeWidth);

            if (pictureBoxGraph.Image == null)
            {
                pictureBoxGraph.Image = new Bitmap(pictureBoxGraph.Width, pictureBoxGraph.Height);
            }

            // adding sample 2D expressions
            comboBoxInputExpression.Items.AddRange(new string[]
            {
                "(sin(x1)^2 + cos(x2)^2) / (5 + x1^2 + x2^2)",
                "(x1-2)^2+(x1-x2^2)^2",
                "x1^2 + (x1-x2)^2",
                "x1 * exp(-(x1^2 + x2^2))",
                "x1^4 + x2^4 - 2*x1^2*x2 - 4*x1 + 3",
                "(x1 - 2)^2 + (x2 - 2)^2",
                "(x1^2 + x2 - 11)^2 + (x1 + x2^2 - 7)^2 - 200",
                "x1^4 + x2^4 - 0.62*x1^2 - 0.62*x2^2",
                "-(1 + cos(12*sqrt(x1^2 + x2^2))) / (0.5*(x1^2 + x2^2) + 2)",
                "(x1 - x2 + x3)^2 + (-x1 + x2 + x3)^2 + (x1 + x2 - x3)^2"
            });

            // set initial starting point to [0;0]
            ChangeStartingPoint(new float[decimal.ToInt32(numericUpDownDimension.Value)]);

            groupBoxInput.Focus();
            comboBoxInputExpression.Select();
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
        private List<float[]> FindOptimum(int dimension, float[] startingPoint, Solver.Restrictions restrictions, string expression, Range horizontalRange, Range verticalRange)
        {
            // solver instance
            Solver solver = new Solver(dimension, expression, restrictions);

            // find optimal point
            bool pointFound = solver.FindOptimalPoint(startingPoint);

            // visualize results if dim = 2
            solver.VisualizeResults(
                pictureBoxGraph,
                horizontalRange,
                verticalRange);

            // print optimum in messagebox (if it was found)
            if (pointFound)
            {
                string optimum = string.Format("[{0}]", string.Join(", ", solver.ConsecutivePointSeries.Last()));
                MessageBox.Show(string.Format(Properties.Resources.MiscOptimum, optimum), Properties.Resources.InfoOptimumFound, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return solver.ConsecutivePointSeries;
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
            // necessary verifications
            // range
            if (numericUpDownRangeX1Lower.Value >= numericUpDownRangeX1Upper.Value || numericUpDownRangeX2Lower.Value >= numericUpDownRangeX2Upper.Value)
            {
                MessageBox.Show(Properties.Resources.ErrorRangeInvalid, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // expression empty
            if (comboBoxInputExpression.Text == "")
            {
                MessageBox.Show(Properties.Resources.ErrorExpressionEmpty, Properties.Resources.Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // problem dimension
            int dimension = decimal.ToInt32(numericUpDownDimension.Value);

            // algorithm restrictions
            Solver.Restrictions restrictions = new Solver.Restrictions(
                decimal.ToInt32(numericUpDownNumberOfIters.Value), 
                (float)numericUpDownArgDiff.Value,
                (float)numericUpDownFunValDiff.Value,
                (float)numericUpDownMinStepSize.Value,
                (float)numericUpDownRangeWidth.Value
                );

            // expression string
            Expression = comboBoxInputExpression.Text;

            // start algorithm
            Steps = FindOptimum(
                dimension,
                StartingPoint,
                restrictions,
                Expression,
                new Range((float)numericUpDownRangeX1Lower.Value, (float)numericUpDownRangeX1Upper.Value),
                new Range((float)numericUpDownRangeX2Lower.Value, (float)numericUpDownRangeX2Upper.Value)
                );

            buttonShowSteps.Enabled = true;
        }

        /// <summary>
        /// Change the problem dimension.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numericUpDownDimension_ValueChanged(object sender, EventArgs e)
        {
            // if dim = 2, we can edit graph params
            if (decimal.ToInt32(numericUpDownDimension.Value) == 2)
            {
                groupBoxGraphParams.Enabled = true;

                Image img = pictureBoxGraph.Image;
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(SystemColors.Control);
                }
            }
            else
            {
                groupBoxGraphParams.Enabled = false;

                // draw text with info (no graph for n > 2)
                Image img = pictureBoxGraph.Image;
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(SystemColors.Control);
                    g.DrawString(
                        Properties.Resources.InfoDrawingOnly2D, 
                        new Font(FontFamily.GenericMonospace, 10), 
                        new SolidBrush(Color.Red), 
                        new PointF(0.05f * img.Width, 0.05f * img.Height));
                }
            }

            pictureBoxGraph.Invalidate();

            // change the dim of the starting point
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
            FormPoint formPoint = new FormPoint(Properties.Resources.TitleChangeStartingPoint, decimal.ToInt32(numericUpDownDimension.Value));

            // generate new point or null (if canceled)
            float[] newPoint = formPoint.CreateNewPoint();

            // if the generated point is not null, swap it with current starting point
            if (newPoint != null)
                ChangeStartingPoint(newPoint);
        }

        /// <summary>
        /// After first successful algorithm carryout, one can view the consecutive steps.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonShowSteps_Click(object sender, EventArgs e)
        {
            FormPoint formPoint = new FormPoint(Properties.Resources.TitleSteps, Steps[0].Count(), new ExpressionExt(Expression, Steps[0].Count()), Steps);
            formPoint.ShowDialog();
        }
    }
}
