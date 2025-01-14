using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
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
    public partial class Frm_tarjetas : Form
    {
        MariaDB db = new MariaDB();
        List<tarjetas> tar = new List<tarjetas>();

        public Frm_tarjetas()
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

        private void Frm_tarjetas_Load(object sender, EventArgs e)
        {
            //tar.Clear();
            //tar = db.LeerTarjetas();
            //if (tar.Count == 0) 
            //{
            //    tarjetas tt = new tarjetas();
            //    tar.Add(tt);
            //}
            //dgv.DataSource = tar;
            cargue();
        }

        private void cargue()
        {
            tar.Clear();
            tar = db.LeerTarjetas();
            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            if (tar.Count > 0)
            {
                foreach (tarjetas inv in tar)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = inv.evo;
                    row.Cells[1].Value = inv.qb;
                    dgv.Rows.Add(row);
                }
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void Frm_tarjetas_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.deleteTarjetas();
            foreach (DataGridViewRow dr in dgv.Rows)
            {
                if (dr.Cells[0].Value != null && dr.Cells[1].Value != null)
                {

                    db.InsertTarteja(dr.Cells[0].Value.ToString(), dr.Cells[1].Value.ToString());
                }
            }
        }
    }
}
