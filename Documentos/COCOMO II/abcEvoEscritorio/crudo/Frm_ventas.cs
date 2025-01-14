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
    public partial class Frm_ventas : Form
    {
        List<Sale> ventas = new List<Sale>();

        MariaDB db = new MariaDB();

        public Frm_ventas()
        {
            InitializeComponent();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            cargue();
        }

        private void Frm_ventas_Load(object sender, EventArgs e)
        {
            dtpInicio.Value = DateTime.Today.AddDays(-1);
            dtpHasta.Value = DateTime.Today;
        }

        private void cargue()
        {
            ventas.Clear();
            string inicio = dtpInicio.Value.ToString("yyyy-MM-dd");
            string fin = dtpHasta.Value.ToString("yyyy-MM-dd");
            ventas = db.GetVentas(inicio, fin);
            dgv.DataSource = ventas;

            //dgv.AutoGenerateColumns = false;
            //dgv.Rows.Clear();
            //if (ventas.Count > 0)
            //{
            //    foreach (Sale inv in ventas)
            //    {
            //        DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
            //        row.Cells[0].Value = inv.nombre;
            //        row.Cells[1].Value = inv.ruc;
            //        row.Cells[2].Value = inv.mail;
            //        row.Cells[3].Value = inv.telefono;
            //        row.Cells[4].Value = inv.direccion;
            //        row.Cells[5].Value = inv.ciudad;
            //        dgv.Rows.Add(row);
            //    }
            //    dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
            //    //dgv.DataSource = clientes;
            //}

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sale sale = ventas.ElementAt(dgv.CurrentRow.Index);
            List<saleItem> sl = sale.saleItens;
            Frm_itemsEvo frm_Items = new Frm_itemsEvo(sl);
            frm_Items.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sale sale = ventas.ElementAt(dgv.CurrentRow.Index);
            List<receivable> rc = sale.receivables;
            Frm_recibidoEvo frm_Recibido = new Frm_recibidoEvo(rc);
            frm_Recibido.ShowDialog();
        }
    }
}
