using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvoCraft2.Core;
using EvoCraft2.Common;

namespace TestConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new Engine();
            Engine.OnUpdateFinished += Engine_OnUpdateFinished;

            Engine.StartEngine();      
        }

        private static void Engine_OnUpdateFinished(Dictionary<int, Unit> map)
        {
            foreach (var item in map)
            {
                Console.WriteLine(item.Value);
            }

            Engine.AddCommand(new MoveCommand(map[1], map[0]));
        }
    }
}
