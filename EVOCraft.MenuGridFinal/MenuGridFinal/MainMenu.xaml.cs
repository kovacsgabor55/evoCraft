using System;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Xml.Serialization;
using System.Windows.Media.Animation;
using System.Media;
using ChatBackend;

namespace View
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

            Sounds.StartMenuMusic();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Sounds.StopMenuMusic();
            Sounds.ShutDown();
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

        }
    }
}
