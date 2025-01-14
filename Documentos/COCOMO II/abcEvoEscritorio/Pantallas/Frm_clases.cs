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

namespace ABCEvoEscritorio
{
    public partial class Frm_clases : Form
    {
        List<clases> Lista = new List<clases>();
        MariaDB db = new MariaDB();

        public Frm_clases()
        {
            InitializeComponent();
        }

        private void Frm_clases_Load(object sender, EventArgs e)
        {


            this.Text = "CLASES -> ACTUALIZADAS - > " + DateTime.Now;
            cargue();
        }

        private void cargue()
        {
            Lista.Clear();
            Lista = db.GetClases();
            dgv.AutoGenerateColumns = false;
            dgv.DataSource = Lista;
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

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            QBLea qBLea = new QBLea();
            Lista = qBLea.QBLeaClases();

            foreach (var li in Lista)
            {
                clases cla = db.GetClase(li.idclase);
                if (li.idclase == cla.idclase)
                {
                    db.UpdateClase(li);
                }
                else
                {
                    db.InsertClase(li);
                }
            }

            Cursor.Current = Cursors.Default;
            cargue();



        }
    }
}
