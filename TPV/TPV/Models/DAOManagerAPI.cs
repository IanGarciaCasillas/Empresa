using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TPV.Item;

namespace TPV.Models
{
    public class DAOManagerAPI
    {
        public const string LINK_WEB_ARTICLES = "https://localhost:7151/articles/";
        public const string LINK_WEB_EMPL = "https://localhost:7151/empl/";
        public const string LINK_WEB_TICKETS = "https://localhost:7151/tickets/";
        public const string LINK_WEB_TICKETDETALLS = "https://localhost:7151/ticketDetalls/";
        public const string LINK_WEB_COMANDESVENDES = "https://localhost:7151/comandaVenda/";
        public const string LINK_WEB_COMANDESVENDESDETALLS = "https://localhost:7151/comandaVendaDetalls/";
        public const string LINK_WEB_ALBARAVENDA = "https://localhost:7151/albaraVenda/";
        public const string LINK_WEB_ALBARAVENDADETALLS = "https://localhost:7151/albaraVendaDetall/";
        public const string LINK_WEB_CLIENT = "https://localhost:7151/client/";




        #region TICKET / TICKETS DETALLS

        public List<TicketDetall> GetTicketsDetalls(int idTicket, int numDocument)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var jsonTicketsDetalls = client.DownloadString($"{LINK_WEB_TICKETDETALLS}{idTicket}/{numDocument}");

            List<TicketDetall>ticketsDetalls = JsonSerializer.Deserialize<List<TicketDetall>>(jsonTicketsDetalls);


            var algo = 1;


            return ticketsDetalls;
        }


        public Ticket PagarTPV(List<ItemCompraTpv> itemCompraTpvs)
        {
            Ticket newTickert = new Ticket();
            int lastTicket = LastTicket();
            lastTicket++;


            newTickert.IdTicket = lastTicket;
            newTickert.NumDocument = DateTime.Now.Year;
            newTickert.DataTicket = DateTime.Now;
            newTickert.IdClient = null;
            newTickert.IdComanda = null;
            newTickert.IdAlbara = null;

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var jsonTicket = JsonSerializer.Serialize(newTickert);
            var resposta = client.UploadString($"{LINK_WEB_TICKETS}", "POST", jsonTicket);


            foreach (var item in itemCompraTpvs)
            {
                TicketDetall detalls = new TicketDetall();

                detalls.IdTicket = newTickert.IdTicket;
                detalls.NumDocument = newTickert.NumDocument;
                detalls.IdArticle = item.IdArticle;
                detalls.Quantitat = item.CountArticle;
                detalls.PreuArticle = item.PreuArticle;
                detalls.IvaAplicar = 0;
                detalls.Descompte = 0;

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonDetalls = JsonSerializer.Serialize(detalls);
                var respostaDetalls = client.UploadString($"{LINK_WEB_TICKETDETALLS}", "POST", jsonDetalls);
            }
            

            return newTickert;
        }



        public static int LastTicket()
        {
            int result = -1;
            var anyActual = DateTime.Now.Year; 

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            string resposta = client.UploadString($"{LINK_WEB_TICKETS}{anyActual}", "POST");

            result = Convert.ToInt32(resposta);

            return result;
        }
        #endregion

        #region EMPL
        public static bool LoginEmpl(string userEmpl, string passwordEmpl)
        {
            bool result = false;
            
            
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            
            string resposta = client.UploadString($"{LINK_WEB_EMPL}{userEmpl}/{passwordEmpl}","POST");
            JsonObject jsonResposta = JsonObject.Parse(resposta).AsObject();

            int status = Convert.ToInt32(jsonResposta["status"].ToString());

            if (status == 200)
            {
                //Empl emplSelect = JsonSerializer.Deserialize<Empl>(jsonResposta["empl"]);
                result = true;
            }
            

            return result;
        }


        #endregion

        #region ARTICLES


        public static void UpdateStock(Article article)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var jsonArticle = JsonSerializer.Serialize(article);

            var resposta = client.UploadString($"{LINK_WEB_ARTICLES}{article.IdArticle}", "PUT", jsonArticle);
            JsonObject jsonResposta = JsonObject.Parse(resposta).AsObject();
            

        }

        public static List<Article> GetArticles()
        {
            WebClient client = new WebClient();

            var resposta = client.DownloadString($"{LINK_WEB_ARTICLES}");
            
            
            List<Article> listArticles = JsonSerializer.Deserialize<List<Article>>( resposta );
            listArticles = listArticles.Where(article => article.Stock != 0).ToList();

             
            return listArticles;
        }

        public static Article GetArticle(int idArticle)
        {
            WebClient client = new WebClient();

            var resposta = client.DownloadString($"{LINK_WEB_ARTICLES}{idArticle}");

            Article article = JsonSerializer.Deserialize<Article>(resposta);

            return article;
        }

        #endregion


        #region COMANDES / COMANDES DETALLS

        public static List<ComandaVendum> GetComandesVenda()
        {
            WebClient client = new WebClient();

            var resposta = client.DownloadString($"{LINK_WEB_COMANDESVENDES}");
            List<ComandaVendum> llistaComandesVenda = JsonSerializer.Deserialize<List<ComandaVendum>>(resposta);
            var llista = llistaComandesVenda.Where(venda => venda.EstatComandaVenda == "Pendent").ToList();

            return llista;
        }

        public static List<ComandaVendaDetall> GetDetalls(ComandaVendum comandaSelect)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var resposta = client.DownloadString($"{LINK_WEB_COMANDESVENDESDETALLS}GetDetalls/{comandaSelect.IdComanda}");

            List<ComandaVendaDetall> llistaDetalls = JsonSerializer.Deserialize<List<ComandaVendaDetall>>(resposta);

            return llistaDetalls;
        }

        #endregion


        #region ALBARA / ALBARA DETALLS

        public void CrearAlbara(ComandaVendum comandaSelect)
        {
            AlbaraVendum newAlbara = new AlbaraVendum();
            newAlbara.Data = DateTime.Now;


            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var jsonAlabara = JsonSerializer.Serialize(newAlbara);
            var resposta = client.UploadString($"{LINK_WEB_ALBARAVENDA}", "POST", jsonAlabara);

            var albaraNou = JsonSerializer.Deserialize<AlbaraVendum>(resposta);

            var llistaDetalls = GetDetalls(comandaSelect);


            for (int idx = 0; idx < llistaDetalls.Count; idx++)
            {
                var detall = llistaDetalls[idx];
            
                var albaraDetall = new AlbaraVendaDetall();

                var article = GetArticle(detall.IdArticle);

                albaraDetall.IdAlbaraVenda = albaraNou.IdAlbara;
                albaraDetall.IdArticle = article.IdArticle;
                albaraDetall.Quantitat = detall.QuantitatDemanada;
                detall.QuantitatServida = detall.QuantitatDemanada;


                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonDetalls = JsonSerializer.Serialize(detall);
                var respostaDetall = client.UploadString($"{LINK_WEB_COMANDESVENDESDETALLS}UpdateComandaVendaDetall/{detall.IdComandaVenda}/{detall.IdArticle}", "PUT", jsonDetalls);
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonAlbaraDetall = JsonSerializer.Serialize(albaraDetall);
                var respostaAlbaraDetall = client.UploadString($"{LINK_WEB_ALBARAVENDADETALLS}", "POST", jsonAlbaraDetall);

                var algo = 1;

            }

            comandaSelect.EstatComandaVenda = EstatComandaVenda.Recollir.ToString();

            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var jsonComanda = JsonSerializer.Serialize(comandaSelect);
            var respostaComanda = client.UploadString($"{LINK_WEB_COMANDESVENDES}{comandaSelect.IdComanda}", "PUT", jsonComanda);


            //TICKET


            Ticket newTickert = new Ticket();
            int lastTicket = LastTicket();
            lastTicket++;


            newTickert.IdTicket = lastTicket;
            newTickert.NumDocument = DateTime.Now.Year;
            newTickert.DataTicket = DateTime.Now;
            newTickert.IdClient = comandaSelect.IdClient;
            newTickert.IdComanda = comandaSelect.IdComanda;
            newTickert.IdAlbara = albaraNou.IdAlbara;



            
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var jsonTicket = JsonSerializer.Serialize(newTickert);
            var respostaTicket = client.UploadString($"{LINK_WEB_TICKETS}", "POST", jsonTicket);

            foreach (var item in llistaDetalls)
            {
                TicketDetall detalls = new TicketDetall();

                var article = GetArticle(item.IdArticle);

                detalls.IdTicket = newTickert.IdTicket;
                detalls.NumDocument = newTickert.NumDocument;
                detalls.IdArticle = item.IdArticle;
                detalls.Quantitat = item.QuantitatServida;
                detalls.PreuArticle = article.PreuVenta;
                detalls.IvaAplicar = 0;
                detalls.Descompte = 0;

                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var jsonDetalls = JsonSerializer.Serialize(detalls);
                var respostaDetalls = client.UploadString($"{LINK_WEB_TICKETDETALLS}", "POST", jsonDetalls);
            }


           

        }


        public bool EnviarNotific(ComandaVendum comanda)
        {
            bool result = false;
            var clientSelect = GetClient(comanda.IdClient);
            string titol = "COMPRA";
            string body = "Comanda preparada per recollir";

            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";

            var resposta = client.UploadString($"{LINK_WEB_CLIENT}{titol}/{body}/{clientSelect.IdClient}","POST");

            if (resposta == "1")
                result = true;
            
            return result;
        }

        private Client GetClient(int idClient)
        {
            WebClient client = new WebClient();
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            var resposta = client.DownloadString($"{LINK_WEB_CLIENT}{idClient}");

            Client clientSelect = JsonSerializer.Deserialize<Client>(resposta);

            return clientSelect;
        }



        #endregion

    }
}
