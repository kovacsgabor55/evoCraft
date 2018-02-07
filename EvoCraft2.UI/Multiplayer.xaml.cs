using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvoCraft2.UI
{
    /// <summary>
    /// Interaction logic for Multiplayer.xaml
    /// </summary>
    public partial class Multiplayer : Page
    {
        public Multiplayer()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            Create create = new Create();
            this.NavigationService.Navigate(create);
        }

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {
            Join join = new Join();
            this.NavigationService.Navigate(join);
        }
    }
}
