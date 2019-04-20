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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();

            // example R^2 equations:
            // -------------------------------------
            // x1*exp(-x1^2-x2^2)
            // (sin(x1)^2 + cos(x2)^2) / (5 + x1^2 + x2^2)
            // x1^4 + x2^4 - 2*x1^2*x2 - 4*x1 + 3
            // (x1-2)^2 + (x2-2)^2
            // -------------------------------------

            Solver solver = new Solver(2, "(x1-2)^2 + (x2-2)^2");
            solver.FindOptimalPoint(new float[] { 1.35f, -0.74f });
            solver.VisualizeResults(
                pictureBoxGraph,
                new Range(-2.0f, 6.0f),
                new Range(-2.0f, 6.0f));
        }
    }
}
