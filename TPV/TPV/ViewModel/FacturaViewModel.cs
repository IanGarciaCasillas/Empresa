using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPV.Infraestructura;
using TPV.Models;

namespace TPV.ViewModel
{
    public class FacturaViewModel : ObservableBase
    {
        private string idTicketString;
        private string numDocumentString;
        private int idTicket;
        private int numDocument;


        public int IdTicket { get => idTicket;set=>SetProperty(ref idTicket, value); }
        public int NumDocument { get => numDocument;set=>SetProperty(ref numDocument, value); }
        public string IdTicketString { get => idTicketString; set => SetProperty(ref idTicketString, value); }
        public string NumDocumentString { get => numDocumentString;set=>SetProperty(ref numDocumentString, value); }

        public FacturaViewModel(int idTicket, int numDocument)
        {

            IdTicket = idTicket;
            NumDocument = numDocument;

            IdTicketString = $"ID TICKET: #{IdTicket}";
            NumDocumentString = $"NUM DOCUMENT: {NumDocument}";

        }

    }
}
