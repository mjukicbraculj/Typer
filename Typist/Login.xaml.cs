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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Typist.Contoller;

namespace Typist
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        MainWindow mainWindow;

        public Login(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }


        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string error = UserContoller.VerifyLogin(UsernameTB.Text, PasswordTB.Password);
            if (error == null)
            {
                LoginErrorTB.Visibility = Visibility.Collapsed;
                mainWindow.ShowMainScreen(UsernameTB.Text);
            }
            else
            {
                LoginErrorTB.Text = error;
                LoginErrorTB.Visibility = Visibility.Visible;
            }
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            string error = UserContoller.AddUser(UsernameTB.Text, PasswordTB.Password);
            if (error == null)
            {
                LoginErrorTB.Visibility = Visibility.Collapsed;
                SuccessfulRegTB.Visibility = Visibility.Visible;
            }
            else
            {
                LoginErrorTB.Text = error;
                LoginErrorTB.Visibility = Visibility.Visible;
            }
        }

        private void ProceedClick(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowMainScreen(null);
        }
    }
}
