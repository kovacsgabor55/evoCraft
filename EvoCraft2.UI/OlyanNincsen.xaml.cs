using EvoCraft2.Common;
using EvoCraft2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvoCraft2.UI
{
    /// <summary>
    /// Interaction logic for OlyanNincsen.xaml
    /// </summary>
    public partial class OlyanNincsen : Page
    {
        List<Unit> Map;
        public OlyanNincsen()
        {
            StaticClass.Client.MapReceived += Client_MapReceived1;
            //StaticClass.Client.CommandReceived += Client_CommandReceived;
            InitializeComponent();

            Engine.StartEngine();
        }

        private void Client_MapReceived1(object sender, List<Unit> e)
        {
            //ez az amit gabi csinál


            foreach (var item in e)
            {
                Console.WriteLine(item);
            }

            Map = e;
        }

        private void Client_CommandReceived(object sender, Command e)
        {
            throw new NotImplementedException();
        }

        private void Client_MapReceived(object sender, Common.Command e)
        {
            
        }

        private void MouseClick()
        {
            StaticClass.Client.AddCommand(new MoveCommand(Map[0], Map[1]));
            //StaticClass.Client.AddCommand(new MoveCommand(kijelöltegység, hovamegyen));
        }
    }
}
