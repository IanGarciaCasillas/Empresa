using Servidor.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TPV.Models;

namespace TPV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DAOManagerAPI DAOManagerAPI;

        public MainWindow(DAOManagerAPI manager)
        {
            
            DAOManagerAPI = manager;
            InitializeComponent();
            
            tbControl.SelectedIndex = 2;
            List<Article> llistaArticles = DAOManagerAPI.GetArticles();
            CarregarArticles(llistaArticles);
            
            
            
        }


        private void btnConsultaTicket_TPV_Click(object sender, RoutedEventArgs e)
        {
            WndConsultaTicket wndConsultaTicket = new WndConsultaTicket();
            wndConsultaTicket.ShowDialog();


        }


        private void CarregarArticles(List<Article> llistaArticles)
        {
            foreach(var article in llistaArticles)
            {
                BitmapImage imgDb = new BitmapImage();
                if(article.FotoArticle != null)
                {
                    using(var mem = new MemoryStream(article.FotoArticle))
                    {
                        mem.Position = 0;
                        imgDb.BeginInit();
                        imgDb.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        imgDb.CacheOption = BitmapCacheOption.OnLoad;
                        imgDb.UriSource = null;
                        imgDb.StreamSource = mem;
                        imgDb.EndInit();
                    }
                }

                lsbArticles_Tpv.Items.Add(new { Img = imgDb, NomArticle = article.NomArticle, PreuArticle = $"{article.PreuVenta} €/u", IdArticle = article.IdArticle} );
            }
        }

        private void AfegirArticleCompra(object sender, RoutedEventArgs e)
        {
            var btnSelect = (Button)sender;
            var idArticle = btnSelect.Tag.ToString();

            Article articleSeclect = DAOManagerAPI.GetArticle(Convert.ToInt32(idArticle));

        }

        private void btnRestaArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSumaArticle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleterArticle_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
