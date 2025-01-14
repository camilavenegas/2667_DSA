namespace ABCEvoEscritorio.Pantallas
{
    partial class Frm_members
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_members));
            panel1 = new Panel();
            btExcel = new Button();
            panel2 = new Panel();
            dgv = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            idmember = new DataGridViewTextBoxColumn();
            sst = new StatusStrip();
            tstProgreso = new ToolStripProgressBar();
            tslabel = new ToolStripStatusLabel();
            panel3 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            sst.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Controls.Add(btExcel);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(879, 75);
            panel1.TabIndex = 0;
            // 
            // btExcel
            // 
            btExcel.Image = Properties.Resources.Excel__2_;
            btExcel.Location = new Point(38, 3);
            btExcel.Name = "btExcel";
            btExcel.Size = new Size(75, 57);
            btExcel.TabIndex = 0;
            btExcel.UseVisualStyleBackColor = true;
            btExcel.Click += btExcel_Click;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 75);
            panel2.Name = "panel2";
            panel2.Size = new Size(879, 375);
            panel2.TabIndex = 1;
            // 
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3, Column4, Column5, Column6, idmember });
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(879, 375);
            dgv.TabIndex = 0;
            // 
            // Column1
            // 
            Column1.HeaderText = "nombres";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 140;
            // 
            // Column2
            // 
            Column2.HeaderText = "Apellidos";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 140;
            // 
            // Column3
            // 
            Column3.HeaderText = "Sucursal";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 140;
            // 
            // Column4
            // 
            Column4.HeaderText = "Ci /RUC";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            // 
            // Column5
            // 
            Column5.HeaderText = "Email";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.Width = 160;
            // 
            // Column6
            // 
            Column6.HeaderText = "Dirección";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.Width = 120;
            // 
            // idmember
            // 
            idmember.HeaderText = "idmember";
            idmember.Name = "idmember";
            idmember.ReadOnly = true;
            idmember.Visible = false;
            // 
            // sst
            // 
            sst.Dock = DockStyle.Fill;
            sst.Items.AddRange(new ToolStripItem[] { tstProgreso, tslabel });
            sst.Location = new Point(0, 0);
            sst.Name = "sst";
            sst.Size = new Size(879, 51);
            sst.TabIndex = 2;
            sst.Text = "statusStrip1";
            // 
            // tstProgreso
            // 
            tstProgreso.Name = "tstProgreso";
            tstProgreso.Size = new Size(500, 45);
            // 
            // tslabel
            // 
            tslabel.Name = "tslabel";
            tslabel.Size = new Size(117, 46);
            tslabel.Text = "LEYENDO FACTURAS";
            // 
            // panel3
            // 
            panel3.Controls.Add(sst);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 399);
            panel3.Name = "panel3";
            panel3.Size = new Size(879, 51);
            panel3.TabIndex = 3;
            panel3.Visible = false;
            // 
            // Frm_members
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(879, 450);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Frm_members";
            ShowInTaskbar = false;
            Text = "MIEMBROS";
            Load += Frm_members_Load;
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            sst.ResumeLayout(false);
            sst.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Button btExcel;
        private Panel panel2;
        private DataGridView dgv;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn idmember;
        private StatusStrip sst;
        private ToolStripProgressBar tstProgreso;
        private ToolStripStatusLabel tslabel;
        private Panel panel3;
    }
}