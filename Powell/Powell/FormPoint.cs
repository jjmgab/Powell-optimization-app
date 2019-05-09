using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Powell
{
    /// <summary>
    /// Class responsible for generating a new point.
    /// </summary>
    public partial class FormPoint : Form
    {
        /// <summary>
        /// Problem dimension.
        /// </summary>
        public int Dimension { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dimension"></param>
        public FormPoint(int dimension)
        {
            InitializeComponent();
            Dimension = dimension;
        }

        /// <summary>
        /// Shows the form as a dialog and returns the point generation result.
        /// </summary>
        /// <returns></returns>
        public float[] CreateNewPoint()
        {
            float[] newPoint = new float[Dimension];

            for (int i = 0; i < Dimension; i++)
                newPoint[i] = 1f;

            // if accept button was clicked, the point is returned, null otherwise
            if (ShowDialog() == DialogResult.OK)
                return newPoint;
            else
                return null;
        }

        /// <summary>
        /// Accept button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Cancel button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
