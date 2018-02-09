using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class OrcFarmControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.ORC;

        public OrcFarmControl()
        {
            InitializeComponent();
        }

        public OrcFarmControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public override void SelectedSound(bool selected)
        {
            if (selected)
            {
                SoundPlayer.PlaySelectionSound("Orc\\Buildings\\Farm\\OFarm.wav");
            }
        }

        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad5)
            {
                Storyboard b = (Storyboard)this.FindResource("OFarmBuildCompleted");
                SoundPlayer.PlaySelectionSound("Orc\\Buildings\\Farm\\OFarm.wav");
                b.Begin();
            }
        }

        private void OrcFarmControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void OrcFarmControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void OrcFarmControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}
