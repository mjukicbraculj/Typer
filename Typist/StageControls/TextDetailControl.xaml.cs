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

namespace Typist.StageControls
{
    /// <summary>
    /// Interaction logic for LessonDetailControl.xaml
    /// </summary>
    public partial class TextDetailControl : UserControl
    {
        public string Text { get; set; }
        public int Error { get; set; }
        public string Time { get; set; }
        public DateTime Date { get; set; }
        public bool Selected { get; set; }

        public TextDetailControl(string text, int error, string time, DateTime date)
        {
            InitializeComponent();
            Time = time;
            Error = error;
            Text = text;
            Date = date;
            Selected = true;
            this.DataContext = this;
        }
    }
}
