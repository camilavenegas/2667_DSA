using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ABCEvoEscritorio.Clases;
using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using System.Runtime.Intrinsics.X86;
using QBFC15Lib;


namespace ABCEvoEscritorio
{
    public partial class Frm_clientes : Form
    {
        List<clientes> clientes = new List<clientes>();
        MariaDB db = new MariaDB();

        public Frm_clientes()
        {
            InitializeComponent();
        }


        private void leerPsg()
        {
            clientes = db.Leerclientes();
            //clientes = db.LeerclientesDuplicados(); ;
            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            if (clientes.Count > 0)
            {
                foreach (clientes inv in clientes)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = inv.nombre;
                    row.Cells[1].Value = inv.ruc;
                    row.Cells[2].Value = inv.mail;
                    row.Cells[3].Value = inv.telefono;
                    row.Cells[4].Value = inv.direccion;
                    row.Cells[5].Value = inv.ciudad;
                    dgv.Rows.Add(row);
                }
                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);
                //dgv.DataSource = clientes;
            }

        }

        private void Frm_clientes_Load(object sender, EventArgs e)
        {
            leerPsg();
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


        #region BARRA
        public void sstProgreso()
        {
            tstProgreso.PerformStep();
        }

        public void sstDatos(string texto, int maximo)
        {
            tslabel.Text = texto;
            tstProgreso.Value = 0;
            tstProgreso.Maximum = maximo;
            sst.Refresh();
        }

        private void sstInicie()
        {
            panel3.Visible = true;
            sst.Visible = true;
            tstProgreso.Visible = true;
            tstProgreso.Value = 0;
            tstProgreso.Step = 1;
            tslabel.Visible = true;
            tslabel.Text = "";
            sst.Refresh();
        }

        private void sstFinalice()
        {
            sst.Visible = false;
            tslabel.Visible = false;
            tstProgreso.Visible = false;
            panel3.Visible = false;
        }
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            leaClientes(0,0);
        }

        private void leaClientes(int cuales, int cuantos)
        {
            sstInicie();
            sstDatos("Leyendo Clientes desde QuickBooks", 0);

            QBSessionManager sessionQB1 = new QBSessionManager();
            sessionQB1.OpenConnection2("", "Pichurco", ENConnectionType.ctLocalQBD);
            sessionQB1.BeginSession("", ENOpenMode.omDontCare);
            IMsgSetRequest requestMsgSet1 = sessionQB1.CreateMsgSetRequest("US", 11, 0);
            requestMsgSet1.Attributes.OnError = ENRqOnError.roeContinue;

            ICustomerQuery custUltimos = requestMsgSet1.AppendCustomerQueryRq();
            if (cuales != 0)
                custUltimos.ORCustomerListQuery.CustomerListFilter.FromModifiedDate.SetValue(DateTime.Now.AddDays(- cuantos), true);

            IMsgSetResponse resp1 = sessionQB1.DoRequests(requestMsgSet1);
            sessionQB1.EndSession();
            sessionQB1.CloseConnection();

            IResponse respuesta1 = resp1.ResponseList.GetAt(0);
            {
                clientes.Clear();
                if (respuesta1.StatusCode == 0)
                {
                    //pone en clis ids y edits

                    ICustomerRetList Customer = respuesta1.Detail as ICustomerRetList;
                    sstDatos("Actualizando Clientes desde QuickBooks (" + Customer.Count.ToString() + ")", Customer.Count);
                    for (int i = 0; i < Customer.Count; i++)
                    {
                        clientes itemNew = new clientes();
                        ICustomerRet customerqb = Customer.GetAt(i);
                        itemNew.idcliente = customerqb.ListID.GetValue();
                        itemNew.nombre = customerqb.Name.GetValue();
                        if (customerqb.Email != null) itemNew.mail = customerqb.Email.GetValue();
                        if (customerqb.AccountNumber != null) itemNew.ruc = customerqb.AccountNumber.GetValue();
                        if (customerqb.Phone != null) itemNew.telefono = customerqb.Phone.GetValue();
                        if (customerqb.ShipAddressBlock != null)
                        {
                            if (customerqb.ShipAddressBlock.Addr1 != null)
                                itemNew.direccion = customerqb.ShipAddressBlock.Addr1.GetValue();
                        }
                        if (customerqb.JobTitle != null) itemNew.ciudad = customerqb.JobTitle.GetValue();
                        clientes.Add(itemNew);
                        sstProgreso();
                    }
                    sstDatos("Guardando Clientes en DB (" + clientes.Count.ToString() + ")", clientes.Count);

                    db.TrataInsertarClientes(clientes);
                    //foreach (var cliente in clientes)
                    //{
                    //    db.InsertarClientes(cliente);
                    //    sstProgreso();
                    //}
                    sstFinalice();
                }
                leerPsg();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ImputDialog id = new ImputDialog();
            string valorIngresado = id.ShowDialog("Número de dias que recupero", "Por favor");

            if (int.TryParse(valorIngresado, out int numero))
            {
                leaClientes(1,numero);
            }
            else
            {
                MessageBox.Show("Por favor, ingresa un número válido.");
            }

           
        }
    }
}
