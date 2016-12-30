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
using System.Configuration;

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

        /// <summary>
        /// Verify user login and writes to config file 
        /// if keep me loged in is checked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginClick(object sender, RoutedEventArgs e)
        {
            string error = UserContoller.VerifyLogin(UsernameTB.Text, PasswordTB.Password);
            if (error == null)
            {
                LoginErrorTB.Visibility = Visibility.Collapsed;
                if (KeepMeLogedInCB.IsChecked == true)
                    WriteToConfigSettings(UsernameTB.Text);
                mainWindow.ShowMainScreen(UsernameTB.Text);
            }
            else
            {
                LoginErrorTB.Text = error;
                LoginErrorTB.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// Adds new User to database.
        /// Shows message.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// On hyperlink "Proceed without login" click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProceedClick(object sender, RoutedEventArgs e)
        {
            mainWindow.ShowMainScreen(null);
        }

        /// <summary>
        /// Adds key "username" to app.config
        /// </summary>
        /// <param name="username"></param>
        private void WriteToConfigSettings(string username)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Add("username", username);
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
