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
using System.Threading;
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

        DAOManagerAPI DAOManagerAPI;
        
        #region VARIABLES TPV

        //PRIVADES
        ObservableCollection<object> articles;
        ObservableCollection<ItemCompraTpv> llistaCompra;
        ObservableCollection<ComandaVendum> llistaComandes;
        ObservableCollection<object> llistaComandaDetall;
        private string compraTotal;
        private double preuCompraTotal;
        private int idTicketCompra;
        private int numDocumentTicket;
        private string titolComanda;
        ComandaVendum comandaSelect;
        

        //PUBLIQUES
        public ICommand AddArticleCompraCommand { get; set; }
        public ICommand SumaArticleCommand { get; set; }
        public ICommand RestaArticleCommand { get; set; }
        public ICommand EliminarArticleCommand { get; set; }
        public ICommand AfegirCommand { get; set; }
        public ICommand CancelarCompraCommand { get; set; }
        public ICommand PagarCompraCommand { get; set; }
        public ICommand ConsultarTicketCommand { get; set; }

        public ICommand PreparaComandaCommand { get; set; }
        


        public ObservableCollection<object> Articles {get => articles; set => SetProperty(ref articles, value); }
        public ObservableCollection<ItemCompraTpv> LlistaCompra { get => llistaCompra; set => SetProperty(ref llistaCompra, value); }
        public string CompraTotal { get => compraTotal; set => SetProperty(ref compraTotal, value); }
        public double PreuCompraTotal { get => preuCompraTotal; set => SetProperty(ref preuCompraTotal, value); }
        public int IdTicketCompra { get => idTicketCompra; set => SetProperty(ref idTicketCompra, value); }
        public int NumDocumentTicket { get => numDocumentTicket; set => SetProperty(ref numDocumentTicket, value); }
        public string TitolComanda { get => titolComanda;set=>SetProperty(ref titolComanda, value); }
        public ComandaVendum ComandaSelect { get => comandaSelect;set=>SetProperty(ref comandaSelect, value); }

        public ObservableCollection<ComandaVendum> LlistaComandes { get => llistaComandes; set => SetProperty(ref llistaComandes, value); }
        public ObservableCollection<object> LlistaComandaDetall { get => llistaComandaDetall; set=>SetProperty(ref llistaComandaDetall,value); }






        #endregion


        public TPVViewModel() 
        {
            DAOManagerAPI = new DAOManagerAPI();
            
            //TPV
            articles = new ObservableCollection<object>();
            llistaCompra= new ObservableCollection<ItemCompraTpv>();
            llistaComandes = new ObservableCollection<ComandaVendum>();
            llistaComandaDetall = new ObservableCollection<object>();

            AddArticleCompraCommand = new RelayCommand<int>(AddArticleCompra);
            SumaArticleCommand = new RelayCommand<int>(SumaArticleCompra);
            RestaArticleCommand = new RelayCommand<int>(RestaArticleCompra);
            EliminarArticleCommand = new RelayCommand<int>(EliminarArticleCompra);
            CancelarCompraCommand = new RelayCommand(obj => CancelarCompra(), obj=>CanCancelarCompra());
            PagarCompraCommand = new RelayCommand(obj => PagarCompra(), obj=>CanPagarCompra());
            ConsultarTicketCommand = new RelayCommand(obj => ConsultarTicket());

            PreparaComandaCommand = new RelayCommand(obj => PrepararComanda(), obj=>CanPreparaComanda());
            
            PreuCompraTotal = 0;
            CompraTotal = $"TOTAL {PreuCompraTotal}€";


            //AFEGIR ARTICLES AL LISTVIEW TPV
            GetArticles();
            GetComandes();
        }



        #region Funcions TPV

        private bool CanPreparaComanda()
        {
            bool value;
            if (ComandaSelect == null)
                value = false;
            else
                value = true;

            return value;
        }



        private void ConsultarTicket()
        {
            WndConsultaTicket window = new WndConsultaTicket();
            window.Show();
        }

        private bool CanPagarCompra()
        {
            bool value;
            if (LlistaCompra.Count > 0)
                value = true;
            else
                value = false;

            return value;
        }
        private bool CanCancelarCompra()
        {
            bool value;

            if (LlistaCompra.Count > 0)
                value = true;
            else
                value = false;
            return value;
        }

        private void PagarCompra()
        {
            var newTicket =  DAOManagerAPI.PagarTPV(LlistaCompra.ToList());
            
            wndPagaments window = new wndPagaments(newTicket.IdTicket, newTicket.NumDocument);
            window.Show();
            
            CancelarCompra();


        }

        private void CancelarCompra()
        {
            LlistaCompra.Clear();
            PreuCompraTotal = 0;
            UpdatePreuTotal();
        }


        private void EliminarArticleCompra(int idx)
        {
            ItemCompraTpv item = LlistaCompra.First<ItemCompraTpv>(item => item.Idx == idx);
            LlistaCompra.Remove(item);
            PreuCompraTotal -= item.PreuArticle * item.CountArticle;
            UpdatePreuTotal();

        }

        private void RestaArticleCompra(int idx)
        {
            ItemCompraTpv item = LlistaCompra.First<ItemCompraTpv>(item => item.Idx == idx);
            item.CountArticle--;
            item.Article.Stock += 1.0;

            if (item.countArticle == 0)
            {
                PreuCompraTotal -= item.PreuArticle;
                UpdatePreuTotal();
                EliminarArticleCompra(idx);
            }
            else
            {
                PreuCompraTotal -= item.PreuArticle;
                UpdatePreuTotal();
            }
            DAOManagerAPI.UpdateStock(item.Article);

        }

        private void SumaArticleCompra(int idx)
        {
            ItemCompraTpv item = LlistaCompra.First<ItemCompraTpv>(item=>item.Idx == idx);
            item.Article.Stock -= 1.0;
            if (item.Article.Stock > 0)
            {
                item.CountArticle++;
                PreuCompraTotal += item.PreuArticle;
                UpdatePreuTotal();
                DAOManagerAPI.UpdateStock(item.Article);
            }
        }

        public void AddArticleCompra(int idArticle)
        {            
            Article articleSelect = DAOManagerAPI.GetArticle(idArticle);


            if (LlistaCompra.Any(item => item.IdArticle == articleSelect.IdArticle))
            {
                ItemCompraTpv item = LlistaCompra.First(item => item.IdArticle == articleSelect.IdArticle);
                    
                item.CountArticle++;
                PreuCompraTotal += item.PreuArticle;
            }
            else
            {
                var idx = LlistaCompra.Count;

                ItemCompraTpv item = new ItemCompraTpv(idx);

                item.NomArticle = articleSelect.NomArticle;
                item.IdArticle = articleSelect.IdArticle;
                item.PreuArticle = articleSelect.PreuVenta;
                item.SumaClick = SumaArticleCommand;
                item.RestaClick = RestaArticleCommand;
                item.EliminarClick = EliminarArticleCommand;
                item.Article = articleSelect;

                LlistaCompra.Add(item);
                PreuCompraTotal += item.PreuArticle;

                DAOManagerAPI.UpdateStock(articleSelect);
            }
            UpdatePreuTotal();
        }

        private void GetComandes()
        {
            var llistaComandes = DAOManagerAPI.GetComandesVenda();
            LlistaComandes.Clear();
            foreach(var comanda in llistaComandes)
            {
                LlistaComandes.Add(comanda);
            }
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
            }
        }

        private void UpdatePreuTotal()
        {
            CompraTotal = $"TOTAL {PreuCompraTotal.ToString("0.##")}€";
        }

        public void ChangeComanda(int idxComanda)
        {
            if(idxComanda != -1)
            {
                ComandaVendum comandaSelect = LlistaComandes[idxComanda];

                List<ComandaVendaDetall>comandaDetalls = DAOManagerAPI.GetDetalls(comandaSelect);
                LlistaComandaDetall.Clear();

                for (int idx = 0; idx < comandaDetalls.Count; idx++)
                {
                    var detall = comandaDetalls[idx];
                    Article article = DAOManagerAPI.GetArticle(detall.IdArticle);
                    LlistaComandaDetall.Add(new { UnitatsDemanades = detall.QuantitatDemanada, NomArticleDetall = article.NomArticle });
                }
                ComandaSelect = comandaSelect;
                TitolComanda = $"COMANDA: {comandaSelect.IdComanda}";
            }
        }


        private void PrepararComanda()
        {
            DAOManagerAPI.CrearAlbara(ComandaSelect);
            DAOManagerAPI.EnviarNotific(ComandaSelect);
            GetComandes();
            ComandaSelect = null;
            TitolComanda = "";
            LlistaComandaDetall.Clear();

        }

        #endregion
    }
}
