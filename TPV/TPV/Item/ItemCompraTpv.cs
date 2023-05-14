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
        public int countArticle;
        public int idx;
        public string nomArticle;
        public double preuArticle;
        public ICommand EliminarClick { get; set; }
        public ICommand SumaClick { get; set; }
        public ICommand RestaClick { get; set; }


        public int CountArticle { get => countArticle; set => SetProperty(ref countArticle, value); }
        public int Idx{get => idx;set=>SetProperty(ref idx,value); }    
        public string NomArticle { get => nomArticle; set => SetProperty(ref nomArticle, value); }
        public double PreuArticle { get => preuArticle; set => SetProperty(ref preuArticle, value); }


        public ItemCompraTpv(int idx) 
        {
            CountArticle = 1;
            Idx = idx;
            /*
            SumaClick = new RelayCommand(obj => SumaCommand());
            RestaClick = new RelayCommand(obj => RestaCommand());
            EliminarClick = new RelayCommand(obj => EliminarCommand());
            */

        }

        private void SumaCommand()
        {
            CountArticle++;
            var algo = 1;
        }

        private void RestaCommand()
        {
            if (CountArticle == 1)
                EliminarCommand();
            else
                CountArticle--;

            var nada = 1;
        }

        private void EliminarCommand()
        {

            var algo = 1;
        }

    }
}
