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

namespace Typist.StageControls
{
    /// <summary>
    /// Interaction logic for ShowSettingsControl.xaml
    /// </summary>
    public partial class ShowSettingsControl : UserControl
    {
        public ShowSettingsControl()
        {
            InitializeComponent();
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            string theme = config.AppSettings.Settings["Theme"].Value;
            if (theme.Equals("RedTheme.xaml"))
                redCB.IsChecked = true;
            else if (theme.Equals("GreenTheme.xaml"))
                greenCB.IsChecked = true;
        }

        private void SetRedTheme(object sender, RoutedEventArgs e)
        {
            WriteTemeToConfig("RedTheme.xaml");
            SetTheme("RedTheme.xaml");
        }

        private void SetGreenTheme(object sender, RoutedEventArgs e)
        {
            WriteTemeToConfig("GreenTheme.xaml");
            SetTheme("GreenTheme.xaml");
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
            dict.Source = new Uri("Themes/"+themeName, UriKind.Relative);

            Application.Current.Resources.MergedDictionaries.Add(dict);
        }

        private void WriteTemeToConfig(string themeName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Theme"].Value = themeName;
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
}
