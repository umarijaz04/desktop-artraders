namespace AhmadSanitary
{
    partial class Order_List
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Order_List));
            this.cmbcus = new System.Windows.Forms.ComboBox();
            this.btnsubmit = new System.Windows.Forms.Button();
            this.btnclear = new System.Windows.Forms.Button();
            this.label32 = new System.Windows.Forms.Label();
            this.manufacturerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.avilableStockToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.cmbname = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtqty2 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.btnenter2 = new System.Windows.Forms.Button();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.cmbquality2 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.panelavailorder = new System.Windows.Forms.Panel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.btndel = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btndetail = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.cmbavailname = new System.Windows.Forms.ComboBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panelavailorder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbcus
            // 
            this.cmbcus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbcus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbcus.BackColor = System.Drawing.Color.White;
            this.cmbcus.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbcus.FormattingEnabled = true;
            this.cmbcus.Location = new System.Drawing.Point(360, 63);
            this.cmbcus.Name = "cmbcus";
            this.cmbcus.Size = new System.Drawing.Size(171, 21);
            this.cmbcus.TabIndex = 12;
            this.cmbcus.Enter += new System.EventHandler(this.cmbcus_Enter);
            this.cmbcus.Leave += new System.EventHandler(this.cmbcus_Leave);
            // 
            // btnsubmit
            // 
            this.btnsubmit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(178)))));
            this.btnsubmit.Enabled = false;
            this.btnsubmit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnsubmit.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnsubmit.ForeColor = System.Drawing.Color.White;
            this.btnsubmit.Location = new System.Drawing.Point(387, 556);
            this.btnsubmit.Name = "btnsubmit";
            this.btnsubmit.Size = new System.Drawing.Size(69, 25);
            this.btnsubmit.TabIndex = 13;
            this.btnsubmit.Text = "Submit";
            this.btnsubmit.UseVisualStyleBackColor = false;
            this.btnsubmit.Click += new System.EventHandler(this.btnsubmit_Click);
            // 
            // btnclear
            // 
            this.btnclear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(178)))));
            this.btnclear.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnclear.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnclear.ForeColor = System.Drawing.Color.White;
            this.btnclear.Location = new System.Drawing.Point(462, 556);
            this.btnclear.Name = "btnclear";
            this.btnclear.Size = new System.Drawing.Size(69, 25);
            this.btnclear.TabIndex = 14;
            this.btnclear.Text = "Clear";
            this.btnclear.UseVisualStyleBackColor = false;
            this.btnclear.Click += new System.EventHandler(this.btnclear_Click);
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.Color.White;
            this.label32.Location = new System.Drawing.Point(12, 27);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(174, 33);
            this.label32.TabIndex = 60;
            this.label32.Text = "Add Gate Pass";
            // 
            // manufacturerToolStripMenuItem
            // 
            this.manufacturerToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("manufacturerToolStripMenuItem.Image")));
            this.manufacturerToolStripMenuItem.Name = "manufacturerToolStripMenuItem";
            this.manufacturerToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.manufacturerToolStripMenuItem.Text = "Add";
            this.manufacturerToolStripMenuItem.Click += new System.EventHandler(this.addorderToolStripMenuItem_Click);
            // 
            // avilableStockToolStripMenuItem
            // 
            this.avilableStockToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("avilableStockToolStripMenuItem.Image")));
            this.avilableStockToolStripMenuItem.Name = "avilableStockToolStripMenuItem";
            this.avilableStockToolStripMenuItem.Size = new System.Drawing.Size(55, 20);
            this.avilableStockToolStripMenuItem.Text = "Edit";
            this.avilableStockToolStripMenuItem.Click += new System.EventHandler(this.avilableStockToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manufacturerToolStripMenuItem,
            this.avilableStockToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(543, 24);
            this.menuStrip1.TabIndex = 323;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // cmbname
            // 
            this.cmbname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbname.BackColor = System.Drawing.Color.White;
            this.cmbname.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbname.FormattingEnabled = true;
            this.cmbname.Location = new System.Drawing.Point(164, 114);
            this.cmbname.Name = "cmbname";
            this.cmbname.Size = new System.Drawing.Size(197, 21);
            this.cmbname.TabIndex = 7;
            this.cmbname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbname_KeyDown);
            this.cmbname.Leave += new System.EventHandler(this.cmbname_Leave);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(101, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 21);
            this.label7.TabIndex = 379;
            this.label7.Text = "Name:";
            // 
            // txtcode
            // 
            this.txtcode.BackColor = System.Drawing.Color.White;
            this.txtcode.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtcode.Location = new System.Drawing.Point(164, 90);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(197, 21);
            this.txtcode.TabIndex = 8;
            this.txtcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtcode_KeyDown);
            this.txtcode.Leave += new System.EventHandler(this.txtcode_Leave);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(107, 90);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 21);
            this.label8.TabIndex = 378;
            this.label8.Text = "Code:";
            // 
            // txtqty2
            // 
            this.txtqty2.BackColor = System.Drawing.Color.White;
            this.txtqty2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtqty2.Location = new System.Drawing.Point(62, 90);
            this.txtqty2.Name = "txtqty2";
            this.txtqty2.Size = new System.Drawing.Size(39, 21);
            this.txtqty2.TabIndex = 9;
            this.txtqty2.Text = "1";
            this.txtqty2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtqty2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtqty2_MouseClick);
            this.txtqty2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtqty2_KeyPress);
            this.txtqty2.Leave += new System.EventHandler(this.txtqty2_Leave);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(15, 90);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(41, 21);
            this.label9.TabIndex = 377;
            this.label9.Text = "Qty:";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeColumns = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView2.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView2.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(12, 141);
            this.dataGridView2.MultiSelect = false;
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView2.Size = new System.Drawing.Size(519, 409);
            this.dataGridView2.TabIndex = 376;
            this.dataGridView2.TabStop = false;
            this.dataGridView2.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellClick);
            this.dataGridView2.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView2_CellValueChanged);
            this.dataGridView2.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView2_EditingControlShowing);
            this.dataGridView2.Enter += new System.EventHandler(this.dataGridView2_Enter);
            this.dataGridView2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView2_KeyDown);
            // 
            // btnenter2
            // 
            this.btnenter2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(178)))));
            this.btnenter2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnenter2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnenter2.ForeColor = System.Drawing.Color.White;
            this.btnenter2.Location = new System.Drawing.Point(367, 114);
            this.btnenter2.Name = "btnenter2";
            this.btnenter2.Size = new System.Drawing.Size(69, 21);
            this.btnenter2.TabIndex = 10;
            this.btnenter2.Text = "Enter";
            this.btnenter2.UseVisualStyleBackColor = false;
            this.btnenter2.Click += new System.EventHandler(this.btnenter_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // cmbquality2
            // 
            this.cmbquality2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbquality2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbquality2.BackColor = System.Drawing.Color.White;
            this.cmbquality2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbquality2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbquality2.FormattingEnabled = true;
            this.cmbquality2.Items.AddRange(new object[] {
            "Customer",
            "Manufacturer",
            "Stakeholder"});
            this.cmbquality2.Location = new System.Drawing.Point(164, 63);
            this.cmbquality2.Name = "cmbquality2";
            this.cmbquality2.Size = new System.Drawing.Size(99, 21);
            this.cmbquality2.TabIndex = 11;
            this.cmbquality2.SelectedIndexChanged += new System.EventHandler(this.cmbquality2_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(82, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(76, 21);
            this.label5.TabIndex = 369;
            this.label5.Text = "A/c Type:";
            // 
            // label11
            // 
            this.label11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(269, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 21);
            this.label11.TabIndex = 368;
            this.label11.Text = "A/c Name:";
            // 
            // panelavailorder
            // 
            this.panelavailorder.Controls.Add(this.comboBox1);
            this.panelavailorder.Controls.Add(this.btndel);
            this.panelavailorder.Controls.Add(this.label4);
            this.panelavailorder.Controls.Add(this.btndetail);
            this.panelavailorder.Controls.Add(this.label10);
            this.panelavailorder.Controls.Add(this.cmbavailname);
            this.panelavailorder.Controls.Add(this.dataGridView3);
            this.panelavailorder.Controls.Add(this.label3);
            this.panelavailorder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelavailorder.Location = new System.Drawing.Point(0, 24);
            this.panelavailorder.Name = "panelavailorder";
            this.panelavailorder.Size = new System.Drawing.Size(543, 569);
            this.panelavailorder.TabIndex = 385;
            this.panelavailorder.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.comboBox1.BackColor = System.Drawing.Color.White;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Customer",
            "Manufacturer",
            "Stakeholder"});
            this.comboBox1.Location = new System.Drawing.Point(114, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(99, 21);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btndel
            // 
            this.btndel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btndel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btndel.Enabled = false;
            this.btndel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btndel.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold);
            this.btndel.ForeColor = System.Drawing.Color.White;
            this.btndel.Location = new System.Drawing.Point(383, 532);
            this.btndel.Name = "btndel";
            this.btndel.Size = new System.Drawing.Size(71, 25);
            this.btndel.TabIndex = 3;
            this.btndel.Text = "Delete";
            this.btndel.UseVisualStyleBackColor = false;
            this.btndel.Visible = false;
            this.btndel.Click += new System.EventHandler(this.btndel_Click);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(32, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 21);
            this.label4.TabIndex = 387;
            this.label4.Text = "A/c Type:";
            // 
            // btndetail
            // 
            this.btndetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(134)))), ((int)(((byte)(178)))));
            this.btndetail.Enabled = false;
            this.btndetail.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btndetail.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btndetail.ForeColor = System.Drawing.Color.White;
            this.btndetail.Location = new System.Drawing.Point(460, 532);
            this.btndetail.Name = "btndetail";
            this.btndetail.Size = new System.Drawing.Size(71, 25);
            this.btndetail.TabIndex = 2;
            this.btndetail.Text = " Detail";
            this.btndetail.UseVisualStyleBackColor = false;
            this.btndetail.Click += new System.EventHandler(this.btndetail_Click);
            // 
            // label10
            // 
            this.label10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 12.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(219, 42);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(85, 21);
            this.label10.TabIndex = 386;
            this.label10.Text = "A/c Name:";
            // 
            // cmbavailname
            // 
            this.cmbavailname.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cmbavailname.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cmbavailname.BackColor = System.Drawing.Color.White;
            this.cmbavailname.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbavailname.FormattingEnabled = true;
            this.cmbavailname.Location = new System.Drawing.Point(310, 42);
            this.cmbavailname.Name = "cmbavailname";
            this.cmbavailname.Size = new System.Drawing.Size(197, 21);
            this.cmbavailname.TabIndex = 1;
            this.cmbavailname.Enter += new System.EventHandler(this.cmbavailname_Enter);
            this.cmbavailname.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbavailname_KeyDown);
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.AllowUserToResizeColumns = false;
            this.dataGridView3.AllowUserToResizeRows = false;
            this.dataGridView3.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView3.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView3.BackgroundColor = System.Drawing.Color.White;
            this.dataGridView3.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Location = new System.Drawing.Point(12, 69);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView3.Size = new System.Drawing.Size(519, 457);
            this.dataGridView3.TabIndex = 67;
            this.dataGridView3.TabStop = false;
            this.dataGridView3.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView3_CellClick);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 20.25F, ((System.Drawing.FontStyle)(((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic)
                | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(7, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(234, 33);
            this.label3.TabIndex = 66;
            this.label3.Text = "Available Gate Pass";
            // 
            // Order_List
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(543, 593);
            this.Controls.Add(this.panelavailorder);
            this.Controls.Add(this.cmbquality2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnenter2);
            this.Controls.Add(this.cmbname);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtcode);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtqty2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.btnclear);
            this.Controls.Add(this.cmbcus);
            this.Controls.Add(this.btnsubmit);
            this.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Order_List";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gate Pass";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Order_List_FormClosing);
            this.Load += new System.EventHandler(this.Order_List_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panelavailorder.ResumeLayout(false);
            this.panelavailorder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnsubmit;
        private System.Windows.Forms.Button btnclear;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.ToolStripMenuItem manufacturerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem avilableStockToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ComboBox cmbcus;
        private System.Windows.Forms.ComboBox cmbname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtqty2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Button btnenter2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ComboBox cmbquality2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panelavailorder;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button btndel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btndetail;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cmbavailname;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label3;
    }
}