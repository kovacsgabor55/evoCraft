using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class HumanFarmControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.ORC;

        public HumanFarmControl()
        {
            InitializeComponent();
        }

        public HumanFarmControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public override void SelectedSound(bool selected)
        {
            if (selected)
            {
                SoundPlayer.PlaySelectionSound("Human\\Buildings\\Farm\\HFarm.wav");
            }
        }

        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad5)
            {
                Storyboard b = (Storyboard)this.FindResource("HFarmBuildCompleted");
                SoundPlayer.PlaySelectionSound("Human\\Buildings\\Farm\\HFarm.wav");
                b.Begin();
            }
        }

        private void HumanFarmControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void HumanFarmControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void HumanFarmControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}
