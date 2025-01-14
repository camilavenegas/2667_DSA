
using ABCEvoEscritorio.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Pkcs;
using Newtonsoft.Json;
using ABCEvoEscritorio.BDD;
using Microsoft.Office.Interop.Excel;
using QBFC15Lib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.Logging;

namespace ABCEvoEscritorio
{
    internal class QBLea
    {

        #region BASE
        public bool QBVerificar()
        {
            bool estabaierto = true;
            QBFC15Lib.QBSessionManager sessionQB = new QBFC15Lib.QBSessionManager();
            sessionQB.OpenConnection2("", "Apower", ENConnectionType.ctLocalQBD);
            try
            {
                sessionQB.BeginSession("", ENOpenMode.omDontCare);

            }
            catch (Exception eq)
            {
                MessageBox.Show("No pude iniciar sesión en QuickBooks\r" + eq.Message, "Disculpe Usted", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                estabaierto = false;
                System.Windows.Forms.Application.Exit();
            }

            return estabaierto;
        }

        public QBSessionManager QBAbrir()
        {

            QBSessionManager sessionQB = new QBSessionManager();
            sessionQB.OpenConnection2("", "Apower", ENConnectionType.ctLocalQBD);
            sessionQB.BeginSession("", ENOpenMode.omDontCare);
            return sessionQB;
        }

        #endregion

        #region LEER TABLAS
        public List<invoices> QBLeaInvoices(DateTime desde, DateTime hasta)
        {
            List<invoices> Lista = new List<invoices>();

            QBSessionManager sessionQB = new QBSessionManager();
            sessionQB.OpenConnection2("", "Apower", ENConnectionType.ctLocalQBD);
            sessionQB.BeginSession("", ENOpenMode.omDontCare);

            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

            var invoices = requestMsgSet.AppendInvoiceQueryRq();
            //invoices.IncludeLineItems.SetValue(true);
            invoices.OwnerIDList.Add("0");

            invoices.ORInvoiceQuery.InvoiceFilter.ORDateRangeFilter.TxnDateRangeFilter.ORTxnDateRangeFilter.TxnDateFilter.FromTxnDate.SetValue(desde);
            invoices.ORInvoiceQuery.InvoiceFilter.ORDateRangeFilter.TxnDateRangeFilter.ORTxnDateRangeFilter.TxnDateFilter.ToTxnDate.SetValue(hasta);


            IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            IResponse respuesta = resp.ResponseList.GetAt(0);

            if (respuesta.StatusCode == 0)
            {

                IInvoiceRetList Items = respuesta.Detail as IInvoiceRetList;

                for (int i = 0; i < Items.Count; i++)
                {
                    IInvoiceRet itemqb = Items.GetAt(i);

                    invoices item = new invoices();



                    item.idinvoice = itemqb.TxnID.GetValue();
                    item.numero = itemqb.RefNumber.GetValue().ToString();
                    item.fecha = itemqb.TxnDate.GetValue();
                    item.nombre = itemqb.CustomerRef.FullName.GetValue();

                    item.monto = itemqb.Subtotal.GetValue();
                    item.iva = itemqb.SalesTaxTotal.GetValue();
                    item.amount = item.monto + item.iva;


                    if (itemqb.SalesRepRef != null)
                    {
                        item.rep = itemqb.SalesRepRef.FullName.GetValue();

                    }

                    if (itemqb.DataExtRetList != null)
                    {
                        for (int j = 0; itemqb.DataExtRetList.Count > j; j++)
                        {
                            var JE = itemqb.DataExtRetList.GetAt(j);
                            if (JE.DataExtName.GetValue() == "FORMA DE PAGO")
                                item.formapago = JE.DataExtValue.GetValue();
                        }
                    }








                    Lista.Add(item);
                }

            }

            return Lista;
        }


        public List<clases> QBLeaClases()
        {
            List<clases> Lista = new List<clases>();
            if (QBVerificar())
            {
                try
                {


                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 11, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                    IClassQuery clases = requestMsgSet.AppendClassQueryRq();
                    clases.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);


                    IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                    sessionQB.EndSession();
                    sessionQB.CloseConnection();

                    IResponse respuesta = resp.ResponseList.GetAt(0);

                    if (respuesta.StatusCode == 0)
                    {

                        IClassRetList Items = respuesta.Detail as IClassRetList;

                        for (int i = 0; i < Items.Count; i++)
                        {
                            IClassRet itemqb = Items.GetAt(i);
                            if (itemqb.FullName.GetValue().Contains("PHISIQUE"))
                            {

                                clases item = new clases();

                                item.fullname = itemqb.FullName.GetValue();
                                item.idclase = itemqb.ListID.GetValue();
                                item.name = itemqb.Name.GetValue();
                                Lista.Add(item);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show("Error al leer Clases " + e.Message);
                }
            }

            return Lista;
        }


        public List<templates> QBLeaTemplates()
        {
            List<templates> Lista = new List<templates>();
            if (QBVerificar())
            {
                try
                {


                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 11, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                    ITemplateQuery templa = requestMsgSet.AppendTemplateQueryRq();


                    IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                    sessionQB.EndSession();
                    sessionQB.CloseConnection();

                    IResponse respuesta = resp.ResponseList.GetAt(0);

                    if (respuesta.StatusCode == 0)
                    {

                        ITemplateRetList Items = respuesta.Detail as ITemplateRetList;

                        for (int i = 0; i < Items.Count; i++)
                        {
                            ITemplateRet itemqb = Items.GetAt(i);
                            if (itemqb.IsActive.GetValue() == true)
                            {
                                if (itemqb.TemplateType.GetValue() == ENTemplateType.tttInvoice || itemqb.TemplateType.GetValue() == ENTemplateType.tttCreditMemo)
                                {
                                    templates item = new templates();


                                    item.name = itemqb.Name.GetValue();
                                    item.idtemplate = itemqb.ListID.GetValue();
                                    Lista.Add(item);
                                }
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show("Error al leer Templates " + e.Message);
                }
            }

            return Lista;
        }


        public List<items> QBLeaItems()
        {

            List<items> Lista = new List<items>();
            if (QBVerificar())
            {
                try
                {

                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;


                    IItemQuery grccuentas = requestMsgSet.AppendItemQueryRq();
                    grccuentas.OwnerIDList.Add("0");
                    grccuentas.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);

                    IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                    IResponse respuesta = resp.ResponseList.GetAt(0);
                    sessionQB.EndSession();
                    sessionQB.CloseConnection();

                    if (respuesta.StatusCode == 0)
                    {
                        IORItemRetList Items = respuesta.Detail as IORItemRetList;

                        for (int i = 0; i < Items.Count; i++)
                        {

                            IORItemRet itemGqb = Items.GetAt(i);



                            if (itemGqb.ItemServiceRet != null)
                            {
                                var itemqb = itemGqb.ItemServiceRet;

                                items it = new items();
                                it.iditem = itemqb.ListID.GetValue();
                                it.item = itemqb.Name.GetValue();

                                if (itemqb.ORSalesPurchase.SalesOrPurchase != null)
                                {
                                    if (itemqb.ORSalesPurchase.SalesOrPurchase.Desc != null)
                                        it.descripcion = itemqb.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();

                                }

                                Lista.Add(it);
                            }


                        }
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al leer Items " + e.Message);
                }
            }
            return Lista;
        }

        public List<vendedores> QBLeaVendedores()
        {
            List<vendedores> Lista = new List<vendedores>();

            if (QBVerificar())
            {
                try
                {


                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 11, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                    var templa = requestMsgSet.AppendSalesRepQueryRq();


                    IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                    sessionQB.EndSession();
                    sessionQB.CloseConnection();

                    IResponse respuesta = resp.ResponseList.GetAt(0);

                    if (respuesta.StatusCode == 0)
                    {

                        ISalesRepRetList Items = respuesta.Detail as ISalesRepRetList;

                        for (int i = 0; i < Items.Count; i++)
                        {
                            ISalesRepRet itemqb = Items.GetAt(i);
                            if (itemqb.IsActive.GetValue() == true)
                            {

                                vendedores item = new vendedores();


                                item.idvendor = itemqb.ListID.GetValue();
                                item.nick = itemqb.Initial.GetValue();
                                item.rep = itemqb.SalesRepEntityRef.FullName.GetValue();
                                Lista.Add(item);
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    MessageBox.Show("Error al leer Vendedores " + e.Message);
                }
            }

            return Lista;
        }

        public void ActualiceClientes(DateTime desde)
        {
            MariaDB db = new MariaDB();
            QBSessionManager sessionQB = QBAbrir();
            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

            ICustomerQuery custUltimos = requestMsgSet.AppendCustomerQueryRq();
            custUltimos.ORCustomerListQuery.CustomerListFilter.FromModifiedDate.SetValue(desde, true);
            custUltimos.ORCustomerListQuery.CustomerListFilter.ToModifiedDate.SetValue(DateTime.Now, true);

            IMsgSetResponse resp1 = sessionQB.DoRequests(requestMsgSet);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            IResponse respuesta = resp1.ResponseList.GetAt(0);
            {

                if (respuesta.StatusCode == 0)
                {
                    //pone en clis ids y edits

                    ICustomerRetList Customer = respuesta.Detail as ICustomerRetList;
                    List<clientes> clientes = new List<clientes>();
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

                    }

                    db.TrataInsertarClientes(clientes);

                }

            }
        }

        public void ActualiceItems(DateTime desde)
        {
            MariaDB db = new MariaDB();
            List<items> Lista = new List<items>();
            if (QBVerificar())
            {
                try
                {

                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;


                    IItemQuery grccuentas = requestMsgSet.AppendItemQueryRq();
                    grccuentas.OwnerIDList.Add("0");
                    grccuentas.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly);
                    grccuentas.ORListQuery.ListFilter.FromModifiedDate.SetValue(desde, true);
                    grccuentas.ORListQuery.ListFilter.ToModifiedDate.SetValue(DateTime.Now, true);


                    IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                    IResponse respuesta = resp.ResponseList.GetAt(0);
                    sessionQB.EndSession();
                    sessionQB.CloseConnection();

                    if (respuesta.StatusCode == 0)
                    {
                        IORItemRetList Items = respuesta.Detail as IORItemRetList;

                        for (int i = 0; i < Items.Count; i++)
                        {

                            IORItemRet itemGqb = Items.GetAt(i);



                            if (itemGqb.ItemServiceRet != null)
                            {
                                var itemqb = itemGqb.ItemServiceRet;

                                items it = new items();
                                it.iditem = itemqb.ListID.GetValue();
                                it.item = itemqb.Name.GetValue();

                                if (itemqb.ORSalesPurchase.SalesOrPurchase != null)
                                {
                                    if (itemqb.ORSalesPurchase.SalesOrPurchase.Desc != null)
                                        it.descripcion = itemqb.ORSalesPurchase.SalesOrPurchase.Desc.GetValue();

                                }

                                Lista.Add(it);
                            }


                        }

                        db.TrataInsertarItems(Lista);
                    }

                }
                catch (Exception e)
                {
                    MessageBox.Show("Error al leer Items " + e.Message);
                }
            }

        }

        #endregion

        #region SUBIR A QB

        public int subaInvoice(filaSale sale)
        {
            int vale = 0;
            MariaDB db = new MariaDB();
            locales loc = db.GetLocal(sale.sucursal);

            QBSessionManager sessionQB = QBAbrir();
            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;


            IInvoiceAdd grcInvoice = requestMsgSet.AppendInvoiceAddRq();

            grcInvoice.CustomerRef.ListID.SetValue(sale.idcustomer);
            grcInvoice.TxnDate.SetValue(sale.saleDate.Value);
            grcInvoice.ARAccountRef.FullName.SetValue("1201 · CLIENTES POR COBRAR:120101 · Clientes por cobrar");

            grcInvoice.TemplateRef.FullName.SetValue(loc.templaInvoice);
            grcInvoice.ClassRef.FullName.SetValue(loc.clase);
            grcInvoice.RefNumber.SetValue(Convert.ToString(loc.numfactura + 1));
            grcInvoice.PONumber.SetValue(sale.idSale.ToString());
            grcInvoice.Other.SetValue(Convert.ToString(sale.idMember));

            var it = JsonConvert.DeserializeObject<List<filaItem>>(sale.items);
            foreach (filaItem si in it)
            {
                IORInvoiceLineAdd ln1 = grcInvoice.ORInvoiceLineAddList.Append();

                ln1.InvoiceLineAdd.ItemRef.ListID.SetValue(si.idQbItem);
                ln1.InvoiceLineAdd.Quantity.SetValue(Convert.ToDouble(si.quantity));
                if (si.discount != null && si.discount.HasValue)
                    ln1.InvoiceLineAdd.Amount.SetValue( Convert.ToDouble(si.itemValue - si.discount));
                else
                    ln1.InvoiceLineAdd.Amount.SetValue(Convert.ToDouble(si.itemValue));

                if (si.tax > 0)
                    ln1.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue("T");
                else
                    ln1.InvoiceLineAdd.SalesTaxCodeRef.FullName.SetValue("N");

            }

            IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
            IResponse respuesta = resp.ResponseList.GetAt(0);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            vale = respuesta.StatusCode;
            if (vale == 0)
            {
                db.guardarNumeroFactura(sale.sucursal, loc.numfactura + 1);
                db.UpdateSaleFactura(sale.idSale.Value, loc.numfactura + 1);
            }
            return vale;

        }


        public string QBdevolverIdFactura(string facnumero)
        {
            string resid = "";
            try
            {
                QBSessionManager sessionQB = QBAbrir();
                IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                IInvoiceQuery grcClientes = requestMsgSet.AppendInvoiceQueryRq();
                grcClientes.ORInvoiceQuery.InvoiceFilter.ORRefNumberFilter.RefNumberFilter.RefNumber.SetValue(facnumero);
                grcClientes.ORInvoiceQuery.InvoiceFilter.ORRefNumberFilter.RefNumberFilter.MatchCriterion.SetValue(ENMatchCriterion.mcStartsWith);

                IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                IResponse respuesta = resp.ResponseList.GetAt(0);
                sessionQB.EndSession();
                sessionQB.CloseConnection();




                IInvoiceRetList Facturas = respuesta.Detail as IInvoiceRetList;

                for (int i = 0; i < Facturas.Count; i++)
                {
                    IInvoiceRet fac = Facturas.GetAt(i);
                    if (fac.RefNumber.GetValue() == facnumero) //& fac.TemplateRef.FullName.GetValue() == template
                    {
                        resid = fac.TxnID.GetValue();

                    }
                }

            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema al tratar de leer pagos de factura:  \r\r " + e, "Disulpe Usted");
            }
            return resid;
        }



        public void QBEscribirPayments(filaSale sale)
        {
            MariaDB db = new MariaDB();
            List<filaPago> it = JsonConvert.DeserializeObject<List<filaPago>>(sale.tarjetas);
            foreach (filaPago pago in it)
            {
                if (pago.procesado == false)
                {
                    QBSessionManager sessionQB = QBAbrir();
                    IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
                    requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;



                    IReceivePaymentAdd grcpago = requestMsgSet.AppendReceivePaymentAddRq();

                    grcpago.ARAccountRef.FullName.SetValue("1201 · CLIENTES POR COBRAR:120101 · Clientes por cobrar");
                    grcpago.DepositToAccountRef.FullName.SetValue("12000 · Undeposited Funds");


                    grcpago.CustomerRef.ListID.SetValue(sale.idcustomer);
                    grcpago.RefNumber.SetValue("Vta " + sale.idSale);
                    //grcpago.TxnDate.SetValue(sale.saleDate.Value);
                    grcpago.TxnDate.SetValue(pago.receivingDate.Value);


                    grcpago.TotalAmount.SetValue(Convert.ToDouble(pago.ammountPaid));

                    grcpago.PaymentMethodRef.FullName.SetValue(pago.QbMethod);

                    if (pago.authorization != null)
                        grcpago.Memo.SetValue(pago.authorization);


                    grcpago.ORApplyPayment.IsAutoApply.SetValue(true);

                    IMsgSetResponse resp1 = sessionQB.DoRequests(requestMsgSet);
                    IResponse respuesta1 = resp1.ResponseList.GetAt(0);
                    sessionQB.EndSession();

                    sessionQB.CloseConnection();

                    if (respuesta1.StatusCode == 0)
                    {
                        pago.procesado = true;
                        sale.tarjetas = JsonConvert.SerializeObject(it);
                        db.UpdateTexTarjeta(sale);
                        if (sale.invoiceNumber == "PP")
                        {
                            db.UpdateSaleFactura(sale.idSale.Value, sale.idSale.Value);
                        }
                    }

                }
            }


        }

        public void subaCliente(member cli)
        {
            QBSessionManager sessionQB = QBAbrir();
            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

            ICustomerAdd qb = requestMsgSet.AppendCustomerAddRq();
            qb.Name.SetValue(cli.firstName + " " + cli.lastName);
            if (cli.document != null)
                qb.AccountNumber.SetValue(cli.document);

            if (cli.email != null)
                qb.Email.SetValue(cli.email);
            if (cli.address != null)
            {
                string sh = cli.address;
                if (sh.Length < 41)
                {
                    qb.ShipAddress.Addr1.SetValue(sh);
                }
                else
                {
                    qb.ShipAddress.Addr1.SetValue(sh.Substring(0, 40));
                    sh = sh.Substring(40);
                    if (sh.Length < 41)
                    {
                        qb.ShipAddress.Addr2.SetValue(sh);
                    }
                    else
                    {
                        qb.ShipAddress.Addr2.SetValue(sh.Substring(0, 40));
                        sh = sh.Substring(40);
                        if (sh.Length < 41)
                        {
                            qb.ShipAddress.Addr3.SetValue(sh);
                        }
                        else
                        {
                            qb.ShipAddress.Addr1.SetValue(sh.Substring(0, 40));
                        }
                    }

                }
            }

            IMsgSetResponse resp1 = sessionQB.DoRequests(requestMsgSet);
            IResponse respuesta = resp1.ResponseList.GetAt(0);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            if (respuesta.StatusCode != 0)
            {
                //               System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema:  \r\r " + respuesta.StatusMessage, "Al subir Nuevos Clientes");
            }


        }

        public void subaItem(filaItem item)
        {
            QBSessionManager sessionQB = QBAbrir();
            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

            IItemServiceAdd qb = requestMsgSet.AppendItemServiceAddRq();
            qb.Name.SetValue(item.item.Trim());
            qb.ORSalesPurchase.SalesOrPurchase.Desc.SetValue(item.description);


            if (item.description.StartsWith("Producto"))
            {
                qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411002 · Juice Bar 12%");
            }
            else
            {
                if (item.description.StartsWith("Servicio"))
                {
                    if (item.description.ToUpper().Contains("CLUBES")) { qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411006 · Servicios Clubs"); }
                    else
                    {
                        if (item.description.ToUpper().Contains("TRAINING")) { qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411007 · Personal Training"); }
                        else
                        {
                            if (item.description.ToUpper().Contains("PILATES"))
                            {
                                qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411010 · Servicios Otros");
                            }
                            else
                            {
                                if (item.description.ToUpper().Contains("SPA"))
                                {
                                    qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411013 · Servicios SPA");
                                }
                                else
                                {
                                    qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411010 · Servicios Otros");
                                }
                            }
                        }
                    }                   
                }
                else
                {
                    if (item.description.StartsWith("Plan"))
                    {
                        qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES:411003 · Membresias");
                    }
                    else
                    {
                        qb.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("4 · INGRESOS:411 · VENTAS:41101 · VENTAS EN LOCALES");
                    }
                }
            }


           

            IMsgSetResponse resp1 = sessionQB.DoRequests(requestMsgSet);
            IResponse respuesta = resp1.ResponseList.GetAt(0);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            if (respuesta.StatusCode != 0)
            {
                if (respuesta.StatusCode != 3100)
                    System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema:  \r\r " + respuesta.StatusMessage);
            }
        }

        public void subaRep(Usuario item)
        {
            QBSessionManager sessionQB = QBAbrir();
            IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
            requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

            ISalesRepAdd qb = requestMsgSet.AppendSalesRepAddRq();
            qb.SalesRepEntityRef.FullName.SetValue(item.name);
            qb.Initial.SetValue(item.name.Substring(0, 5));
      


            IMsgSetResponse resp1 = sessionQB.DoRequests(requestMsgSet);
            IResponse respuesta = resp1.ResponseList.GetAt(0);
            sessionQB.EndSession();
            sessionQB.CloseConnection();

            if (respuesta.StatusCode != 0)
            {
                if (respuesta.StatusCode != 3100)
                    System.Windows.Forms.MessageBox.Show("No pude Crear REP, problema:  \r\r " + respuesta.StatusMessage);
            }
        }

        public Boolean SubaVendor(Usuario item)
        {
            Boolean sipudo = false;
            try
            {
                QBSessionManager sessionQB = QBAbrir();
                IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 13, 0);
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;


                IVendorAdd grcAsesor = requestMsgSet.AppendVendorAddRq();

                grcAsesor.Name.SetValue(item.name);
                IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                IResponse respuesta = resp.ResponseList.GetAt(0);
                sessionQB.EndSession();
                sessionQB.CloseConnection();

                if (respuesta.StatusCode != 0)
                {
                    System.Windows.Forms.MessageBox.Show("No pude Crear VENDOR, problema:  \r\r " + respuesta.StatusMessage);
                }
                else
                {
                    sipudo = true;
                }


            }

            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema al tratar de crear Asesores:  \r\r " + e, "Disulpe Usted");
            }
            return sipudo;

        }
        #endregion

        #region IRaQB
        public void QBiraTransaccio(String apwId, ENTxnDisplayModType apwEntidad)
        {

            try
            {
                QBSessionManager sessionQB = new QBSessionManager();
                sessionQB.OpenConnection2("", "Apower", ENConnectionType.ctLocalQBD);
                sessionQB.BeginSession("", ENOpenMode.omDontCare);

                IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 8, 0);
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                ITxnDisplayMod grcProveedor = requestMsgSet.AppendTxnDisplayModRq();

                grcProveedor.TxnID.SetValue(apwId);
                grcProveedor.TxnDisplayModType.SetValue(apwEntidad);

                IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                IResponse respuesta = resp.ResponseList.GetAt(0);
                sessionQB.EndSession();
                sessionQB.CloseConnection();

                if (respuesta.StatusCode > 0)
                {
                    switch (apwEntidad)
                    {
                        case ENTxnDisplayModType.tdmtBill:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Bill.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtBillPaymentCheck:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Cheque en pago de facturas.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtBillPaymentCreditCard:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Cheque en pago de tarjetas.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtCheck:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Cheque.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtCreditMemo:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Credit Memo.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtDeposit:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Depósito.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtInvoice:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Invoice.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtJournalEntry:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Asiento de Diario.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtPurchaseOrder:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Purchase Order.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtReceivePayment:
                            System.Windows.Forms.MessageBox.Show("No pude abrir Pago recibido.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENTxnDisplayModType.tdmtVendorCredit:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Vendor Credit.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema al Abrir Transacción:  \r\r " + e, "Disculpe Usted");
            }
        }

        public void QBiraFicha(String apwId, ENListDisplayModType apwEntidad)

        {
            try
            {
                QBSessionManager sessionQB = new QBSessionManager();

                sessionQB.OpenConnection2("", "Apower", ENConnectionType.ctLocalQBD);
                sessionQB.BeginSession("", ENOpenMode.omDontCare);
                IMsgSetRequest requestMsgSet = sessionQB.CreateMsgSetRequest("US", 8, 0);
                requestMsgSet.Attributes.OnError = ENRqOnError.roeContinue;

                IListDisplayMod grcProveedor = requestMsgSet.AppendListDisplayModRq();

                grcProveedor.ListID.SetValue(apwId);
                grcProveedor.ListDisplayModType.SetValue(apwEntidad);

                IMsgSetResponse resp = sessionQB.DoRequests(requestMsgSet);
                IResponse respuesta = resp.ResponseList.GetAt(0);
                sessionQB.EndSession();
                sessionQB.CloseConnection();

                if (respuesta.StatusCode > 0)
                    switch (apwEntidad)
                    {
                        case ENListDisplayModType.ldmtAccount:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Cuenta.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENListDisplayModType.ldmtCustomer:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Ficha del Cliente.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENListDisplayModType.ldmtEmployee:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Ficha del Empleado.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENListDisplayModType.ldmtItem:
                            System.Windows.Forms.MessageBox.Show("No pude abrir el Item.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                        case ENListDisplayModType.ldmtVendor:
                            System.Windows.Forms.MessageBox.Show("No pude abrir la Ficha del Vendedor.\r\rQuickBooks reporta lo siguiente: \r" + respuesta.StatusMessage, "Lo siento");
                            break;
                    }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("Se presento el siguiente problema:  \r\r " + e, "Disulpe Usted");
            }
        }

        #endregion

    }

}

