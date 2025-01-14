using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ABCEvoEscritorio.crudo
{
    public partial class Frm_pagos : Form
    {
        List<Clases.filaReceivable> receivables = new List<Clases.filaReceivable>();
        MariaDB db = new MariaDB();

        public Frm_pagos()
        {
            InitializeComponent();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            cargue();
        }

        private void cargue()
        {
            receivables.Clear();
            string inicio = dtpInicio.Value.ToString("yyyy-MM-dd");
            string fin = dtpHasta.Value.ToString("yyyy-MM-dd");
            receivables = db.GetReceivables(inicio, fin);
            dgv.DataSource = receivables;

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

        private void btDetalles_Click(object sender, EventArgs e)
        {
            List<receivable> lt = new List<receivable>();
            receivable rcd  = JsonConvert.DeserializeObject<receivable>(dgv.CurrentRow.Cells[4].Value.ToString());
            lt.Add(rcd);
            Frm_pagos_detalles frm_Pagos_Detalles = new Frm_pagos_detalles(lt);
            frm_Pagos_Detalles.Show();
        }
    }
}
