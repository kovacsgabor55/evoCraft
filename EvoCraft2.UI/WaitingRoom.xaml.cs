using EvoCraft2.Common;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Threading;
using System.Xml.Serialization;

namespace EvoCraft2.UI
{
    /// <summary>
    /// Interaction logic for WaitingRoom.xaml
    /// </summary>
    public partial class WaitingRoom : Page
    {
        string nickname;
        public WaitingRoom(GameDescription gd)
        {
            InitializeComponent();

            StaticClass.Client.MessageReceived += Client_MessageReceived;
            StaticClass.Client.JoinGame(Guid.NewGuid().ToString());

            lblGameName.Content = gd.GameName;

            if (File.Exists("options.xml"))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Information));
                FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
                Information info = (Information)xs.Deserialize(read);
                nickname = info.UserName;
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Multiplayer multiplayer = new Multiplayer();
            this.NavigationService.Navigate(multiplayer);
        }

        void Client_MessageReceived(object sender, string e)
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
            new Action(() =>
            {
                chat.Text += nickname + ": " + e + Environment.NewLine;
            }
            ));
        }

        void Client_GameStarted(object sender, EventArgs e)
        {
            //Debugger.Launch();

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle,
            new Action(() =>
            {
                //OlyanNincs olyannincs = new OlyanNincs();
                //this.NavigationService.Navigate(olyannincs);
            }
            ));
        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            StaticClass.Client.SendMessage(text.Text);
        }

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            OlyanNincsen olyannincs = new OlyanNincsen();
            this.NavigationService.Navigate(olyannincs);
        }
    }
}
