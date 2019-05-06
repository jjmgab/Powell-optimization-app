﻿using System.Linq;
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

            Debug.DebugOn();

            // problem dimension
            const int dim = 2;

            // starting point
            float[] startingPoint = new float[dim] { -5f, -2f };

            // algorithm restrictions
            Solver.Restrictions restrictions = new Solver.Restrictions(100, 0.001f, 0.001f);

            // solver instance
            Solver solver = new Solver(dim, "x1^4 + x2^4 - 2*x1^2*x2 - 4*x1 + 3");

            // find optimal point
            if (solver.FindOptimalPoint(startingPoint, restrictions))
            {
                // visualize results if dim = 2
                solver.VisualizeResults(
                    pictureBoxGraph,
                    new Range(-6.0f, 6.0f),
                    new Range(-6.0f, 6.0f));

                // print optimum in messagebox
                string optimum = string.Format("[{0}]", string.Join(", ", solver.Steps.Last()));
                MessageBox.Show("Punkt optymalny:\r\n" + optimum, "Znaleziono punkt optymalny", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show("Nie znaleziono punktu optymalnego", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
    }
}
