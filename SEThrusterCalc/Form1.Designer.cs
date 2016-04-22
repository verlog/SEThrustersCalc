namespace SEThrustersCalc
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox_GMass = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_gravity = new System.Windows.Forms.TextBox();
            this.label_Accel = new System.Windows.Forms.Label();
            this.trackBar_Height = new System.Windows.Forms.TrackBar();
            this.label_Height = new System.Windows.Forms.Label();
            this.button_add = new System.Windows.Forms.Button();
            this.dataGridView_ThrustersOnGrid = new System.Windows.Forms.DataGridView();
            this.Id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.trackBar_Accel = new System.Windows.Forms.TrackBar();
            this.label_ThrustSumm = new System.Windows.Forms.Label();
            this.radioButton_small = new System.Windows.Forms.RadioButton();
            this.radioButton_large = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.pnl_GRAPH = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.labelGRange = new System.Windows.Forms.Label();
            this.textBoxGrange = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Height)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ThrustersOnGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Accel)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_GMass
            // 
            this.textBox_GMass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_GMass.Location = new System.Drawing.Point(18, 215);
            this.textBox_GMass.Name = "textBox_GMass";
            this.textBox_GMass.Size = new System.Drawing.Size(288, 30);
            this.textBox_GMass.TabIndex = 0;
            this.textBox_GMass.Text = "0";
            this.textBox_GMass.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_GMass.TextChanged += new System.EventHandler(this.textBox_GMass_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(15, 187);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(194, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Масса корабля (kg.)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(13, 110);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(161, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "Гравитация (G.)";
            // 
            // textBox_gravity
            // 
            this.textBox_gravity.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox_gravity.Location = new System.Drawing.Point(12, 138);
            this.textBox_gravity.Name = "textBox_gravity";
            this.textBox_gravity.Size = new System.Drawing.Size(103, 30);
            this.textBox_gravity.TabIndex = 2;
            this.textBox_gravity.Text = "1";
            this.textBox_gravity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_gravity.TextChanged += new System.EventHandler(this.textBox_gravity_TextChanged);
            // 
            // label_Accel
            // 
            this.label_Accel.AutoSize = true;
            this.label_Accel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Accel.Location = new System.Drawing.Point(13, 270);
            this.label_Accel.Name = "label_Accel";
            this.label_Accel.Size = new System.Drawing.Size(214, 25);
            this.label_Accel.TabIndex = 5;
            this.label_Accel.Text = "Желаемое ускорение";
            // 
            // trackBar_Height
            // 
            this.trackBar_Height.Location = new System.Drawing.Point(18, 392);
            this.trackBar_Height.Maximum = 45000;
            this.trackBar_Height.Name = "trackBar_Height";
            this.trackBar_Height.Size = new System.Drawing.Size(385, 56);
            this.trackBar_Height.TabIndex = 6;
            this.trackBar_Height.Value = 45000;
            this.trackBar_Height.ValueChanged += new System.EventHandler(this.trackBar_Height_ValueChanged);
            // 
            // label_Height
            // 
            this.label_Height.AutoSize = true;
            this.label_Height.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_Height.Location = new System.Drawing.Point(15, 356);
            this.label_Height.Name = "label_Height";
            this.label_Height.Size = new System.Drawing.Size(134, 25);
            this.label_Height.TabIndex = 7;
            this.label_Height.Text = "Высота 0 км.";
            // 
            // button_add
            // 
            this.button_add.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_add.Location = new System.Drawing.Point(357, 456);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(46, 32);
            this.button_add.TabIndex = 9;
            this.button_add.Text = "+";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button1_Click);
            // 
            // dataGridView_ThrustersOnGrid
            // 
            this.dataGridView_ThrustersOnGrid.AllowUserToAddRows = false;
            this.dataGridView_ThrustersOnGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView_ThrustersOnGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_ThrustersOnGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Id,
            this.ThName,
            this.ThCount});
            this.dataGridView_ThrustersOnGrid.Location = new System.Drawing.Point(20, 494);
            this.dataGridView_ThrustersOnGrid.Name = "dataGridView_ThrustersOnGrid";
            this.dataGridView_ThrustersOnGrid.RowTemplate.Height = 24;
            this.dataGridView_ThrustersOnGrid.Size = new System.Drawing.Size(383, 134);
            this.dataGridView_ThrustersOnGrid.TabIndex = 10;
            this.dataGridView_ThrustersOnGrid.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_ThrustersOnGrid_CellValueChanged);
            this.dataGridView_ThrustersOnGrid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.dataGridView_ThrustersOnGrid_RowsRemoved);
            // 
            // Id
            // 
            this.Id.HeaderText = "Id";
            this.Id.Name = "Id";
            this.Id.Visible = false;
            // 
            // ThName
            // 
            this.ThName.HeaderText = "Name";
            this.ThName.Name = "ThName";
            this.ThName.ReadOnly = true;
            this.ThName.Width = 240;
            // 
            // ThCount
            // 
            this.ThCount.HeaderText = "Count";
            this.ThCount.Name = "ThCount";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 455);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(339, 33);
            this.comboBox1.TabIndex = 11;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // trackBar_Accel
            // 
            this.trackBar_Accel.LargeChange = 0;
            this.trackBar_Accel.Location = new System.Drawing.Point(18, 298);
            this.trackBar_Accel.Maximum = 1000;
            this.trackBar_Accel.Name = "trackBar_Accel";
            this.trackBar_Accel.Size = new System.Drawing.Size(376, 56);
            this.trackBar_Accel.TabIndex = 13;
            this.trackBar_Accel.ValueChanged += new System.EventHandler(this.trackBar_Accel_ValueChanged);
            // 
            // label_ThrustSumm
            // 
            this.label_ThrustSumm.AutoSize = true;
            this.label_ThrustSumm.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_ThrustSumm.Location = new System.Drawing.Point(550, 60);
            this.label_ThrustSumm.Name = "label_ThrustSumm";
            this.label_ThrustSumm.Size = new System.Drawing.Size(102, 25);
            this.label_ThrustSumm.TabIndex = 16;
            this.label_ThrustSumm.Text = "Тяга: 0 / 0";
            // 
            // radioButton_small
            // 
            this.radioButton_small.AutoSize = true;
            this.radioButton_small.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_small.Location = new System.Drawing.Point(156, 5);
            this.radioButton_small.Name = "radioButton_small";
            this.radioButton_small.Size = new System.Drawing.Size(82, 29);
            this.radioButton_small.TabIndex = 17;
            this.radioButton_small.Text = "Small";
            this.radioButton_small.UseVisualStyleBackColor = true;
            this.radioButton_small.CheckedChanged += new System.EventHandler(this.radioButton_small_CheckedChanged);
            // 
            // radioButton_large
            // 
            this.radioButton_large.AutoSize = true;
            this.radioButton_large.Checked = true;
            this.radioButton_large.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_large.Location = new System.Drawing.Point(156, 40);
            this.radioButton_large.Name = "radioButton_large";
            this.radioButton_large.Size = new System.Drawing.Size(83, 29);
            this.radioButton_large.TabIndex = 18;
            this.radioButton_large.TabStop = true;
            this.radioButton_large.Text = "Large";
            this.radioButton_large.UseVisualStyleBackColor = true;
            this.radioButton_large.CheckedChanged += new System.EventHandler(this.radioButton_large_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(17, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 25);
            this.label3.TabIndex = 19;
            this.label3.Text = "Тип грида";
            // 
            // pnl_GRAPH
            // 
            this.pnl_GRAPH.Location = new System.Drawing.Point(564, 161);
            this.pnl_GRAPH.Name = "pnl_GRAPH";
            this.pnl_GRAPH.Size = new System.Drawing.Size(713, 437);
            this.pnl_GRAPH.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1137, 110);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 21;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(859, 110);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 22;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // labelGRange
            // 
            this.labelGRange.AutoSize = true;
            this.labelGRange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelGRange.Location = new System.Drawing.Point(180, 110);
            this.labelGRange.Name = "labelGRange";
            this.labelGRange.Size = new System.Drawing.Size(232, 25);
            this.labelGRange.TabIndex = 24;
            this.labelGRange.Text = "Диаметр планеты  (m.)";
            // 
            // textBoxGrange
            // 
            this.textBoxGrange.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxGrange.Location = new System.Drawing.Point(170, 138);
            this.textBoxGrange.Name = "textBoxGrange";
            this.textBoxGrange.Size = new System.Drawing.Size(136, 30);
            this.textBoxGrange.TabIndex = 23;
            this.textBoxGrange.Text = "120000";
            this.textBoxGrange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxGrange.TextChanged += new System.EventHandler(this.textBoxGrange_TextChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 640);
            this.Controls.Add(this.labelGRange);
            this.Controls.Add(this.textBoxGrange);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnl_GRAPH);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.radioButton_large);
            this.Controls.Add(this.radioButton_small);
            this.Controls.Add(this.label_ThrustSumm);
            this.Controls.Add(this.trackBar_Accel);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView_ThrustersOnGrid);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.label_Height);
            this.Controls.Add(this.trackBar_Height);
            this.Controls.Add(this.label_Accel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_gravity);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_GMass);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "SE thruster calculator";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Height)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_ThrustersOnGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_Accel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_GMass;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_gravity;
        private System.Windows.Forms.Label label_Accel;
        private System.Windows.Forms.TrackBar trackBar_Height;
        private System.Windows.Forms.Label label_Height;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.DataGridView dataGridView_ThrustersOnGrid;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TrackBar trackBar_Accel;
        private System.Windows.Forms.Label label_ThrustSumm;
        private System.Windows.Forms.DataGridViewTextBoxColumn Id;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThCount;
        private System.Windows.Forms.RadioButton radioButton_small;
        private System.Windows.Forms.RadioButton radioButton_large;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel pnl_GRAPH;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelGRange;
        private System.Windows.Forms.TextBox textBoxGrange;

    }
}

