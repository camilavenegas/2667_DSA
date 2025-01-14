using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCEvoEscritorio.Clases
{

    public class items
    {
        public string iditem { get; set; }
        public string item { get; set; }
        public string descripcion { get; set; }
   
    }

    public class invoices
    {

        public string idinvoice { get; set; }
        public string numero { get; set; } //en QB
        public DateTime fecha { get; set; }

        public string nombre { get; set; }
        public double monto { get; set; }

        public double iva { get; set; }
        public double amount { get; set; }
        public string rep { get; set; }
        public string formapago { get; set; }

    }

    public class clases
    {
        public string idclase { get; set; }
        public string fullname { get; set; }
        public string name { get; set; }

    }

    public class templates
    {
        public string idtemplate { get; set; }
        public string name { get; set; }

    }

    public class clientes
    {
        public string idcliente { get; set; }
        public string nombre { get; set; }
        public string ruc { get; set; }
        public string direccion { get; set; }
        public string ciudad { get; set; }
        public string telefono { get; set; }
        public string mail { get; set; }
    }

    public class tarjetas
    {
        public string evo { get; set; }
        public string qb { get; set; }

    }


    public class vendedores
    {
        public string idvendor { get; set; }
        public string nick { get; set; }
        public string rep { get; set; }
        public Int32 idEmployee { get; set; }

    }

    public class locales
    {
        public Int32 idlocales { get; set; }
        public string sucursal { get; set; }
        public string templaInvoice { get; set; }
        public string templaCredito { get; set; }
        public string clase { get; set; }
        public Int32 numfactura { get; set; }
        public Int32 numcredito  { get; set; }

        public string? evo { get; set; }

    }
}
