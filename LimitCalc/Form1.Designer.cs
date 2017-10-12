namespace LimitCalc
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.NumColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PowerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoefColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IsFixedCoeffColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.coeffsSumLabel = new System.Windows.Forms.Label();
            this.selectedMeterName = new System.Windows.Forms.Label();
            this.saveSettingsButton = new System.Windows.Forms.Button();
            this.monthLimit12 = new System.Windows.Forms.TextBox();
            this.monthLimit11 = new System.Windows.Forms.TextBox();
            this.monthLimit10 = new System.Windows.Forms.TextBox();
            this.monthLimit8 = new System.Windows.Forms.TextBox();
            this.monthLimit9 = new System.Windows.Forms.TextBox();
            this.monthLabel12 = new System.Windows.Forms.Label();
            this.monthLabel11 = new System.Windows.Forms.Label();
            this.monthLabel10 = new System.Windows.Forms.Label();
            this.monthLabel9 = new System.Windows.Forms.Label();
            this.monthLabel8 = new System.Windows.Forms.Label();
            this.monthLimit7 = new System.Windows.Forms.TextBox();
            this.monthLabel7 = new System.Windows.Forms.Label();
            this.monthLimit6 = new System.Windows.Forms.TextBox();
            this.monthLimit5 = new System.Windows.Forms.TextBox();
            this.monthLimit4 = new System.Windows.Forms.TextBox();
            this.monthLimit2 = new System.Windows.Forms.TextBox();
            this.monthLimit3 = new System.Windows.Forms.TextBox();
            this.monthLabel6 = new System.Windows.Forms.Label();
            this.monthLabel5 = new System.Windows.Forms.Label();
            this.monthLabel4 = new System.Windows.Forms.Label();
            this.monthLabel3 = new System.Windows.Forms.Label();
            this.monthLabel2 = new System.Windows.Forms.Label();
            this.monthLimit1 = new System.Windows.Forms.TextBox();
            this.monthLabel1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.priceTextBox = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.totalMoneySumTextBox = new System.Windows.Forms.TextBox();
            this.calcLimitesButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progInfoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.tenderVolumeTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.enterVolume = new System.Windows.Forms.CheckBox();
            this.importDataMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Bisque;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NumColumn,
            this.NameColumn,
            this.PowerColumn,
            this.CoefColumn,
            this.IsFixedCoeffColumn});
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(12, 75);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.White;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(601, 262);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.dataGridView1_CellBeginEdit);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // NumColumn
            // 
            this.NumColumn.DataPropertyName = "Number";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.NumColumn.DefaultCellStyle = dataGridViewCellStyle2;
            this.NumColumn.FillWeight = 60.91371F;
            this.NumColumn.HeaderText = "№";
            this.NumColumn.Name = "NumColumn";
            this.NumColumn.ReadOnly = true;
            this.NumColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.NumColumn.Width = 30;
            // 
            // NameColumn
            // 
            this.NameColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.NameColumn.DataPropertyName = "Name";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopLeft;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.NameColumn.DefaultCellStyle = dataGridViewCellStyle3;
            this.NameColumn.FillWeight = 314.8366F;
            this.NameColumn.HeaderText = "Назва ЛПЗ";
            this.NameColumn.Name = "NameColumn";
            this.NameColumn.ReadOnly = true;
            this.NameColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PowerColumn
            // 
            this.PowerColumn.DataPropertyName = "ConnectedPower";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.Format = "F3";
            dataGridViewCellStyle4.NullValue = null;
            this.PowerColumn.DefaultCellStyle = dataGridViewCellStyle4;
            this.PowerColumn.FillWeight = 14.11351F;
            this.PowerColumn.HeaderText = "Потужність";
            this.PowerColumn.Name = "PowerColumn";
            this.PowerColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.PowerColumn.Width = 90;
            // 
            // CoefColumn
            // 
            this.CoefColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CoefColumn.DataPropertyName = "GeneralCoefficient";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.Format = "F3";
            this.CoefColumn.DefaultCellStyle = dataGridViewCellStyle5;
            this.CoefColumn.FillWeight = 10.1362F;
            this.CoefColumn.HeaderText = "Коеффіцієнт";
            this.CoefColumn.Name = "CoefColumn";
            this.CoefColumn.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CoefColumn.Width = 94;
            // 
            // IsFixedCoeffColumn
            // 
            this.IsFixedCoeffColumn.DataPropertyName = "IsFixedGeneralCoefficient";
            this.IsFixedCoeffColumn.HeaderText = "Фіксований";
            this.IsFixedCoeffColumn.Name = "IsFixedCoeffColumn";
            this.IsFixedCoeffColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.IsFixedCoeffColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.coeffsSumLabel);
            this.panel1.Controls.Add(this.selectedMeterName);
            this.panel1.Controls.Add(this.saveSettingsButton);
            this.panel1.Controls.Add(this.monthLimit12);
            this.panel1.Controls.Add(this.monthLimit11);
            this.panel1.Controls.Add(this.monthLimit10);
            this.panel1.Controls.Add(this.monthLimit8);
            this.panel1.Controls.Add(this.monthLimit9);
            this.panel1.Controls.Add(this.monthLabel12);
            this.panel1.Controls.Add(this.monthLabel11);
            this.panel1.Controls.Add(this.monthLabel10);
            this.panel1.Controls.Add(this.monthLabel9);
            this.panel1.Controls.Add(this.monthLabel8);
            this.panel1.Controls.Add(this.monthLimit7);
            this.panel1.Controls.Add(this.monthLabel7);
            this.panel1.Controls.Add(this.monthLimit6);
            this.panel1.Controls.Add(this.monthLimit5);
            this.panel1.Controls.Add(this.monthLimit4);
            this.panel1.Controls.Add(this.monthLimit2);
            this.panel1.Controls.Add(this.monthLimit3);
            this.panel1.Controls.Add(this.monthLabel6);
            this.panel1.Controls.Add(this.monthLabel5);
            this.panel1.Controls.Add(this.monthLabel4);
            this.panel1.Controls.Add(this.monthLabel3);
            this.panel1.Controls.Add(this.monthLabel2);
            this.panel1.Controls.Add(this.monthLimit1);
            this.panel1.Controls.Add(this.monthLabel1);
            this.panel1.Location = new System.Drawing.Point(619, 75);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(365, 294);
            this.panel1.TabIndex = 1;
            // 
            // coeffsSumLabel
            // 
            this.coeffsSumLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.coeffsSumLabel.ForeColor = System.Drawing.Color.Red;
            this.coeffsSumLabel.Location = new System.Drawing.Point(-1, 240);
            this.coeffsSumLabel.Name = "coeffsSumLabel";
            this.coeffsSumLabel.Size = new System.Drawing.Size(365, 15);
            this.coeffsSumLabel.TabIndex = 26;
            this.coeffsSumLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // selectedMeterName
            // 
            this.selectedMeterName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.selectedMeterName.ForeColor = System.Drawing.Color.Green;
            this.selectedMeterName.Location = new System.Drawing.Point(-1, 11);
            this.selectedMeterName.Name = "selectedMeterName";
            this.selectedMeterName.Size = new System.Drawing.Size(365, 15);
            this.selectedMeterName.TabIndex = 25;
            this.selectedMeterName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveSettingsButton
            // 
            this.saveSettingsButton.Location = new System.Drawing.Point(107, 261);
            this.saveSettingsButton.Name = "saveSettingsButton";
            this.saveSettingsButton.Size = new System.Drawing.Size(147, 23);
            this.saveSettingsButton.TabIndex = 24;
            this.saveSettingsButton.Text = "Встановити коефіцієнти";
            this.saveSettingsButton.UseVisualStyleBackColor = true;
            this.saveSettingsButton.Click += new System.EventHandler(this.saveSettingsButton_Click);
            // 
            // monthLimit12
            // 
            this.monthLimit12.Location = new System.Drawing.Point(269, 206);
            this.monthLimit12.Name = "monthLimit12";
            this.monthLimit12.Size = new System.Drawing.Size(81, 20);
            this.monthLimit12.TabIndex = 23;
            this.monthLimit12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit12.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit11
            // 
            this.monthLimit11.Location = new System.Drawing.Point(269, 177);
            this.monthLimit11.Name = "monthLimit11";
            this.monthLimit11.Size = new System.Drawing.Size(81, 20);
            this.monthLimit11.TabIndex = 22;
            this.monthLimit11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit11.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit10
            // 
            this.monthLimit10.Location = new System.Drawing.Point(269, 151);
            this.monthLimit10.Name = "monthLimit10";
            this.monthLimit10.Size = new System.Drawing.Size(81, 20);
            this.monthLimit10.TabIndex = 21;
            this.monthLimit10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit10.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit8
            // 
            this.monthLimit8.Location = new System.Drawing.Point(269, 66);
            this.monthLimit8.Name = "monthLimit8";
            this.monthLimit8.Size = new System.Drawing.Size(81, 20);
            this.monthLimit8.TabIndex = 20;
            this.monthLimit8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit8.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit9
            // 
            this.monthLimit9.Location = new System.Drawing.Point(269, 96);
            this.monthLimit9.Name = "monthLimit9";
            this.monthLimit9.Size = new System.Drawing.Size(81, 20);
            this.monthLimit9.TabIndex = 19;
            this.monthLimit9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit9.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLabel12
            // 
            this.monthLabel12.AutoSize = true;
            this.monthLabel12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel12.Location = new System.Drawing.Point(200, 209);
            this.monthLabel12.Name = "monthLabel12";
            this.monthLabel12.Size = new System.Drawing.Size(59, 13);
            this.monthLabel12.TabIndex = 18;
            this.monthLabel12.Text = "Грудень:";
            // 
            // monthLabel11
            // 
            this.monthLabel11.AutoSize = true;
            this.monthLabel11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel11.Location = new System.Drawing.Point(200, 180);
            this.monthLabel11.Name = "monthLabel11";
            this.monthLabel11.Size = new System.Drawing.Size(68, 13);
            this.monthLabel11.TabIndex = 17;
            this.monthLabel11.Text = "Листопад:";
            // 
            // monthLabel10
            // 
            this.monthLabel10.AutoSize = true;
            this.monthLabel10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel10.Location = new System.Drawing.Point(200, 154);
            this.monthLabel10.Name = "monthLabel10";
            this.monthLabel10.Size = new System.Drawing.Size(64, 13);
            this.monthLabel10.TabIndex = 16;
            this.monthLabel10.Text = "Жовтень:";
            // 
            // monthLabel9
            // 
            this.monthLabel9.AutoSize = true;
            this.monthLabel9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel9.Location = new System.Drawing.Point(200, 99);
            this.monthLabel9.Name = "monthLabel9";
            this.monthLabel9.Size = new System.Drawing.Size(68, 13);
            this.monthLabel9.TabIndex = 15;
            this.monthLabel9.Text = "Вересень:";
            // 
            // monthLabel8
            // 
            this.monthLabel8.AutoSize = true;
            this.monthLabel8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel8.Location = new System.Drawing.Point(200, 69);
            this.monthLabel8.Name = "monthLabel8";
            this.monthLabel8.Size = new System.Drawing.Size(61, 13);
            this.monthLabel8.TabIndex = 14;
            this.monthLabel8.Text = "Серпень:";
            // 
            // monthLimit7
            // 
            this.monthLimit7.Location = new System.Drawing.Point(269, 38);
            this.monthLimit7.Name = "monthLimit7";
            this.monthLimit7.Size = new System.Drawing.Size(81, 20);
            this.monthLimit7.TabIndex = 13;
            this.monthLimit7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit7.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLabel7
            // 
            this.monthLabel7.AutoSize = true;
            this.monthLabel7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel7.Location = new System.Drawing.Point(200, 41);
            this.monthLabel7.Name = "monthLabel7";
            this.monthLabel7.Size = new System.Drawing.Size(55, 13);
            this.monthLabel7.TabIndex = 12;
            this.monthLabel7.Text = "Липень:";
            // 
            // monthLimit6
            // 
            this.monthLimit6.Location = new System.Drawing.Point(73, 206);
            this.monthLimit6.Name = "monthLimit6";
            this.monthLimit6.Size = new System.Drawing.Size(81, 20);
            this.monthLimit6.TabIndex = 11;
            this.monthLimit6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit6.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit5
            // 
            this.monthLimit5.Location = new System.Drawing.Point(73, 177);
            this.monthLimit5.Name = "monthLimit5";
            this.monthLimit5.Size = new System.Drawing.Size(81, 20);
            this.monthLimit5.TabIndex = 10;
            this.monthLimit5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit5.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit4
            // 
            this.monthLimit4.Location = new System.Drawing.Point(73, 151);
            this.monthLimit4.Name = "monthLimit4";
            this.monthLimit4.Size = new System.Drawing.Size(81, 20);
            this.monthLimit4.TabIndex = 9;
            this.monthLimit4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit4.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit2
            // 
            this.monthLimit2.Location = new System.Drawing.Point(73, 66);
            this.monthLimit2.Name = "monthLimit2";
            this.monthLimit2.Size = new System.Drawing.Size(81, 20);
            this.monthLimit2.TabIndex = 8;
            this.monthLimit2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit2.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLimit3
            // 
            this.monthLimit3.Location = new System.Drawing.Point(73, 96);
            this.monthLimit3.Name = "monthLimit3";
            this.monthLimit3.Size = new System.Drawing.Size(81, 20);
            this.monthLimit3.TabIndex = 7;
            this.monthLimit3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit3.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLabel6
            // 
            this.monthLabel6.AutoSize = true;
            this.monthLabel6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel6.Location = new System.Drawing.Point(4, 209);
            this.monthLabel6.Name = "monthLabel6";
            this.monthLabel6.Size = new System.Drawing.Size(62, 13);
            this.monthLabel6.TabIndex = 6;
            this.monthLabel6.Text = "Червень:";
            // 
            // monthLabel5
            // 
            this.monthLabel5.AutoSize = true;
            this.monthLabel5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel5.Location = new System.Drawing.Point(4, 180);
            this.monthLabel5.Name = "monthLabel5";
            this.monthLabel5.Size = new System.Drawing.Size(61, 13);
            this.monthLabel5.TabIndex = 5;
            this.monthLabel5.Text = "Травень:";
            // 
            // monthLabel4
            // 
            this.monthLabel4.AutoSize = true;
            this.monthLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel4.Location = new System.Drawing.Point(4, 154);
            this.monthLabel4.Name = "monthLabel4";
            this.monthLabel4.Size = new System.Drawing.Size(56, 13);
            this.monthLabel4.TabIndex = 4;
            this.monthLabel4.Text = "Квітень:";
            // 
            // monthLabel3
            // 
            this.monthLabel3.AutoSize = true;
            this.monthLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel3.Location = new System.Drawing.Point(4, 99);
            this.monthLabel3.Name = "monthLabel3";
            this.monthLabel3.Size = new System.Drawing.Size(68, 13);
            this.monthLabel3.TabIndex = 3;
            this.monthLabel3.Text = "Березень:";
            // 
            // monthLabel2
            // 
            this.monthLabel2.AutoSize = true;
            this.monthLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel2.Location = new System.Drawing.Point(4, 69);
            this.monthLabel2.Name = "monthLabel2";
            this.monthLabel2.Size = new System.Drawing.Size(49, 13);
            this.monthLabel2.TabIndex = 2;
            this.monthLabel2.Text = "Лютий:";
            // 
            // monthLimit1
            // 
            this.monthLimit1.Location = new System.Drawing.Point(73, 38);
            this.monthLimit1.Name = "monthLimit1";
            this.monthLimit1.Size = new System.Drawing.Size(81, 20);
            this.monthLimit1.TabIndex = 1;
            this.monthLimit1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.monthLimit1.Leave += new System.EventHandler(this.monthLimit_Leave);
            // 
            // monthLabel1
            // 
            this.monthLabel1.AutoSize = true;
            this.monthLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.monthLabel1.Location = new System.Drawing.Point(4, 41);
            this.monthLabel1.Name = "monthLabel1";
            this.monthLabel1.Size = new System.Drawing.Size(49, 13);
            this.monthLabel1.TabIndex = 0;
            this.monthLabel1.Text = "Січень:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(725, 66);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(161, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Помісяцеві коефіцієнти";
            // 
            // priceTextBox
            // 
            this.priceTextBox.Location = new System.Drawing.Point(136, 39);
            this.priceTextBox.Name = "priceTextBox";
            this.priceTextBox.Size = new System.Drawing.Size(80, 20);
            this.priceTextBox.TabIndex = 3;
            this.priceTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priceTextBox.Leave += new System.EventHandler(this.priceTextBox_Leave);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label14.Location = new System.Drawing.Point(14, 42);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(119, 13);
            this.label14.TabIndex = 4;
            this.label14.Text = "Ціна за 1 кВт, грн.";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label15.Location = new System.Drawing.Point(223, 42);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 5;
            this.label15.Text = "Сумма, грн.";
            // 
            // totalMoneySumTextBox
            // 
            this.totalMoneySumTextBox.Location = new System.Drawing.Point(305, 39);
            this.totalMoneySumTextBox.Name = "totalMoneySumTextBox";
            this.totalMoneySumTextBox.Size = new System.Drawing.Size(100, 20);
            this.totalMoneySumTextBox.TabIndex = 6;
            this.totalMoneySumTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.totalMoneySumTextBox.Leave += new System.EventHandler(this.totalMoneySumTextBox_Leave);
            // 
            // calcLimitesButton
            // 
            this.calcLimitesButton.Location = new System.Drawing.Point(679, 37);
            this.calcLimitesButton.Name = "calcLimitesButton";
            this.calcLimitesButton.Size = new System.Drawing.Size(126, 23);
            this.calcLimitesButton.TabIndex = 7;
            this.calcLimitesButton.Text = "Виконати розрахунок";
            this.calcLimitesButton.UseVisualStyleBackColor = true;
            this.calcLimitesButton.Click += new System.EventHandler(this.calcLimitesButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.progInfoMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(994, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFileMenuItem,
            this.saveFileMenuItem,
            this.toolStripSeparator1,
            this.importDataMenuItem,
            this.toolStripSeparator2,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.fileToolStripMenuItem.Text = "Файл";
            // 
            // openFileMenuItem
            // 
            this.openFileMenuItem.Name = "openFileMenuItem";
            this.openFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.openFileMenuItem.Text = "Конфігурація";
            // 
            // saveFileMenuItem
            // 
            this.saveFileMenuItem.Name = "saveFileMenuItem";
            this.saveFileMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveFileMenuItem.Text = "Зберегти";
            this.saveFileMenuItem.Click += new System.EventHandler(this.saveFileMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.exitToolStripMenuItem.Text = "Вихід";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // progInfoMenuItem
            // 
            this.progInfoMenuItem.Name = "progInfoMenuItem";
            this.progInfoMenuItem.Size = new System.Drawing.Size(99, 20);
            this.progInfoMenuItem.Text = "Про програму";
            this.progInfoMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 346);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(601, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "Розрахувати коеффіцієнти";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tenderVolumeTextBox
            // 
            this.tenderVolumeTextBox.Enabled = false;
            this.tenderVolumeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tenderVolumeTextBox.Location = new System.Drawing.Point(573, 39);
            this.tenderVolumeTextBox.Name = "tenderVolumeTextBox";
            this.tenderVolumeTextBox.Size = new System.Drawing.Size(100, 20);
            this.tenderVolumeTextBox.TabIndex = 12;
            this.tenderVolumeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(413, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(157, 13);
            this.label2.TabIndex = 11;
            this.label2.Text = "Об\'єм закупівлі, тис. кВт";
            // 
            // enterVolume
            // 
            this.enterVolume.AutoSize = true;
            this.enterVolume.Location = new System.Drawing.Point(826, 41);
            this.enterVolume.Name = "enterVolume";
            this.enterVolume.Size = new System.Drawing.Size(129, 17);
            this.enterVolume.TabIndex = 13;
            this.enterVolume.Text = "Ввести об\'єм вручну";
            this.enterVolume.UseVisualStyleBackColor = true;
            this.enterVolume.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // importDataMenuItem
            // 
            this.importDataMenuItem.Name = "importDataMenuItem";
            this.importDataMenuItem.Size = new System.Drawing.Size(152, 22);
            this.importDataMenuItem.Text = "Імпорт даних";
            this.importDataMenuItem.Click += new System.EventHandler(this.importDataMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(994, 378);
            this.Controls.Add(this.enterVolume);
            this.Controls.Add(this.tenderVolumeTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.calcLimitesButton);
            this.Controls.Add(this.totalMoneySumTextBox);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.Text = "Калькулятор лімітів";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button saveSettingsButton;
        private System.Windows.Forms.TextBox monthLimit12;
        private System.Windows.Forms.TextBox monthLimit11;
        private System.Windows.Forms.TextBox monthLimit10;
        private System.Windows.Forms.TextBox monthLimit8;
        private System.Windows.Forms.TextBox monthLimit9;
        private System.Windows.Forms.Label monthLabel12;
        private System.Windows.Forms.Label monthLabel11;
        private System.Windows.Forms.Label monthLabel10;
        private System.Windows.Forms.Label monthLabel9;
        private System.Windows.Forms.Label monthLabel8;
        private System.Windows.Forms.TextBox monthLimit7;
        private System.Windows.Forms.Label monthLabel7;
        private System.Windows.Forms.TextBox monthLimit6;
        private System.Windows.Forms.TextBox monthLimit5;
        private System.Windows.Forms.TextBox monthLimit4;
        private System.Windows.Forms.TextBox monthLimit2;
        private System.Windows.Forms.TextBox monthLimit3;
        private System.Windows.Forms.Label monthLabel6;
        private System.Windows.Forms.Label monthLabel5;
        private System.Windows.Forms.Label monthLabel4;
        private System.Windows.Forms.Label monthLabel3;
        private System.Windows.Forms.Label monthLabel2;
        private System.Windows.Forms.TextBox monthLimit1;
        private System.Windows.Forms.Label monthLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox totalMoneySumTextBox;
        private System.Windows.Forms.Button calcLimitesButton;
        private System.Windows.Forms.Label selectedMeterName;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label coeffsSumLabel;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openFileMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem progInfoMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn NumColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn NameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn PowerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoefColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IsFixedCoeffColumn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tenderVolumeTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox enterVolume;
        private System.Windows.Forms.ToolStripMenuItem importDataMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    }
}

