using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCEvoEscritorio.Pantallas
{
    public partial class Frm_locales : Form
    {
        MariaDB db = new MariaDB();
        List<locales> locales = new List<locales>();
        public Frm_locales()
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

        private void toolStripButton1_Click(object sender, EventArgs e) //nuevo
        {
            locales loc = new locales();
            Frm_localesEditar frm_LocalesEditar = new Frm_localesEditar(loc);
            frm_LocalesEditar.ShowDialog();
            cargue();
        }

        private void Frm_locales_Leave(object sender, EventArgs e)
        {
            cargue();
        }

        private void cargue()
        {
            locales.Clear();
            locales = db.GetLocales();

            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            dgv.AllowUserToAddRows = true;
            if (locales.Count > 0)
            {
                foreach (locales inv in locales)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();

                    row.Cells[0].Value = inv.sucursal;
                    row.Cells[1].Value = inv.templaInvoice;
                    row.Cells[2].Value = inv.templaCredito;
                    row.Cells[3].Value = inv.clase;
                    row.Cells[4].Value = inv.numfactura;
                    row.Cells[5].Value = inv.numcredito;
                    row.Cells[6].Value = inv.idlocales;


                    dgv.Rows.Add(row);
                }
                dgv.AllowUserToAddRows = false;

                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);

            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)  //editar
        {
            locales loc = locales.Find(x => x.idlocales == Convert.ToUInt32(dgv.CurrentRow.Cells["idlocales"].Value));
            if (loc != null)
            {
                Frm_localesEditar frm_LocalesEditar = new Frm_localesEditar(loc);
                frm_LocalesEditar.ShowDialog();
                cargue();
            }
        }

        private void Frm_locales_Load(object sender, EventArgs e)
        {
            cargue();
        }

        private void toolStripDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Elimino local " + dgv.CurrentRow.Cells[0].Value.ToString(), "Esta Usted SEGURO ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                locales loc = locales.Find(x => x.idlocales == Convert.ToUInt32(dgv.CurrentRow.Cells["idlocales"].Value));
                if (loc != null)
                {
                    db.DeleteLocal(loc);
                    cargue();
                }
            }
        }
    }
}
