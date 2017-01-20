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
using System.Configuration;

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

        int stopWhenError;

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
                UserDetailsSP.Visibility = Visibility.Collapsed;
            HyperLinkText.Text = "List previous results...";
            InitializeDictionaryOfButtons();
            nextBtn.IsEnabled = false;

            texts = new List<string>();

            sw = new Stopwatch();
            dt = new DispatcherTimer();
            dt.Tick += new EventHandler(Tick);
            dt.Interval = new TimeSpan(0, 0, 1);

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            stopWhenError = Int32.Parse(config.AppSettings.Settings["ErrorStop"].Value);

        }

        /// <summary>
        /// calls when dispatcher timer interval passes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tick(object sender, EventArgs e)   
        {  
            if (sw.IsRunning)   
            {  
                TimeSpan ts = sw.Elapsed;
                TimeTB.Text = ts.ToString(@"mm\:ss\:ff");
                SpeedTB.Text = (letterCounter / ts.TotalSeconds).ToString();
                ErrorsTB.Text = errors.ToString();
            }  
        }  

        /// <summary>
        /// Click to GO button.
        /// Have to put focus to typing grid for receiving key input!
        /// sets visibility of UserControls.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GoClick(object sender, RoutedEventArgs e)
        {
            errors = 0;
            TimeTB.Text = "00:00:00";
            SpeedTB.Text = "0.000";
            sw.Reset();
            ErrorsTB.Text = errors.ToString();
            texts = TextController.GetTexts(lessonId);
            if (texts.Count() == 0)
            {
                EmptyLessonTB.Visibility = Visibility.Visible;
                return;
            }
            EndOfLessonGrid.Visibility = Visibility.Collapsed;
            WellcomeGird.Visibility = Visibility.Collapsed;
            TypeGrid.Visibility = Visibility.Visible;
            TypeGrid.Focus();       //key down affects it now
            currentTextId = -1;
            NextBtnClick(null, null);
        }

        /// <summary>
        /// Switch to next text in selectd lesson.
        /// Disables next button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NextBtnClick(object sender, RoutedEventArgs e)
        {
            RTBParagraph.Inlines.Clear();
            nextBtn.IsEnabled = false;
            ++currentTextId;
            EmptyLessonTB.Visibility = Visibility.Collapsed;
            TextCounterTB.Text = (currentTextId+1) + "/" + texts.Count;
            currentText = texts[currentTextId];
            currentText = currentText.Replace(Environment.NewLine, " \n").Replace("''", "'");
            RTBParagraph.Inlines.Add(new Run(currentText));
            letterCounter = -1;
            currentPosition = null;
            MarkNextLetter();
        }

        /// <summary>
        /// Moves ponter to next letter and mark it with blue color.
        /// if end of text and end of lesson occur go to end of lesson screen
        /// if end of text occur stop watch and timer and enable next button
        /// </summary>
        private void MarkNextLetter()
        {
            letterCounter++;
            if (letterCounter == currentText.Length)
            {
                sw.Stop();
                dt.Stop();
                nextBtn.IsEnabled = true;
                if (currentTextId == texts.Count() - 1)
                {
                    ErraseBtnBackground(null, null);
                    ShowLessonEndScreen();
                }
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


        /// <summary>
        /// shows controls of end lesson screen
        /// saved obtained lesson detail to database 
        /// and drows it on screen
        /// </summary>
        private void ShowLessonEndScreen()
        {
            if (username != null)
            {
                LessonDetail detail = new LessonDetail(Convert.ToDouble(SpeedTB.Text),
                                                        Convert.ToInt32(ErrorsTB.Text),
                                                        sw.Elapsed.TotalSeconds,
                                                        DateTime.Now.ToString(),
                                                        0,
                                                        lessonId,
                                                        userId);
                ResultSavedTB.Text = LessonDetailController.AddLessonDetail(detail);
                ResultSavedTB.Visibility = Visibility.Visible;
            }
            TypeGrid.Visibility = Visibility.Collapsed;
            EndOfLessonGrid.Visibility = Visibility.Visible;
            ResultErrorTB.Text = "Errors: " + ErrorsTB.Text;
            ResultTimeTB.Text = "Passed time: " + sw.Elapsed.TotalSeconds;
            ResultSpeedTB.Text = "Speed: " + SpeedTB.Text;
        }

        /// <summary>
        /// Method moves Text pointer forward for given offset.
        /// </summary>
        /// <param name="position">Current TextPointer value</param>
        /// <param name="characterCount">offset</param>
        /// <returns>TextPointer</returns>
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

        /// <summary>
        /// Hyperlink click.
        /// Changes name to current opposite.
        /// And hides or shows details table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                        LessonDetailsLV.ItemsSource = LessonDetailController.GetLessonDetails(lessonId, userId);
                }
            }
            else
            {
                HyperLinkText.Text = "List previous results...";
                UserDetailsSP.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Methods finds char of typed key.
        /// Marks associated Button with red if wrong key else green.
        /// Counts errors
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                if (letterCounter == 0)
                {
                    sw.Start();
                    dt.Start();
                }
                if (pressedKeyChar == currentLetter)
                {
                    btn.Background = Brushes.Green;
                    markedLetter.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.LightBlue);
                    MarkNextLetter();
                }
                else
                {
                    errors++;
                    btn.Background = Brushes.Red;
                    markedLetter.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);
                    if(stopWhenError == 0)
                        MarkNextLetter();
                }
            }
            catch (Exception exc)
            {
                return;
            }
            
        }


        /// <summary>
        /// Method scrolls richTextBox to current letter.
        /// </summary>
        private void ScrollRTBToCurrentLetter()
        {
            TextRTB.LineDown();
        }

        /// <summary>
        /// Maps Buttons with chars.
        /// </summary>
        private void InitializeDictionaryOfButtons()
        {
            charBtnDict = new Dictionary<char, Button>();
            AddOneRowOfButtons(FirstRowKeysBtn);
            AddOneRowOfButtons(SecondRowKeysBtn);
            AddOneRowOfButtons(ThirdRowKeysBtn);
            AddOneRowOfButtons(ForthRowKeysBtn);
            charBtnDict[' '] = SpaceBtn;
        }

        /// <summary>
        /// Add key, value pars to charBtnDict.
        /// For every char in Button content
        /// adds one key value pair to dictionary.
        /// </summary>
        /// <param name="grid"></param>
        private void AddOneRowOfButtons(UniformGrid grid)
        {
            foreach (Button btn in grid.Children)
                foreach(char c in btn.Content.ToString().ToCharArray())
                charBtnDict[Char.ToLower(Convert.ToChar(c))] = btn;
        }

        /// <summary>
        /// clears bbackground color of all button that make keyboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ErraseBtnBackground(object sender, KeyEventArgs e)
        {
            foreach (char key in charBtnDict.Keys)
                charBtnDict[key].Background = SystemColors.ControlBrush;
        }

        /// <summary>
        /// Solution to the problem with focus and space key
        /// Fires on previewKeyDown and simulates space key input
        /// For some reason when we change focus while typing 
        /// program does not recognize space key input any more.
        /// Method is added to solve described problem.
        /// Also, switches to next text in seleced lesson if 
        /// enter is pressed and it's end of current text.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIfSpaceKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                //if (!currentLetter.Equals(' '))
                //    errors++;
                e.Handled = true;
                CheckCurrentLetter(null, null);
            }
            if (e.Key == Key.Enter && nextBtn.IsEnabled == true)
            {
                e.Handled = true;
                NextBtnClick(null, null);
            }
        }
    }
}
