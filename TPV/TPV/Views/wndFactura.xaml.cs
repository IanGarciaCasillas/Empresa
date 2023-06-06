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
using TPV.ViewModel;

namespace TPV.Views
{
    /// <summary>
    /// Lógica de interacción para wndFactura.xaml
    /// </summary>
    public partial class wndFactura : Window
    {
        public wndFactura(int idTicket, int numDocument)
        {
            InitializeComponent();
            var viewModel = new FacturaViewModel(idTicket, numDocument);
            DataContext = viewModel;

        }
    }
}
