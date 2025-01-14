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
    public partial class Frm_itemsEvo : Form
    {
        public Frm_itemsEvo(List<saleItem> si)
        {
            InitializeComponent();
            dgv.DataSource = si;
        }
    }
}
