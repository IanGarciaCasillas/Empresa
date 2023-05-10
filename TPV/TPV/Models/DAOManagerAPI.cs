using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TPV.Models
{
    public class DAOManagerAPI
    {
        public const string LINK_WEB_ARTICLES = "https://localhost:7151/articles/";
        public const string LINK_WEB_EMPL = "https://localhost:7151/empl/";



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
