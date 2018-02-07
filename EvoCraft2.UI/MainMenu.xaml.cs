using EvoCraft2.Common;
using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace EvoCraft2.UI
{
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        SoundPlayer soundPlayer = new SoundPlayer();
        private static bool isYellow = true;
        private static Timer timer;

        public MainMenu()
        {
            InitializeComponent();

            //Sounds.StartMenuMusic();

            timer = new Timer(timerTick, null, 0, 50);
        }

        private void timerTick(object state)
        {
            if (isYellow)
            {
                Dispatcher.Invoke(() =>
                {
                    newFeatureLabel.Foreground = Brushes.Aqua;
                    multiplayerButton.Foreground = Brushes.Aqua;
                });
                isYellow = !isYellow;
            }
            else
            {
                Dispatcher.Invoke(() =>
                {
                    newFeatureLabel.Foreground = Brushes.Yellow;
                    multiplayerButton.Foreground = Brushes.Yellow;
                });
                isYellow = !isYellow;
            }
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
            //Sounds.StopMenuMusic();
            //Sounds.ShutDown();
            System.Threading.Thread.Sleep(3000);
            Application.Current.Shutdown();
        }

        private void creditsButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
            Credits credits = new Credits();
            this.NavigationService.Navigate(credits);
        }

        private void optionsButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
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
            timer.Dispose();
            TutorialsPage page = new TutorialsPage();
            this.NavigationService.Navigate(page);
        }

        private void singleplayerButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
            //Sounds.StopMenuMusic();
            //LoadingScreen page = new LoadingScreen();
            //RunningGame page = new RunningGame();
            MapSelector page = new MapSelector();
            NavigationService.Navigate(page);
        }

        private void multiplayerButton_Click(object sender, RoutedEventArgs e)
        {
            timer.Dispose();
            Multiplayer page = new Multiplayer();
            NavigationService.Navigate(page);
        }
    }
}
