using ABCEvoEscritorio.BDD;
using ABCEvoEscritorio.Clases;
using Mysqlx;
using Mysqlx.Crud;
using MySqlX.XDevAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using QBFC15Lib;
using Org.BouncyCastle.Crypto;
using static log4net.Appender.RollingFileAppender;


namespace ABCEvoEscritorio.Pantallas
{
    public partial class frmPrincipal : Form
    {
        List<filaSale> listado = new List<filaSale>();
        MariaDB mariaDB = new MariaDB();

        Boolean okclientes = true;
        Boolean okitems = true;
        QBLea qb = new QBLea();

        public frmPrincipal()
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

        private void cargue()
        {
            string inicio = dtpInicio.Value.ToString("yyyy-MM-dd");
            string fin = dtpHasta.Value.ToString("yyyy-MM-dd");
            listado = mariaDB.GetSales(inicio, fin);

            okclientes = true;
            okitems = true;
            DateTime inicia = DateTime.Now.AddSeconds(-50);


            verifique(true);

            if (!okclientes || !okitems)
            {
                sstInicie();
                if (!okclientes)
                {
                    sstDatos("Subiendo Nuevos Clientes", listado.Count);
                    qb.ActualiceClientes(inicia);
                }

                if (!okitems)
                {
                    sstDatos("Subiendo Nuevos Items", listado.Count);
                    qb.ActualiceItems(inicia);
                }
                sstFinalice();

                verifique(false);
            }
            dgv.AutoGenerateColumns = false;
            dgv.Rows.Clear();
            dgv.AllowUserToAddRows = true;
            if (listado.Count > 0)
            {
                foreach (filaSale inv in listado)
                {
                    DataGridViewRow row = (DataGridViewRow)dgv.Rows[0].Clone();
                    row.Cells[0].Value = false;
                    row.Cells[1].Value = inv.saleDate;
                    row.Cells[2].Value = inv.invoiceNumber;
                    row.Cells[3].Value = inv.customer;
                    row.Cells[4].Value = inv.document;
                    row.Cells[5].Value = inv.valor;
                    row.Cells[6].Value = inv.iva;
                    row.Cells[7].Value = inv.ammount;
                    row.Cells[8].Value = inv.ammountPaid;
                    row.Cells[9].Value = inv.sucursal;
                    row.Cells[10].Value = inv.responsable;
                    row.Cells[11].Value = inv.rep;
                    if (string.IsNullOrEmpty(inv.rep))
                        row.Cells[11].Style.BackColor = Color.Yellow;
                    row.Cells[12].Value = inv.VqbCliente;
                    row.Cells[13].Value = inv.VqbItems;
                    row.Cells[14].Value = inv.Vqbtarjetas;
                    row.Cells[15].Value = inv.idSale;


                    dgv.Rows.Add(row);
                }
                dgv.AllowUserToAddRows = false;

                dgv.Sort(dgv.Columns[0], ListSortDirection.Ascending);

            }

        }

        private void verifique(Boolean proceso)
        {
            sstInicie();


            sstDatos("Verificando Información", listado.Count);
            foreach (filaSale li in listado)
            {
                if (!li.VqbCliente)
                {
                    if (string.IsNullOrEmpty(li.responsable))
                    {
                        clientes cli = mariaDB.Leercliente(li.document);
                        if (cli.idcliente != null)
                        {
                            li.idcustomer = cli.idcliente;
                            li.VqbCliente = true;
                            mariaDB.UpdateVBCliente(li);
                        }
                        else
                        {
                            if (proceso)
                            {
                                filaMember clienteExiste = mariaDB.GetMember(li.idMember);
                                member clinue = JsonConvert.DeserializeObject<member>(clienteExiste.data);

                                qb.subaCliente(clinue);
                                okclientes = false;
                            }
                        }
                    }
                    else  //paganini
                    {
                        clientes cli = mariaDB.Leercliente(li.documentResponsable);
                        if (cli.idcliente != null)
                        {
                            li.idcustomer = cli.idcliente;
                            li.VqbCliente = true;
                            mariaDB.UpdateVBCliente(li);
                        }
                        else
                        {
                            if (proceso)
                            {
                                //busca paganinis

                                filaMember clienteExiste = mariaDB.GetMember(li.idMember);
                                member clinue = JsonConvert.DeserializeObject<member>(clienteExiste.data);
                                List<responsible> sl = clinue.responsibles;
                                if (sl != null && sl.Count > 0)
                                {
                                    foreach (responsible responsible in sl)
                                    {
                                        member pagani = new member();
                                        pagani.email = responsible.email;
                                        pagani.firstName = responsible.name;
                                        pagani.document = responsible.cpf;
                                        pagani.number = responsible.phone;
                                        pagani.address = clinue.address;
                                        qb.subaCliente(pagani);

                                    }
                                }

                                okclientes = false;
                            }
                        }
                    }
                    
                }

                if (!li.VqbItems)
                {
                    Boolean vbitems = true;
                    var it = JsonConvert.DeserializeObject<List<filaItem>>(li.items);
                    if (it != null)
                    {
                        foreach (filaItem si in it)
                        {
                            if (si.idQbItem == null)
                            {
                                if (si.item != null)
                                {
                                    if (si.item.Length > 31)
                                        si.item = si.item.Substring(0, 30);

                                    items ite = mariaDB.LeerItem(si.item.Trim());
                                    if (ite.iditem == null)
                                    {
                                        vbitems = false;
                                        qb.subaItem(si);
                                        okitems = false;
                                    }
                                    else
                                    {
                                        si.idQbItem = ite.iditem;
                                    }
                                }
                            }

                        }
                        li.VqbItems = vbitems;
                        li.items = JsonConvert.SerializeObject(it);
                        mariaDB.UpdateVBItem(li);
                    }
                }

                if (!li.Vqbtarjetas)
                {
                    Boolean vbitems = true;
                    var it = JsonConvert.DeserializeObject<List<filaPago>>(li.tarjetas);
                    if (it != null)
                    {
                        foreach (filaPago si in it)
                        {

                            if (string.IsNullOrEmpty(si.cardFlag))
                            {
                                tarjetas ite = mariaDB.LeerTarjeta("Transferencia");
                                if (ite.qb == null)
                                {
                                    vbitems = false;
                                }
                                else
                                {
                                    si.QbMethod = ite.qb;
                                }
                            }
                            else
                            {
                                tarjetas ite = mariaDB.LeerTarjeta(si.cardFlag);
                                if (ite.qb == null)
                                {
                                    vbitems = false;
                                }
                                else
                                {
                                    si.QbMethod = ite.qb;
                                }
                            }

                        }
                        li.Vqbtarjetas = vbitems;
                        li.tarjetas = JsonConvert.SerializeObject(it);
                        mariaDB.UpdateVBTarjeta(li);
                    }
                }

                if (string.IsNullOrEmpty(li.rep))
                {
                    if (li.idEmployee != 0)
                    {
                        string mrep = mariaDB.LeerNick(Convert.ToInt32(li.idEmployee));
                        li.rep = mrep;
                    }
                }
                sstProgreso();
            }


            sstFinalice();

        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            dtpInicio.Text = Properties.Settings.Default.dtpdesde.ToString();
            dtpHasta.Text = Properties.Settings.Default.dtphasta.ToString();
            cargue();
        }



        private void frmPrincipal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.dtpdesde = dtpInicio.Text;
            Properties.Settings.Default.dtphasta = dtpHasta.Text;
            Properties.Settings.Default.Save();
        }

        //este es el PODEROSO
        private void btnLeer_Click(object sender, EventArgs e)
        {
            sstInicie();
            sstDatos("Leyendo del Portal ABC EVO", 10);

            if (dtpInicio.Value > dtpHasta.Value)
            {
                MessageBox.Show("Rango de fechas incorrecto", "Atención",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
            }
            else
            {
                Cursor.Current = Cursors.WaitCursor;
                btnLeer.Text = "Buscando...";
                btnLeer.Enabled = false;
                btnEnviar.Enabled = false;
                string inicio = dtpInicio.Value.ToString("yyyy-MM-dd");
                string fin = dtpHasta.Value.ToString("yyyy-MM-dd");
                Int32 cuantos = 25;
                Int32 saltar = 0;
                List<locales> locales = new List<locales>();
                locales = mariaDB.GetLocales();

                foreach (locales local in locales)
                {
                    if (local.evo == "")
                    {
                        MessageBox.Show(local.sucursal + ": sin token", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }
                    tokenEvo token = null;
                    try
                    {
                        token = JsonConvert.DeserializeObject<tokenEvo>(local.evo);
                    }
                    catch (Exception)
                    {
                        MessageBox.Show(local.sucursal + ": error al leer el token", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }

                    ABCEvo.ABCEvo aBCEvo = new ABCEvo.ABCEvo(token);
                    List<Clases.Sale> sales = new List<Clases.Sale>();
                    sales = aBCEvo.LeerVentas(inicio, fin, cuantos, saltar);
                    if (sales.Count == 0)
                    {
                        MessageBox.Show(token.nombre + ": " + "No hay ventas registradas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //List<filaSale> listado = new List<filaSale>();
                        //this.dgv.DataSource = listado;
                    }
                    else
                    {

                        Int32 oks = 0;
                        Int32 errors = 0;
                        foreach (Clases.Sale item in sales)
                        {
                            if (item != null)
                            {
  

                                //validar si existe ya la venta
                                filaSale filaSale = new();
                                filaSale = mariaDB.GetSale(item.idSale, local.sucursal);
                                bool guardar = true;
                                if (filaSale.idSale == item.idSale)
                                {
                                    //ya existe no agrego
                                }
                                else
                                {
                                    Clases.filaSale fila = new();
                                    fila.idSale = item.idSale;
                                    fila.saleDate = item.saleDate;
                                    //fila.invoiceNumber = item.;
                                    string? invoiceNumber = "S/N";
                                    decimal? totalAmmount = 0;
                                    decimal? totalAmountPaid = 0;
                                    List<filaPago> pagos = new List<filaPago>();
                                    foreach (receivable rec in item.receivables)
                                    {

                                        totalAmmount += rec.ammount != null ? rec.ammount : 0;
                                        totalAmountPaid += rec.ammountPaid != null ? rec.ammountPaid : 0;
                                        if (rec.invoiceDetails != null)
                                        {
                                            foreach (invoiceDetail id in rec.invoiceDetails)
                                            {
                                                invoiceNumber = id.invoiceNumber != null ? id.invoiceNumber : invoiceNumber;
                                            }
                                        }

                                        filaPago fp = new filaPago();
                                        fp.ammount = rec.ammount;
                                        fp.ammountPaid = rec.ammountPaid;
                                        fp.cardFlag = rec.cardFlag;
                                        fp.authorization = rec.authorization;
                                        //fp.receivingDate = rec.receivingDate;
                                        fp.receivingDate = rec.registrationDate;

                                        pagos.Add(fp);

                                    }

                                    fila.ammount = totalAmmount;
                                    fila.ammountPaid = totalAmountPaid;
                                    fila.invoiceNumber = invoiceNumber; 


                                    decimal? valor = 0;
                                    decimal? iva = 0;
                                    List<filaItem> it = new List<filaItem>();
                                    foreach (saleItem si in item.saleItens)
                                    {
                                        filaItem fi = new filaItem();
                                        fi.description = si.description;
                                        fi.item = si.item;
                                        fi.itemValue = si.itemValue;
                                        fi.tax = si.tax;
                                        fi.saleValue = si.saleValue;
                                        fi.discount = si.discount;
                                        fi.quantity = si.quantity;
                                        it.Add(fi);

                                        valor += si.itemValue;
                                        iva += si.tax;

                                    }

                                    fila.valor = valor;
                                    fila.iva = iva;

                                    fila.idEmployee = item.idEmployeeSale;
                                    fila.idMember = item.idMember;
                                    fila.data = JsonConvert.SerializeObject(item);
                                    fila.items = JsonConvert.SerializeObject(it);
                                    fila.tarjetas = JsonConvert.SerializeObject(pagos);

                                    //clientes
                                    //busco si el cliente existe si no leo y guardo los datos


                                    filaMember clienteExiste = mariaDB.GetMember(item.idMember);
                                    if (clienteExiste == null || clienteExiste.idMember == null || clienteExiste.idMember == 0)
                                    {
                                        member clienteServer = new();
                                        clienteServer = aBCEvo.LeerCliente(item.idMember);
                                        if (clienteServer != null && clienteServer.idMember != null)
                                        {
                                            Clases.filaMember filaCli = new();
                                            filaCli.idMember = clienteServer.idMember;
                                            filaCli.document = clienteServer.document;
                                            filaCli.data = JsonConvert.SerializeObject(clienteServer);
                                            bool guardarCliente = mariaDB.InsertMember(filaCli);

                                            //busca paganinis
                                            List<responsible> sl = clienteServer.responsibles;
                                            if (sl != null && sl.Count > 0)
                                            {
                                                foreach (responsible responsible in sl)
                                                {
                                                    fila.responsable = responsible.name;
                                                    fila.documentResponsable = responsible.cpf;
                                                }
                                            }
                                            //TODO: si no se guarda el cliente
                                        }
                                    }
                                    filaMember cliente = mariaDB.GetMember(item.idMember);
                                    if (cliente != null && cliente.data != null)
                                    {
                                        fila.document = cliente.document;
                                        member datCliente = JsonConvert.DeserializeObject<member>(cliente.data);
                                        fila.customer = datCliente.firstName + " " + datCliente.lastName;
                                    }

                                    fila.sucursal = local.sucursal;

                                    guardar = mariaDB.InsertSale(fila);
                                }

                                //TODO: al final deberia leer de la base de datos
                                if (guardar)
                                {
                                    oks++;
                                }
                                else
                                {
                                    errors++;
                                }
                            }
                            else
                            {
                                errors++;
                            }
                        }

                        //MessageBox.Show(token.nombre + ": " + oks + " ventas guardadas, " + errors + " ventas no guardadas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        //if (oks > 0)
                        //{
                        //    List<filaSale> listado = new List<filaSale>();
                        //    listado = mariaDB.GetSales(inicio, fin);
                        //    this.dgv.DataSource = listado;
                        //    btnEnviar.Enabled = true;
                        //}
                        //else
                        //{
                        //    List<filaSale> listado = new List<filaSale>();
                        //    this.dgv.DataSource = listado;
                        //}


                    }

                    //nuevo pagos
                    ////////////////////
                    List<Clases.receivable> receivables = new List<Clases.receivable>();
                    receivables = aBCEvo.LeerPagos(inicio, fin, cuantos, saltar);
                    Int32 oks1 = 0;
                    Int32 errors1 = 0;
                    foreach (Clases.receivable item in receivables)
                    {
                        if (item != null)
                        {
                            //validar si existe ya la venta
                            filaReceivable filaReceivable = new();
                            filaReceivable = mariaDB.GetReceivable(item.idReceivable);

                            if (filaReceivable.idReceivable == item.idReceivable)
                            {
                                //ya existe no agrego
                                errors1++;

                            }
                            else
                            {
                                var shortDateValue = item.receivingDate.Value.ToShortDateString();
                                var shortDateValue1 = item.saleDate.Value.ToShortDateString();
                                if (shortDateValue != shortDateValue1)
                                {
                                    bool guardar = false;
                                    Clases.filaReceivable fila = new();
                                    fila.idSale = item.idSale;
                                    fila.idReceivable = item.idReceivable;
                                    fila.receivingDate = item.registrationDate;
                                    
                                    fila.data = JsonConvert.SerializeObject(item);
                                    guardar = mariaDB.InsertReceivable(fila);


                                    if (guardar)
                                    {
                                        oks1++;
                                    }
 
                                    //trepa para procesar 
                                    filaSale flp = new filaSale();
                                    flp.idSale = item.idSale + 9000000;
                                    flp.idMember = item.idMemberPayer;

                                    flp.saleDate = item.registrationDate;

                                    flp.ammount = 0;
                                    flp.ammountPaid = item.ammountPaid;
                                    flp.invoiceNumber = "PP";

                                    //flp.idEmployee = item.
                                    flp.idMember = item.idMemberPayer;
                                    flp.data = JsonConvert.SerializeObject(item);

                                    List<filaPago> pagos = new List<filaPago>();


                                    filaPago fp = new filaPago();
                                    fp.ammount = item.ammount;
                                    fp.ammountPaid = item.ammountPaid;
                                    fp.cardFlag = item.cardFlag;
                                    fp.authorization = item.authorization;
                                    fp.receivingDate = item.registrationDate;

                                    pagos.Add(fp);


                                    flp.tarjetas = JsonConvert.SerializeObject(pagos);
                                    flp.sucursal = local.sucursal;

                                    flp.idMember = item.idMemberPayer;
                                    filaMember clienteExiste = mariaDB.GetMember(flp.idMember);
                                    if (clienteExiste == null || clienteExiste.idMember == null || clienteExiste.idMember == 0)
                                    {
                                        member clienteServer = new();
                                        clienteServer = aBCEvo.LeerCliente(flp.idMember);
                                        if (clienteServer != null && clienteServer.idMember != null)
                                        {
                                            Clases.filaMember filaCli = new();
                                            filaCli.idMember = clienteServer.idMember;
                                            filaCli.document = clienteServer.document;
                                            filaCli.data = JsonConvert.SerializeObject(clienteServer);
                                            bool guardarCliente = mariaDB.InsertMember(filaCli);

                                            //busca paganinis
                                            List<responsible> sl = clienteServer.responsibles;
                                            if (sl != null && sl.Count > 0)
                                            {
                                                foreach (responsible responsible in sl)
                                                {
                                                    flp.responsable = responsible.name;
                                                    flp.documentResponsable = responsible.cpf;
                                                }
                                            }
                                            //TODO: si no se guarda el cliente
                                        }
                                    }
                                    filaMember cliente = mariaDB.GetMember(flp.idMember);
                                    if (cliente != null && cliente.data != null)
                                    {
                                        flp.document = cliente.document;
                                        member datCliente = JsonConvert.DeserializeObject<member>(cliente.data);
                                        flp.customer = datCliente.firstName + " " + datCliente.lastName;

                                    }




                                    guardar = mariaDB.InsertSale(flp);

                                }
                                else
                                {
                                    filaSale hay = mariaDB.GetSale(item.idSale, local.sucursal);
                                    if (hay.idSale == null) // no hay la venta
                                    {
                                        bool guardar = false;
                                        Clases.filaReceivable fila = new();
                                        fila.idSale = item.idSale;
                                        fila.idReceivable = item.idReceivable;
                                        fila.receivingDate = item.registrationDate;

                                        fila.data = JsonConvert.SerializeObject(item);
                                        guardar = mariaDB.InsertReceivable(fila);

                                        //trepa para procesar 
                                        filaSale flp = new filaSale();
                                        flp.idSale = item.idSale;
                                        flp.idMember = item.idMemberPayer;

                                        flp.saleDate = item.registrationDate;

                                        flp.valor = item.ammount;
                                        flp.ammount = item.ammount;
                                        flp.ammountPaid = item.ammountPaid;
                                        flp.invoiceNumber = "S/N";


                                        //decimal? valor = 0;
                                        //decimal? iva = 0;
                                        List<filaItem> it = new List<filaItem>();
  
                                            filaItem fi = new filaItem();
                                            fi.description = item.source;
                                            fi.item = item.source;


                                            fi.itemValue = item.ammount;
                                            //fi.tax = si.tax;
                                            //fi.saleValue = item.saleValue;
                                            //fi.discount = si.discount;
                                            //fi.quantity = si.quantity;
                                            it.Add(fi);

                                            //valor += si.itemValue;
                                            //iva += si.tax;

                                      

                                        //flp.idEmployee = item.id;
                                        flp.idMember = item.idMemberPayer;
                                        flp.data = JsonConvert.SerializeObject(item);
                                        flp.items = JsonConvert.SerializeObject(it);
                                      


                                        List<filaPago> pagos = new List<filaPago>();


                                        filaPago fp = new filaPago();
                                        fp.ammount = item.ammount;
                                        fp.ammountPaid = item.ammountPaid;
                                        fp.cardFlag = item.cardFlag;
                                        fp.authorization = item.authorization;
                                        //fp.receivingDate = item.registrationDate;
                                        fp.receivingDate = item.receivingDate;

                                        pagos.Add(fp);


                                        flp.tarjetas = JsonConvert.SerializeObject(pagos);
                                        flp.sucursal = local.sucursal;

                                        flp.idMember = item.idMemberPayer;
                                        filaMember clienteExiste = mariaDB.GetMember(flp.idMember);
                                        if (clienteExiste == null || clienteExiste.idMember == null || clienteExiste.idMember == 0)
                                        {
                                            member clienteServer = new();
                                            clienteServer = aBCEvo.LeerCliente(flp.idMember);
                                            if (clienteServer != null && clienteServer.idMember != null)
                                            {
                                                Clases.filaMember filaCli = new();
                                                filaCli.idMember = clienteServer.idMember;
                                                filaCli.document = clienteServer.document;
                                                filaCli.data = JsonConvert.SerializeObject(clienteServer);
                                                bool guardarCliente = mariaDB.InsertMember(filaCli);

                                                //busca paganinis
                                                List<responsible> sl = clienteServer.responsibles;
                                                if (sl != null && sl.Count > 0)
                                                {
                                                    foreach (responsible responsible in sl)
                                                    {
                                                        flp.responsable = responsible.name;
                                                        flp.documentResponsable = responsible.cpf;
                                                    }
                                                }
                                                //TODO: si no se guarda el cliente
                                            }
                                        }
                                        filaMember cliente = mariaDB.GetMember(flp.idMember);
                                        if (cliente != null && cliente.data != null)
                                        {
                                            flp.document = cliente.document;
                                            member datCliente = JsonConvert.DeserializeObject<member>(cliente.data);
                                            flp.customer = datCliente.firstName + " " + datCliente.lastName;

                                        }




                                        guardar = mariaDB.InsertSale(flp);

                                    }

                                }
   
                            }

                        }
 
                    }
                    //// MessageBox.Show(oks1 + " receivable guardadas, " + errors1 + " receivable no guardadas", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Information);



                }

                Cursor.Current = Cursors.Default;
                btnLeer.Text = "Leer de EVO";
                btnLeer.Enabled = true;
                sstFinalice();
                cargue();
            }
        }

        private void dgvComprobantes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            List<Comprobante> comprobantes = new();
            foreach (DataGridViewRow item in dgv.Rows)
            {

                if ((bool)item.Cells[0].Value)
                {
                    Comprobante comprobante = new();
                    comprobante = JObject.Parse((string)item.Cells[5].Value).ToObject<Comprobante>();
                    //leo el cliente
                    MariaDB mariaDB = new MariaDB();
                    filaMember fila = mariaDB.GetMember(comprobante.idMember);
                    if (fila != null)
                    {
                        member cliente = new member();
                        cliente = JObject.Parse(fila.data).ToObject<member>();
                        comprobante.Cliente = cliente;
                    }
                    comprobantes.Add(comprobante);
                }

            }
            //TODO: cada objeto cde comprobantes contiene toda la informacion de la venta y el cliente
            MessageBox.Show(comprobantes.Count.ToString() + " a exportar");

        }

        private void btActualizar_Click(object sender, EventArgs e)
        {
            cargue();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sstInicie();
            sstDatos("SUBIENDO A QUICKBOOKS", dgv.Rows.Count);
            QBLea qb = new QBLea();
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if ((bool)row.Cells[0].Value)
                {

                    filaSale esta = listado.Find(x => x.idSale == (Int32)row.Cells["idSale"].Value);
                    if (esta != null)
                    {
                        if (esta.VqbCliente & esta.VqbItems)
                        {
                            if (esta.invoiceNumber == "S/N")
                                qb.subaInvoice(esta);
                        }
                        if (esta.VqbCliente & esta.Vqbtarjetas)
                        {
                            if (esta.ammountPaid > 0)
                            {
                                qb.QBEscribirPayments(esta);
                            }
                        }
                    }
                }
                sstProgreso();
            }
            sstFinalice();
            cargue();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (button3.Text == "Marcar")
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Cells[0].Value = true;
                    button3.Text = "Desmarcar";
                }

            }
            else
            {
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    row.Cells[0].Value = false;
                    button3.Text = "Marcar";
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void dgv_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (e.RowIndex >= 0)
                {
                    if (e.ColumnIndex > -1)
                    {
                        dgv.CurrentCell = dgv[e.ColumnIndex, e.RowIndex]; //ubica el cursor en la celda
                        contextMenuStrip2.Show(Cursor.Position.X, Cursor.Position.Y); //muestra el menu ahi mismo
                    }
                }

            }
        }

        private void irAInvoiceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgv.CurrentRow.Cells["invoiceNumber"].Value.ToString() != "S/N")
            {
                string id = qb.QBdevolverIdFactura(dgv.CurrentRow.Cells["invoiceNumber"].Value.ToString());
                if (id != null)
                    qb.QBiraTransaccio(id, ENTxnDisplayModType.tdmtInvoice);
            }
        }

        private void irAClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filaSale fs = listado.Find(x => x.idSale == Convert.ToInt32(dgv.CurrentRow.Cells["idSale"].Value.ToString()));
            if (fs.idcustomer != null)
                qb.QBiraFicha(fs.idcustomer, ENListDisplayModType.ldmtCustomer);
        }

        private void verItemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filaSale fs = listado.Find(x => x.idSale == Convert.ToInt32(dgv.CurrentRow.Cells["idSale"].Value.ToString()));
            if (fs.customer != null)
            {
                string litext = fs.items;
                var it = JsonConvert.DeserializeObject<List<filaItem>>(litext);
                string titulo = "ITEMS -> " + fs.customer;
                Frm_itemsVer frm_ItemsVer = new Frm_itemsVer(it, titulo);
                frm_ItemsVer.ShowDialog();
            }
        }

        private void verPagosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filaSale fs = listado.Find(x => x.idSale == Convert.ToInt32(dgv.CurrentRow.Cells["idSale"].Value.ToString()));
            if (fs.customer != null)
            {
                string litext = fs.tarjetas;
                var it = JsonConvert.DeserializeObject<List<filaPago>>(litext);
                string titulo = "PAGOS -> " + fs.customer;
                Frm_recibidoVer frm_ItemsVer = new Frm_recibidoVer(it, titulo);
                frm_ItemsVer.ShowDialog();
            }
        }

        private void eliminarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Elimino Registro Número " + dgv.CurrentRow.Cells["invoiceNumber"].Value.ToString(), "Esta usted Seguro ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (dgv.CurrentRow.Cells["invoiceNumber"].Value.ToString() == "S/N")
                {
                    mariaDB.DelVenta(Convert.ToInt32(dgv.CurrentRow.Cells["idSale"].Value.ToString()));
                    cargue();
                }
                else
                {
                    MessageBox.Show("No puedo Eliminar registro transferido a Quick ", "Por favor...", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }


            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            sstInicie();
            Excel_Letras let = new Excel_Letras();

            var excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Workbooks.Add();
            Microsoft.Office.Interop.Excel._Worksheet ht = (Microsoft.Office.Interop.Excel.Worksheet)excelApp.ActiveSheet;

            int NFI = 1;
            String uc = "O";

            ht.Cells[NFI, 1] = "Información dse Ventas y Pagos EVO";
            string ER = "A1:" + uc + "2";

            ht.Range[ER].Font.Bold = true;
            NFI++;
            ht.Cells[NFI, 1] = DateTime.Now.ToString("d");
            NFI++;

            for (int i = 0; i < dgv.ColumnCount - 1; i++)
            {
                ht.Cells[NFI, i + 1] = dgv.Columns[i].HeaderText;
            }
            ER = "A2:" + uc + "3";
            //ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;
            ht.Range[ER].Font.Bold = true;


            NFI++;
            ER = "A3:" + uc + "3";
            ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;
            sstDatos("Copiando a Excel", dgv.Rows.Count);

            for (int i = 0; i < dgv.Rows.Count; i++)
            {
                DataGridViewRow presu = dgv.Rows[i];

                for (int j = 0; j < dgv.ColumnCount - 1; j++)
                {
                    if (presu.Cells[j].Value != null)
                    {
                        var YU = presu.Cells[j].Value.GetType().Name;
                        if (presu.Cells[j].Value.GetType().Name == "Boolean")
                        {
                            if ((bool)presu.Cells[j].Value == true)
                            {
                                ht.Cells[NFI, j + 1] = "SI";
                            }
                        }
                        else
                        {
                            if (presu.Cells[j].Value.GetType().Name == "DateTime")
                            {
                                if ((DateTime)presu.Cells[j].Value != DateTime.MinValue)
                                {
                                    ht.Cells[NFI, j + 1] = presu.Cells[j].Value.ToString().Substring(0, 10);
                                }
                            }
                            else
                            {
                                ht.Cells[NFI, j + 1] = presu.Cells[j].Value.ToString();
                            }
                        }


                    }

                }
                ER = "A" + NFI.ToString() + ":" + uc + NFI.ToString();
                ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;
                NFI++;
                sstProgreso();
            }

            ER = "A" + NFI.ToString() + ":" + uc + NFI.ToString();
            ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlEdgeTop].LineStyle = 7;



            ER = "A3:P" + (NFI - 1).ToString();
            ht.Range[ER].Borders[Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical].LineStyle = 7;

            //alto de las filas
            ER = "A2:" + uc + NFI.ToString();
            ht.Rows.Range[ER].RowHeight = 15;

            excelApp.Columns.AutoFit();

            ER = "A:A";
            ht.Columns.Range[ER].ColumnWidth = 11;
            ER = "L:N";
            ht.Columns.Range[ER].ColumnWidth = 16;

            ER = "A3:" + uc + "3";
            ht.Rows.Range[ER].RowHeight = 46;

            ER = "L3:L3";
            ht.Rows.Range[ER].EntireRow.WrapText = true;

            sstFinalice();

            excelApp.Visible = true;

            MessageBox.Show("Información fue transferida a Excel", "Tenga la bondad", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
