using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
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
    public partial class Frm_localesEditar : Form
    {
        MariaDB db = new MariaDB();
        locales local = new locales();
        List<templates> invoices = new List<templates>();
        List<templates> creditos = new List<templates>();
        List<clases> clases = new List<clases>();
        tokenEvo tke = new tokenEvo();


        public Frm_localesEditar(locales loc)
        {
            InitializeComponent();
            local = loc;
            if (local.idlocales == 0) { this.Text = "NUEVO LOCAL"; } else { this.Text = "LOCAL " + local.sucursal; }
            invoices = db.GetTemplatesInvoices();
            creditos = db.GetTemplatesCreditos();
            clases = db.GetClases();
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

        private void Frm_localesEditar_Load(object sender, EventArgs e)
        {
            texSucursal.Text = local.sucursal;
            texSecuencialInvoices.Text = local.numfactura.ToString("D");
            texSecuencialCreditos.Text = local.numcredito.ToString("D");

            cbInvoice.DataSource = invoices;
            cbInvoice.DisplayMember = "name";
            if (local.templaInvoice != null)
                cbInvoice.Text = local.templaInvoice.ToString();

            cbCredito.DataSource = creditos;
            cbCredito.DisplayMember = "name";
            if (local.templaCredito != null)
                cbCredito.Text = local.templaCredito.ToString();

            cbClase.DataSource = clases;
            cbClase.DisplayMember = "fullname";
            if (local.clase != null)
                cbClase.Text = local.clase.ToString();
            
            
            if (!string.IsNullOrEmpty(local.evo))
            {
                tke = JsonConvert.DeserializeObject<tokenEvo>(local.evo);
                texTnombre.Text = tke.nombre;
                texTdns.Text = tke.dns;
                texTtoken.Text = tke.token;
            }

        }

        private void btOk_Click(object sender, EventArgs e)
        {
            local.sucursal = texSucursal.Text;
            local.numfactura = Convert.ToInt32(texSecuencialInvoices.Text);
            local.numcredito = Convert.ToInt32(texSecuencialCreditos.Text);

            local.templaInvoice = cbInvoice.Text;
            local.templaCredito = cbCredito.Text;
            local.clase = cbClase.Text;

            tke.nombre =  texTnombre.Text;
            tke.dns = texTdns.Text;
            tke.token = texTtoken.Text;
            local.evo = JsonConvert.SerializeObject(tke);

            if (local.idlocales == 0)
                db.InsertLocal(local);
            else
                db.UpdateLocal(local);

            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
