namespace ABCEvoEscritorio
{
    partial class Frm_clientes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_clientes));
            panel1 = new Panel();
            button2 = new Button();
            button1 = new Button();
            panel2 = new Panel();
            dgv = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            panel3 = new Panel();
            sst = new StatusStrip();
            tstProgreso = new ToolStripProgressBar();
            tslabel = new ToolStripStatusLabel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel3.SuspendLayout();
            sst.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 55);
            panel1.TabIndex = 0;
            // 
            // button2
            // 
            button2.Location = new Point(38, 12);
            button2.Name = "button2";
            button2.Size = new Size(217, 23);
            button2.TabIndex = 1;
            button2.Text = "Actualizar ultimos";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.Location = new Point(476, 12);
            button1.Name = "button1";
            button1.Size = new Size(217, 23);
            button1.TabIndex = 0;
            button1.Text = "Coipiar TODO desde QuickBooks";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 55);
            panel2.Name = "panel2";
            panel2.Size = new Size(800, 395);
            panel2.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6 });
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(800, 395);
            dgv.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Cliente";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            // 
            // Column2
            // 
            Column2.HeaderText = "RUC";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "Mail";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            // 
            // Column4
            // 
            Column4.HeaderText = "Telefono";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Direccion";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column6
            // 
            Column6.HeaderText = "Ciudad";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // panel3
            // 
            panel3.Controls.Add(sst);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 408);
            panel3.Name = "panel3";
            panel3.Size = new Size(800, 42);
            panel3.TabIndex = 2;
            panel3.Visible = false;
            // 
            // sst
            // 
            sst.Items.AddRange(new ToolStripItem[] { tstProgreso, tslabel });
            sst.Location = new Point(0, 6);
            sst.Name = "sst";
            sst.Size = new Size(800, 36);
            sst.TabIndex = 0;
            sst.Text = "statusStrip1";
            // 
            // tstProgreso
            // 
            tstProgreso.Name = "tstProgreso";
            tstProgreso.Size = new Size(500, 30);
            // 
            // tslabel
            // 
            tslabel.Name = "tslabel";
            tslabel.Size = new Size(117, 31);
            tslabel.Text = "LEYENDO FACTURAS";
            // 
            // Frm_clientes
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Frm_clientes";
            ShowInTaskbar = false;
            Text = "CLIENTES EN QUICKBOOKS";
            Load += Frm_clientes_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            sst.ResumeLayout(false);
            sst.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Button button1;
        private StatusStrip sst;
        private ToolStripProgressBar tstProgreso;
        private ToolStripStatusLabel tslabel;
        private DataGridView dgv;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private Button button2;
    }
}