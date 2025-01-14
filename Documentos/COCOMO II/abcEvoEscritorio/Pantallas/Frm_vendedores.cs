using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
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
    public partial class Frm_vendedores : Form
    {
        List<vendedores> ven = new List<vendedores>();
        MariaDB db = new MariaDB();

        public Frm_vendedores()
        {
            InitializeComponent();
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

        private void cargue()
        {
            ven.Clear();
            ven = db.LeerVendedores();
            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            if (ven.Count > 0)
            {
                foreach (vendedores inv in ven)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = inv.nick;
                    row.Cells[1].Value = inv.rep;
                    row.Cells[2].Value = inv.idEmployee;
                    row.Cells[3].Value = inv.idvendor;

                    dgv.Rows.Add(row);
                }
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            //BAJAR DE EVO
            ABCEvo.ABCEvo aBCEvo = new ABCEvo.ABCEvo(null);            

            List<Clases.Usuario> usuarios = new List<Clases.Usuario>();
            usuarios = aBCEvo.LeerUsuarios();
            foreach (Clases.Usuario item in usuarios)
            {
                if (item != null)
                {
                    //validar si existe ya la venta
                    filaUsuario filaUsuario = new();
                    filaUsuario = db.GetUsuario(item.idEmployee);
                    bool guardar = true;
                    if (filaUsuario.idEmployee == item.idEmployee)
                    {
                        //ya existe no agrego
                    }
                    else
                    {
                        Clases.filaUsuario fila = new();
                        fila.idEmployee = item.idEmployee;
                        fila.name = item.name;
                        fila.data = JsonConvert.SerializeObject(item);
                        guardar = db.InsertUsuario(fila);
                    }
                   
                }
            }

            //bajar de QB

            QBLea qb = new QBLea();
            ven.Clear();
            ven = qb.QBLeaVendedores();

            //ASIGNAR ID Y BUSCAR FALTANTES
            List<Usuario> nuevos = new List<Usuario>();
            foreach(Usuario usr in usuarios)
            {
                vendedores hay = ven.Find(x=>x.rep.ToUpper() == usr.name.ToUpper().Trim());
                if (hay != null)
                {
                    hay.idEmployee = usr.idEmployee;
                }
                else
                {
                    Usuario usnew = usr;
                    nuevos.Add(usnew);

                }
            }
            
            //SUBIR FALTANTES A qUICKbOOKS
            foreach (Usuario usr in nuevos)
            {
                if(qb.SubaVendor(usr))
                    qb.subaRep(usr);
            }



            //GUARDAR EN TABLA
            foreach (vendedores inv in ven)
            {
                vendedores hay = db.LeerVendedor(inv.rep);
                if (hay.idvendor == null)
                    db.InsertVendedor(inv);
                else
                    db.UpdateVendedor(inv);
            }

            ven.Clear();
            ven = db.LeerVendedores();

            Cursor.Current = Cursors.Default;
            cargue();

        }

        private void Frm_vendedores_Load(object sender, EventArgs e)
        {
            cargue();
        }
    }
}
