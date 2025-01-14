namespace ABCEvoEscritorio.Pantallas
{
    partial class frmPrincipal
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            dgv = new DataGridView();
            Seleccionar = new DataGridViewCheckBoxColumn();
            saleDate = new DataGridViewTextBoxColumn();
            invoiceNumber = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            ammount = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewCheckBoxColumn();
            Column8 = new DataGridViewCheckBoxColumn();
            Column9 = new DataGridViewCheckBoxColumn();
            idSale = new DataGridViewTextBoxColumn();
            panel1 = new Panel();
            button3 = new Button();
            button2 = new Button();
            btActualizar = new Button();
            btnEnviar = new Button();
            label2 = new Label();
            label1 = new Label();
            btnLeer = new Button();
            dtpHasta = new DateTimePicker();
            dtpInicio = new DateTimePicker();
            panel2 = new Panel();
            sst = new StatusStrip();
            tstProgreso = new ToolStripProgressBar();
            tslabel = new ToolStripStatusLabel();
            panel3 = new Panel();
            contextMenuStrip2 = new ContextMenuStrip(components);
            irAInvoiceToolStripMenuItem = new ToolStripMenuItem();
            irAClienteToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            verItemsToolStripMenuItem = new ToolStripMenuItem();
            verPagosToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            eliminarToolStripMenuItem = new ToolStripMenuItem();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            sst.SuspendLayout();
            panel3.SuspendLayout();
            contextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // dgv
            // 
            dgv.Columns.AddRange(new DataGridViewColumn[] { Seleccionar, saleDate, invoiceNumber, Column3, Column6, Column1, Column2, ammount, Column4, Column5, Column10, Column11, Column7, Column8, Column9, idSale });
            dgv.Dock = DockStyle.Fill;
            dgv.Location = new Point(0, 0);
            dgv.Name = "dgv";
            dgv.RowTemplate.Height = 35;
            dgv.Size = new Size(1479, 370);
            dgv.TabIndex = 0;
            dgv.CellMouseClick += dgv_CellMouseClick;
            // 
            // Seleccionar
            // 
            Seleccionar.HeaderText = "";
            Seleccionar.MinimumWidth = 10;
            Seleccionar.Name = "Seleccionar";
            Seleccionar.Width = 50;
            // 
            // saleDate
            // 
            saleDate.DataPropertyName = "saleDate";
            saleDate.HeaderText = "Fecha";
            saleDate.MinimumWidth = 10;
            saleDate.Name = "saleDate";
            saleDate.ReadOnly = true;
            saleDate.Width = 90;
            // 
            // invoiceNumber
            // 
            invoiceNumber.DataPropertyName = "invoiceNumber";
            invoiceNumber.HeaderText = "Factura";
            invoiceNumber.MinimumWidth = 10;
            invoiceNumber.Name = "invoiceNumber";
            invoiceNumber.ReadOnly = true;
            invoiceNumber.Width = 80;
            // 
            // Column3
            // 
            Column3.HeaderText = "Cliente";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Width = 150;
            // 
            // Column6
            // 
            Column6.HeaderText = "CI / RUC";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            // 
            // Column1
            // 
            Column1.HeaderText = "Valor";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Width = 80;
            // 
            // Column2
            // 
            Column2.HeaderText = "IVA";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Width = 80;
            // 
            // ammount
            // 
            ammount.DataPropertyName = "ammount";
            ammount.HeaderText = "Total";
            ammount.MinimumWidth = 10;
            ammount.Name = "ammount";
            ammount.ReadOnly = true;
            ammount.Width = 80;
            // 
            // Column4
            // 
            Column4.HeaderText = "Pagado";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.Width = 80;
            // 
            // Column5
            // 
            Column5.HeaderText = "Local";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            // 
            // Column10
            // 
            Column10.HeaderText = "Responsable";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            // 
            // Column11
            // 
            Column11.HeaderText = "REP";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.Width = 50;
            // 
            // Column7
            // 
            Column7.HeaderText = "Cliente";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.Width = 60;
            // 
            // Column8
            // 
            Column8.HeaderText = "Items";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.Width = 60;
            // 
            // Column9
            // 
            Column9.HeaderText = "Pagos";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.Width = 60;
            // 
            // idSale
            // 
            idSale.DataPropertyName = "idSale";
            idSale.HeaderText = "IdSale";
            idSale.MinimumWidth = 10;
            idSale.Name = "idSale";
            idSale.ReadOnly = true;
            idSale.Width = 200;
            // 
            // panel1
            // 
            panel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(btActualizar);
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
            panel1.Size = new Size(1479, 82);
            panel1.TabIndex = 1;
            // 
            // button3
            // 
            button3.Location = new Point(28, 59);
            button3.Name = "button3";
            button3.Size = new Size(72, 23);
            button3.TabIndex = 12;
            button3.Text = "Marcar";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Image = Properties.Resources.qb16;
            button2.Location = new Point(495, 8);
            button2.Name = "button2";
            button2.Size = new Size(61, 52);
            button2.TabIndex = 11;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // btActualizar
            // 
            btActualizar.Image = Properties.Resources.check_1_1;
            btActualizar.Location = new Point(411, 8);
            btActualizar.Name = "btActualizar";
            btActualizar.Size = new Size(61, 52);
            btActualizar.TabIndex = 9;
            btActualizar.UseVisualStyleBackColor = true;
            btActualizar.Click += btActualizar_Click;
            // 
            // btnEnviar
            // 
            btnEnviar.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnEnviar.Enabled = false;
            btnEnviar.Location = new Point(1302, 7);
            btnEnviar.Margin = new Padding(2, 1, 2, 1);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.Size = new Size(147, 22);
            btnEnviar.TabIndex = 8;
            btnEnviar.Text = "Enviar seleccionados";
            btnEnviar.UseVisualStyleBackColor = true;
            btnEnviar.Visible = false;
            btnEnviar.Click += btnEnviar_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(274, 49);
            label2.Margin = new Padding(2, 0, 2, 0);
            label2.Name = "label2";
            label2.Size = new Size(16, 15);
            label2.TabIndex = 7;
            label2.Text = "al";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(263, 15);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(24, 15);
            label1.TabIndex = 6;
            label1.Text = "Del";
            // 
            // btnLeer
            // 
            btnLeer.Image = Properties.Resources.logoevo_1;
            btnLeer.ImageAlign = ContentAlignment.TopCenter;
            btnLeer.Location = new Point(126, 10);
            btnLeer.Margin = new Padding(2, 1, 2, 1);
            btnLeer.Name = "btnLeer";
            btnLeer.Size = new Size(133, 58);
            btnLeer.TabIndex = 5;
            btnLeer.Text = "Leer de EVO";
            btnLeer.TextAlign = ContentAlignment.BottomCenter;
            btnLeer.UseVisualStyleBackColor = true;
            btnLeer.Click += btnLeer_Click;
            // 
            // dtpHasta
            // 
            dtpHasta.CustomFormat = "dd/MM/yyyy";
            dtpHasta.Format = DateTimePickerFormat.Custom;
            dtpHasta.Location = new Point(294, 45);
            dtpHasta.Margin = new Padding(2, 1, 2, 1);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(95, 23);
            dtpHasta.TabIndex = 4;
            // 
            // dtpInicio
            // 
            dtpInicio.CustomFormat = "dd/MM/yyyy";
            dtpInicio.Format = DateTimePickerFormat.Custom;
            dtpInicio.Location = new Point(294, 11);
            dtpInicio.Margin = new Padding(2, 1, 2, 1);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(95, 23);
            dtpInicio.TabIndex = 3;
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 82);
            panel2.Name = "panel2";
            panel2.Size = new Size(1479, 370);
            panel2.TabIndex = 2;
            // 
            // sst
            // 
            sst.Items.AddRange(new ToolStripItem[] { tstProgreso, tslabel });
            sst.Location = new Point(0, 0);
            sst.Name = "sst";
            sst.Size = new Size(1479, 36);
            sst.TabIndex = 1;
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
            // panel3
            // 
            panel3.Controls.Add(sst);
            panel3.Dock = DockStyle.Bottom;
            panel3.Location = new Point(0, 416);
            panel3.Name = "panel3";
            panel3.Size = new Size(1479, 36);
            panel3.TabIndex = 3;
            panel3.Visible = false;
            // 
            // contextMenuStrip2
            // 
            contextMenuStrip2.Items.AddRange(new ToolStripItem[] { irAInvoiceToolStripMenuItem, irAClienteToolStripMenuItem, toolStripSeparator1, verItemsToolStripMenuItem, verPagosToolStripMenuItem, toolStripSeparator2, eliminarToolStripMenuItem });
            contextMenuStrip2.Name = "contextMenuStrip2";
            contextMenuStrip2.Size = new Size(132, 126);
            // 
            // irAInvoiceToolStripMenuItem
            // 
            irAInvoiceToolStripMenuItem.Name = "irAInvoiceToolStripMenuItem";
            irAInvoiceToolStripMenuItem.Size = new Size(131, 22);
            irAInvoiceToolStripMenuItem.Text = "Ir a Invoice";
            irAInvoiceToolStripMenuItem.Click += irAInvoiceToolStripMenuItem_Click;
            // 
            // irAClienteToolStripMenuItem
            // 
            irAClienteToolStripMenuItem.Name = "irAClienteToolStripMenuItem";
            irAClienteToolStripMenuItem.Size = new Size(131, 22);
            irAClienteToolStripMenuItem.Text = "Ir a Cliente";
            irAClienteToolStripMenuItem.Click += irAClienteToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(128, 6);
            // 
            // verItemsToolStripMenuItem
            // 
            verItemsToolStripMenuItem.Name = "verItemsToolStripMenuItem";
            verItemsToolStripMenuItem.Size = new Size(131, 22);
            verItemsToolStripMenuItem.Text = "Ver Items";
            verItemsToolStripMenuItem.Click += verItemsToolStripMenuItem_Click;
            // 
            // verPagosToolStripMenuItem
            // 
            verPagosToolStripMenuItem.Name = "verPagosToolStripMenuItem";
            verPagosToolStripMenuItem.Size = new Size(131, 22);
            verPagosToolStripMenuItem.Text = "Ver Pagos";
            verPagosToolStripMenuItem.Click += verPagosToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(128, 6);
            // 
            // eliminarToolStripMenuItem
            // 
            eliminarToolStripMenuItem.Name = "eliminarToolStripMenuItem";
            eliminarToolStripMenuItem.Size = new Size(131, 22);
            eliminarToolStripMenuItem.Text = "Eliminar";
            eliminarToolStripMenuItem.Click += eliminarToolStripMenuItem_Click;
            // 
            // button1
            // 
            button1.Image = Properties.Resources.Excel__2_;
            button1.Location = new Point(575, 10);
            button1.Name = "button1";
            button1.Size = new Size(61, 52);
            button1.TabIndex = 13;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click_1;
            // 
            // frmPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1479, 452);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2, 1, 2, 1);
            Name = "frmPrincipal";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VENTAS ABC EVO";
            WindowState = FormWindowState.Maximized;
            FormClosing += frmPrincipal_FormClosing;
            Load += frmPrincipal_Load;
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            sst.ResumeLayout(false);
            sst.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            contextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgv;
        private Panel panel1;
        private Button btnLeer;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpInicio;
        private Button btnEnviar;
        private Label label2;
        private Label label1;
        private Button button2;
        private Button btActualizar;
        private Panel panel2;
        private StatusStrip sst;
        private ToolStripProgressBar tstProgreso;
        private ToolStripStatusLabel tslabel;
        private Panel panel3;
        private Button button3;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem irAInvoiceToolStripMenuItem;
        private ToolStripMenuItem irAClienteToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem verItemsToolStripMenuItem;
        private ToolStripMenuItem verPagosToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem eliminarToolStripMenuItem;
        private DataGridViewCheckBoxColumn Seleccionar;
        private DataGridViewTextBoxColumn saleDate;
        private DataGridViewTextBoxColumn invoiceNumber;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn ammount;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewCheckBoxColumn Column7;
        private DataGridViewCheckBoxColumn Column8;
        private DataGridViewCheckBoxColumn Column9;
        private DataGridViewTextBoxColumn idSale;
        private Button button1;
    }
}