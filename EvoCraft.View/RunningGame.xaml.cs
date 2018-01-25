using System.ComponentModel;
using System.Windows.Controls;
using EvoCraft.Core;
using System.IO;
using System.Media;
using System.Windows.Input;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects.Resources;
using EvoCraft.Core.MapObjects.PlayerControlled.Buildings;

namespace View
{
    /// <summary>
    /// Interaction logic for RunningGame.xaml
    /// </summary>
    public partial class RunningGame : Page
    {
        ViewModel viewModel;
        BackgroundWorker threadForBackEnd;
        SoundPlayer soundPlayer = new SoundPlayer();

        public RunningGame()
        {
            InitializeComponent();


            if (!global::View.Properties.Settings.Default.RunningGameSoundPlayerActive)
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                path = new DirectoryInfo(path).FullName.ToString();
                soundPlayer.SoundLocation = path + "\\Sounds\\EpicBattleMusic.wav";
                soundPlayer.PlayLooping();
                global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = true;
            }
            
            viewModel = new ViewModel(RenderHelper.Instance.Size.Height, RenderHelper.Instance.Size.Width);

            this.DataContext = viewModel;

            viewModel.StartTimer();
        }

        public void Page_onClose()
        {
            soundPlayer.Stop();
            global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = false;
        }       

        public void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            viewModel.KeyDown(e);
        }

        public void theGame_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            theGame.Focus();
        }

        public void MainMenuButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Page_onClose();
            MainMenu page = new MainMenu();
            this.NavigationService.Navigate(page);
        }
    }
}
