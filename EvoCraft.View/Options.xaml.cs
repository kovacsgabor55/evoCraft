using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

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

        public void button_Click_1(object sender, RoutedEventArgs e)
        {
            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        public void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Information info = new Information();
                //info.UserName = yourNameTextBox.Text;
                //SaveXML.SaveData(info, "options.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            MainMenu mainmenu = new MainMenu();
            this.NavigationService.Navigate(mainmenu);
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(2));
            MainGrid.BeginAnimation(Image.OpacityProperty, fadeInAnimation);
        }

        //public void Options_Loaded(object sender, EventArgs e)
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
