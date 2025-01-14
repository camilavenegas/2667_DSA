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

namespace ABCEvoEscritorio.crudo
{
    public partial class Frm_pagos_detalles : Form
    {
        List<receivable> rcd1 = new List<receivable>();
        public Frm_pagos_detalles(List<receivable> rcd)
        {
            InitializeComponent();
            this.rcd1 = rcd;

        }

        private void Frm_pagos_detalles_Load(object sender, EventArgs e)
        {
            dgv.AutoGenerateColumns = true;
            dgv.DataSource = rcd1;

        }
    }
}
