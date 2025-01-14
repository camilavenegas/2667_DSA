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
    public partial class Frm_templates : Form
    {
        List<templates> Lista = new List<templates>();
        MariaDB db = new MariaDB();
        public Frm_templates()
        {
            InitializeComponent();
        }

        private void Frm_templates_Load(object sender, EventArgs e)
        {
            this.Text = "TEMPLATES -> ACTUALIZADOS - > " + DateTime.Now;
            cargue();
        }

        private void cargue()
        {

            Lista.Clear();
            Lista = db.GetTemplates();

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
            Lista = qBLea.QBLeaTemplates();


            foreach (var li in Lista)
            {
                templates cla = db.GetTemplate(li.idtemplate);
                if (li.idtemplate == cla.idtemplate)
                {
                    db.UpdateTemplate(li);
                }
                else
                {
                    db.InsertTemplate(li);
                }
            }
            Cursor.Current = Cursors.Default;
            cargue();
        }
    }
}
