using System.Windows;
using System.Windows.Controls;
using WPFQuiz.DataModels;


namespace WPFQuiz.PlayView
{
    /// <summary>
    /// Interaction logic for PlayQuiz.xaml
    /// </summary>
    public partial class PlayQuiz : UserControl
    {
        public PlayQuizViewModel ViewModel { get; set; }
        public PlayQuiz(Quiz quiz)
        {
            InitializeComponent();
            ViewModel = new PlayQuizViewModel(quiz);
            DataContext = ViewModel;
        }

        public void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            int selectedIndex = int.Parse(button.Tag.ToString());
            ViewModel.AnswerQuestion(selectedIndex);

            if (ViewModel.IsQuizFinished)  //om quiz slut gå till resultview 
            {
                if (Application.Current.MainWindow is MainWindow mw)
                {
                    mw.Content = new WPFQuiz.ResultView(ViewModel);
                }
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
