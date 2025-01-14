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
    public partial class Frm_responsables : Form
    {

        public Frm_responsables(List<responsible> resp)
        {
            InitializeComponent();
            dgv.DataSource = resp;
        }
    }
}
