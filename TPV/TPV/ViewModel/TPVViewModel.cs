using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;
using TPV.Infraestructura;
using TPV.Item;
using TPV.Models;

namespace TPV.ViewModel
{
    public class TPVViewModel : ObservableBase
    {
        ObservableCollection<object> articles;
        ObservableCollection<ItemCompraTpv> llistaCompra;

        
        DAOManagerAPI DAOManagerAPI;
        
        public ICommand AddArticleCompraCommand { get; set; }
        public ICommand SumaArticle { get; set; }
        public ICommand RestaArticle { get; set; }
        public ICommand EliminarArticle { get; set; }
        public ICommand AfegirCommand { get; set; }
        public ObservableCollection<object> Articles {get => articles; set => SetProperty(ref articles, value); }
        public ObservableCollection<ItemCompraTpv> LlistaCompra { get => llistaCompra; set => SetProperty(ref llistaCompra, value); }

        public TPVViewModel() 
        {
            DAOManagerAPI = new DAOManagerAPI();

            //AddArticleCompraCommand = new RelayCommand(o=> AddArticleCompra(o), o=>CanExecute());

            articles = new ObservableCollection<object>();
            llistaCompra= new ObservableCollection<ItemCompraTpv>();

            AddArticleCompraCommand = new RelayCommand<int>(AddArticleCompra);
            SumaArticle = new RelayCommand<int>(SumaArticleCompra);
            RestaArticle = new RelayCommand<int>(RestaArticleCompra);
            EliminarArticle = new RelayCommand<int>(EliminarArticleCompra);

            //AFEGIR ARTICLES AL LISTVIEW
            GetArticles();
        }

        private void EliminarArticleCompra(int idx)
        {
            var nada = "";


            LlistaCompra.RemoveAt(idx);
        }

        private void RestaArticleCompra(int idx)
        {

            ItemCompraTpv item = LlistaCompra[idx];
            item.CountArticle--;

            if (item.countArticle == 0)
                EliminarArticleCompra(idx);
            var nada = 1;

        }

        private void SumaArticleCompra(int idx)
        {
            ItemCompraTpv item = LlistaCompra[idx];
            item.CountArticle++;

            var nada = 1;
        }

        public void AddArticleCompra(int idArticle)
{
            
            Article articleSelect = DAOManagerAPI.GetArticle(idArticle);
          
            
            var idx = LlistaCompra.Count;

            ItemCompraTpv item = new ItemCompraTpv(idx);

            item.NomArticle = articleSelect.NomArticle;
            item.PreuArticle = articleSelect.PreuVenta;

            LlistaCompra.Add(item);
            
            item.SumaClick = SumaArticle;
            item.RestaClick = RestaArticle;
            item.EliminarClick = EliminarArticle;
            

            /*
            LlistaCompra.Add(new { NomArticle = articleSelect.NomArticle, PreuArticle = $"{articleSelect.PreuVenta} €/u",
                CountArticle = 1, SumaClick = SumaArticle, RestaClick = RestaArticle, EliminarClick = EliminarArticle, Idx = idx});
            
             */
            var nada = 1;
        }

        
        private void GetArticles()
        {            
            var llistaArticles = DAOManagerAPI.GetArticles();

            foreach (var article in llistaArticles)
            {
                BitmapImage imgDb = new BitmapImage();
                if (article.FotoArticle != null)
                {
                    using (var mem = new MemoryStream(article.FotoArticle))
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
                
                Articles.Add(new { Img = imgDb, NomArticle = article.NomArticle.ToString(), PreuArticle = $"{article.PreuVenta} €/u", Command = AddArticleCompraCommand, IdArticle = Convert.ToInt32(article.IdArticle.ToString()) });
                //IdArticle = Convert.ToInt32(article.IdArticle.ToString())
                //DataContext="{Binding RelativeSource={RelativeSource AncestorType={x:Type ListBox}}, Path=DataContext}"
            }
        }
        
      
        

    }
}
