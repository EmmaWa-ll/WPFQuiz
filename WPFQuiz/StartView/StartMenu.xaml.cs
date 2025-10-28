using System.Windows;
using System.Windows.Controls;
using WPFQuiz.CreatView;
using WPFQuiz.PlayView;


namespace WPFQuiz.StartView
{
    /// <summary>
    /// Interaction logic for StartMenu.xaml
    /// </summary>
    public partial class StartMenu : UserControl
    {
        public StartMenu()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new SelectQuiz();
            }
        }
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new CreateQuiz();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Current.MainWindow is MainWindow mw)
            {
                mw.Content = new WPFQuiz.PlayView.SelectQuiz(editMode: true);
            }
        }
    }
}
