using System.Windows;
using WPFQuiz.Files;


namespace WPFQuiz
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            QuizFile.EnsureSeeded();
            this.Content = new WPFQuiz.StartView.StartMenu();
        }
    }
}