namespace Dashboard
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Title title2 = new System.Windows.Forms.DataVisualization.Charting.Title();
            this.chartPrice = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button1 = new System.Windows.Forms.Button();
            this.startDate = new System.Windows.Forms.TextBox();
            this.endDate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BitcoinRangeBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartPrice)).BeginInit();
            this.SuspendLayout();
            // 
            // chartPrice
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPrice.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPrice.Legends.Add(legend2);
            this.chartPrice.Location = new System.Drawing.Point(87, 203);
            this.chartPrice.Margin = new System.Windows.Forms.Padding(4);
            this.chartPrice.Name = "chartPrice";
            series2.BackImageTransparentColor = System.Drawing.Color.White;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Green;
            series2.IsXValueIndexed = true;
            series2.Legend = "Legend1";
            series2.Name = "Price";
            series2.YValuesPerPoint = 2;
            this.chartPrice.Series.Add(series2);
            this.chartPrice.Size = new System.Drawing.Size(1036, 494);
            this.chartPrice.TabIndex = 0;
            this.chartPrice.Text = "chart1";
            title2.Name = "Title1";
            title2.Text = "Price Chart";
            this.chartPrice.Titles.Add(title2);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GrayText;
            this.button1.ForeColor = System.Drawing.SystemColors.Info;
            this.button1.Location = new System.Drawing.Point(87, 89);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 48);
            this.button1.TabIndex = 1;
            this.button1.Text = "Past 30 Days";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // startDate
            // 
            this.startDate.Location = new System.Drawing.Point(660, 113);
            this.startDate.Name = "startDate";
            this.startDate.Size = new System.Drawing.Size(100, 24);
            this.startDate.TabIndex = 2;
            // 
            // endDate
            // 
            this.endDate.Location = new System.Drawing.Point(839, 113);
            this.endDate.Name = "endDate";
            this.endDate.Size = new System.Drawing.Size(100, 24);
            this.endDate.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(657, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Start Date";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(836, 89);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "End Date";
            // 
            // BitcoinRangeBtn
            // 
            this.BitcoinRangeBtn.BackColor = System.Drawing.SystemColors.GrayText;
            this.BitcoinRangeBtn.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BitcoinRangeBtn.Location = new System.Drawing.Point(977, 100);
            this.BitcoinRangeBtn.Name = "BitcoinRangeBtn";
            this.BitcoinRangeBtn.Size = new System.Drawing.Size(146, 48);
            this.BitcoinRangeBtn.TabIndex = 6;
            this.BitcoinRangeBtn.Text = "Update";
            this.BitcoinRangeBtn.UseVisualStyleBackColor = false;
            this.BitcoinRangeBtn.Click += new System.EventHandler(this.BitcoinRangeBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1233, 712);
            this.Controls.Add(this.BitcoinRangeBtn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.endDate);
            this.Controls.Add(this.startDate);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.chartPrice);
            this.Font = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chartPrice)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartPrice;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox startDate;
        private System.Windows.Forms.TextBox endDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button BitcoinRangeBtn;
    }
}

