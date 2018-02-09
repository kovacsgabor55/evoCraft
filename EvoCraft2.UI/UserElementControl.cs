using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace UserControlUnits
{
    public abstract class UserElementControl : UserControl
    {
        private int objazon = 0;
        public enum Cast { NATURAL, HUMAN, ORC };

        public UserElementControl()
        {
            objazon++;
        }
        public int Azon()
        {
            return objazon;
        }
    }
}
