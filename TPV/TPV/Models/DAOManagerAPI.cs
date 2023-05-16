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


        #region TICKET / TICKETS DETALLS
        public int PagarTPV(List<ItemCompraTpv> itemCompraTpvs)
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



            return lastTicket;
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
        public static List<Article> GetArticles()
        {
            WebClient client = new WebClient();

            var resposta = client.DownloadString($"{LINK_WEB_ARTICLES}");
            
            
            List<Article> listArticles = JsonSerializer.Deserialize<List<Article>>( resposta );
            var espera = "";
             
            return listArticles;
        }

        public static Article GetArticle(int idArticle)
        {
            WebClient client = new WebClient();

            var resposta = client.DownloadString($"{LINK_WEB_ARTICLES}{idArticle}");

            var espero = "";
            Article article = JsonSerializer.Deserialize<Article>(resposta);

            return article;
        }

        #endregion

    }
}
