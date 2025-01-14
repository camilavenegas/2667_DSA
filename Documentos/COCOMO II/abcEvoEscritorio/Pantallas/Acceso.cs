using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using ABCEvoEscritorio.ABCEvo;

namespace ABCEvoEscritorio.Pantallas
{
    public partial class Acceso : Form
    {
        public Acceso()
        {
            InitializeComponent();

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {

            if (txtUsuario.Text != "")
            {
                btnEntrar.Enabled = false;
                btnEntrar.Text = "Validando...";
                //ABCEvo.ABCEvo aBCEvo = new ABCEvo.ABCEvo();
                //bool valido = aBCEvo.Login(txtUsuario.Text);
                if (true)
                {
                    //frmPrincipal frmPrincipal = new frmPrincipal();
                    //frmPrincipal.Show();
                    //this.Hide();
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Usuario no verificado", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    btnEntrar.Enabled = true;
                    btnEntrar.Text = "Entrar";
                }
            }
            else
            {
                MessageBox.Show("Llene todos los datos", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                btnEntrar.Enabled = true;
                btnEntrar.Text = "Entrar";
            }

        }

        private void txtUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                this.btnEntrar_Click(sender, e);
        }

        private void Acceso_Load(object sender, EventArgs e)
        {
            this.btnEntrar_Click(null, null);
        }
    }
}
