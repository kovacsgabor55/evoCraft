using EvoCraft.Core;
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

namespace MenuGridFinal
{
    /// <summary>
    /// Interaction logic for MapSelector.xaml
    /// </summary>
    public partial class MapSelector : Page
    {
        public MapSelector()
        {
            InitializeComponent();

            maps = MapLoader.LoadAll();

            lvMaps.ItemsSource = maps;
            this.DataContext = maps;

            lvMaps.Foreground = Brushes.White;
            lvMaps.FontSize = 20;
        }

        private List<Map> maps;
        public List<Map> Maps
        {
            get { return maps; }
            set
            {
                maps = value;
                OnPropertyChanged(new DependencyPropertyChangedEventArgs());
            }
        }

        private Map selectedMap = null;
        
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MainMenu page = new MainMenu();
            this.NavigationService.Navigate(page);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedMap != null)
            {
                Engine.Map = selectedMap;
                Sounds.StopMenuMusic();
                LoadingScreen page = new LoadingScreen();
                this.NavigationService.Navigate(page);
            }
        }

        private void LvMaps_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listview = sender as ListView;
            var item = listview.SelectedItem as Map;
            if (item != null)
            {
                selectedMap = item;
            }
        }
    
    }
}
