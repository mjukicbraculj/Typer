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
using Typist.StageControls;
using Typist.Contoller;
using System.Configuration;

namespace Typist
{
    /// <summary>
    /// Interaction logic for MainScreen.xaml
    /// </summary>
    public partial class MainScreen : UserControl
    {
        MainWindow mainWind;
        public MainWindow MainWind { get { return mainWind; } }

        public MainScreen(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWind = mainWindow;
            if (mainWind.Username == null)
            {
                ProfileBtn.IsEnabled = false;
                LogOutBtn.Content = "Log in";
            }
            this.DataContext = this;
            BeginnerSP.Visibility = Visibility.Collapsed;
            IntermediateSP.Visibility = Visibility.Collapsed;
            AdvancedSP.Visibility = Visibility.Collapsed;
            PractiseSP.Visibility = Visibility.Collapsed;
            MakePraciseButtons();
            //AddLessonsToDataBase();
        }


        #region adding lesson to database

        /// <summary>
        /// Called only once for adding lessons to database.
        /// </summary>
        /// <param name="panel"></param>
        /// <param name="parent"></param>
        private void AddLesson(StackPanel panel, string parent)
        {
            string error;
            foreach (Button b in panel.Children)
            {
                error = LessonController.AddLesson(b.Content.ToString(), parent);
                if (error != null)
                    MessageBox.Show(error);
            }
        }

        private void AddLessonsToDataBase()
        {
            AddLesson(BeginnerSP, "Beginner");
            AddLesson(IntermediateSP, "Intermediate");
            AddLesson(AdvancedSP, "Advanced");
            AddLesson(PractiseSP, "Practise");
        }
        #endregion adding lesson to database

        /// <summary>
        /// hide or show buttons for clicked Group (Beginner, ...)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region menu visiblity
        private void ShowBeginnerChoice(object sender, RoutedEventArgs e)
        {
            IntermediateSP.Visibility = Visibility.Collapsed;
            AdvancedSP.Visibility = Visibility.Collapsed;
            PractiseSP.Visibility = Visibility.Collapsed;
            if (BeginnerSP.Visibility == Visibility.Collapsed)
                BeginnerSP.Visibility = Visibility.Visible;
            else
                BeginnerSP.Visibility = Visibility.Collapsed;
        }

        private void ShowIntermediateChoice(object sender, RoutedEventArgs e)
        {
            BeginnerSP.Visibility = Visibility.Collapsed;
            AdvancedSP.Visibility = Visibility.Collapsed;
            PractiseSP.Visibility = Visibility.Collapsed;
            if (IntermediateSP.Visibility == Visibility.Collapsed)
                IntermediateSP.Visibility = Visibility.Visible;
            else
                IntermediateSP.Visibility = Visibility.Collapsed;
        }

        private void ShowAdvancedChoice(object sender, RoutedEventArgs e)
        {
            BeginnerSP.Visibility = Visibility.Collapsed;
            IntermediateSP.Visibility = Visibility.Collapsed;
            PractiseSP.Visibility = Visibility.Collapsed;
            if (AdvancedSP.Visibility == Visibility.Collapsed)
                AdvancedSP.Visibility = Visibility.Visible;
            else
                AdvancedSP.Visibility = Visibility.Collapsed;
        }

        private void ShowPractiseChoice(object sender, RoutedEventArgs e)
        {
            BeginnerSP.Visibility = Visibility.Collapsed;
            IntermediateSP.Visibility = Visibility.Collapsed;
            AdvancedSP.Visibility = Visibility.Collapsed;
            if (PractiseSP.Visibility == Visibility.Collapsed)
                PractiseSP.Visibility = Visibility.Visible;
            else
                PractiseSP.Visibility = Visibility.Collapsed;
        }
        #endregion menu visibilty

        /// <summary>
        /// Create Buttons for Practise group.
        /// Needed because app alows creating lessons in this group.
        /// When new lesson is added than new button is added to.
        /// </summary>
        public void MakePraciseButtons()
        {
            PractiseSP.Children.Clear();
            List<string> practiseLessons = LessonController.GetLessonsName("Practise");
            foreach (string s in practiseLessons)
            {
                AddPractiseButton(s);
            }
        }

        /// <summary>
        /// Creates practise button.
        /// Adds is it to Stack Panel.
        /// </summary>
        /// <param name="name">content of the button</param>
        public void AddPractiseButton(string name)
        {
            Button btn = new Button();
            btn.Content = name;
            btn.SetResourceReference(Grid.BackgroundProperty, "childButtonBackground");
            btn.Height = 25;
            btn.Click += LessonClick;
            PractiseSP.Children.Add(btn);
        }

        /// <summary>
        /// Shows introductionControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IntroductionClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new IntroductionControl(mainWind.Username));
        }

        /// <summary>
        /// Shows LessonPlayControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new LessonPlay(btn.Content.ToString(), mainWind.Username));                             
        }

        /// <summary>
        /// Shows AddTextControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTextClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new AddTextControl(mainWind.Username, this, 
                                                            (sender as Button).Content.ToString()));
        }
        /// <summary>
        /// Shows ShowProfileControl
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowProfileClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new ShowProfileControl(mainWind.Username));
        }

        /// <summary>
        /// Shows ShowSettingsControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new ShowSettingsControl());
        }

        /// <summary>
        /// Removes key "username" from app.config
        /// Switches to login screen.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LogOutClick(object sender, RoutedEventArgs e)
        {
            DeleteToConfigSettings();
            mainWind.ShowLoginScreen();
        }

        /// <summary>
        /// removes key "username" from app.config
        /// </summary>
        private void DeleteToConfigSettings()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings.Remove("username");
            config.Save(ConfigurationSaveMode.Modified);
        }

    }
}
