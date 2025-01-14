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
    public partial class Frm_miembros : Form

    {
        List<member> vagos = new List<member>();
        MariaDB db = new MariaDB();


        public Frm_miembros()
        {
            InitializeComponent();
        }

        private void btnLeer_Click(object sender, EventArgs e)
        {
            vagos.Clear();
            vagos = db.GetMembersl();
            dgv.DataSource = vagos;

        }

        private void responsable(object sender, EventArgs e)
        {
            member sale = vagos.ElementAt(dgv.CurrentRow.Index);
            List<responsible> sl = sale.responsibles;
            Frm_responsables frm_Responsables = new Frm_responsables(sl);
            frm_Responsables.ShowDialog();
        }


    }
}
