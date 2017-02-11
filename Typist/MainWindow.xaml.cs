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
            //DB.Drop();
            DB.Prepare();
            if ((username = ConfigurationManager.AppSettings["username"]) != null)
                ShowMainScreen(username);
            else
                ShowLoginScreen();

            if (ConfigurationManager.AppSettings["Theme"].Equals("GreenTheme.xaml"))
                SetTheme("GreenTheme.xaml");
            else if (ConfigurationManager.AppSettings["Theme"].Equals("RedTheme.xaml"))
                SetTheme("RedTheme.xaml");
        }

        /// <summary>
        /// Method removes all dictionary resources from app resources and
        /// adds new dictionary
        /// </summary>
        /// <param name="themeName">name of new t</param>
        private void SetTheme(string themeName)
        {

            Application.Current.Resources.MergedDictionaries.Clear();

            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri("Themes/" + themeName, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Add(dict);
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
