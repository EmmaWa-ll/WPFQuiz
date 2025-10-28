using System.Windows;
using System.Windows.Controls;
using WPFQuiz.Files;

namespace WPFQuiz.PlayView
{
    /// <summary>
    /// Interaction logic for SelectQuiz.xaml
    /// </summary>
    public partial class SelectQuiz : UserControl
    {
        private string selectedTitle;
        private readonly bool isEditMode;
        public SelectQuiz(bool editMode = false)
        {
            InitializeComponent();
            isEditMode = editMode;
            LoadTitles();
        }

        private void LoadTitles()
        {
            var titles = QuizFile.LoadTitles();
            if (titles.Count == 0)
            {
                MessageBox.Show("No saved quiz yet");
                return;
            }
            QuizList.ItemsSource = titles;
        }
        public async void Quiz_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Content is string title)
            {
                selectedTitle = title;
                if (isEditMode)
                {
                    await OpenForEdit(title);
                }
                else
                {
                    await PlaySelectedQuiz(title);

                }
            }
        }
        private async Task OpenForEdit(string title)
        {
            var quiz = await QuizFile.LoadFile(title);
            if (quiz == null)
            {
                MessageBox.Show("Coudn't read quiz");
                return;
            }
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new WPFQuiz.EditView.EditQuiz(quiz, title);
            }
        }

        private async Task PlaySelectedQuiz(string title)
        {
            var quiz = await QuizFile.LoadFile(title);
            if (quiz == null)
            {
                MessageBox.Show("Couldn't read quiz.");
                return;
            }
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new WPFQuiz.PlayView.PlayQuiz(quiz);
            }
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
