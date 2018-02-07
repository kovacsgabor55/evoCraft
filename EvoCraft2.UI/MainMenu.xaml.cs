using EvoCraft2.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace EvoCraft2.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        SoundPlayer soundPlayer = new SoundPlayer();

        public MainMenu()
        {
            InitializeComponent();

            //Sounds.StartMenuMusic();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            //Sounds.StopMenuMusic();
            //Sounds.ShutDown();
            System.Threading.Thread.Sleep(3000);
            Application.Current.Shutdown();
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            Credits credits = new Credits();
            this.NavigationService.Navigate(credits);
        }

        private void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            Options options = new Options();
            this.NavigationService.Navigate(options);

            if (File.Exists("options.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);

                options.yourNameTextBox.Text = info.UserName;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            MainGrid.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        private void tutorialButton_Click(object sender, RoutedEventArgs e)
        {
            TutorialsPage page = new TutorialsPage();
            this.NavigationService.Navigate(page);
        }

        private void singleplayerButton_Click(object sender, RoutedEventArgs e)
        {
            //Sounds.StopMenuMusic();
            //LoadingScreen page = new LoadingScreen();
            //RunningGame page = new RunningGame();
            MapSelector page = new MapSelector();
            NavigationService.Navigate(page);
        }

        private void multiplayerButton_Click(object sender, RoutedEventArgs e)
        {
            Multiplayer page = new Multiplayer();
            NavigationService.Navigate(page);
        }
    }
}
