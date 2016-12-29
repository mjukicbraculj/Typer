using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Typist.Contoller;
using Typist.Objects;
using System.Windows.Threading;

namespace Typist.StageControls
{
    /// <summary>
    /// Interaction logic for LessonPlay.xaml
    /// </summary>
    public partial class LessonPlay : UserControl
    {
        public string LessonName {get; set;}
        string username;
        int lessonId;
        int userId;

        int currentTextId;
        string currentText;
        List<string> texts;
        char currentLetter;
        TextPointer currentPosition;
        int letterCounter;
        TextRange markedLetter;

        int errors;

        Stopwatch sw;
        DispatcherTimer dt;

        Dictionary<char, Button> charBtnDict;

        public string ShowHideHistroyText { get; set; }
        public LessonPlay(string lessonName, string username)
        {
            InitializeComponent();
            this.DataContext = this;
            this.username = username;
            LessonName = lessonName;

            lessonId = LessonController.GetLessonId(lessonName);
            userId = UserContoller.GetUserId(username);

            TypeGrid.Visibility = Visibility.Collapsed;
            EndOfLessonGrid.Visibility = Visibility.Collapsed;
            if (username == null)
            {
                UserDetailsSP.Visibility = Visibility.Collapsed;
                WellcomeTB.Text = "Dear user,";
            }
            else
                WellcomeTB.Text = "Dear " + username + ", ";
            HyperLinkText.Text = "List previous results...";
            InitializeDictionaryOfButtons();
            nextBtn.IsEnabled = false;

            texts = new List<string>();
            errors = 0;

            TimeTB.Text = "00:00:00";
            SpeedTB.Text = "0.000";
            ErrorsTB.Text = errors.ToString();

            sw = new Stopwatch();
            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(Tick);
            dt.Interval = new TimeSpan(0, 0, 1);

        }

        private void Tick(object sender, EventArgs e)   
        {  
            if (sw.IsRunning)   
            {  
                TimeSpan ts = sw.Elapsed;  
                TimeTB.Text = String.Format("{0:00}:{1:00}:{2:00}",  
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
                SpeedTB.Text = (letterCounter / ts.TotalSeconds).ToString();
                ErrorsTB.Text = errors.ToString();
            }  
        }  

        private void GoClick(object sender, RoutedEventArgs e)
        {
            EndOfLessonGrid.Visibility = Visibility.Collapsed;
            WellcomeGird.Visibility = Visibility.Collapsed;
            TypeGrid.Visibility = Visibility.Visible;
            TypeGrid.Focus();       //key down affects it now
            texts = TextController.GetTexts(lessonId);
            currentTextId = -1;
            NextBtnClick(null, null);
        }

        private void NextBtnClick(object sender, RoutedEventArgs e)
        {
            RTBParagraph.Inlines.Clear();
            nextBtn.IsEnabled = false;
            ++currentTextId;
            TextCounterTB.Text = currentTextId + "/" + texts.Count;
            currentText = texts[currentTextId];
            currentText = currentText.Replace(Environment.NewLine, " \n");
            RTBParagraph.Inlines.Add(new Run(currentText));
            letterCounter = -1;
            currentPosition = null;
            MarkNextLetter();
        }

        private void ShowLessonEndScreen()
        {
            if (username != null)
            {
                LessonDetail detail = new LessonDetail(Convert.ToDouble(SpeedTB.Text),
                                                        Convert.ToInt32(ErrorsTB.Text),
                                                        TimeTB.Text,
                                                        DateTime.Now.ToString(),
                                                        0,
                                                        lessonId,
                                                        userId);
                ResultSavedTB.Text = LessonDetailContoller.AddLessonDetail(detail);
                ResultSavedTB.Visibility = Visibility.Visible;
            }
            TypeGrid.Visibility = Visibility.Collapsed;
            EndOfLessonGrid.Visibility = Visibility.Visible;
            ResultErrorTB.Text = "Errors: " + ErrorsTB.Text;
            ResultTimeTB.Text = "Passed time: " + TimeTB.Text;
            ResultSpeedTB.Text = "Speed: " + SpeedTB.Text;
        }


        private void MarkNextLetter()
        {
            letterCounter++;
            if (letterCounter == currentText.Length)
            {
                sw.Stop();
                dt.Stop();
                nextBtn.IsEnabled = true;
                if (currentTextId == texts.Count() - 1)
                    ShowLessonEndScreen();
                return;
            }
            currentLetter = currentText[letterCounter];
            if (currentLetter == '\n')
            {
                ScrollRTBToCurrentLetter();
                letterCounter++;
                currentLetter = currentText[letterCounter];
                currentPosition = GetTextPositionAtOffset(currentPosition, 1);
            }
            if (currentPosition == null)
                currentPosition = TextRTB.Document.ContentStart;
            else
                currentPosition = GetTextPositionAtOffset(currentPosition, 1);
            currentLetter = currentText[letterCounter];
            markedLetter = new TextRange(currentPosition, GetTextPositionAtOffset(currentPosition, 1));
            markedLetter.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Aquamarine);
        }


        private TextPointer GetTextPositionAtOffset(TextPointer position, int characterCount)
        {
            while (position != null)
            {
                if (position.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                {
                    int count = position.GetTextRunLength(LogicalDirection.Forward);
                    if (characterCount <= count)
                    {
                        return position.GetPositionAtOffset(characterCount);
                    }
                    characterCount -= count;
                }
                TextPointer nextContextPosition = position.GetNextContextPosition(LogicalDirection.Forward);
                if (nextContextPosition == null)
                    return position;
                position = nextContextPosition;
            }
            return position;
        }

        private void ViewLessonDetailsClick(object sender, RoutedEventArgs e)
        {
            if (HyperLinkText.Text.Equals("List previous results..."))
            {
                HyperLinkText.Text = "Hide previous results...";
                UserDetailsSP.Visibility = Visibility.Visible;
                if (username == null)
                    WarningTB.Visibility = Visibility.Visible;
                else
                {
                    LessonDetailsLV.Visibility = Visibility.Visible;
                    if (LessonDetailsLV.ItemsSource == null)
                        LessonDetailsLV.ItemsSource = LessonDetailContoller.GetLessonDetails(lessonId, userId);
                }
            }
            else
            {
                HyperLinkText.Text = "List previous results...";
                UserDetailsSP.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckCurrentLetter(object sender, TextCompositionEventArgs e)
        {
            if (letterCounter == currentText.Length)
                return;
            try
            {
                Char pressedKeyChar;
                if(sender == null)
                    pressedKeyChar = ' ';
                else
                    pressedKeyChar = (Char)System.Text.Encoding.ASCII.GetBytes(e.Text)[0];
                Button btn = null;
                if (charBtnDict.Keys.Contains(Char.ToLower(pressedKeyChar)))
                    btn = charBtnDict[Char.ToLower(pressedKeyChar)];
                if (btn == null)
                {
                    MessageBox.Show("Unknown key!!");
                    return;
                }
                if (pressedKeyChar == currentLetter)
                {
                    if (letterCounter == 0)
                    {
                        sw.Start();
                        dt.Start();
                    }
                    btn.Background = Brushes.Green;
                    markedLetter.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.LightBlue);
                    MarkNextLetter();
                }
                else
                {
                    errors++;
                    btn.Background = Brushes.Red;
                    markedLetter.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);
                }
            }
            catch (Exception exc)
            {
                return;
            }
            
        }

        private void ScrollRTBToCurrentLetter()
        {
            TextRTB.LineDown();
        }

        private void InitializeDictionaryOfButtons()
        {
            charBtnDict = new Dictionary<char, Button>();
            AddOneRowOfButtons(FirstRowKeysBtn);
            AddOneRowOfButtons(SecondRowKeysBtn);
            AddOneRowOfButtons(ThirdRowKeysBtn);
            AddOneRowOfButtons(ForthRowKeysBtn);
            charBtnDict[' '] = SpaceBtn;
        }

        private void AddOneRowOfButtons(UniformGrid grid)
        {
            foreach (Button btn in grid.Children)
                charBtnDict[Char.ToLower(Convert.ToChar(btn.Content))] = btn;
        }

        private void ErraseBtnBackground(object sender, KeyEventArgs e)
        {
            foreach (char key in charBtnDict.Keys)
                charBtnDict[key].Background = SystemColors.ControlBrush;
        }

        private void CheckIfSpaceKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (!currentLetter.Equals(' '))
                    errors++;
                e.Handled = true;
                CheckCurrentLetter(null, null);
            }
        }
    }
}
