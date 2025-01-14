using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.VisualBasic.Devices;
using ABCEvoEscritorio.Clases;
using Newtonsoft.Json.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using ABCEvoEscritorio.BDD;

namespace ABCEvoEscritorio.ABCEvo
{
    public class ABCEvo
    {

        Utiles.Utiles utiles = new Utiles.Utiles();
        string dns = "";
        string token = "";

        public ABCEvo(tokenEvo tokenEvo)
        {
            if (tokenEvo == null)
            {
                dns = ConfigurationManager.AppSettings["evoDns"].ToString();
                token = ConfigurationManager.AppSettings["evoToken"].ToString();
            }
            else
            {
                dns = tokenEvo.dns;
                token = tokenEvo.token;
            }
        }
        public bool Login(string username)
        {

            return true;

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
            string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
            string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v1/employees?email=" + username + "&take=1&skip=0");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);

            if (response != null)
            {
                Usuario usuario = new Usuario();
                if (response.Content != null)
                {
                    foreach (JObject item in JArray.Parse(response.Content))
                    {
                        try
                        {
                            usuario = item.ToObject<Usuario>();
                            if (usuario.status != null && usuario.status == true)
                            {
                                return true;
                            }
                        }
                        catch (Exception e)
                        {
                            utiles.LogError(e);
                            throw;
                        }
                    }
                }
            }
            return false;
        }

        public List<Sale> LeerVentas(string desde, string hasta, Int32 take, Int32 skip)
        {
            List<Sale> ventas = new List<Sale>();

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
            //string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
            //string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v2/sales?dateSaleStart=" + desde + "&dateSaleEnd=" + hasta + "&showReceivables=true&take=" + take + "&skip=" + skip + "&onlyMembership=false&atLeastMonthly=false&showOnlyActiveMemberships=false");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "text/plain");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);

            MariaDB mariaDB = new MariaDB();

            if (response != null)
            {

                if (response.Content != null)
                {
                    foreach (JObject item in JArray.Parse(response.Content))
                    {
                        try
                        {
                            Sale venta = new();
                            venta = item.ToObject<Sale>();
                            if (venta.idSale != null)
                            {
                                ventas.Add(venta);
                            }
                        }
                        catch (Exception e)
                        {
                            utiles.LogError(e);
                            throw;
                        }
                    }
                }
            }

            return ventas;
        }

        /* public member LeerCliente(Int32? idMember)
        {
            member elCliente = new member();

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
            string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
            string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v1/members/" + idMember.ToString());
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "text/plain");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);

            MariaDB mariaDB = new MariaDB();

            if (response != null)
            {

                if (response.Content != null)
                {
                    JObject item = JObject.Parse(response.Content);
                    try
                    {
                        member cliente = new();
                        cliente = item.ToObject<member>();
                        if (cliente.idMember != null)
                        {
                            filaMember fila = new();
                            fila = mariaDB.GetMember(cliente.idMember);
                            if (fila.idMember == cliente.idMember)
                            {
                                //ya existe no agrego
                            }
                            else
                            {
                                elCliente = cliente;
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        utiles.LogError(e);
                        throw;
                    }
                }
            }

            return elCliente;
        }*/

        public member LeerCliente(Int32? idMember)
        {
            member elCliente = new member();

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
            string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
            string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v1/members?take=50&skip=0&idsMembers=" + idMember.ToString() + "&onlyPersonal=false&showActivityData=false");
            //var options = new RestClientOptions(url + "v1/members/" + idMember.ToString());
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);

            MariaDB mariaDB = new MariaDB();

            if (response != null)
            {

                if (response.Content != null)
                {
                    foreach (JObject items in JArray.Parse(response.Content))
                    {
                        //JObject item = JObject.Parse(items);
                        try
                        {
                            member cliente = new();
                            cliente = items.ToObject<member>();
                            if (cliente.idMember != null)
                            {
                                filaMember fila = new();
                                fila = mariaDB.GetMember(cliente.idMember);
                                if (fila.idMember == cliente.idMember)
                                {
                                    //ya existe no agrego
                                }
                                else
                                {
                                    elCliente = cliente;
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            utiles.LogError(e);
                            throw;
                        }
                    }

                }
            }

            return elCliente;
        }

        public List<Usuario> LeerUsuarios()
        {

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
           // string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
           // string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v1/employees?take=100&skip=0");
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);
            List<Usuario> usuarios = new List<Usuario>();
            if (response != null)
            {
                if (response.Content != null)
                {
                    foreach (JObject item in JArray.Parse(response.Content))
                    {
                        try
                        {
                            usuarios.Add(item.ToObject<Usuario>());

                        }
                        catch (Exception e)
                        {
                            utiles.LogError(e);
                            throw;
                        }
                    }
                }
            }
            return usuarios;
        }


        public List<receivable> LeerPagos(string desde, string hasta, Int32 take, Int32 skip)
        {
            List<receivable> pagos = new List<receivable>();

            string url = ConfigurationManager.AppSettings["evoUrl"].ToString();
            //string dns = ConfigurationManager.AppSettings["evoDns"].ToString();
            //string token = ConfigurationManager.AppSettings["evoToken"].ToString();
            var auth = Encoding.UTF8.GetBytes(dns + ":" + token);
            string coded = Convert.ToBase64String(auth);
            var options = new RestClientOptions(url + "v1/receivables?receivingDateStart=" + desde + "&receivingDateEnd=" + hasta + "&take=" + take + "&skip=" + skip);
            var client = new RestClient(options);
            var request = new RestRequest("");
            request.AddHeader("accept", "application/json");
            request.AddHeader("authorization", "Basic " + coded);
            var response = client.Get(request);

            if (response != null)
            {

                if (response.Content != null)
                {
                    foreach (JObject item in JArray.Parse(response.Content))
                    {
                        try
                        {
                            receivable receivable = new();
                            receivable = item.ToObject<receivable>();
                            if (receivable.idReceivable != null)
                            {
                                pagos.Add(receivable);
                            }
                        }
                        catch (Exception e)
                        {
                            utiles.LogError(e);
                            throw;
                        }
                    }
                }
            }

            return pagos;
        }


    }
}
