using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class CatapultControl : UserSelectabIeMovingControl
    {
        public readonly Cast cast = Cast.ORC;
        Angle pos = Angle.U;

        public CatapultControl()
        {
            InitializeComponent();
        }

        public CatapultControl(double x, double y)
        {
            InitializeComponent();
            Canvas.SetLeft(this, x);
            Canvas.SetTop(this, y);
        }

        public void rida()
        {
            Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
        }
        public override void Canvas_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.NumPad8)
            {
                pos = Angle.U;
                transform.Angle = 0;
                scaleTransform.ScaleY = 1;
                scaleTransform.ScaleX = 1;
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardU1");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardU2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardD1");
                    b.Begin();
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardD2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardL2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardR2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUL2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) - DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUR2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDL1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) - DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDL2");
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
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultMove.wav");
                if (status)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDR1");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = false;
                }
                else
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDR2");
                    b.Begin();
                    Canvas.SetLeft(this, Canvas.GetLeft(this) + DEFAULTSTEP);
                    Canvas.SetTop(this, Canvas.GetTop(this) + DEFAULTSTEP);
                    status = true;
                }

            }
            else if (e.Key == Key.NumPad5)
            {
                SoundPlayer.PlaySelectionSound("Orc\\Units\\Catapult\\CatapultAttack.wav");
                if (pos == Angle.U)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardU");
                    b.Begin();
                }
                else if (pos == Angle.UR)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUR");
                    b.Begin();
                }
                else if (pos == Angle.UL)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardUL");
                    b.Begin();
                }
                else if (pos == Angle.D)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardD");
                    b.Begin();
                }
                else if (pos == Angle.DR)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDR");
                    b.Begin();
                }
                else if (pos == Angle.DL)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardDL");
                    b.Begin();
                }
                else if (pos == Angle.L)
                {
                    transform.Angle = 180;
                    scaleTransform.ScaleY = 1;
                    scaleTransform.ScaleX = -1;
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardL");
                    b.Begin();
                }
                else if (pos == Angle.R)
                {
                    Storyboard b = (Storyboard)this.FindResource("CatapultStoryBoardR");
                    b.Begin();
                }
            }
        }

        private void CatapultControl_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = true;
        }

        private void CatapultControl_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            this.IsSelected = false;
        }

        private void CatapultControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.IsSelected = true;
            this.Focus();
        }
    }
}
