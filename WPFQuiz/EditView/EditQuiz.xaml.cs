using System.Windows;
using System.Windows.Controls;
using WPFQuiz.DataModels;
using WPFQuiz.Files;

namespace WPFQuiz.EditView
{
    /// <summary>
    /// Interaction logic for EditQuiz.xaml
    /// </summary>
    public partial class EditQuiz : UserControl
    {
        private Quiz _quiz;
        private string _title;

        public EditQuiz(Quiz quiz, string title)
        {
            InitializeComponent();
            _quiz = quiz;
            _title = title;

            TitleBox.Text = _quiz.Title;
            QuestionList.ItemsSource = _quiz.Questions;

        }
        private void QuestionList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionList.SelectedItem is Question q)
            {
                QuestionBox.Text = q.Statement;
                Answer1Box.Text = q.Answers[0];
                Answer2Box.Text = q.Answers[1];
                Answer3Box.Text = q.Answers[2];
                CorrectCombo.SelectedIndex = q.CorrectAnswer;
            }
        }
        private bool UpdateSelectedQuestion()
        {
            if (QuestionList.SelectedItem == null)
            {
                MessageBox.Show("Choose a question first");
                return false;
            }
            Question choosenQuestion = (Question)QuestionList.SelectedItem;

            string questionText = QuestionBox.Text.Trim();
            string answer1 = Answer1Box.Text.Trim();
            string answer2 = Answer2Box.Text.Trim();
            string answer3 = Answer3Box.Text.Trim();

            if (questionText == "")
            {
                MessageBox.Show("Write a question first");
                return false;
            }
            if (answer1 == "" || answer2 == "" || answer3 == "")
            {
                MessageBox.Show("Fill all the answer boxes");
                return false;
            }
            if (CorrectCombo.SelectedIndex < 0)
            {
                MessageBox.Show("Choose a right answe (1,2 or 3)");
                return false;
            }

            choosenQuestion.Statement = questionText;
            List<string> newAnswer = new List<string>();
            newAnswer.Add(answer1);
            newAnswer.Add(answer2);
            newAnswer.Add(answer3);
            choosenQuestion.Answers = newAnswer.ToArray();
            choosenQuestion.CorrectAnswer = CorrectCombo.SelectedIndex;

            QuestionList.Items.Refresh();
            MessageBox.Show("Question has been updated!");
            return true;
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (QuestionList.SelectedItem is not Question q)
            {
                MessageBox.Show("Choose a question to deleate.");
                return;
            }
            _quiz.Questions.Remove(q);
            QuestionList.Items.Refresh();
            ClearForm();
            MessageBox.Show("Question has been deleated");
        }
        private async void SaveQuiz_CLick(object sender, RoutedEventArgs e)
        {
            _quiz.Title = TitleBox.Text.Trim();
            bool saved = UpdateSelectedQuestion();
            if (!saved)
            {
                return;
            }
            try
            {
                await QuizFile.SaveFile(_quiz);
                MessageBox.Show("Quiz has been saved");
                QuestionList.Items.Refresh();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void ClearForm()
        {
            QuestionBox.Text = "";
            Answer1Box.Text = "";
            Answer2Box.Text = "";
            Answer3Box.Text = "";
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
