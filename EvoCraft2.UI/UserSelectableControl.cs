using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace UserControlUnits
{
    public abstract class UserSelectableControl : UserElementControl
    {
        public bool status = true;
        static Random rnd = new Random();



        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedProperty =
            DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(UserSelectabIeMovingControl), new PropertyMetadata(false));

        // kijeloles hanglejatszas
        public virtual void SelectedSound(bool selected) { }
    }
}
