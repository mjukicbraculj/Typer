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

namespace Typist.StageControls
{
    /// <summary>
    /// Interaction logic for AddTextControl.xaml
    /// </summary>
    public partial class AddTextControl : UserControl
    {
        private string username;
        Dictionary<string, List<string>> groupLessons;
        public AddTextControl(string username)
        {
            InitializeComponent();
            this.username = username;
            groupLessons = new Dictionary<string, List<string>>();
            LessonSP.Visibility = Visibility.Collapsed;
            TextSP.Visibility = Visibility.Collapsed;
        }

        private void AddTextClick(object sender, RoutedEventArgs e)
        {
            ResultTB.Text = TextController.AddText(TextTB.Text, LessonCB.Text);
            if (ResultTB.Text.Equals("Done successfully!"))
                TextTB.Text = String.Empty;
        }

        private void GroupSelectionDone(object sender, EventArgs e)
        {
            ComboBox CBox = sender as ComboBox;
            if (CBox.SelectedIndex > -1)
            {
                LessonSP.Visibility = Visibility.Visible;
                if (CBox.SelectedIndex != 3)
                    NewLessonSP.Visibility = Visibility.Collapsed;
                else
                    NewLessonSP.Visibility = Visibility.Visible;
                if (!groupLessons.Keys.Contains(CBox.Text))
                    groupLessons.Add(CBox.Text, LessonController.GetLessonsName(CBox.Text));
                LessonCB.ItemsSource = groupLessons[CBox.Text];
            }
        }

        private void AddNewLessonClick(object sender, RoutedEventArgs e)
        {
            string error = LessonController.AddLesson(LessonNameTB.Text, GroupCB.Text);
            if (error != null)
                MessageBox.Show(error);
            else
            {
                groupLessons[GroupCB.Text] = LessonController.GetLessonsName(GroupCB.Text);
                LessonCB.ItemsSource = groupLessons[GroupCB.Text];
            }
        }

        private void LessonSelectionDone(object sender, EventArgs e)
        {
            TextSP.Visibility = Visibility.Visible;
        }

        private void ClearResultTB(object sender, KeyEventArgs e)
        {
            ResultTB.Text = String.Empty;
        }

        
    }
}
