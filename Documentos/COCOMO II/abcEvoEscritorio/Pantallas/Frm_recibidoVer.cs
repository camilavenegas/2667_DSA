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
    public partial class Frm_recibidoVer : Form
    {
        List<filaPago> it = new List<filaPago>();
        public Frm_recibidoVer(List<filaPago> items, string titulo)
        {
            InitializeComponent();
            this.Text = titulo;
            it = items;
            dgv.DataSource = it;
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

    }
}
