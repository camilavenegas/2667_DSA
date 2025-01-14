using ABCEvoEscritorio.Clases;
using Microsoft.VisualBasic.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;
using log4net;
using Newtonsoft.Json;
using System.Security.Policy;
using ABCEvoEscritorio.Pantallas;
using System.Numerics;

namespace ABCEvoEscritorio.BDD
{



    public class MariaDB
    {
        private MySqlConnection? conn;
        Utiles.Utiles utiles = new();

        /// <summary>
        /// Se conecta a la base de datos MariaDB usando la cadena de conexion del app.config
        /// </summary>
        /// <returns>True o False dependiendo si se conecto o no</returns>
        public Boolean ConectToDB()
        {

            string myConnectionString = ConfigurationManager.ConnectionStrings["mariadb"].ToString();

            try
            {
                conn = new MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                utiles.LogError(ex);
                return false;
            }
        }

        /// <summary>
        /// Se desconecta de la base MariaDB
        /// </summary>
        /// <returns>True o False dependiendo si se desconecto o no</returns>
        public Boolean DisconnectFromDB()
        {
            try
            {
                if (conn != null)
                {
                    conn.Close();
                }
                return true;
            }
            catch (MySqlException ex)
            {
                utiles.LogError(ex);
                return false;
            }
        }

        #region SALES
        public Boolean InsertSale(filaSale sale)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"INSERT INTO sales 
 (`idSale`, `saleDate`, `data`, `invoiceNumber`, `ammount`, `idMember`, `sucursal`, `valor`,`iva`,`amountPaid`, `document`, `customer`, `idcustomer`,`vqbitems`, `vqbtarjetas`, `vqbclientes`,`idinvoice`, `items`, `tarjetas`, `responsable`,  `documentResponsable`, `rep`,`idEmployee`) 
VALUES 
(@idSale, @saleDate, @data, @invoiceNumber, @ammount, @idMember, @sucursal, @valor,@iva,@amountPaid, @document, @customer, @idcustomer,@vqbitems, @vqbtarjetas, @vqbclientes,@idinvoice, @items, @tarjetas, @responsable, @documentResponsable, @rep, @idEmployee)";
                        cmd.Parameters.AddWithValue("@idSale", sale.idSale);
                        cmd.Parameters.AddWithValue("@saleDate", sale.saleDate);
                        cmd.Parameters.AddWithValue("@data", sale.data is null ? "" : sale.data);
                        cmd.Parameters.AddWithValue("@invoiceNumber", sale.invoiceNumber is null ? "" : sale.invoiceNumber);
                        cmd.Parameters.AddWithValue("@ammount", sale.ammount);
                        cmd.Parameters.AddWithValue("@idMember", sale.idMember);

                        cmd.Parameters.AddWithValue("@sucursal", sale.sucursal);
                        cmd.Parameters.AddWithValue("@valor", sale.valor);
                        cmd.Parameters.AddWithValue("@iva", sale.iva);
                        cmd.Parameters.AddWithValue("@amountPaid", sale.ammountPaid);
                        cmd.Parameters.AddWithValue("@document", sale.document);
                        cmd.Parameters.AddWithValue("@customer", sale.customer);
                        cmd.Parameters.AddWithValue("@idcustomer", sale.idcustomer);
                        cmd.Parameters.AddWithValue("@vqbitems", sale.VqbItems);
                        cmd.Parameters.AddWithValue("@vqbtarjetas", sale.Vqbtarjetas);
                        cmd.Parameters.AddWithValue("@vqbclientes", sale.VqbCliente);
                        cmd.Parameters.AddWithValue("@idinvoice", sale.idinvoice);
                        cmd.Parameters.AddWithValue("@items", sale.items);
                        cmd.Parameters.AddWithValue("@tarjetas", sale.tarjetas);
                        cmd.Parameters.AddWithValue("@responsable", sale.responsable);
                        cmd.Parameters.AddWithValue("@documentResponsable", sale.documentResponsable);
                        cmd.Parameters.AddWithValue("@rep", sale.rep);
                        cmd.Parameters.AddWithValue("@idEmployee", sale.idEmployee);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public filaSale GetSale(Int32? idSale, string sucursal)
        {

            filaSale sale = new filaSale();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `idSale`, `saleDate`, `data`, `invoiceNumber`, `ammount`, `idMember` FROM sales WHERE idSale=" + idSale + " AND sucursal='" + sucursal + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            filaSale sa = new();
                            sale.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            sale.idSale = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(1);
                            sale.saleDate = rdr.GetDateTime(2);
                            sale.data = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                            sale.invoiceNumber = rdr.IsDBNull("invoiceNumber") ? "" : rdr.GetString(4);
                            sale.ammount = rdr.GetInt32(5);
                            sale.idMember = rdr.IsDBNull("idMember") ? 0 : rdr.GetInt32(6);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return sale;
        }

        public List<filaSale> GetSales(string desde, string hasta)
        {

            List<filaSale> sales = new List<filaSale>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = @"SELECT * 
                            FROM sales WHERE saleDate BETWEEN '" + desde + "' AND '" + hasta + "'";
                        //string sql = @"SELECT * FROM sales WHERE saleDate BETWEEN '" + desde + " 00:00:00' AND '" + hasta + " 23:59:00'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            filaSale sale = new();
                            sale.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            sale.idSale = rdr.IsDBNull("idSale") ? 0 : rdr.GetInt32(1);
                            sale.saleDate = rdr.GetDateTime(2);
                            //sale.data = rdr.GetString(3);
                            sale.invoiceNumber = rdr.IsDBNull("invoiceNumber") ? "" : rdr.GetString(4);
                            sale.ammount = rdr.IsDBNull("iva") ? 0 : rdr.GetDecimal(5);
                            sale.idMember = rdr.IsDBNull("idMember") ? 0 : rdr.GetInt32(6);

                            sale.sucursal = rdr.IsDBNull("sucursal") ? "" : rdr.GetString(7);
                            sale.valor = rdr.IsDBNull("valor") ? 0 : rdr.GetDecimal(8);
                            sale.iva = rdr.IsDBNull("iva") ? 0 : rdr.GetDecimal(9);
                            sale.ammountPaid = rdr.IsDBNull("amountPaid") ? 0 : rdr.GetDecimal(10);
                            sale.document = rdr.IsDBNull("document") ? "" : rdr.GetString(11);
                            sale.customer = rdr.IsDBNull("customer") ? "" : rdr.GetString(12);
                            sale.idcustomer = rdr.IsDBNull("idcustomer") ? "" : rdr.GetString(13);
                            sale.VqbItems = rdr.GetBoolean(14);
                            sale.Vqbtarjetas = rdr.GetBoolean(15);
                            sale.idinvoice = rdr.IsDBNull("idinvoice") ? "" : rdr.GetString(16);
                            sale.items = rdr.IsDBNull("items") ? "" : rdr.GetString(17);
                            sale.tarjetas = rdr.IsDBNull("tarjetas") ? "" : rdr.GetString(18);
                            sale.VqbCliente = rdr.GetBoolean(19);
                            sale.responsable = rdr.IsDBNull("responsable") ? "" : rdr.GetString(20);
                            sale.documentResponsable = rdr.IsDBNull("documentResponsable") ? "" : rdr.GetString(21);
                            sale.rep = rdr.IsDBNull("rep") ? "" : rdr.GetString(22);
                            sale.idEmployee = rdr.IsDBNull("idEmployee") ? 0 : rdr.GetInt32(23);






                            sales.Add(sale);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return sales;
        }

        public List<Sale> GetVentas(string desde, string hasta)
        {

            List<Sale> sales = new List<Sale>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `idSale`, `saleDate`, `data`, `invoiceNumber`, `ammount`, `idMember` FROM sales WHERE saleDate BETWEEN '" + desde + "' AND '" + hasta + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            Sale sale = new();

                            string detalles = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                            sale = JsonConvert.DeserializeObject<Sale>(detalles);

                            //sale.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            //sale.idSale = rdr.IsDBNull("idSale") ? 0 : rdr.GetInt32(1);
                            //sale.saleDate = rdr.GetDateTime(2);
                            //sale.data = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                            //sale.invoiceNumber = rdr.IsDBNull("invoiceNumber") ? "" : rdr.GetString(4);
                            //sale.ammount = rdr.GetInt32(5);
                            //sale.idMember = rdr.IsDBNull("idMember") ? 0 : rdr.GetInt32(6);

                            sales.Add(sale);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return sales;
        }

        public void DelVenta(Int32 idsale)
        {

    

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "DELETE FROM sales WHERE `idSale` = ?idsale";
                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("?idsale", idsale);
                        cmd.ExecuteNonQuery();

                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
       
        }
        #endregion


        #region UPDATE SALES

        public void UpdateSaleFactura(int idsale, Int32 numfactura)
        {

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = "UPDATE sales SET    `invoiceNumber` = ?invoiceNumber   WHERE `idsale` = ?idsale";

                        cmd.Parameters.AddWithValue("?idsale", idsale);
                        cmd.Parameters.AddWithValue("?invoiceNumber", numfactura);
                        cmd.ExecuteNonQuery();

                        cmd.CommandType = CommandType.Text;

                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }

        }

        public void UpdateRep(int idsale, string rep)
        {

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {

                        cmd.CommandText = "UPDATE sales SET    `rep` = ?rep   WHERE `idsale` = ?idsale";

                        cmd.Parameters.AddWithValue("?idsale", idsale);
                        cmd.Parameters.AddWithValue("?rep", rep);
                        cmd.ExecuteNonQuery();

                        cmd.CommandType = CommandType.Text;

                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }

        }

        public Boolean UpdateVBCliente(filaSale sale)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE sales SET `idcustomer` = @idcustomer, `vqbclientes` = @vqbclientes  WHERE `idSale` = @idSale";
                        cmd.Parameters.AddWithValue("@idSale", sale.idSale);
                        cmd.Parameters.AddWithValue("@idcustomer", sale.idcustomer);
                        cmd.Parameters.AddWithValue("@vqbclientes", sale.VqbCliente);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateVBItem(filaSale sale)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE sales SET  `vqbitems` = @vqbitems,  `items` = @items WHERE `idSale` = @idSale";
                        cmd.Parameters.AddWithValue("@idSale", sale.idSale);

                        cmd.Parameters.AddWithValue("@vqbitems", sale.VqbItems);
                        cmd.Parameters.AddWithValue("@items", sale.items);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateVBTarjeta(filaSale sale)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE sales SET  `vqbtarjetas` = @vqbtarjetas,  `tarjetas` = @tarjetas WHERE `idSale` = @idSale";
                        cmd.Parameters.AddWithValue("@idSale", sale.idSale);

                        cmd.Parameters.AddWithValue("@vqbtarjetas", sale.Vqbtarjetas);
                        cmd.Parameters.AddWithValue("@tarjetas", sale.tarjetas);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateTexTarjeta(filaSale sale)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE sales SET  `tarjetas` = @tarjetas WHERE `idSale` = @idSale";
                        cmd.Parameters.AddWithValue("@idSale", sale.idSale);                        
                        cmd.Parameters.AddWithValue("@tarjetas", sale.tarjetas);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }


        #endregion



        #region MEMBERS

        public filaMember GetMember(Int32? idMember)
        {

            filaMember member = new filaMember();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `document`, `data`, `idMember` FROM members WHERE idMember=" + idMember;

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            member.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            member.document = rdr.IsDBNull("document") ? "" : rdr.GetString(1);
                            member.data = rdr.IsDBNull("data") ? "" : rdr.GetString(2);
                            member.idMember = rdr.GetInt32(3);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return member;
        }

        public filaMember GetMember(string ci)
        {

            filaMember member = new filaMember();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `document`, `data`, `idMember` FROM members WHERE document =" + ci;

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            member.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            member.document = rdr.IsDBNull("document") ? "" : rdr.GetString(1);
                            member.data = rdr.IsDBNull("data") ? "" : rdr.GetString(2);
                            member.idMember = rdr.GetInt32(3);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return member;
        }

        public Boolean InsertMember(filaMember member)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO members (`document`, `data`, `idMember`) VALUES (@document, @data, @idMember)";
                        cmd.Parameters.AddWithValue("@document", member.document);
                        cmd.Parameters.AddWithValue("@data", member.data is null ? "" : member.data);
                        cmd.Parameters.AddWithValue("@idMember", member.idMember);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public List<filaMember> GetMembers()
        {

            List<filaMember> memberList = new List<filaMember>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `document`, `data`, `idMember` FROM members";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            filaMember member = new filaMember();
                            member.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            member.document = rdr.IsDBNull("document") ? "" : rdr.GetString(1);
                            member.data = rdr.IsDBNull("data") ? "" : rdr.GetString(2);
                            member.idMember = rdr.GetInt32(3);
                            memberList.Add(member);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return memberList;
        }

        public List<member> GetMembersl()
        {

            List<member> memberList = new List<member>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `data` FROM members";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            member member = new member();
                            string detalles = rdr.IsDBNull("data") ? "" : rdr.GetString(0);


                            member = JsonConvert.DeserializeObject<member>(detalles);
                            memberList.Add(member);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return memberList;
        }

        #endregion

        #region CLASES
        public clases GetClase(string idclase)
        {

            clases clas = new clases();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idclase`, `fullname`, `name` FROM clases WHERE idclase = '" + idclase + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            clas.idclase = rdr.IsDBNull("idclase") ? "" : rdr.GetString(0);
                            clas.fullname = rdr.IsDBNull("fullname") ? "" : rdr.GetString(1);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(2);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return clas;
        }


        public Boolean InsertClase(clases clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO clases (`idclase`, `fullname`, `name`) VALUES (@idclase, @fullname, @name)";
                        cmd.Parameters.AddWithValue("@idclase", clas.idclase);
                        cmd.Parameters.AddWithValue("@fullname", clas.fullname is null ? "" : clas.fullname);
                        cmd.Parameters.AddWithValue("@name", clas.name);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }


        public Boolean UpdateClase(clases clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE clases SET  `fullname` =  @fullname, `name` =  @name WHERE `idclase` =@idclase";
                        cmd.Parameters.AddWithValue("@idclase", clas.idclase);
                        cmd.Parameters.AddWithValue("@fullname", clas.fullname is null ? "" : clas.fullname);
                        cmd.Parameters.AddWithValue("@name", clas.name);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public List<clases> GetClases()
        {

            List<clases> clases = new List<clases>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idclase`, `fullname`, `name` FROM clases";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            clases clas = new clases();
                            clas.idclase = rdr.IsDBNull("idclase") ? "" : rdr.GetString(0);
                            clas.fullname = rdr.IsDBNull("fullname") ? "" : rdr.GetString(1);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(2);
                            clases.Add(clas);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return clases;
        }

        #endregion


        #region TEMPLATES

        public templates GetTemplate(string idtemplate)
        {

            templates clas = new templates();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idtemplate`, `name` FROM templates WHERE idtemplate = '" + idtemplate + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            clas.idtemplate = rdr.IsDBNull("idtemplate") ? "" : rdr.GetString(0);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(1);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return clas;
        }

        public Boolean InsertTemplate(templates clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO templates (`idtemplate`, `name`) VALUES (@idtemplate, @name)";
                        cmd.Parameters.AddWithValue("@idtemplate", clas.idtemplate);

                        cmd.Parameters.AddWithValue("@name", clas.name);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateTemplate(templates clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE templates SET   `name` =  @name WHERE `idtemplate` =@idtemplate";
                        cmd.Parameters.AddWithValue("@idtemplate", clas.idtemplate);
                        cmd.Parameters.AddWithValue("@name", clas.name);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }


        public List<templates> GetTemplates()
        {

            List<templates> templates = new List<templates>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idtemplate`,  `name` FROM templates";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            templates clas = new templates();
                            clas.idtemplate = rdr.IsDBNull("idtemplate") ? "" : rdr.GetString(0);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(1);
                            templates.Add(clas);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return templates;
        }

        public List<templates> GetTemplatesInvoices()
        {

            List<templates> templates = new List<templates>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idtemplate`,  `name` FROM templates";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            templates clas = new templates();
                            clas.idtemplate = rdr.IsDBNull("idtemplate") ? "" : rdr.GetString(0);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(1);
                            if (!clas.name.Contains("CREDIT"))
                                templates.Add(clas);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return templates;
        }

        public List<templates> GetTemplatesCreditos()
        {

            List<templates> templates = new List<templates>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idtemplate`,  `name` FROM templates";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            templates clas = new templates();
                            clas.idtemplate = rdr.IsDBNull("idtemplate") ? "" : rdr.GetString(0);
                            clas.name = rdr.IsDBNull("name") ? "" : rdr.GetString(1);
                            if (clas.name.Contains("CREDIT"))
                                templates.Add(clas);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return templates;
        }

        #endregion


        #region CLIENTES qb

        public clientes Leercliente(string ruc)
        {
            if(ruc.Length > 10)
                ruc = ruc.Substring(0, 10);
            
           clientes cli = new clientes();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM clientes WHERE ruc LIKE '%" + ruc+"%'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            cli = ConvertirEntidad(rdr);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return cli;
        }

        public List<clientes> Leerclientes()
        {
            List<clientes> list = new List<clientes>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM clientes";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            list.Add(ConvertirEntidad(rdr));

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return list;
        }

        private clientes ConvertirEntidad(IDataReader reader)
        {
            clientes entidad = new clientes();

            entidad.idcliente = Convert.ToString(reader["idcliente"]);
            entidad.nombre = Convert.ToString(reader["nombre"]);
            entidad.ruc = Convert.ToString(reader["ruc"]);

            entidad.direccion = Convert.ToString(reader["direccion"]);
            entidad.ciudad = Convert.ToString(reader["ciudad"]);
            entidad.telefono = Convert.ToString(reader["telefono"]);
            entidad.mail = Convert.ToString(reader["mail"]);

            return entidad;
        }

        public void TrataInsertarClientes(List<clientes> clientes)
        {
            List<clientes> estaban = Leerclientes();

            Frm_clientes objc = (Frm_clientes)Application.OpenForms["Frm_clientes"];

            string query = string.Empty;
            if (this.ConectToDB())
            {
 

                foreach (clientes cliente in clientes)
                {
                    try
                    {

 
  

                        clientes sihay = estaban.Find(x => x.idcliente == cliente.idcliente);

                        //query = @"SELECT * FROM clientes WHERE idcliente = ?idcliente";
                        //MySqlCommand command = new MySqlCommand(query, conn);
                        //command.Parameters.AddWithValue("?idcliente", cliente.idcliente);

                        //MySqlDataReader reader = command.ExecuteReader();
                        //if (reader.HasRows)
                        if (sihay != null)
                        {
                            //reader.Close();

                            //actualiza
                            query = @"UPDATE clientes SET  nombre= ?nombre, ruc=?ruc,  
                                direccion=?direccion, ciudad= ?ciudad, telefono=?telefono, mail= ?mail WHERE 
                                idcliente=?idcliente";
                            MySqlCommand commandU = new MySqlCommand(query, conn);
                            commandU.Parameters.AddWithValue("?idcliente", cliente.idcliente);
                            commandU.Parameters.AddWithValue("?nombre", cliente.nombre);
                            commandU.Parameters.AddWithValue("?ruc", cliente.ruc);

                            commandU.Parameters.AddWithValue("?direccion", cliente.direccion);
                            commandU.Parameters.AddWithValue("?ciudad", cliente.ciudad);
                            commandU.Parameters.AddWithValue("?telefono", cliente.telefono);
                            commandU.Parameters.AddWithValue("?mail", cliente.mail);
                            commandU.ExecuteNonQuery();

                        }



                        else
                        {
                            //reader.Close();
                            

                            query = @"INSERT INTO clientes( idcliente, nombre, ruc,  direccion, ciudad, telefono, mail)
                                VALUES (?idcliente, ?nombre, ?ruc,  ?direccion, ?ciudad, ?telefono, ?mail)";
                            MySqlCommand commandU = new MySqlCommand(query, conn);
                            commandU.Parameters.AddWithValue("?idcliente", cliente.idcliente);
                            commandU.Parameters.AddWithValue("?nombre", cliente.nombre);
                            commandU.Parameters.AddWithValue("?ruc", cliente.ruc);

                            commandU.Parameters.AddWithValue("?direccion", cliente.direccion);
                            commandU.Parameters.AddWithValue("?ciudad", cliente.ciudad);
                            commandU.Parameters.AddWithValue("?telefono", cliente.telefono);
                            commandU.Parameters.AddWithValue("?mail", cliente.mail);
                            commandU.ExecuteNonQuery();


                        }

                        //objc.sstProgreso();
                    }
                    catch (MySqlException ex)
                    {
                        utiles.LogError(ex);
                    }
                    catch (Exception ex)
                    {
                        utiles.LogError(ex);
                    }
                    //finally
                    //{
                    //    this.DisconnectFromDB();
                    //}

                }

                this.DisconnectFromDB();
            }
        }

        public void InsertarClientes(clientes cliente)
        {

            if (this.ConectToDB())
            {



                try
                {



                    string query = @"INSERT INTO clientes( idcliente, nombre, ruc,  direccion, ciudad, telefono, mail)
                                VALUES (?idcliente, ?nombre, ?ruc,  ?direccion, ?ciudad, ?telefono, ?mail)";
                    MySqlCommand commandU = new MySqlCommand(query, conn);
                    commandU.Parameters.AddWithValue("?idcliente", cliente.idcliente);
                    commandU.Parameters.AddWithValue("?nombre", cliente.nombre);
                    commandU.Parameters.AddWithValue("?ruc", cliente.ruc);

                    commandU.Parameters.AddWithValue("?direccion", cliente.direccion);
                    commandU.Parameters.AddWithValue("?ciudad", cliente.ciudad);
                    commandU.Parameters.AddWithValue("?telefono", cliente.telefono);
                    commandU.Parameters.AddWithValue("?mail", cliente.mail);
                    commandU.ExecuteNonQuery();



                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }


            }
        }

        #endregion


        #region ITEMS

        public List<items> LeerItems()
        {
            List<items> cli = new List<items>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM items";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            items it = new items();
                            it.iditem = rdr.IsDBNull("iditem") ? "" : rdr.GetString(1);
                            it.item = rdr.IsDBNull("item") ? "" : rdr.GetString(2);
                            it.descripcion = rdr.IsDBNull("descripcion") ? "" : rdr.GetString(3);


                            cli.Add(it);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return cli;
        }

        public items LeerItem(string este)
        {
            items it = new items();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM items WHERE item = '" + este + "'";

                        cmd.CommandText = sql;

                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            
                            it.iditem = rdr.IsDBNull("iditem") ? "" : rdr.GetString(1);
                            it.item = rdr.IsDBNull("item") ? "" : rdr.GetString(2);
                            it.descripcion = rdr.IsDBNull("descripcion") ? "" : rdr.GetString(3);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return it;
        }


        public void TrataInsertarItems(List<items> itms)
        {
            frm_items objc = (frm_items)Application.OpenForms["frm_items"];
            List<items> estan = LeerItems();

            string query = string.Empty;
            if (this.ConectToDB())
            {




                try
                {
                    foreach (items it in itms)
                    {
                        
                        items hey = estan.Find(x => x.iditem == it.iditem);
                        if (hey != null)
                        {

                            //actualiza
                            query = @"UPDATE items SET  item = ?item, descripcion = ?descripcion WHERE iditem = ?iditem";
                            MySqlCommand commandU = new MySqlCommand(query, conn);
                            commandU.Parameters.AddWithValue("?iditem", it.iditem);


                            commandU.Parameters.AddWithValue("?item", it.item);
                            commandU.Parameters.AddWithValue("?descripcion", it.descripcion);

                            commandU.ExecuteNonQuery();

                        }



                        else
                        {
                            //rdr.Close();
                            //inserta

                            query = @"INSERT INTO items( iditem, item, descripcion)
                                VALUES (?iditem, ?item, ?descripcion)";
                            MySqlCommand commandU = new MySqlCommand(query, conn);

                            commandU.Parameters.AddWithValue("?iditem", it.iditem);
                            commandU.Parameters.AddWithValue("?item", it.item);
                            commandU.Parameters.AddWithValue("?descripcion", it.descripcion);

                            commandU.ExecuteNonQuery();


                        }
                    }
                }

                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

                }
            }

        #endregion

        #region TARJETAS


        public List<tarjetas> LeerTarjetas()
        {
            List<tarjetas> cli = new List<tarjetas>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM tarjetas";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            tarjetas it = new tarjetas();
                            it.evo = rdr.IsDBNull("evo") ? "" : rdr.GetString(1);
                            it.qb = rdr.IsDBNull("qb") ? "" : rdr.GetString(2);
                            cli.Add(it);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return cli;
        }

        public tarjetas LeerTarjeta(string tar)
        {
            tarjetas it = new tarjetas();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM tarjetas WHERE evo = '" + tar + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {

                            it.evo = rdr.IsDBNull("evo") ? "" : rdr.GetString(1);
                            it.qb = rdr.IsDBNull("qb") ? "" : rdr.GetString(2);


                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return it;
        }


        public Boolean InsertTarteja(string evo, string qb)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO tarjetas (`evo`, `qb`) VALUES (@evo, @qb)";
                        cmd.Parameters.AddWithValue("@evo", evo);
                        cmd.Parameters.AddWithValue("@qb", qb);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateTarjeta(string evo, string qb)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE tarjetas SET   `qb` =  @qb WHERE `evo` =@evo";
                        cmd.Parameters.AddWithValue("@evo", evo);
                        cmd.Parameters.AddWithValue("@qb", qb);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean deleteTarjetas()
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM tarjetas WHERE idtarjetas < 1000";
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        #endregion


        #region VENDEDORES

        public List<vendedores> LeerVendedores()
        {
            List<vendedores> cli = new List<vendedores>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM vendedores";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            vendedores it = new vendedores();
                            it.idvendor = rdr.IsDBNull("idvendor") ? "" : rdr.GetString(1);
                            it.nick = rdr.IsDBNull("nick") ? "" : rdr.GetString(2);
                            it.rep = rdr.IsDBNull("rep") ? "" : rdr.GetString(3);
                            it.idEmployee = rdr.IsDBNull("idEmployee") ? 0 : rdr.GetInt32(4);
                            cli.Add(it);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return cli;
        }

        public vendedores LeerVendedor(string rep)
        {
            vendedores it = new vendedores();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT * FROM vendedores  WHERE rep = @rep";

                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@rep", rep);
                        using MySqlDataReader rdr = cmd.ExecuteReader();
                        

                        while (rdr.Read())
                        {
                            
                            it.idvendor = rdr.IsDBNull("idvendor") ? "" : rdr.GetString(1);
                            it.nick = rdr.IsDBNull("nick") ? "" : rdr.GetString(2);
                            it.rep = rdr.IsDBNull("rep") ? "" : rdr.GetString(3);
                            it.idEmployee = rdr.IsDBNull("idEmployee") ? 0 : rdr.GetInt32(4);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return it;
        }

        public string LeerNick(Int32 idEmployee)
        {
            string mrep = "";

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `nick` FROM vendedores  WHERE idEmployee = @idEmployee";

                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("@idEmployee", idEmployee);
                        using MySqlDataReader rdr = cmd.ExecuteReader();


                        while (rdr.Read())
                        {
                            mrep = rdr.IsDBNull("nick") ? "" : rdr.GetString(0);

                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return mrep;
        }

        public Boolean InsertVendedor(vendedores vendor)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO vendedores (`idvendor`, `nick`, `rep`,`idEmployee`) VALUES (@idvendor, @nick, @rep, @idEmployee)";
                        cmd.Parameters.AddWithValue("@idvendor", vendor.idvendor);
                        cmd.Parameters.AddWithValue("@nick", vendor.nick);
                        cmd.Parameters.AddWithValue("@rep", vendor.rep);
                        cmd.Parameters.AddWithValue("@idEmployee", vendor.idEmployee);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateVendedor(vendedores vendor)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE vendedores SET   `nick` =  @nick, `rep` = @rep, `idEmployee` = @idEmployee WHERE `idvendor` =@idvendor";
                        cmd.Parameters.AddWithValue("@idvendor", vendor.idvendor);
                        cmd.Parameters.AddWithValue("@nick", vendor.nick);
                        cmd.Parameters.AddWithValue("@rep", vendor.rep);
                        cmd.Parameters.AddWithValue("@idEmployee", vendor.idEmployee);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        #endregion


        #region LOCALES

        public List<locales> GetLocales()
        {

            List<locales> templates = new List<locales>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `idlocales`,  `sucursal`,  `templateInvoice`,  `templateCredito`,  `clase`,  `numfactura`,  `numcredito`, `evo` FROM locales";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            locales clas = new locales();
                            clas.idlocales = rdr.IsDBNull("idlocales") ? 0 : rdr.GetInt32(0);
                            clas.sucursal = rdr.IsDBNull("sucursal") ? "" : rdr.GetString(1);
                            clas.templaInvoice = rdr.IsDBNull("templateInvoice") ? "" : rdr.GetString(2);
                            clas.templaCredito = rdr.IsDBNull("templateCredito") ? "" : rdr.GetString(3);
                            clas.clase = rdr.IsDBNull("clase") ? "" : rdr.GetString(4);
                            clas.numfactura = rdr.IsDBNull("numfactura") ? 0 : rdr.GetInt32(5);
                            clas.numcredito = rdr.IsDBNull("numcredito") ? 0 : rdr.GetInt32(6);
                            clas.evo = rdr.IsDBNull("evo") ? "" : rdr.GetString(7);
                            templates.Add(clas);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return templates;
        }

        public locales GetLocal(string sucursal)
        {

            locales clas = new locales();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT   `templateInvoice`,  `templateCredito`,  `clase`, `numfactura`,  `numcredito`, `evo` FROM locales WHERE `sucursal` = ?sucursal";

                        cmd.CommandText = sql;
                        cmd.Parameters.AddWithValue("?sucursal", sucursal);
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {                                                    
                            clas.templaInvoice = rdr.IsDBNull("templateInvoice") ? "" : rdr.GetString(0);
                            clas.templaCredito = rdr.IsDBNull("templateCredito") ? "" : rdr.GetString(1);
                            clas.clase = rdr.IsDBNull("clase") ? "" : rdr.GetString(2);
                            clas.numfactura = rdr.IsDBNull("numfactura") ? 0 : rdr.GetInt32(3);
                            clas.numcredito = rdr.IsDBNull("numcredito") ? 0 : rdr.GetInt32(4);
                            clas.evo = rdr.IsDBNull("evo") ? "" : rdr.GetString(5);


                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return clas;
        }

        public Boolean InsertLocal(locales clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO locales (  `sucursal`,  `templateInvoice`,  `templateCredito`,  `clase`,  `numfactura`,  `numcredito`, `evo`) VALUES (  ?sucursal,  ?templateInvoice,  ?templateCredito,  ?clase,  ?numfactura,  ?numcredito, ?evo)";
 
                        
                        
                        cmd.Parameters.AddWithValue("?sucursal", clas.sucursal);
                        cmd.Parameters.AddWithValue("?templateInvoice", clas.templaInvoice);
                        cmd.Parameters.AddWithValue("templateCredito", clas.templaCredito);
                        cmd.Parameters.AddWithValue("?clase", clas.clase);
                        cmd.Parameters.AddWithValue("?numfactura", clas.numfactura);
                        cmd.Parameters.AddWithValue("?numcredito", clas.numcredito);
                        cmd.Parameters.AddWithValue("?evo", clas.evo);

                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public Boolean UpdateLocal(locales clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE locales SET   `sucursal` = ?sucursal,  `templateInvoice` = ?templateInvoice,  `templateCredito` = ?templateCredito,  `clase` = ?clase,  `numfactura` = ?numfactura,  `numcredito` = ?numcredito, `evo` = ?evo WHERE `idlocales` = ?idlocales";
                        cmd.Parameters.AddWithValue("?sucursal", clas.sucursal);
                        cmd.Parameters.AddWithValue("?templateInvoice", clas.templaInvoice);
                        cmd.Parameters.AddWithValue("templateCredito", clas.templaCredito);
                        cmd.Parameters.AddWithValue("?clase", clas.clase);
                        cmd.Parameters.AddWithValue("?numfactura", clas.numfactura);
                        cmd.Parameters.AddWithValue("?numcredito", clas.numcredito);
                        cmd.Parameters.AddWithValue("?idlocales", clas.idlocales);
                        cmd.Parameters.AddWithValue("?evo", clas.evo);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }


        public Boolean DeleteLocal(locales clas)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "DELETE FROM locales  WHERE `idlocales` = ?idlocales";

                        cmd.Parameters.AddWithValue("?idlocales", clas.idlocales);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public void guardarNumeroFactura(string sucursal, Int32 numero)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "UPDATE locales SET    `numfactura` = ?numfactura   WHERE `sucursal` = ?sucursal";

                        cmd.Parameters.AddWithValue("?sucursal", sucursal);
                        cmd.Parameters.AddWithValue("?numfactura", numero);
                        cmd.ExecuteNonQuery();


                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }
            }
        }
        #endregion

        #region REP

        public filaUsuario GetUsuario(Int32? idEmployee)
        {

            filaUsuario usuario = new filaUsuario();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `idEmployee`, `name`, `data` FROM usuarios WHERE idEmployee=" + idEmployee;

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            usuario.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            usuario.idEmployee = rdr.IsDBNull("idEmployee") ? 0 : rdr.GetInt32(1);
                            usuario.name = rdr.IsDBNull("name") ? "" : rdr.GetString(2);
                            usuario.data = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return usuario;
        }

        public Boolean InsertUsuario(filaUsuario usuario)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO usuarios (`idEmployee`, `name`, `data`) VALUES (@idEmployee, @name, @data)";
                        cmd.Parameters.AddWithValue("@idEmployee", usuario.idEmployee);
                        cmd.Parameters.AddWithValue("@name", usuario.name);
                        cmd.Parameters.AddWithValue("@data", usuario.data is null ? "" : usuario.data);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }


        #endregion


        public filaReceivable GetReceivable(Int32? idReceivable)
        {

            filaReceivable receivable = new filaReceivable();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `idSale`, `idReceivable`, `data` FROM receivables WHERE idReceivable=" + idReceivable;

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            receivable.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            receivable.idSale = rdr.IsDBNull("idSale") ? 0 : rdr.GetInt32(1);
                            receivable.idReceivable = rdr.IsDBNull("idReceivable") ? 0 : rdr.GetInt32(2);
                            receivable.data = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return receivable;
        }

        public Boolean InsertReceivable(filaReceivable receivable)
        {
            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "INSERT INTO `receivables`(`idSale`, `receivingDate`, `data`, idReceivable) VALUES (@idSale, @receivingDate, @data, @idReceivable)";
                        cmd.Parameters.AddWithValue("@idSale", receivable.idSale);
                        cmd.Parameters.AddWithValue("@receivingDate", receivable.receivingDate);
                        cmd.Parameters.AddWithValue("@data", receivable.data is null ? "" : receivable.data);
                        cmd.Parameters.AddWithValue("@idReceivable", receivable.idReceivable);
                        cmd.ExecuteNonQuery();

                        return true;
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return false;
        }

        public List<filaReceivable> GetReceivables(string desde, string hasta)
        {

            List<filaReceivable> receivable = new List<filaReceivable>();

            if (this.ConectToDB())
            {
                try
                {
                    using (MySqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 300;

                        string sql = "SELECT `id`, `idSale`, `idReceivable`, `data` , `receivingDate` FROM receivables WHERE receivingDate BETWEEN '" + desde + "' AND '" + hasta + "'";

                        cmd.CommandText = sql;
                        using MySqlDataReader rdr = cmd.ExecuteReader();

                        while (rdr.Read())
                        {
                            filaReceivable fila = new filaReceivable();
                            fila.id = rdr.IsDBNull("id") ? 0 : rdr.GetInt32(0);
                            fila.idSale = rdr.IsDBNull("idSale") ? 0 : rdr.GetInt32(1);
                            fila.idReceivable = rdr.IsDBNull("idReceivable") ? 0 : rdr.GetInt32(2);
                            fila.data = rdr.IsDBNull("data") ? "" : rdr.GetString(3);
                            fila.receivingDate = rdr.GetDateTime(4);
                            receivable.Add(fila);
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    utiles.LogError(ex);
                }
                catch (Exception ex)
                {
                    utiles.LogError(ex);
                }
                finally
                {
                    this.DisconnectFromDB();
                }

            }
            return receivable;
        }

    }
}