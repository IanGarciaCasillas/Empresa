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
using TPV.Models;
using TPV.ViewModel;

namespace TPV
{
    /// <summary>
    /// Lógica de interacción para wndLogin.xaml
    /// </summary>
    public partial class wndLogin : Window
    {
        DAOManagerAPI DAOManagerAPI = new DAOManagerAPI();
        public wndLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userEmpl = txUserEmpl.Text.ToString();
            var passwordEmpl = PbEmpl.Password.ToString();


            if(userEmpl != "" && passwordEmpl != "")
            {
                bool statusLogin = DAOManagerAPI.LoginEmpl(userEmpl, passwordEmpl);

                if (statusLogin)
                {
                    MainWindow window = new MainWindow();
                    this.Close();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("ALGUNA CREDENCIAL NO ES CORRECTA");
                }
            }
        }
    }
}
