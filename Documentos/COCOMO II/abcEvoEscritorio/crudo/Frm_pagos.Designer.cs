namespace ABCEvoEscritorio.crudo
{
    partial class Frm_pagos
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
            sst = new StatusStrip();
            tstProgreso = new ToolStripProgressBar();
            tslabel = new ToolStripStatusLabel();
            dgv = new DataGridView();
            panel1 = new Panel();
            btnEnviar = new Button();
            label2 = new Label();
            label1 = new Label();
            btnLeer = new Button();
            dtpHasta = new DateTimePicker();
            dtpInicio = new DateTimePicker();
            btDetalles = new Button();
            sst.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // sst
            // 
            sst.Items.AddRange(new ToolStripItem[] { tstProgreso, tslabel });
            sst.Location = new Point(0, 414);
            sst.Name = "sst";
            sst.Size = new Size(800, 36);
            sst.TabIndex = 7;
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
            // dgv
            // 
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 42);
            dgv.Name = "dgv";
            dgv.RowTemplate.Height = 25;
            dgv.Size = new Size(800, 408);
            dgv.TabIndex = 5;
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(btDetalles);
            panel1.Controls.Add(btnEnviar);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnLeer);
            panel1.Controls.Add(dtpHasta);
            panel1.Controls.Add(dtpInicio);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(2, 1, 2, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 42);
            panel1.TabIndex = 6;
            // 
            // btnEnviar
            // 
            btnEnviar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEnviar.Enabled = false;
            btnEnviar.Location = new Point(2418, 7);
            btnEnviar.Margin = new Padding(2, 1, 2, 1);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(147, 22);
            btnEnviar.TabIndex = 8;
            btnEnviar.Text = "Enviar seleccionados";
            btnEnviar.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 10);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 7;
            label2.Text = "al";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 9);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 6;
            label1.Text = "Del";
            // 
            // btnLeer
            // 
            btnLeer.Location = new Point(281, 9);
            btnLeer.Margin = new Padding(2, 1, 2, 1);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(133, 22);
            btnLeer.TabIndex = 5;
            btnLeer.Text = "Leer Pagos";
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += btnLeer_Click;
            // 
            // dtpHasta
            // 
            dtpHasta.CustomFormat = "dd/MM/yyyy";
            dtpHasta.Format = DateTimePickerFormat.Custom;
            dtpHasta.Location = new Point(171, 10);
            dtpHasta.Margin = new Padding(2, 1, 2, 1);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(98, 23);
            dtpHasta.TabIndex = 4;
            // 
            // dtpInicio
            // 
            dtpInicio.CustomFormat = "dd/MM/yyyy";
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.Location = new Point(37, 10);
            dtpInicio.Margin = new Padding(2, 1, 2, 1);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(113, 23);
            dtpInicio.TabIndex = 3;
            // 
            // btDetalles
            // 
            btDetalles.Location = new Point(435, 9);
            btDetalles.Name = "btDetalles";
            btDetalles.Size = new Size(75, 23);
            btDetalles.TabIndex = 9;
            btDetalles.Text = "Detalles";
            btDetalles.UseVisualStyleBackColor = true;
            btDetalles.Click += btDetalles_Click;
            // 
            // Frm_pagos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(sst);
            Controls.Add(dgv);
            Controls.Add(panel1);
            Name = "Frm_pagos";
            Text = "Frm_pagos";
            sst.ResumeLayout(false);
            sst.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private StatusStrip sst;
        private ToolStripProgressBar tstProgreso;
        private ToolStripStatusLabel tslabel;
        private DataGridView dgv;
        private Panel panel1;
        private Button btnEnviar;
        private Label label2;
        private Label label1;
        private Button btnLeer;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpInicio;
        private Button btDetalles;
    }
}