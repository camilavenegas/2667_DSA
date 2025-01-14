namespace ABCEvoEscritorio.Pantallas
{
    partial class Frm_localesEditar
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_localesEditar));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            btOk = new Button();
            texSucursal = new TextBox();
            texSecuencialInvoices = new TextBox();
            texSecuencialCreditos = new TextBox();
            cbInvoice = new ComboBox();
            cbCredito = new ComboBox();
            cbClase = new ComboBox();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            texTnombre = new TextBox();
            texTdns = new TextBox();
            texTtoken = new TextBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(37, 25);
            label1.Name = "label1";
            label1.Size = new Size(51, 15);
            label1.TabIndex = 0;
            label1.Text = "Sucursal";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(37, 65);
            label2.Name = "label2";
            label2.Size = new Size(96, 15);
            label2.TabIndex = 1;
            label2.Text = "Template Invoice";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(37, 109);
            label3.Name = "label3";
            label3.Size = new Size(97, 15);
            label3.TabIndex = 2;
            label3.Text = "Template Credito";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(37, 144);
            label4.Name = "label4";
            label4.Size = new Size(35, 15);
            label4.TabIndex = 3;
            label4.Text = "Clase";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(350, 65);
            label5.Name = "label5";
            label5.Size = new Size(109, 15);
            label5.TabIndex = 4;
            label5.Text = "Secuencial Invoices";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(350, 109);
            label6.Name = "label6";
            label6.Size = new Size(110, 15);
            label6.TabIndex = 5;
            label6.Text = "Secuencial Créditos";
            // 
            // btOk
            // 
            btOk.BackColor = SystemColors.ActiveCaption;
            btOk.Location = new Point(265, 344);
            btOk.Name = "btOk";
            btOk.Size = new Size(132, 52);
            btOk.TabIndex = 6;
            btOk.Text = "OK";
            btOk.UseVisualStyleBackColor = false;
            btOk.Click += btOk_Click;
            // 
            // texSucursal
            // 
            texSucursal.Location = new Point(103, 22);
            texSucursal.Name = "texSucursal";
            texSucursal.Size = new Size(167, 23);
            texSucursal.TabIndex = 7;
            // 
            // texSecuencialInvoices
            // 
            texSecuencialInvoices.Location = new Point(465, 62);
            texSecuencialInvoices.Name = "texSecuencialInvoices";
            texSecuencialInvoices.Size = new Size(97, 23);
            texSecuencialInvoices.TabIndex = 8;
            // 
            // texSecuencialCreditos
            // 
            texSecuencialCreditos.Location = new Point(466, 101);
            texSecuencialCreditos.Name = "texSecuencialCreditos";
            texSecuencialCreditos.Size = new Size(97, 23);
            texSecuencialCreditos.TabIndex = 9;
            // 
            // cbInvoice
            // 
            cbInvoice.FormattingEnabled = true;
            cbInvoice.Location = new Point(149, 62);
            cbInvoice.Name = "cbInvoice";
            cbInvoice.Size = new Size(182, 23);
            cbInvoice.TabIndex = 10;
            // 
            // cbCredito
            // 
            cbCredito.FormattingEnabled = true;
            cbCredito.Location = new Point(149, 106);
            cbCredito.Name = "cbCredito";
            cbCredito.Size = new Size(182, 23);
            cbCredito.TabIndex = 11;
            // 
            // cbClase
            // 
            cbClase.FormattingEnabled = true;
            cbClase.Location = new Point(149, 144);
            cbClase.Name = "cbClase";
            cbClase.Size = new Size(182, 23);
            cbClase.TabIndex = 12;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(texSucursal);
            groupBox1.Controls.Add(cbClase);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(cbCredito);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cbInvoice);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(texSecuencialCreditos);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(texSecuencialInvoices);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label6);
            groupBox1.Dock = DockStyle.Top;
            groupBox1.Location = new Point(0, 0);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(650, 183);
            groupBox1.TabIndex = 13;
            groupBox1.TabStop = false;
            groupBox1.Text = "Generales";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(texTtoken);
            groupBox2.Controls.Add(texTdns);
            groupBox2.Controls.Add(texTnombre);
            groupBox2.Controls.Add(label9);
            groupBox2.Controls.Add(label8);
            groupBox2.Controls.Add(label7);
            groupBox2.Dock = DockStyle.Top;
            groupBox2.Location = new Point(0, 183);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(650, 144);
            groupBox2.TabIndex = 14;
            groupBox2.TabStop = false;
            groupBox2.Text = "Token";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(37, 30);
            label7.Name = "label7";
            label7.Size = new Size(51, 15);
            label7.TabIndex = 13;
            label7.Text = "Nombre";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(37, 65);
            label8.Name = "label8";
            label8.Size = new Size(30, 15);
            label8.TabIndex = 14;
            label8.Text = "DNS";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(37, 103);
            label9.Name = "label9";
            label9.Size = new Size(38, 15);
            label9.TabIndex = 15;
            label9.Text = "Token";
            // 
            // texTnombre
            // 
            texTnombre.Location = new Point(115, 26);
            texTnombre.Name = "texTnombre";
            texTnombre.Size = new Size(241, 23);
            texTnombre.TabIndex = 16;
            // 
            // texTdns
            // 
            texTdns.Location = new Point(115, 61);
            texTdns.Name = "texTdns";
            texTdns.Size = new Size(241, 23);
            texTdns.TabIndex = 17;
            // 
            // texTtoken
            // 
            texTtoken.Location = new Point(115, 99);
            texTtoken.Name = "texTtoken";
            texTtoken.Size = new Size(241, 23);
            texTtoken.TabIndex = 18;
            // 
            // Frm_localesEditar
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(650, 410);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(btOk);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Frm_localesEditar";
            ShowInTaskbar = false;
            Text = "EDITAR LOCAL";
            Load += Frm_localesEditar_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button btOk;
        private TextBox texSucursal;
        private TextBox texSecuencialInvoices;
        private TextBox texSecuencialCreditos;
        private ComboBox cbInvoice;
        private ComboBox cbCredito;
        private ComboBox cbClase;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label9;
        private Label label8;
        private Label label7;
        private TextBox texTtoken;
        private TextBox texTdns;
        private TextBox texTnombre;
    }
}