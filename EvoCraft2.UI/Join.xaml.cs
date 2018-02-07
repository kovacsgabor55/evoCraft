using EvoCraft2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Join.xaml
    /// </summary>
    public partial class Join : Page
    {
        public Join()
        {
            InitializeComponent();
        }

        //public void DisplayMessage(ChatBackend.CompositeType composite)
        //{
        //    //if (File.Exists("options.xml"))
        //    //{
        //    //    XmlSerializer xs = new XmlSerializer(typeof(Information));
        //    //    FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //    //    Information info = (Information)xs.Deserialize(read);

        //    //    string username = info.UserName;

        //    //    string message = composite.Message == null ? "" : composite.Message;
        //    //    textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);
        //    //}
        //}

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {
            //RunningGame runningGame = new RunningGame();
            //this.NavigationService.Navigate(runningGame);
        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            Multiplayer multiplayer = new Multiplayer();
            this.NavigationService.Navigate(multiplayer);
        }

        private void joinButton_Click_1(object sender, RoutedEventArgs e)
        {
            GameServer selectedServer = (GameServer)lvGames.SelectedItem;
            var services = ServiceFinder.GetAviableServices();

            var gameServer = services.FirstOrDefault(X => X.GameName == selectedServer.GameName);

            GameDescription gd = new GameDescription();
            gd.GameName = gameServer.GameName;
            gd.MapName = gameServer.MapName;

            StaticClass.Client = new GameClient(gameServer.EndpointAddress);
            StaticClass.Client.GetServerDetails();

            WaitingRoom waitingRoom = new WaitingRoom(gd);
            this.NavigationService.Navigate(waitingRoom);
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            ServiceFinder.FindServiceAsync();
            Thread.Sleep(10000);
            var services = ServiceFinder.GetAviableServices();
            foreach (GameServer item in services)
            {
                Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                {
                    lvGames.Items.Add(item);
                }));
            }
        }
    }
}
