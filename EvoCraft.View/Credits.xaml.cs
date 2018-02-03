using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace View
{
    /// <summary>
    /// Interaction logic for Credits.xaml
    /// </summary>
    public partial class Credits : Page
    {
        public Credits()
        {
            InitializeComponent();
        }

        public void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            MainGrid.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }
    }
}
