namespace ABCEvoEscritorio.Pantallas
{
    partial class frm_items
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frm_items));
            panel1 = new Panel();
            button1 = new Button();
            panel2 = new Panel();
            dgv = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
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
            panel1.Controls.Add(button1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(666, 48);
            panel1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(31, 12);
            button1.Name = "button1";
            button1.Size = new Size(219, 23);
            button1.TabIndex = 1;
            button1.Text = "Actualizar Items desde QuickBooks";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 48);
            panel2.Name = "panel2";
            panel2.Size = new Size(666, 402);
            panel2.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(666, 402);
            dgv.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "Item";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 220;
            // 
            // Column2
            // 
            Column2.HeaderText = "Descripción";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            // 
            // Column3
            // 
            Column3.HeaderText = "iditem";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Visible = false;
            // 
            // panel3
            // 
            panel3.Controls.Add(sst);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 411);
            panel3.Name = "panel3";
            panel3.Size = new Size(666, 39);
            panel3.TabIndex = 2;
            panel3.Visible = false;
            // 
            // sst
            // 
            sst.Items.AddRange(new ToolStripItem[] { tstProgreso, tslabel });
            sst.Location = new Point(0, 3);
            sst.Name = "sst";
            sst.Size = new Size(666, 36);
            sst.TabIndex = 2;
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
            // frm_items
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(666, 450);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "frm_items";
            ShowInTaskbar = false;
            Text = "Listado de Items en QB";
            Load += frm_items_Load;
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
        private StatusStrip sst;
        private ToolStripProgressBar tstProgreso;
        private ToolStripStatusLabel tslabel;
        private Button button1;
        private DataGridView dgv;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
    }
}