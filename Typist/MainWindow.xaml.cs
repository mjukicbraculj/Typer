using System;
using System.Collections.Generic;
using System.Configuration;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string username;
        public string Username
        {
            get
            {
                return username;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DB.Prepare();
            if ((username = ConfigurationManager.AppSettings["username"]) != null)
                ShowMainScreen(username);
            else
                ShowLoginScreen();
            //DB.Drop();
        }

        #region screenVisibility

        public void ShowLoginScreen()
        {
            this.username = null;
            this.Content = new Login(this);
        }

        public void ShowMainScreen(string username)
        {
            this.username = username;
            this.Content = new MainScreen(this);
        }
        
        #endregion screenVisibility

        

    }
}
