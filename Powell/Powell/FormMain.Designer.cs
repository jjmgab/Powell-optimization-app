namespace Powell
{
    partial class FormMain
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBoxGraph = new System.Windows.Forms.PictureBox();
            this.labelExpressionInput = new System.Windows.Forms.Label();
            this.comboBoxInputExpression = new System.Windows.Forms.ComboBox();
            this.buttonFindOptimum = new System.Windows.Forms.Button();
            this.groupBoxRestrictions = new System.Windows.Forms.GroupBox();
            this.numericUpDownArgDiff = new System.Windows.Forms.NumericUpDown();
            this.labelEq6 = new System.Windows.Forms.Label();
            this.labelEq5 = new System.Windows.Forms.Label();
            this.labelEq4 = new System.Windows.Forms.Label();
            this.numericUpDownFunValDiff = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownNumberOfIters = new System.Windows.Forms.NumericUpDown();
            this.labelNumberOfIters = new System.Windows.Forms.Label();
            this.labelFunValDiff = new System.Windows.Forms.Label();
            this.labelArgDiff = new System.Windows.Forms.Label();
            this.groupBoxInput = new System.Windows.Forms.GroupBox();
            this.buttonChangeStartingPoint = new System.Windows.Forms.Button();
            this.textBoxStartingPointValue = new System.Windows.Forms.TextBox();
            this.labelEq3 = new System.Windows.Forms.Label();
            this.labelStartingPoint = new System.Windows.Forms.Label();
            this.labelEq2 = new System.Windows.Forms.Label();
            this.labelEq1 = new System.Windows.Forms.Label();
            this.numericUpDownDimension = new System.Windows.Forms.NumericUpDown();
            this.labelDimension = new System.Windows.Forms.Label();
            this.groupBoxGraphParams = new System.Windows.Forms.GroupBox();
            this.numericUpDownRangeX2Upper = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRangeX2Lower = new System.Windows.Forms.NumericUpDown();
            this.labelRangeX23 = new System.Windows.Forms.Label();
            this.labelRangeX22 = new System.Windows.Forms.Label();
            this.labelRangeX21 = new System.Windows.Forms.Label();
            this.labelRangeX13 = new System.Windows.Forms.Label();
            this.labelRangeX12 = new System.Windows.Forms.Label();
            this.labelRangeX11 = new System.Windows.Forms.Label();
            this.numericUpDownRangeX1Upper = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownRangeX1Lower = new System.Windows.Forms.NumericUpDown();
            this.buttonShowSteps = new System.Windows.Forms.Button();
            this.labelEq7 = new System.Windows.Forms.Label();
            this.numericUpDownMinStepSize = new System.Windows.Forms.NumericUpDown();
            this.labelMinStepSize = new System.Windows.Forms.Label();
            this.labelEq8 = new System.Windows.Forms.Label();
            this.numericUpDownRangeWidth = new System.Windows.Forms.NumericUpDown();
            this.labelRangeWidth = new System.Windows.Forms.Label();
            this.toolTipHints = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).BeginInit();
            this.groupBoxRestrictions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArgDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFunValDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfIters)).BeginInit();
            this.groupBoxInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDimension)).BeginInit();
            this.groupBoxGraphParams.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX2Upper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX2Lower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX1Upper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX1Lower)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStepSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGraph
            // 
            this.pictureBoxGraph.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBoxGraph.Location = new System.Drawing.Point(433, 12);
            this.pictureBoxGraph.Name = "pictureBoxGraph";
            this.pictureBoxGraph.Size = new System.Drawing.Size(471, 459);
            this.pictureBoxGraph.TabIndex = 0;
            this.pictureBoxGraph.TabStop = false;
            // 
            // labelExpressionInput
            // 
            this.labelExpressionInput.AutoSize = true;
            this.labelExpressionInput.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelExpressionInput.Location = new System.Drawing.Point(6, 22);
            this.labelExpressionInput.Name = "labelExpressionInput";
            this.labelExpressionInput.Size = new System.Drawing.Size(19, 13);
            this.labelExpressionInput.TabIndex = 2;
            this.labelExpressionInput.Text = "x0";
            // 
            // comboBoxInputExpression
            // 
            this.comboBoxInputExpression.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.comboBoxInputExpression.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBoxInputExpression.DropDownHeight = 150;
            this.comboBoxInputExpression.DropDownWidth = 370;
            this.comboBoxInputExpression.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.comboBoxInputExpression.FormattingEnabled = true;
            this.comboBoxInputExpression.IntegralHeight = false;
            this.comboBoxInputExpression.Location = new System.Drawing.Point(76, 19);
            this.comboBoxInputExpression.Name = "comboBoxInputExpression";
            this.comboBoxInputExpression.Size = new System.Drawing.Size(329, 21);
            this.comboBoxInputExpression.TabIndex = 3;
            // 
            // buttonFindOptimum
            // 
            this.buttonFindOptimum.Location = new System.Drawing.Point(12, 448);
            this.buttonFindOptimum.Name = "buttonFindOptimum";
            this.buttonFindOptimum.Size = new System.Drawing.Size(266, 23);
            this.buttonFindOptimum.TabIndex = 4;
            this.buttonFindOptimum.Text = "Start";
            this.buttonFindOptimum.UseVisualStyleBackColor = true;
            this.buttonFindOptimum.Click += new System.EventHandler(this.buttonFindOptimum_Click);
            // 
            // groupBoxRestrictions
            // 
            this.groupBoxRestrictions.Controls.Add(this.labelEq8);
            this.groupBoxRestrictions.Controls.Add(this.numericUpDownArgDiff);
            this.groupBoxRestrictions.Controls.Add(this.numericUpDownRangeWidth);
            this.groupBoxRestrictions.Controls.Add(this.labelEq6);
            this.groupBoxRestrictions.Controls.Add(this.labelRangeWidth);
            this.groupBoxRestrictions.Controls.Add(this.labelEq5);
            this.groupBoxRestrictions.Controls.Add(this.labelEq7);
            this.groupBoxRestrictions.Controls.Add(this.labelEq4);
            this.groupBoxRestrictions.Controls.Add(this.numericUpDownFunValDiff);
            this.groupBoxRestrictions.Controls.Add(this.numericUpDownMinStepSize);
            this.groupBoxRestrictions.Controls.Add(this.labelMinStepSize);
            this.groupBoxRestrictions.Controls.Add(this.numericUpDownNumberOfIters);
            this.groupBoxRestrictions.Controls.Add(this.labelNumberOfIters);
            this.groupBoxRestrictions.Controls.Add(this.labelFunValDiff);
            this.groupBoxRestrictions.Controls.Add(this.labelArgDiff);
            this.groupBoxRestrictions.Location = new System.Drawing.Point(12, 176);
            this.groupBoxRestrictions.Name = "groupBoxRestrictions";
            this.groupBoxRestrictions.Size = new System.Drawing.Size(411, 153);
            this.groupBoxRestrictions.TabIndex = 5;
            this.groupBoxRestrictions.TabStop = false;
            this.groupBoxRestrictions.Text = "Ograniczenia";
            // 
            // numericUpDownArgDiff
            // 
            this.numericUpDownArgDiff.DecimalPlaces = 4;
            this.numericUpDownArgDiff.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownArgDiff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownArgDiff.Location = new System.Drawing.Point(76, 45);
            this.numericUpDownArgDiff.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownArgDiff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownArgDiff.Name = "numericUpDownArgDiff";
            this.numericUpDownArgDiff.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownArgDiff.TabIndex = 14;
            this.numericUpDownArgDiff.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // labelEq6
            // 
            this.labelEq6.AutoSize = true;
            this.labelEq6.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq6.Location = new System.Drawing.Point(45, 74);
            this.labelEq6.Name = "labelEq6";
            this.labelEq6.Size = new System.Drawing.Size(13, 13);
            this.labelEq6.TabIndex = 13;
            this.labelEq6.Text = "=";
            // 
            // labelEq5
            // 
            this.labelEq5.AutoSize = true;
            this.labelEq5.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq5.Location = new System.Drawing.Point(45, 47);
            this.labelEq5.Name = "labelEq5";
            this.labelEq5.Size = new System.Drawing.Size(13, 13);
            this.labelEq5.TabIndex = 12;
            this.labelEq5.Text = "=";
            // 
            // labelEq4
            // 
            this.labelEq4.AutoSize = true;
            this.labelEq4.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq4.Location = new System.Drawing.Point(45, 22);
            this.labelEq4.Name = "labelEq4";
            this.labelEq4.Size = new System.Drawing.Size(13, 13);
            this.labelEq4.TabIndex = 11;
            this.labelEq4.Text = "=";
            // 
            // numericUpDownFunValDiff
            // 
            this.numericUpDownFunValDiff.DecimalPlaces = 4;
            this.numericUpDownFunValDiff.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownFunValDiff.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownFunValDiff.Location = new System.Drawing.Point(76, 71);
            this.numericUpDownFunValDiff.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownFunValDiff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            327680});
            this.numericUpDownFunValDiff.Name = "numericUpDownFunValDiff";
            this.numericUpDownFunValDiff.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownFunValDiff.TabIndex = 4;
            this.numericUpDownFunValDiff.Value = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            // 
            // numericUpDownNumberOfIters
            // 
            this.numericUpDownNumberOfIters.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownNumberOfIters.Location = new System.Drawing.Point(76, 19);
            this.numericUpDownNumberOfIters.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownNumberOfIters.Name = "numericUpDownNumberOfIters";
            this.numericUpDownNumberOfIters.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownNumberOfIters.TabIndex = 3;
            this.numericUpDownNumberOfIters.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // labelNumberOfIters
            // 
            this.labelNumberOfIters.AutoSize = true;
            this.labelNumberOfIters.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelNumberOfIters.Location = new System.Drawing.Point(7, 22);
            this.labelNumberOfIters.Name = "labelNumberOfIters";
            this.labelNumberOfIters.Size = new System.Drawing.Size(13, 13);
            this.labelNumberOfIters.TabIndex = 2;
            this.labelNumberOfIters.Text = "L";
            // 
            // labelFunValDiff
            // 
            this.labelFunValDiff.AutoSize = true;
            this.labelFunValDiff.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelFunValDiff.Location = new System.Drawing.Point(6, 74);
            this.labelFunValDiff.Name = "labelFunValDiff";
            this.labelFunValDiff.Size = new System.Drawing.Size(19, 13);
            this.labelFunValDiff.TabIndex = 1;
            this.labelFunValDiff.Text = "ε1";
            // 
            // labelArgDiff
            // 
            this.labelArgDiff.AutoSize = true;
            this.labelArgDiff.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelArgDiff.Location = new System.Drawing.Point(6, 47);
            this.labelArgDiff.Name = "labelArgDiff";
            this.labelArgDiff.Size = new System.Drawing.Size(13, 13);
            this.labelArgDiff.TabIndex = 0;
            this.labelArgDiff.Text = "ε";
            // 
            // groupBoxInput
            // 
            this.groupBoxInput.Controls.Add(this.buttonChangeStartingPoint);
            this.groupBoxInput.Controls.Add(this.textBoxStartingPointValue);
            this.groupBoxInput.Controls.Add(this.labelEq3);
            this.groupBoxInput.Controls.Add(this.labelStartingPoint);
            this.groupBoxInput.Controls.Add(this.labelEq2);
            this.groupBoxInput.Controls.Add(this.labelEq1);
            this.groupBoxInput.Controls.Add(this.numericUpDownDimension);
            this.groupBoxInput.Controls.Add(this.labelDimension);
            this.groupBoxInput.Controls.Add(this.labelExpressionInput);
            this.groupBoxInput.Controls.Add(this.comboBoxInputExpression);
            this.groupBoxInput.Location = new System.Drawing.Point(12, 12);
            this.groupBoxInput.Name = "groupBoxInput";
            this.groupBoxInput.Size = new System.Drawing.Size(411, 158);
            this.groupBoxInput.TabIndex = 6;
            this.groupBoxInput.TabStop = false;
            this.groupBoxInput.Text = "Parametry wejściowe";
            // 
            // buttonChangeStartingPoint
            // 
            this.buttonChangeStartingPoint.Location = new System.Drawing.Point(272, 128);
            this.buttonChangeStartingPoint.Name = "buttonChangeStartingPoint";
            this.buttonChangeStartingPoint.Size = new System.Drawing.Size(133, 23);
            this.buttonChangeStartingPoint.TabIndex = 11;
            this.buttonChangeStartingPoint.Text = "Zmień punkt startowy";
            this.buttonChangeStartingPoint.UseVisualStyleBackColor = true;
            this.buttonChangeStartingPoint.Click += new System.EventHandler(this.buttonChangeStartingPoint_Click);
            // 
            // textBoxStartingPointValue
            // 
            this.textBoxStartingPointValue.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.textBoxStartingPointValue.Location = new System.Drawing.Point(76, 72);
            this.textBoxStartingPointValue.Multiline = true;
            this.textBoxStartingPointValue.Name = "textBoxStartingPointValue";
            this.textBoxStartingPointValue.ReadOnly = true;
            this.textBoxStartingPointValue.Size = new System.Drawing.Size(329, 50);
            this.textBoxStartingPointValue.TabIndex = 10;
            // 
            // labelEq3
            // 
            this.labelEq3.AutoSize = true;
            this.labelEq3.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq3.Location = new System.Drawing.Point(45, 75);
            this.labelEq3.Name = "labelEq3";
            this.labelEq3.Size = new System.Drawing.Size(13, 13);
            this.labelEq3.TabIndex = 9;
            this.labelEq3.Text = "=";
            // 
            // labelStartingPoint
            // 
            this.labelStartingPoint.AutoSize = true;
            this.labelStartingPoint.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelStartingPoint.Location = new System.Drawing.Point(7, 75);
            this.labelStartingPoint.Name = "labelStartingPoint";
            this.labelStartingPoint.Size = new System.Drawing.Size(31, 13);
            this.labelStartingPoint.TabIndex = 8;
            this.labelStartingPoint.Text = "x(0)";
            // 
            // labelEq2
            // 
            this.labelEq2.AutoSize = true;
            this.labelEq2.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq2.Location = new System.Drawing.Point(45, 48);
            this.labelEq2.Name = "labelEq2";
            this.labelEq2.Size = new System.Drawing.Size(13, 13);
            this.labelEq2.TabIndex = 7;
            this.labelEq2.Text = "=";
            // 
            // labelEq1
            // 
            this.labelEq1.AutoSize = true;
            this.labelEq1.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq1.Location = new System.Drawing.Point(45, 22);
            this.labelEq1.Name = "labelEq1";
            this.labelEq1.Size = new System.Drawing.Size(13, 13);
            this.labelEq1.TabIndex = 6;
            this.labelEq1.Text = "=";
            // 
            // numericUpDownDimension
            // 
            this.numericUpDownDimension.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownDimension.Location = new System.Drawing.Point(76, 46);
            this.numericUpDownDimension.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDownDimension.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDimension.Name = "numericUpDownDimension";
            this.numericUpDownDimension.Size = new System.Drawing.Size(67, 20);
            this.numericUpDownDimension.TabIndex = 5;
            this.numericUpDownDimension.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.numericUpDownDimension.ValueChanged += new System.EventHandler(this.numericUpDownDimension_ValueChanged);
            // 
            // labelDimension
            // 
            this.labelDimension.AutoSize = true;
            this.labelDimension.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelDimension.Location = new System.Drawing.Point(6, 48);
            this.labelDimension.Name = "labelDimension";
            this.labelDimension.Size = new System.Drawing.Size(13, 13);
            this.labelDimension.TabIndex = 4;
            this.labelDimension.Text = "n";
            // 
            // groupBoxGraphParams
            // 
            this.groupBoxGraphParams.Controls.Add(this.numericUpDownRangeX2Upper);
            this.groupBoxGraphParams.Controls.Add(this.numericUpDownRangeX2Lower);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX23);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX22);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX21);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX13);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX12);
            this.groupBoxGraphParams.Controls.Add(this.labelRangeX11);
            this.groupBoxGraphParams.Controls.Add(this.numericUpDownRangeX1Upper);
            this.groupBoxGraphParams.Controls.Add(this.numericUpDownRangeX1Lower);
            this.groupBoxGraphParams.Location = new System.Drawing.Point(12, 335);
            this.groupBoxGraphParams.Name = "groupBoxGraphParams";
            this.groupBoxGraphParams.Size = new System.Drawing.Size(411, 107);
            this.groupBoxGraphParams.TabIndex = 7;
            this.groupBoxGraphParams.TabStop = false;
            this.groupBoxGraphParams.Text = "Ustawienia wykresu";
            // 
            // numericUpDownRangeX2Upper
            // 
            this.numericUpDownRangeX2Upper.DecimalPlaces = 3;
            this.numericUpDownRangeX2Upper.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownRangeX2Upper.Location = new System.Drawing.Point(181, 53);
            this.numericUpDownRangeX2Upper.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRangeX2Upper.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownRangeX2Upper.Name = "numericUpDownRangeX2Upper";
            this.numericUpDownRangeX2Upper.Size = new System.Drawing.Size(98, 20);
            this.numericUpDownRangeX2Upper.TabIndex = 25;
            this.numericUpDownRangeX2Upper.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownRangeX2Lower
            // 
            this.numericUpDownRangeX2Lower.DecimalPlaces = 3;
            this.numericUpDownRangeX2Lower.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownRangeX2Lower.Location = new System.Drawing.Point(58, 53);
            this.numericUpDownRangeX2Lower.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRangeX2Lower.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownRangeX2Lower.Name = "numericUpDownRangeX2Lower";
            this.numericUpDownRangeX2Lower.Size = new System.Drawing.Size(98, 20);
            this.numericUpDownRangeX2Lower.TabIndex = 24;
            this.numericUpDownRangeX2Lower.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            // 
            // labelRangeX23
            // 
            this.labelRangeX23.AutoSize = true;
            this.labelRangeX23.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX23.Location = new System.Drawing.Point(285, 56);
            this.labelRangeX23.Name = "labelRangeX23";
            this.labelRangeX23.Size = new System.Drawing.Size(13, 13);
            this.labelRangeX23.TabIndex = 23;
            this.labelRangeX23.Text = "]";
            // 
            // labelRangeX22
            // 
            this.labelRangeX22.AutoSize = true;
            this.labelRangeX22.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX22.Location = new System.Drawing.Point(162, 56);
            this.labelRangeX22.Name = "labelRangeX22";
            this.labelRangeX22.Size = new System.Drawing.Size(13, 13);
            this.labelRangeX22.TabIndex = 22;
            this.labelRangeX22.Text = ",";
            // 
            // labelRangeX21
            // 
            this.labelRangeX21.AutoSize = true;
            this.labelRangeX21.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX21.Location = new System.Drawing.Point(7, 56);
            this.labelRangeX21.Name = "labelRangeX21";
            this.labelRangeX21.Size = new System.Drawing.Size(45, 13);
            this.labelRangeX21.TabIndex = 21;
            this.labelRangeX21.Text = "x2 ∈ [";
            // 
            // labelRangeX13
            // 
            this.labelRangeX13.AutoSize = true;
            this.labelRangeX13.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX13.Location = new System.Drawing.Point(285, 30);
            this.labelRangeX13.Name = "labelRangeX13";
            this.labelRangeX13.Size = new System.Drawing.Size(13, 13);
            this.labelRangeX13.TabIndex = 18;
            this.labelRangeX13.Text = "]";
            // 
            // labelRangeX12
            // 
            this.labelRangeX12.AutoSize = true;
            this.labelRangeX12.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX12.Location = new System.Drawing.Point(162, 30);
            this.labelRangeX12.Name = "labelRangeX12";
            this.labelRangeX12.Size = new System.Drawing.Size(13, 13);
            this.labelRangeX12.TabIndex = 17;
            this.labelRangeX12.Text = ",";
            // 
            // labelRangeX11
            // 
            this.labelRangeX11.AutoSize = true;
            this.labelRangeX11.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeX11.Location = new System.Drawing.Point(7, 30);
            this.labelRangeX11.Name = "labelRangeX11";
            this.labelRangeX11.Size = new System.Drawing.Size(45, 13);
            this.labelRangeX11.TabIndex = 16;
            this.labelRangeX11.Text = "x1 ∈ [";
            // 
            // numericUpDownRangeX1Upper
            // 
            this.numericUpDownRangeX1Upper.DecimalPlaces = 3;
            this.numericUpDownRangeX1Upper.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownRangeX1Upper.Location = new System.Drawing.Point(181, 27);
            this.numericUpDownRangeX1Upper.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRangeX1Upper.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownRangeX1Upper.Name = "numericUpDownRangeX1Upper";
            this.numericUpDownRangeX1Upper.Size = new System.Drawing.Size(98, 20);
            this.numericUpDownRangeX1Upper.TabIndex = 15;
            this.numericUpDownRangeX1Upper.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // numericUpDownRangeX1Lower
            // 
            this.numericUpDownRangeX1Lower.DecimalPlaces = 3;
            this.numericUpDownRangeX1Lower.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownRangeX1Lower.Location = new System.Drawing.Point(58, 27);
            this.numericUpDownRangeX1Lower.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRangeX1Lower.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.numericUpDownRangeX1Lower.Name = "numericUpDownRangeX1Lower";
            this.numericUpDownRangeX1Lower.Size = new System.Drawing.Size(98, 20);
            this.numericUpDownRangeX1Lower.TabIndex = 14;
            this.numericUpDownRangeX1Lower.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            // 
            // buttonShowSteps
            // 
            this.buttonShowSteps.Enabled = false;
            this.buttonShowSteps.Location = new System.Drawing.Point(284, 448);
            this.buttonShowSteps.Name = "buttonShowSteps";
            this.buttonShowSteps.Size = new System.Drawing.Size(143, 23);
            this.buttonShowSteps.TabIndex = 8;
            this.buttonShowSteps.Text = "Wyświetl kroki";
            this.buttonShowSteps.UseVisualStyleBackColor = true;
            this.buttonShowSteps.Click += new System.EventHandler(this.buttonShowSteps_Click);
            // 
            // labelEq7
            // 
            this.labelEq7.AutoSize = true;
            this.labelEq7.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq7.Location = new System.Drawing.Point(45, 99);
            this.labelEq7.Name = "labelEq7";
            this.labelEq7.Size = new System.Drawing.Size(13, 13);
            this.labelEq7.TabIndex = 17;
            this.labelEq7.Text = "=";
            // 
            // numericUpDownMinStepSize
            // 
            this.numericUpDownMinStepSize.DecimalPlaces = 3;
            this.numericUpDownMinStepSize.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownMinStepSize.Increment = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownMinStepSize.Location = new System.Drawing.Point(76, 97);
            this.numericUpDownMinStepSize.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            196608});
            this.numericUpDownMinStepSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            196608});
            this.numericUpDownMinStepSize.Name = "numericUpDownMinStepSize";
            this.numericUpDownMinStepSize.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownMinStepSize.TabIndex = 16;
            this.numericUpDownMinStepSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            // 
            // labelMinStepSize
            // 
            this.labelMinStepSize.AutoSize = true;
            this.labelMinStepSize.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelMinStepSize.Location = new System.Drawing.Point(7, 99);
            this.labelMinStepSize.Name = "labelMinStepSize";
            this.labelMinStepSize.Size = new System.Drawing.Size(19, 13);
            this.labelMinStepSize.TabIndex = 15;
            this.labelMinStepSize.Text = "δx";
            // 
            // labelEq8
            // 
            this.labelEq8.AutoSize = true;
            this.labelEq8.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelEq8.Location = new System.Drawing.Point(45, 125);
            this.labelEq8.Name = "labelEq8";
            this.labelEq8.Size = new System.Drawing.Size(13, 13);
            this.labelEq8.TabIndex = 28;
            this.labelEq8.Text = "=";
            // 
            // numericUpDownRangeWidth
            // 
            this.numericUpDownRangeWidth.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.numericUpDownRangeWidth.Location = new System.Drawing.Point(76, 123);
            this.numericUpDownRangeWidth.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDownRangeWidth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDownRangeWidth.Name = "numericUpDownRangeWidth";
            this.numericUpDownRangeWidth.Size = new System.Drawing.Size(120, 20);
            this.numericUpDownRangeWidth.TabIndex = 27;
            this.numericUpDownRangeWidth.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // labelRangeWidth
            // 
            this.labelRangeWidth.AutoSize = true;
            this.labelRangeWidth.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.labelRangeWidth.Location = new System.Drawing.Point(6, 125);
            this.labelRangeWidth.Name = "labelRangeWidth";
            this.labelRangeWidth.Size = new System.Drawing.Size(25, 13);
            this.labelRangeWidth.TabIndex = 26;
            this.labelRangeWidth.Text = "[,]";
            // 
            // toolTipHints
            // 
            this.toolTipHints.IsBalloon = true;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(916, 483);
            this.Controls.Add(this.buttonShowSteps);
            this.Controls.Add(this.groupBoxGraphParams);
            this.Controls.Add(this.groupBoxInput);
            this.Controls.Add(this.groupBoxRestrictions);
            this.Controls.Add(this.buttonFindOptimum);
            this.Controls.Add(this.pictureBoxGraph);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Powell";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGraph)).EndInit();
            this.groupBoxRestrictions.ResumeLayout(false);
            this.groupBoxRestrictions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownArgDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFunValDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownNumberOfIters)).EndInit();
            this.groupBoxInput.ResumeLayout(false);
            this.groupBoxInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownDimension)).EndInit();
            this.groupBoxGraphParams.ResumeLayout(false);
            this.groupBoxGraphParams.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX2Upper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX2Lower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX1Upper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeX1Lower)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMinStepSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownRangeWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGraph;
        private System.Windows.Forms.Label labelExpressionInput;
        private System.Windows.Forms.ComboBox comboBoxInputExpression;
        private System.Windows.Forms.Button buttonFindOptimum;
        private System.Windows.Forms.GroupBox groupBoxRestrictions;
        private System.Windows.Forms.GroupBox groupBoxInput;
        private System.Windows.Forms.Label labelEq2;
        private System.Windows.Forms.Label labelEq1;
        private System.Windows.Forms.NumericUpDown numericUpDownDimension;
        private System.Windows.Forms.Label labelDimension;
        private System.Windows.Forms.GroupBox groupBoxGraphParams;
        private System.Windows.Forms.Label labelEq3;
        private System.Windows.Forms.Label labelStartingPoint;
        private System.Windows.Forms.Label labelNumberOfIters;
        private System.Windows.Forms.Label labelFunValDiff;
        private System.Windows.Forms.Label labelArgDiff;
        private System.Windows.Forms.Label labelEq6;
        private System.Windows.Forms.Label labelEq5;
        private System.Windows.Forms.Label labelEq4;
        private System.Windows.Forms.NumericUpDown numericUpDownFunValDiff;
        private System.Windows.Forms.NumericUpDown numericUpDownNumberOfIters;
        private System.Windows.Forms.NumericUpDown numericUpDownArgDiff;
        private System.Windows.Forms.TextBox textBoxStartingPointValue;
        private System.Windows.Forms.Button buttonChangeStartingPoint;
        private System.Windows.Forms.Button buttonShowSteps;
        private System.Windows.Forms.NumericUpDown numericUpDownRangeX2Upper;
        private System.Windows.Forms.NumericUpDown numericUpDownRangeX2Lower;
        private System.Windows.Forms.Label labelRangeX23;
        private System.Windows.Forms.Label labelRangeX22;
        private System.Windows.Forms.Label labelRangeX21;
        private System.Windows.Forms.Label labelRangeX13;
        private System.Windows.Forms.Label labelRangeX12;
        private System.Windows.Forms.Label labelRangeX11;
        private System.Windows.Forms.NumericUpDown numericUpDownRangeX1Upper;
        private System.Windows.Forms.NumericUpDown numericUpDownRangeX1Lower;
        private System.Windows.Forms.Label labelEq8;
        private System.Windows.Forms.NumericUpDown numericUpDownRangeWidth;
        private System.Windows.Forms.Label labelRangeWidth;
        private System.Windows.Forms.Label labelEq7;
        private System.Windows.Forms.NumericUpDown numericUpDownMinStepSize;
        private System.Windows.Forms.Label labelMinStepSize;
        private System.Windows.Forms.ToolTip toolTipHints;
    }
}

