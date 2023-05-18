using Servidor.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Infraestructura;
using TPV.Models;

namespace TPV.ViewModel
{
    public class TicketViewModel : ObservableBase
    {
        DAOManagerAPI DAOManagerAPI;

        ObservableCollection<object> llistaTicket;
        private string titolTicket;
        private int idTicket;
        private int numDocument;
        private double preuTicket;
        private string totalTicket;


        public ObservableCollection<object> LlistaTicket { get => llistaTicket; set => SetProperty(ref llistaTicket, value); }
        public string TitolTicket { get => titolTicket; set => SetProperty(ref titolTicket, value); }
        public int IdTicket { get => idTicket; set => SetProperty(ref idTicket, value); }
        public int NumDocument { get => numDocument;set=>SetProperty(ref numDocument, value); } 
        public double PreuTicket { get => preuTicket;set=>SetProperty(ref preuTicket, value); } 
        public string TotalTicket { get => totalTicket; set => SetProperty(ref totalTicket, value); }

        public TicketViewModel(int idTicket, int numDocument)
        {
            DAOManagerAPI = new DAOManagerAPI();
            IdTicket = idTicket;
            NumDocument = numDocument;
            TitolTicket = $"Ticket #{IdTicket} - {NumDocument}";
            llistaTicket = new ObservableCollection<object>();
            PreuTicket = 0;

            GetTicket();
        }



        public void GetTicket()
        {
            List<TicketDetall> ticketsDetalls = DAOManagerAPI.GetTicketsDetalls(IdTicket, NumDocument);

            foreach(var detall in ticketsDetalls)
            {
                Article article = DAOManagerAPI.GetArticle(detall.IdArticle);
                LlistaTicket.Add(new { NomArticle = article.NomArticle, UnitatsArticle = detall.Quantitat });
                PreuTicket += article.PreuVenta * (double)detall.Quantitat;
            
            }
            UpdatePreu();
        }


        private void UpdatePreu()
        {
            TotalTicket = $"TOTAL: {PreuTicket.ToString("0.##")}€";
        }
    }
}
