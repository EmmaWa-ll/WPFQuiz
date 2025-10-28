using System.Windows;
using System.Windows.Controls;

namespace WPFQuiz
{
    /// <summary>
    /// Interaction logic for ResultView.xaml
    /// </summary>
    public partial class ResultView : UserControl
    {
        public ResultView(PlayQuizViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void BackToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mW = Application.Current.MainWindow as MainWindow;
            if (mW != null)
            {
                mW.Content = new WPFQuiz.StartView.StartMenu();
            }
        }
    }
}
