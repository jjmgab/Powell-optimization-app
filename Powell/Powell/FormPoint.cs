﻿using System;
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
        /// List of points.
        /// </summary>
        private List<float[]> Points { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="dimension"></param>
        /// <param name="points"></param>
        public FormPoint(string title, int dimension, List<float[]> points = null)
        {
            InitializeComponent();
            Dimension = dimension;

            Text = title;

            // add header
            for (int i = 1; i < Dimension + 1; i++)
            {
                dataGridViewCoordinates.Columns.Add($"x{i}", $"x{i}");
            }

            // add one row for the user to edit
            if (points == null)
            {
                dataGridViewCoordinates.ReadOnly = false;
                dataGridViewCoordinates.Rows.Add();
            }
            else // add all points provided
            {
                // set the datagridview to readonly
                dataGridViewCoordinates.ReadOnly = true;

                // cancel button is irrelevant, hide it
                buttonCancel.Visible = false;

                // move accept button as though there is no cancel button
                buttonOk.Location = buttonCancel.Location;

                foreach (float[] point in points)
                {
                    dataGridViewCoordinates.Rows.Add(point.Select(x=>x.ToString()).ToArray());
                }
            }
            

        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="dimension"></param>
        public FormPoint(string title, int dimension) 
            : this(title, dimension, null) { }

        /// <summary>
        /// Shows the form as a dialog and returns the point generation result.
        /// </summary>
        /// <returns></returns>
        public float[] CreateNewPoint()
        {
            // if there was a point list provided, this method returns null
            if (Points == null)
            {
                // if accept button was clicked, the point is returned, null otherwise
                if (ShowDialog() == DialogResult.OK)
                {
                    float[] newPoint = new float[Dimension];

                    for (int i = 0; i < Dimension; i++)
                    {
                        if (!float.TryParse((string)dataGridViewCoordinates.Rows[0].Cells[i].Value, out float value))
                        {
                            value = 0;
                        }
                        newPoint[i] = value;
                    }

                    return newPoint;
                }
            }
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