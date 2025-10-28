using System.Windows;

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
            this.Content = new WPFQuiz.StartView.StartMenu();
        }
    }
}