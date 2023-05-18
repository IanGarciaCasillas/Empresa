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
using TPV.Infraestructura;
using TPV.Models;
using TPV.ViewModel;

namespace TPV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            
            tbControl.SelectedIndex = 2;
            
        }

        private void btnNada_Click(object sender, RoutedEventArgs e)
        {
            wndPagaments window = new wndPagaments(8,2023);
            window.Show();
        }
    }
}
