
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using EvoCraft2.UI;
using UserControlUnits;

namespace EvoCraft2.UI
{
    public partial class Page1 : Page
    {
        List<UserSelectabIeMovingControl> logicalList = new List<UserSelectabIeMovingControl>();
        public Page1()
        {
            InitializeComponent();
            UserSelectabIeMovingControl obj1 = new CatapultControl(134, 199);
            UserSelectabIeMovingControl obj2 = new CatapultControl(247, 199);
            UserSelectabIeMovingControl obj3 = new BallistaControl(349, 199);
            UserSelectabIeMovingControl obj4 = new BallistaControl(456, 199);
            UserSelectabIeMovingControl obj5 = new BoarControl(134, 309);
            UserSelectabIeMovingControl obj6 = new BoarControl(320, 104);
            UserSelectabIeMovingControl obj7 = new SheepControl(485, 309);
            UserSelectabIeMovingControl obj8 = new SheepControl(320, 309);
            UserSelectabIeMovingControl obj9 = new SealControl(134, 104);
            UserSelectabIeMovingControl obj10 = new SealControl(485, 104);
            UserSelectabIeMovingControl obj11 = new HumanFarmControl(400, 400);
            UserSelectabIeMovingControl obj12 = new OrcFarmControl(500, 500);
            logicalList.Add(obj1);
            logicalList.Add(obj2);
            logicalList.Add(obj3);
            logicalList.Add(obj4);
            logicalList.Add(obj5);
            logicalList.Add(obj6);
            logicalList.Add(obj7);
            logicalList.Add(obj8);
            logicalList.Add(obj9);
            logicalList.Add(obj10);
            logicalList.Add(obj11);
            logicalList.Add(obj12);
            viewList.Children.Add(obj1);
            viewList.Children.Add(obj2);
            viewList.Children.Add(obj3);
            viewList.Children.Add(obj4);
            viewList.Children.Add(obj5);
            viewList.Children.Add(obj6);
            viewList.Children.Add(obj7);
            viewList.Children.Add(obj8);
            viewList.Children.Add(obj9);
            viewList.Children.Add(obj10);
            viewList.Children.Add(obj11);
            viewList.Children.Add(obj12);
        }

        private void Canvas_KeyDown(object sender, KeyEventArgs e)
        {
            foreach (UserSelectabIeMovingControl s in logicalList)
            {
                if (s.IsSelected)
                {
                    s.Canvas_KeyDown(sender, e);
                }
            }
        }

        private Point LastMouseDown { get; set; }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LastMouseDown = e.GetPosition(this);
            if (e.RightButton == MouseButtonState.Pressed)
            {
                //backendnek kuldjuk

                System.Console.WriteLine(LastMouseDown.X + ", " + LastMouseDown.Y);
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point currentMousePosition = e.GetPosition(this);
            Rect referenceRect = new Rect((LastMouseDown.X < currentMousePosition.X ? LastMouseDown.X : currentMousePosition.X), (LastMouseDown.Y < currentMousePosition.Y ? LastMouseDown.Y : currentMousePosition.Y), Math.Abs(LastMouseDown.X - currentMousePosition.X), Math.Abs(LastMouseDown.Y - currentMousePosition.Y));

            foreach (UserSelectabIeMovingControl s in logicalList)
            {
                SelectControl(s, referenceRect);
            }

            Keyboard.IsKeyDown(Key.LeftShift);
        }

        private void SelectControl(UserSelectabIeMovingControl selectedControl, Rect referenceRect)
        {
            double controltTop = Canvas.GetTop(selectedControl);
            double controlLeft = Canvas.GetLeft(selectedControl);
            double controlRight = controlLeft + selectedControl.ActualWidth;
            double controlBottom = controltTop + selectedControl.ActualHeight;

            if (referenceRect.Width > 10 && referenceRect.Height > 10)
            {
                if (controltTop >= referenceRect.Top && controlBottom <= referenceRect.Bottom &&
                    controlLeft >= referenceRect.Left && controlRight <= referenceRect.Right)
                {
                    selectedControl.IsSelected = true;
                    selectedControl.SelectedSound(true);
                }
                else
                {
                    selectedControl.IsSelected = false;
                    selectedControl.SelectedSound(false);
                }
            }
        }
    }
}
