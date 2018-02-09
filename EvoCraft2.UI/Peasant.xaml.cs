using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class PeasantControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.HUMAN;

        public PeasantControl()
        {
            InitializeComponent();
        }

        private void PeasantControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void PeasantControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void PeasantControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}
