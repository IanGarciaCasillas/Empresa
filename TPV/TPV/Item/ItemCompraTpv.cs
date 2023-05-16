using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TPV.Infraestructura;
using TPV.ViewModel;

namespace TPV.Item
{
    public class ItemCompraTpv : ObservableBase
    {
        public Article article;
        public int countArticle;
        public int idArticle;
        public int idx;
        public string nomArticle;
        public double preuArticle;
        public ICommand EliminarClick { get; set; }
        public ICommand SumaClick { get; set; }
        public ICommand RestaClick { get; set; }

        public int IdArticle { get => idArticle; set => SetProperty(ref idArticle, value); }
        public int CountArticle { get => countArticle; set => SetProperty(ref countArticle, value); }
        public int Idx{get => idx;set=>SetProperty(ref idx,value); }    
        public string NomArticle { get => nomArticle; set => SetProperty(ref nomArticle, value); }
        public double PreuArticle { get => preuArticle; set => SetProperty(ref preuArticle, value); }
        public Article Article { get => article;set=>SetProperty(ref article, value); }

        public ItemCompraTpv(int idx) 
        {
            CountArticle = 1;
            Idx = idx;
        }
    }
}
