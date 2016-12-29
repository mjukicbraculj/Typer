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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Login loginScreen;
        MainScreen mainScreen;
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
            loginScreen = new Login(this);
            DB.Prepare();
            ShowLoginScreen();
            //DB.Drop();
        }

        #region screenVisibility

        private void ShowLoginScreen()
        {
            this.Content = loginScreen;
        }

        public void ShowMainScreen(string username)
        {
            this.username = username;
            if (mainScreen == null)
                mainScreen = new MainScreen(this);
            this.Content = mainScreen;
        }
        
        #endregion screenVisibility



    }
}
