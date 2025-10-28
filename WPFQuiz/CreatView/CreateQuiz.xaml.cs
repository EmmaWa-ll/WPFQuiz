using System.Windows;
using System.Windows.Controls;
using WPFQuiz.DataModels;
using WPFQuiz.Files;

namespace WPFQuiz.CreatView
{
    /// <summary>
    /// Interaction logic for CreateQuiz.xaml
    /// </summary>
    public partial class CreateQuiz : UserControl
    {
        private Quiz CurrentQuiz = new Quiz();

        public CreateQuiz()
        {
            InitializeComponent();
        }

        private bool ControlQuestionInputs()
        {
            string statement = StatementBox.Text.Trim();

            string[] answers = new[] { Answer1Box, Answer2Box, Answer3Box }.Select(tb => tb.Text.Trim()).ToArray();

            int correctAnswer = CorrectAnswerComboBox.SelectedIndex;

            //kontroler /felahnt 
            if (string.IsNullOrWhiteSpace(statement))
            {
                MessageBox.Show("Write a  question");
                return false;
            }
            if (answers.Any(a => string.IsNullOrWhiteSpace(a)))
            {
                MessageBox.Show("Fill all the answer boxes");
                return false;
            }
            if (correctAnswer < 0 || correctAnswer >= answers.Length)
            {
                MessageBox.Show("You have to choose a correct answer first");
                return false;
            }
            return true;
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            ControlQuestionInputs();
            if (ControlQuestionInputs() == false)
            {
                return;
            }
            //hämta indata från box
            string statement = StatementBox.Text.Trim();

            //Select = för varje sak gör detta, (blir sedan en ny lista med endsat svaren som anv skrivit in)
            string[] answers = new[] { Answer1Box, Answer2Box, Answer3Box }.Select(tb => tb.Text.Trim()).ToArray();
            int correctAnswer = CorrectAnswerComboBox.SelectedIndex;

            Question NewQuestion = new Question(statement, correctAnswer, answers);
            CurrentQuiz.Questions.Add(NewQuestion);
            MessageBox.Show("Question was added to quiz!");
            ClearForm();
        }

        private async void SaveQuiz_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(TitleBox.Text))
            {
                MessageBox.Show("Add a title.");
                return;
            }
            CurrentQuiz.Title = title;
            //ktrl så att curentwquiz existerar
            if (CurrentQuiz == null)
            {
                CurrentQuiz = new Quiz();
            }

            if (CurrentQuiz.Questions == null || CurrentQuiz.Questions.Count < 3)
            {
                MessageBox.Show("There must be a minimun of 3 questions");
                return;
            }

            await QuizFile.SaveFile(CurrentQuiz);
            MessageBox.Show("Quiz saved");
            ClearForm();
            CurrentQuiz = new Quiz();
        }

        public void ClearForm()
        {
            StatementBox.Text = string.Empty;
            Answer1Box.Text = string.Empty;
            Answer2Box.Text = string.Empty;
            Answer3Box.Text = string.Empty;
            CorrectAnswerComboBox.SelectedIndex = -1;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new WPFQuiz.StartView.StartMenu();
            }
        }
    }
}
