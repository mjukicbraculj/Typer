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
        MainScreen parent;
        public string LessonName { get; set; }
        public AddTextControl(string username, MainScreen parent, string heading)
        {
            InitializeComponent();
            this.username = username;
            if (username != null)
                WellcomeTB.Text = "Dear " + username + ",";
            else
                WellcomeTB.Text = "Dear user,";
            groupLessons = new Dictionary<string, List<string>>();
            LessonSP.Visibility = Visibility.Collapsed;
            TextSP.Visibility = Visibility.Collapsed;
            this.parent = parent;
            LessonName = heading;
            this.DataContext = this;
        }

        /// <summary>
        /// If selected group and lesson,
        /// adds text to database.
        /// </summary>
        /// <param name="sender">Button AddText</param>
        /// <param name="e"></param>
        private void AddTextClick(object sender, RoutedEventArgs e)
        {
            if (LessonCB.SelectedIndex == -1)
            {
                ResultTB.Text = "You must chose lesson name!";
                return;
            }
            ResultTB.Text = TextController.AddText(TextTB.Text.Trim().Replace("'", "''"), LessonCB.Text);
            if (ResultTB.Text.Equals("Done successfully!"))
                TextTB.Text = String.Empty;
        }

        /// <summary>
        /// Called on dropDown of ComboBox
        /// when user selects lesson group.
        /// </summary>
        /// <param name="sender">ComboBox GroupCB</param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Called on Add text Button.
        /// </summary>
        /// <param name="sender">Button Add text</param>
        /// <param name="e"></param>
        private void AddNewLessonClick(object sender, RoutedEventArgs e)
        {
            string error = LessonController.AddLesson(LessonNameTB.Text, GroupCB.Text);
            if (error != null)
                MessageBox.Show(error);
            else
            {
                groupLessons[GroupCB.Text].Add(LessonNameTB.Text);
                LessonCB.ItemsSource = groupLessons[GroupCB.Text];
                LessonCB.Items.Refresh();
                parent.AddPractiseButton(LessonNameTB.Text);
            }
        }

        /// <summary>
        /// Called on drop down of lessons combobox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LessonSelectionDone(object sender, EventArgs e)
        {
            TextSP.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Used for clearing message about adding text 
        /// to database when doing that more than once.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearResultTB(object sender, KeyEventArgs e)
        {
            ResultTB.Text = String.Empty;
        }

        
    }
}
