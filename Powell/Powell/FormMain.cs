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

            ExpressionExt expression = new ExpressionExt("(x1-2)^2 + (x2-2)^2", 2);
            GraphRenderer renderer = new GraphRenderer(pictureBoxGraph, expression);
            renderer.RangeHorizontal = new GraphRenderer.Range(-4, 4);
            renderer.RangeVertical = new GraphRenderer.Range(-4, 4);
            renderer.IsolineCount = 10;
            renderer.Render(true);
            renderer.DrawPoints(new PointF[] {
                new PointF(-1f, -0.73f),
                new PointF(-2.53f, 3.89f),
                new PointF(1.5f, 2.2f) });
        }
    }
}
