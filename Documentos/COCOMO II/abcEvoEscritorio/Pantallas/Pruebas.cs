using ABCEvoEscritorio.Utiles;
using System.Configuration;

namespace ABCEvoEscritorio
{
    public partial class Pruebas : Form
    {

        Utiles.Utiles utiles = new Utiles.Utiles();
        

        public Pruebas()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string email = "jose@thewellness.group";
            
            ABCEvo.ABCEvo aBCEvo = new ABCEvo.ABCEvo(null);
            bool valido = aBCEvo.Login(email);
            MessageBox.Show(valido?"Si":"No");

        }


    }
}