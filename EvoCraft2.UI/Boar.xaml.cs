using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class BoarControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.NATURAL;

        public BoarControl()
        {
            InitializeComponent();
        }

        public BoarControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public override void SelectedSound(bool selected)
        {
            if (selected)
            {
                SoundPlayer.PlaySelectionSound("Miscellaneous\\Units\\Boar\\BoarSelect.wav");
            }
        }

        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad8)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardU");
                b.Begin();
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad2)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardD");
                b.Begin();
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad4)
            {
                transform.Angle = 180;
                scaleTransform.ScaleY = -1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad6)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad7)
            {
                transform.Angle = -90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardUL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad9)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardUR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad1)
            {
                transform.Angle = 90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardDL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad3)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardDR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad5)
            {
                Storyboard b = (Storyboard)this.FindResource("BoarStoryBoardX");
                SoundPlayer.PlaySelectionSound("Miscellaneous\\Units\\Boar\\BoarDeath.wav");
                b.Begin();
            }
        }

        private void BoarControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void BoarControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void BoarControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}