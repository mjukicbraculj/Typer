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
                ProfileBtn.IsEnabled = false;
            this.DataContext = this;
            BeginnerSP.Visibility = Visibility.Collapsed;
            IntermediateSP.Visibility = Visibility.Collapsed;
            AdvancedSP.Visibility = Visibility.Collapsed;
            PractiseSP.Visibility = Visibility.Collapsed;
            MakePraciseButtons();
            //AddLessonsToDataBase();
        }


        #region adding lesson to database
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

        private void MakePraciseButtons()
        {
            foreach(string s in LessonController.GetLessonsName("Practise"))
            {
                Button btn = new Button();
                btn.Content = s;
                btn.Background = Brushes.LightBlue;
                btn.Height = 25;
                btn.Click += LessonClick;
                PractiseSP.Children.Add(btn);
            }
        }

        private void IntroductionClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new IntroductionControl());
        }


        private void LessonClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new LessonPlay(btn.Content.ToString(), mainWind.Username));                             
        }

        private void AddLessonDetails()
        {

        }

        private void AddTextClick(object sender, RoutedEventArgs e)
        {
            StageContainer.Children.Clear();
            StageContainer.Children.Add(new AddTextControl(mainWind.Username));
        }
    }
}
