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
    public partial class frm_items : Form
    {
        List<items> ven = new List<items>();
        MariaDB db = new MariaDB();
        QBLea qb = new QBLea();

        public frm_items()
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


        #region BARRA
        public void sstProgreso()
        {
            tstProgreso.PerformStep();
        }

        public void sstDatos(string texto, int maximo)
        {
            tslabel.Text = texto;
            tstProgreso.Value = 0;
            tstProgreso.Maximum = maximo;
            sst.Refresh();
        }

        private void sstInicie()
        {
            panel3.Visible = true;
            sst.Visible = true;
            tstProgreso.Visible = true;
            tstProgreso.Value = 0;
            tstProgreso.Step = 1;
            tslabel.Visible = true;
            tslabel.Text = "";
            sst.Refresh();
        }

        private void sstFinalice()
        {
            sst.Visible = false;
            tslabel.Visible = false;
            tstProgreso.Visible = false;
            panel3.Visible = false;
        }
        #endregion


        private void button1_Click(object sender, EventArgs e)
        {
            ven.Clear();
            ven = qb.QBLeaItems();
            sstDatos("GUARDANDO ITEMS", ven.Count);
            db.TrataInsertarItems(ven);
            cargue();

        }

        private void frm_items_Load(object sender, EventArgs e)
        {

            cargue();
        }

        private void cargue()
        {
            ven.Clear();
            ven = db.LeerItems();
            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            if (ven.Count > 0)
            {
                foreach (items inv in ven)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = inv.item;
                    row.Cells[1].Value = inv.descripcion;
                    row.Cells[2].Value = inv.iditem;
                    dgv.Rows.Add(row);
                }
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
                //dgv.DataSource = clientes;
            }
        }
    }
}
