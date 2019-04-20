using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Powell
{
    /// <summary>
    /// Class responsible for rendering the graph of ExpressionExt function on provided PictureBox.
    /// </summary>
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

        /// <summary>
        /// Number of points in row/column. Setting up too high value may result in severe performance problems.
        /// </summary>
        public int Points { get; set; } = 300;

        /// <summary>
        /// Numbers of isolines drawn.
        /// </summary>
        public int IsolineCount { get; set; } = 5;

        /// <summary>
        /// Target PictureBox, on which the graph will be rendered.
        /// </summary>
        private PictureBox TargetPictureBox { get; }

        /// <summary>
        /// Defines properties of axes of the graph.
        /// </summary>
        private Pen PenAxis { get; }

        /// <summary>
        /// Expression that gets rendered.
        /// </summary>
        private ExpressionExt AlgebraicExpression { get; }

        private Padding pad = new Padding(20, 0, 0, 20);

        /// <summary>
        /// Constructor. Initializes all starting parameters.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="rangeH"></param>
        /// <param name="rangeV"></param>
        public GraphRenderer(System.Windows.Forms.PictureBox target, ExpressionExt expression, Range rangeH, Range rangeV)
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
        /// Constructor. Initializes target PictureBox and Expression, leaving Range default.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="expression"></param>
        public GraphRenderer(System.Windows.Forms.PictureBox target, ExpressionExt expression)
        : this(target, expression, new Range(-5, 5), new Range(-5, 5)) { }


        /// <summary>
        /// Renders the graph.
        /// </summary>
        public void Render(bool drawIsolines)
        {
            DrawSurface(TargetPictureBox.Image, pad, drawIsolines);
            DrawAxis(TargetPictureBox.Image, pad, 5);
            
            // redraw graph
            TargetPictureBox.Invalidate();
        }

        /// <summary>
        /// Prepares the graph, by drawing axes.
        /// </summary>
        /// <param name="img"></param>
        private void DrawAxis(Image img, Padding pad, int scalePads)
        {
            using (Graphics g = Graphics.FromImage(img))
            {
                Pen pen = new Pen(Color.Black, 1);
                Brush brush = new SolidBrush(Color.Black);
                Font fontAxisLabel = new Font(FontFamily.GenericSerif, 15, GraphicsUnit.Pixel);
                Font fontScaleLabel = new Font(FontFamily.GenericSerif, 10, GraphicsUnit.Pixel);

                // draw axis lines
                g.DrawLine(pen, pad.Left, pad.Top, pad.Left, img.Height - pad.Bottom);
                g.DrawLine(pen, pad.Left, img.Height - pad.Bottom, img.Width - pad.Right, img.Height - pad.Bottom);

                // draw x1 and x2 labels
                g.DrawString("x1", fontAxisLabel, brush, img.Width - pad.Left, img.Height - pad.Bottom);
                g.DrawString("x2", fontAxisLabel, brush, 0, 5);

                // draw axis scale and values
                double horizontalStep = (double)(img.Width - pad.Left - pad.Right) / scalePads;
                double verticalStep = (double)(img.Height - pad.Top - pad.Bottom) / scalePads;
                double horizontalRangeStep = (RangeHorizontal.End - RangeHorizontal.Start) / scalePads;
                double verticalRangeStep = (RangeVertical.End - RangeVertical.Start) / scalePads;

                // drawing lines on horizontal axis
                for (int i = 0; i <= scalePads; i++)
                    g.DrawLine(pen, (float)(pad.Left + i * horizontalStep), img.Height - pad.Bottom, (float)(pad.Left + i * horizontalStep), img.Height - pad.Bottom + 3);
                
                // drawing values
                for (int i = 0; i < scalePads; i++)
                {
                    int labelPadding = (RangeHorizontal.Start + i * horizontalRangeStep) < 0 ? 10 : 7;
                    g.DrawString(((float)(RangeHorizontal.Start + i * horizontalRangeStep)).ToString(), fontScaleLabel, brush, (float)(pad.Left + i * horizontalStep - labelPadding), img.Height - pad.Bottom + 3);
                }

                // drawing lines on vertical axis
                for (int i = 0; i <= scalePads; i++)
                    g.DrawLine(pen, pad.Left, (float)(img.Height - pad.Bottom - i * verticalStep), pad.Left -3, (float)(img.Height - pad.Bottom - i * verticalStep));
                
                // applying transformation, in order to draw rotated text
                g.RotateTransform(-90);
                g.TranslateTransform(-img.Height, 0);
                
                // drawing values
                for (int i = 0; i < scalePads; i++)
                {
                    int labelPadding = (RangeVertical.Start + i * verticalRangeStep) < 0 ? 10 : 7;
                    g.DrawString(((float)(RangeVertical.Start + i * verticalRangeStep)).ToString(), fontScaleLabel, brush, (float)(pad.Bottom + i * verticalStep - labelPadding), pad.Left - 15);
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
                g.Clear(Color.White);

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
                        g.FillRectangle(brush, (float)(pad.Left + x * w), (float)(pad.Top + y * h), (float)w, (float)h);
                    }
                }

                if (isolines)
                {
                    int steps = IsolineCount;
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
                                            g.FillEllipse(new SolidBrush(Color.Black), (float)(pad.Left + x * w), (float)(pad.Top + y * h), 2, 2);
                                            stop = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Transforms value between min and max to a color.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private Color HeatMap(double value, double min, double max)
        {
            double val = (value - min) / (max - min);
            return Color.FromArgb(255, Convert.ToByte(255 * val), Convert.ToByte(255 * (1 - val)), 0);
        }

        /// <summary>
        /// Draws given points as a linked path.
        /// </summary>
        /// <param name="points"></param>
        public void DrawPoints(PointF[] points)
        {
            if (points.Count() < 1)
                return;

            Image image = TargetPictureBox.Image;

            using (Graphics g = Graphics.FromImage(image))
            {
                Pen penPath = new Pen(Color.Cornsilk, 2);
                Pen penPoint = new Pen(Color.Black, 2);

                // transform from function coord system to image coord system
                // (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
                // since y-coord is measured top-down, the range in_max/in_min has to be swapped

                PointF[] pointsTransformed = points.Select(p => new PointF(
                    (p.X - (float)RangeHorizontal.Start) * (image.Width - pad.Right - pad.Left) / (float)(RangeHorizontal.End - RangeHorizontal.Start) + pad.Left,
                    (p.Y - (float)RangeVertical.End) * (image.Height - pad.Top - pad.Bottom) / (float)(RangeVertical.Start - RangeVertical.End) + pad.Top
                    )).ToArray();

                // drawing path
                GraphicsPath graphicsPath = new GraphicsPath();
                graphicsPath.AddLines(pointsTransformed); 
                g.DrawPath(penPath, graphicsPath);

                // drawing points
                float pointRadius = 1f;
                foreach (PointF point in pointsTransformed)
                {
                    g.DrawEllipse(penPoint, point.X - pointRadius, point.Y - pointRadius, 2 * pointRadius, 2 * pointRadius);
                }
            }
        }
    }
}
