using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class BallistaControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.HUMAN;
        Angle pos = Angle.U;

        public BallistaControl()
        {
            InitializeComponent();
        }

        public BallistaControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad8)
            {
                pos = Angle.U;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardU1");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardU2");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad2)
            {
                pos = Angle.D;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardD1");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardD2");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad4)
            {
                pos = Angle.L;
                transform.Angle = 180;
                scaleTransform.ScaleY = -1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardL2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad6)
            {
                pos = Angle.R;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardR2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad7)
            {
                pos = Angle.UL;
                transform.Angle = -90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUL2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad9)
            {
                pos = Angle.UR;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUR2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad1)
            {
                pos = Angle.DL;
                transform.Angle = 90;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = -1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDL2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad3)
            {
                pos = Angle.DR;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDR2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad5)
            {
                SoundPlayer.PlaySelectionSound("Human\\Units\\Ballista\\BallistaAttack.wav");
                if (pos == Angle.U)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardU");
                    b.Begin();
                }
                else if (pos == Angle.UR)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUR");
                    b.Begin();
                }
                else if (pos == Angle.UL)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardUL");
                    b.Begin();
                }
                else if (pos == Angle.D)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardD");
                    b.Begin();
                }
                else if (pos == Angle.DR)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDR");
                    b.Begin();
                }
                else if (pos == Angle.DL)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardDL");
                    b.Begin();
                }
                else if (pos == Angle.L)
                {
                    transform.Angle = 180;
                    scaleTransform.ScaleY = 1;
                    scaleTransform.ScaleX = -1;
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardL");
                    b.Begin();
                }
                else if (pos == Angle.R)
                {
                    Storyboard b = (Storyboard)this.FindResource("BallistaStoryBoardR");
                    b.Begin();
                }
            }
        }

        private void BallistaControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void BallistaControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void BallistaControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}
