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
using System.IO;
using System.Xml.Serialization;

namespace MenuGridFinal
{
    /// <summary>
    /// Interaction logic for Join.xaml
    /// </summary>
    public partial class Join : Page
    {
        private ChatBackend.ChatBackend backend;

        public Join()
        {
            InitializeComponent();
            backend = new ChatBackend.ChatBackend(this.DisplayMessage);
        }

        public void DisplayMessage(ChatBackend.CompositeType composite)
        {
            //if (File.Exists("options.xml"))
            //{
            //    XmlSerializer xs = new XmlSerializer(typeof(Information));
            //    FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            //    Information info = (Information)xs.Deserialize(read);

            //    string username = info.UserName;

            //    string message = composite.Message == null ? "" : composite.Message;
            //    textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);
            //}
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Return || e.Key == Key.Enter)
            {
                backend.SendMessage(textBoxEntryField.Text);
                textBoxEntryField.Clear();
            }
        }

        private void textBoxEntryField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void joinButton_Click(object sender, RoutedEventArgs e)
        {
            RunningGame runningGame = new RunningGame();
            this.NavigationService.Navigate(runningGame);
        }
    }
}
