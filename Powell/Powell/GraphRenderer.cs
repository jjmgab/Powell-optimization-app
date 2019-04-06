using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Powell
{
    public class GraphRenderer
    {
        /// <summary>
        /// Defines horizontal range of the graph.
        /// </summary>
        public Range RangeHorizontal { get; set; }

        /// <summary>
        /// Defines vertical range of the graph.
        /// </summary>
        public Range RangeVertical { get; set; }

        /// <summary>
        /// Graph background color.
        /// </summary>
        public Color BackgroundColor { get; set; } = Color.White;

        public int Points { get; set; } = 300;

        /// <summary>
        /// Target PictureBox, on which the graph will be rendered.
        /// </summary>
        private System.Windows.Forms.PictureBox TargetPictureBox { get; }

        /// <summary>
        /// Defines properties of axes of the graph.
        /// </summary>
        private Pen PenAxis { get; }

        /// <summary>
        /// Expression that gets rendered.
        /// </summary>
        private ExpressionExt AlgebraicExpression { get; }

        /// <summary>
        /// Defines range.
        /// </summary>
        public class Range
        {
            /// <summary>
            /// Starting value of the range.
            /// </summary>
            public double Start { get; }

            /// <summary>
            /// Final value of the range.
            /// </summary>
            public double End { get; }

            /// <summary>
            /// Constructor.
            /// </summary>
            /// <param name="start"></param>
            /// <param name="end"></param>
            public Range(double start, double end)
            {
                Start = start;
                End = end;
            }
        }

        /// <summary>
        /// Constructor. Initializes all starting parameters.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="rangeH"></param>
        /// <param name="rangeV"></param>
        public GraphRenderer(System.Windows.Forms.PictureBox target, Range rangeH, Range rangeV, ExpressionExt expression)
        {
            TargetPictureBox = target;
            RangeHorizontal = rangeH;
            RangeVertical = rangeV;
            AlgebraicExpression = expression;

            if (TargetPictureBox.Image == null)
            {
                TargetPictureBox.Image = new Bitmap(TargetPictureBox.Width, TargetPictureBox.Height);
            }
        }

        /// <summary>
        /// Renders the graph.
        /// </summary>
        public void Render()
        {
            //DrawAxis(TargetPictureBox.Image);
            DrawSurface(TargetPictureBox.Image, new Padding(20,20,20,20), true);
            //DrawGrid(TargetPictureBox.Image);
            
            TargetPictureBox.Invalidate();
        }

        /// <summary>
        /// Prepares the graph, by clearing it and drawing axes.
        /// </summary>
        /// <param name="img"></param>
        private void DrawAxis(Image img)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                g.Clear(BackgroundColor);

                g.TranslateTransform(0, img.Height);

                g.DrawLine(new Pen(Color.Black), 20, -20, 20, -(img.Height - 20));
                g.DrawLine(new Pen(Color.Black), 20, -20, img.Width - 20, -20);

                //g.DrawString("123", new Font(FontFamily.GenericSerif, 10), new SolidBrush(Color.Black), 20, -20);

                //g.RotateTransform(-90);
                //g.TranslateTransform(-100, 0);
            }
        }

        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="img"></param>
        private void DrawGrid(Image img)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                int horizontalStep = (img.Width - 40) / 5;
                int verticalStep = (img.Height - 40) / 5;
                Pen p = new Pen(Color.Gray);
                
                for (int i = 0; i < 5; i++)
                {
                    g.DrawLine(p, 21, 20 + i * verticalStep, img.Width - 20, 20 + i * verticalStep);
                }
                for (int i = 1; i <= 5; i++)
                {
                    g.DrawLine(p, 20 + i * horizontalStep, 21, 20 + i * horizontalStep, img.Height - 20);
                }
            }
        }

        /// <summary>
        /// Draws heatmap with isolines.
        /// </summary>
        /// <param name="img"></param>
        /// <param name="pad"></param>
        /// <param name="isolines"></param>
        private void DrawSurface(Image img, Padding pad, bool isolines)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                // pixel size definition
                double w = 1.0 * (img.Width - pad.Left - pad.Right) / Points;
                double h = 1.0 * (img.Height - pad.Top - pad.Bottom) / Points;

                // value multiplier, used in expression evaluation
                double mulX = (RangeHorizontal.End - RangeHorizontal.Start) / Points;
                double mulY = (RangeVertical.End - RangeVertical.Start) / Points;

                // value matrix
                double[,] values = new double[Points, Points];

                // max and min values are used in both heatmap and isolines generation
                double max = 0, min = 0;

                // fill the value matrix
                for (int x = 0; x < Points; x++)
                {
                    for (int y = 0; y < Points; y++)
                    {
                        values[x, y] = AlgebraicExpression.Evaluate(RangeHorizontal.Start + x * mulX, RangeVertical.Start + y * mulY);

                        if (x == 0 && y == 0)
                        {
                            max = values[x, y];
                            min = values[x, y];
                        }
                        if (max < values[x, y])
                            max = values[x, y];
                        if (min > values[x, y])
                            min = values[x, y];
                    }
                }

                // draw the heatmap
                for (int x = 0; x < Points; x++)
                {
                    for (int y = 0; y < Points; y++)
                    {
                        SolidBrush brush = new SolidBrush(HeatMap(values[x, y], min, max));
                        g.FillRectangle(brush, (float)(pad.Left + x * w), (float)(y * h - pad.Bottom), (float)w, (float)h);
                    }
                }

                if (isolines)
                {
                    int steps = 9;
                    double step = (max - min) / steps;

                    for (int i = 1; i <= steps; i++)
                    {
                        // target list of points, containing the border
                        List<PointF> listOfPoints = new List<PointF>();
                        byte[,] matrix = new byte[Points, Points];
                        double margin = min + i * step;

                        // find all values that satisfy the marginal condition
                        for (int x = 0; x < Points; x++)
                        {
                            for (int y = 0; y < Points; y++)
                            {
                                if (values[x, y] < margin)
                                {
                                    matrix[x, y] = 1;
                                    // debug
                                    //g.FillEllipse(new SolidBrush(Color.Black), (float)(pad.Left + x * w), (float)(y * h - pad.Bottom), 3, 3);
                                }
                            }
                        }

                        for (int x = 0; x < Points; x++)
                        {
                            for (int y = 0; y < Points; y++)
                            {
                                bool stop = false;
                                for (int ix = Math.Max(x-1, 0); !stop && ix <= Math.Min(x+1, Points-1); ix++)
                                {
                                    for (int iy = Math.Max(y - 1, 0); !stop && iy <= Math.Min(y + 1, Points - 1); iy++)
                                    {
                                        if ((x != ix && y != iy) && matrix[x,y] == 1 && matrix[ix, iy] == 0)
                                        {
                                            g.FillEllipse(new SolidBrush(Color.Black), (float)(pad.Left + x * w), (float)(y * h - pad.Bottom), 2, 2);
                                            stop = true;
                                        }
                                    }
                                }
                            }
                        }

                        // TODO: finding and drawing the border

                        //// find border values (== has at least one neighboring 0)
                        //for (int x = 0; x < Points; x++)
                        //    for (int y = 0; y < Points; y++)
                        //    {
                        //        bool stop = false;
                        //        for (i = -1; !stop && i <= 1; i++)
                        //            for (int j = -1; !stop && j <= 1; j++)
                        //            {
                        //                bool notCenter = !(i == 0 && i == j);
                        //                bool xWithinRange = x + i >= 0 && x + i < Points;
                        //                bool yWithinRange = y + j >= 0 && y + j < Points;
                        //                if (notCenter && xWithinRange && yWithinRange && matrix[x + i, y + j] == 0)
                        //                {
                        //                    matrix[x, y] = 2;
                        //                    g.DrawEllipse(Pens.Black, (float)(pad.Left + x * w), (float)(y * h - pad.Bottom), 3, 3);
                        //                }
                        //            }
                        //    }

                        //for (int x = 0; x < Points; x++)
                        //    for (int y = 0; y < Points; y++)
                        //    {
                        //        bool found = false;

                        //        if (matrix[x,y] == 2)
                        //        {
                        //            found = true;

                        //        }
                        //    }
                    }
                }
                

            }
        }

        private Color HeatMap(double value, double min, double max)
        {
            double val = (value - min) / (max - min);
            return Color.FromArgb(255, Convert.ToByte(255 * val), Convert.ToByte(255 * (1 - val)), 0);
        }
    }
}
