using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Windows.Media;

namespace View
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Page
    {
        BitmapImage ChupyImage1;
        BitmapImage ChupyImage2;
        int loadingScreenIterationCounter = 0;
        RunningGame page;

        public LoadingScreen()
        {
            InitializeComponent();

            ChupyImage1 = (BitmapImage)FindResource("ChupyGif1");
            ChupyImage2 = (BitmapImage)FindResource("ChupyGif2");

            LoadingResources();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(4000);
            timer.Tick += TimerTick;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            if (WpfAnimatedGif.ImageBehavior.GetAnimatedSource(ChupacabraImage) == ChupyImage1)
            {
                WpfAnimatedGif.ImageBehavior.SetAnimatedSource(ChupacabraImage, ChupyImage2);
            }
            else
            {
                WpfAnimatedGif.ImageBehavior.SetAnimatedSource(ChupacabraImage, ChupyImage1);
            }
        }

        public void AddMessage(string message)
        {
            TextBlock tmp = new TextBlock();

            tmp.Text = message;
            tmp.FontSize = 20;
            tmp.Foreground = Brushes.Red;



            tmp.SetValue(DockPanel.DockProperty, Dock.Top);
            MessageDockPanel.Children.Add(tmp);
        }
        public void LoadComplete()
        {
            // page.Visibility = System.Windows.Visibility.Visible;
            NavigationService svc = NavigationService.GetNavigationService(this);
            if (svc != null)
            {
                svc.Navigate(page);
            }
        }

        private void LoadingResources()
        {
            DispatcherTimer loadingTimer = new DispatcherTimer();
            loadingTimer.Interval = TimeSpan.FromMilliseconds(1000); //Eredetileg ez 1000, demóig legyen kevesebb
            loadingTimer.Tick += LoadingTimer_Tick;
            loadingTimer.Start();

            page = new RunningGame();
        }

        private void LoadingTimer_Tick(object sender, EventArgs e)
        {
            //Ide kell írni amit kírjon a loadingScreen-en
            switch (loadingScreenIterationCounter)
            {
                case 0: this.AddMessage("Loading Memes..."); loadingScreenIterationCounter++; break;
                case 1: this.AddMessage("Loading Sloths..."); loadingScreenIterationCounter++; break;
                case 2: this.AddMessage("Loading Chupacabras..."); loadingScreenIterationCounter++; break;
                case 3: this.AddMessage("Loading TryTakeDamage Method..."); loadingScreenIterationCounter++; break;
                case 4: this.AddMessage("Loading Epic Battle Songs..."); loadingScreenIterationCounter++; break;
                case 5: this.AddMessage("Loading The Best RTS Game Ever..."); loadingScreenIterationCounter++; break;
                case 6: this.AddMessage("Loading Something..."); loadingScreenIterationCounter++; break;
                case 7: this.AddMessage("Loading Something Else..."); loadingScreenIterationCounter++; break;
                case 8: this.AddMessage("Loading Songs..."); loadingScreenIterationCounter++; break;
                case 9: this.AddMessage("Loading Resources..."); loadingScreenIterationCounter++; break;
                case 10: this.LoadComplete(); break;
            }
        }
    }
}
