using ABCEvoEscritorio.crudo;
using ABCEvoEscritorio.Pantallas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCEvoEscritorio
{
    public partial class Frm_menu : Form
    {
        public Frm_menu()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)  // para jalar el Escape
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("INTEGRADOR ABC EVO\n\nVersión 2.0.2\n\n\nACOS3, La puerta del éxito\n\nPablo Rueda\nViviana Salavarria\n099-903-2375\n098-707-5002\n\nwww.apower.com.ec", "INTEGRADOR ABC EVO", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Frm_menu_Load(object sender, EventArgs e)
        {
            Acceso acceso = new Acceso();
            //acceso.ShowDialog();
            if (acceso.ShowDialog() == DialogResult.OK)
            {
                this.Text = "WHELLNESS GRP  ->  ABC EVO ";
            }
            else
            {
                this.Close();
            }
        }

        private void clasesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_clases menu = new Frm_clases();
            menu.ShowDialog();
        }

        private void templatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_templates menu = new Frm_templates();
            menu.ShowDialog();
        }

        private void ventasToolStripMenuItem_Click(object sender, EventArgs e)
        {



        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_members menua = new Frm_members();
            menua.ShowDialog();
        }

        private void principalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ABCEvoEscritorio.Pantallas.frmPrincipal frmPrincipal = new frmPrincipal();
            frmPrincipal.ShowDialog();

        }

        private void clientesQBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_clientes frm_Clientes = new Frm_clientes();
            frm_Clientes.ShowDialog();
        }

        private void verificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_ventas ventas = new Frm_ventas();
            ventas.ShowDialog();

        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frm_items items = new frm_items();
            items.ShowDialog();
        }

        private void formasDePagoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_tarjetas tarjetas = new Frm_tarjetas();
            tarjetas.ShowDialog();
        }

        private void vendedoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_vendedores frm_Vendedores = new Frm_vendedores();
            frm_Vendedores.ShowDialog();
        }

        private void sucursalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_locales frm_Locales = new Frm_locales();
            frm_Locales.ShowDialog();
        }

        private void verificarMemersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_miembros frm_Miembros = new Frm_miembros();
            frm_Miembros.ShowDialog();
        }

        private void verificarPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm_pagos frm_Pagos = new Frm_pagos();
            frm_Pagos.ShowDialog();
        }
    }
}
