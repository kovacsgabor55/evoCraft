using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class SheepControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.NATURAL;

        public SheepControl()
        {
            InitializeComponent();
        }

        public SheepControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public override void SelectedSound(bool selected)
        {
            if (selected)
            {
                SoundPlayer.PlaySelectionSound("Miscellaneous\\Units\\Sheep\\SheepSelect.wav");
            }
        }

        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad8)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardU");
                b.Begin();
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad2)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardD");
                b.Begin();
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad4)
            {
                transform.Angle = 180;
                scaleTransform.ScaleY = -1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad6)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad7)
            {
                transform.Angle = -90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardUL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad9)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardUR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad1)
            {
                transform.Angle = 90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardDL");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad3)
            {
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardDR");
                b.Begin();
                Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
            }
            else if (e.Key == Key.NumPad5)
            {
                Storyboard b = (Storyboard)this.FindResource("SheepStoryBoardX");
                SoundPlayer.PlaySelectionSound("Miscellaneous\\Units\\Sheep\\SheepDeath.wav");
                b.Begin();
            }
        }

        private void SheepControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void SheepControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void SheepControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}