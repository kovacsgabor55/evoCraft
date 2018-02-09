using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserControlUnits
{
    public abstract class UserSelectabIeMovingControl : UserSelectableControl
    {
        public readonly int DEFAULTSTEP = 3;
        public enum Angle { U, D, L, R, UL, UR, DL, DR };
        public virtual void Canvas_KeyDown(object sender, KeyEventArgs e) { }
    }
}
