using EvoCraft2.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
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
            Multiplayer multiplayer = new Multiplayer();
            this.NavigationService.Navigate(multiplayer);
        }

        private void backButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            GameDescription gd = new GameDescription();
            gd.GameName = txbGameName.Text;
            //gd.MapName = txbMapName.Text;

            ProcessStartInfo psi = new ProcessStartInfo();
            //psi.Arguments = txbGameName.Text + " " + txbMapName.Text;
            psi.FileName = @"C:\Users\z003w6vk\Desktop\ÚjCraft\evoCraft-EvoCraft2\EvoCraft2.Hoster\bin\Debug\EvoCraft2.Hoster.exe";

            Process startHoster = new Process();
            startHoster.StartInfo = psi;

            startHoster.Start();

            AdminClient gsc = new AdminClient();
            gsc.SetServerDetails(gd);

            StaticClass.Client = new GameClient(new EndpointAddress(ServiceHelper.GetServiceUri()));

            WaitingRoom waitingRoom = new WaitingRoom(gd);
            this.NavigationService.Navigate(waitingRoom);
        }
    }
}
