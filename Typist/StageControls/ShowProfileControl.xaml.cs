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
    /// Interaction logic for ShowProfileControl.xaml
    /// </summary>
    public partial class ShowProfileControl : UserControl
    {
        private string Username { get; set; }
        public ShowProfileControl(string username)
        {
            InitializeComponent();
            Username = username;
            if (username != null)
            {
                ProfileGrid.Visibility = Visibility.Visible;
                DearTB.Text = "Dear " + username + ", ";
                LessonDetailsAverageLV.ItemsSource = LessonDetailController.GetLessonDetailsByUser(username);
            }
        }


    }
}
