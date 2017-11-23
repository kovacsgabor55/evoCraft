using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Page
    {
        public Create()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }
    }
}
