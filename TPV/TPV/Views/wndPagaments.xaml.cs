using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using TPV.Views;

namespace TPV
{
    /// <summary>
    /// Lógica de interacción para wndPagaments.xaml
    /// </summary>
    public partial class wndPagaments : Window
    {
        public int IdTicket { get; set; }
        public int NumDocumentTicket { get; set; }

        public wndPagaments(int idTicket, int numdocument)
        {
            InitializeComponent();

            IdTicket = idTicket;
            NumDocumentTicket = numdocument;
        }

        private void btnFactura_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnTicket_Click(object sender, RoutedEventArgs e)
        {
            wndTicket window = new wndTicket(IdTicket, NumDocumentTicket);
            window.Show();


        }
    }
}
