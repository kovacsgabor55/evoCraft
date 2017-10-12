using System.Windows;

namespace MenuGridFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            LogoScreen page = new LogoScreen();
            myFrame.NavigationService.Navigate(page);
        }
    }
}
