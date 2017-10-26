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
using System.Windows.Media.Animation;
using ChatBackend;

namespace View
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Page
    {
        public Options()
        {
            InitializeComponent();
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Information info = new Information();
                info.UserName = yourNameTextBox.Text;
                SaveXML.SaveData(info, "options.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            MainGrid.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        //private void Options_Loaded(object sender, EventArgs e)
        //{
        //    if (File.Exists("options.xml"))
        //    {
        //        XmlSerializer xs = new XmlSerializer(typeof(Information));
        //        FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //        Information info = (Information)xs.Deserialize(read);

        //        yourNameTextBox.Text = info.Data1;
        //    }
        //}
    }
}
