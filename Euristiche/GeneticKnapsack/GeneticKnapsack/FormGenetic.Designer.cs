namespace GeneticKnapsack
{
    partial class FormGenetic
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblFilename = new System.Windows.Forms.Label();
            this.btnLoadFile = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblOutput = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIter = new System.Windows.Forms.TextBox();
            this.cmbBoxProblems = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.outputPrev = new System.Windows.Forms.Label();
            this.outputOtt = new System.Windows.Forms.Label();
            this.lblTimeElapsed = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lblIter = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.grpRisoluzione = new System.Windows.Forms.GroupBox();
            this.buttonPlot = new System.Windows.Forms.Button();
            this.rdbACO = new System.Windows.Forms.RadioButton();
            this.rdbGen = new System.Windows.Forms.RadioButton();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.grpRisoluzione.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFilename
            // 
            this.lblFilename.AutoSize = true;
            this.lblFilename.Location = new System.Drawing.Point(124, 35);
            this.lblFilename.Name = "lblFilename";
            this.lblFilename.Size = new System.Drawing.Size(46, 13);
            this.lblFilename.TabIndex = 0;
            this.lblFilename.Text = "filename";
            // 
            // btnLoadFile
            // 
            this.btnLoadFile.Location = new System.Drawing.Point(33, 30);
            this.btnLoadFile.Name = "btnLoadFile";
            this.btnLoadFile.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFile.TabIndex = 1;
            this.btnLoadFile.Text = "Load File...";
            this.btnLoadFile.UseVisualStyleBackColor = true;
            this.btnLoadFile.Click += new System.EventHandler(this.btnLoadFile_Click);
            // 
            // btnStart
            // 
            this.btnStart.Enabled = false;
            this.btnStart.Location = new System.Drawing.Point(122, 22);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // lblOutput
            // 
            this.lblOutput.AutoSize = true;
            this.lblOutput.Location = new System.Drawing.Point(25, 117);
            this.lblOutput.Name = "lblOutput";
            this.lblOutput.Size = new System.Drawing.Size(84, 13);
            this.lblOutput.TabIndex = 3;
            this.lblOutput.Text = "Output ottenuto:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(33, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "N° di iterazioni:";
            // 
            // txtIter
            // 
            this.txtIter.Location = new System.Drawing.Point(116, 64);
            this.txtIter.Name = "txtIter";
            this.txtIter.Size = new System.Drawing.Size(66, 20);
            this.txtIter.TabIndex = 5;
            this.txtIter.Text = "100";
            // 
            // cmbBoxProblems
            // 
            this.cmbBoxProblems.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBoxProblems.Enabled = false;
            this.cmbBoxProblems.Location = new System.Drawing.Point(28, 24);
            this.cmbBoxProblems.Name = "cmbBoxProblems";
            this.cmbBoxProblems.Size = new System.Drawing.Size(77, 21);
            this.cmbBoxProblems.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Output previsto:";
            // 
            // outputPrev
            // 
            this.outputPrev.AutoSize = true;
            this.outputPrev.Location = new System.Drawing.Point(113, 94);
            this.outputPrev.Name = "outputPrev";
            this.outputPrev.Size = new System.Drawing.Size(13, 13);
            this.outputPrev.TabIndex = 8;
            this.outputPrev.Text = "0";
            // 
            // outputOtt
            // 
            this.outputOtt.AutoSize = true;
            this.outputOtt.Location = new System.Drawing.Point(113, 117);
            this.outputOtt.Name = "outputOtt";
            this.outputOtt.Size = new System.Drawing.Size(13, 13);
            this.outputOtt.TabIndex = 9;
            this.outputOtt.Text = "0";
            // 
            // lblTimeElapsed
            // 
            this.lblTimeElapsed.AutoSize = true;
            this.lblTimeElapsed.Location = new System.Drawing.Point(113, 139);
            this.lblTimeElapsed.Name = "lblTimeElapsed";
            this.lblTimeElapsed.Size = new System.Drawing.Size(29, 13);
            this.lblTimeElapsed.TabIndex = 11;
            this.lblTimeElapsed.Text = "0 ms";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(25, 139);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Tempo trascorso:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblIter
            // 
            this.lblIter.AutoSize = true;
            this.lblIter.Location = new System.Drawing.Point(113, 161);
            this.lblIter.Name = "lblIter";
            this.lblIter.Size = new System.Drawing.Size(13, 13);
            this.lblIter.TabIndex = 13;
            this.lblIter.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(25, 161);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Iterazioni svolte:";
            // 
            // grpRisoluzione
            // 
            this.grpRisoluzione.Controls.Add(this.buttonPlot);
            this.grpRisoluzione.Controls.Add(this.rdbACO);
            this.grpRisoluzione.Controls.Add(this.rdbGen);
            this.grpRisoluzione.Controls.Add(this.label6);
            this.grpRisoluzione.Controls.Add(this.cmbBoxProblems);
            this.grpRisoluzione.Controls.Add(this.lblIter);
            this.grpRisoluzione.Controls.Add(this.btnStart);
            this.grpRisoluzione.Controls.Add(this.lblOutput);
            this.grpRisoluzione.Controls.Add(this.lblTimeElapsed);
            this.grpRisoluzione.Controls.Add(this.label3);
            this.grpRisoluzione.Controls.Add(this.label5);
            this.grpRisoluzione.Controls.Add(this.outputPrev);
            this.grpRisoluzione.Controls.Add(this.outputOtt);
            this.grpRisoluzione.Location = new System.Drawing.Point(12, 100);
            this.grpRisoluzione.Name = "grpRisoluzione";
            this.grpRisoluzione.Size = new System.Drawing.Size(238, 214);
            this.grpRisoluzione.TabIndex = 14;
            this.grpRisoluzione.TabStop = false;
            this.grpRisoluzione.Text = "Risoluzione";
            // 
            // buttonPlot
            // 
            this.buttonPlot.Enabled = false;
            this.buttonPlot.Location = new System.Drawing.Point(83, 185);
            this.buttonPlot.Name = "buttonPlot";
            this.buttonPlot.Size = new System.Drawing.Size(75, 23);
            this.buttonPlot.TabIndex = 16;
            this.buttonPlot.Text = "Plot Graph";
            this.buttonPlot.UseVisualStyleBackColor = true;
            this.buttonPlot.Click += new System.EventHandler(this.buttonPlot_Click);
            // 
            // rdbACO
            // 
            this.rdbACO.AutoSize = true;
            this.rdbACO.Location = new System.Drawing.Point(123, 63);
            this.rdbACO.Name = "rdbACO";
            this.rdbACO.Size = new System.Drawing.Size(47, 17);
            this.rdbACO.TabIndex = 15;
            this.rdbACO.TabStop = true;
            this.rdbACO.Text = "ACO";
            this.rdbACO.UseVisualStyleBackColor = true;
            // 
            // rdbGen
            // 
            this.rdbGen.AutoSize = true;
            this.rdbGen.Checked = true;
            this.rdbGen.Location = new System.Drawing.Point(43, 63);
            this.rdbGen.Name = "rdbGen";
            this.rdbGen.Size = new System.Drawing.Size(62, 17);
            this.rdbGen.TabIndex = 14;
            this.rdbGen.TabStop = true;
            this.rdbGen.Text = "Genetic";
            this.rdbGen.UseVisualStyleBackColor = true;
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(256, 13);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Genetic";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.Name = "ACO";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(383, 300);
            this.chart1.TabIndex = 15;
            this.chart1.Text = "chart1";
            // 
            // FormGenetic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 326);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.grpRisoluzione);
            this.Controls.Add(this.txtIter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnLoadFile);
            this.Controls.Add(this.lblFilename);
            this.Name = "FormGenetic";
            this.Text = "Algoritmo Genetico Knapsack";
            this.grpRisoluzione.ResumeLayout(false);
            this.grpRisoluzione.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFilename;
        private System.Windows.Forms.Button btnLoadFile;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblOutput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIter;
        private System.Windows.Forms.ComboBox cmbBoxProblems;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label outputPrev;
        private System.Windows.Forms.Label outputOtt;
        private System.Windows.Forms.Label lblTimeElapsed;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label lblIter;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpRisoluzione;
        private System.Windows.Forms.RadioButton rdbACO;
        private System.Windows.Forms.RadioButton rdbGen;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Button buttonPlot;
    }
}

