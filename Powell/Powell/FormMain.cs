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

            ExpressionExt expression = new ExpressionExt("x1^2-x2^2", 2);
            GraphRenderer renderer = new GraphRenderer(pictureBoxGraph, new GraphRenderer.Range(-5, 5), new GraphRenderer.Range(-5,5), expression);
            renderer.Render();
        }
    }
}
