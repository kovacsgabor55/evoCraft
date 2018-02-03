using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvoCraft2.Core;

namespace TestConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Engine.StartEngine();

            while (Engine.isEngineRunning)
            {
                foreach (var item in Engine.Map)
                {
                    Console.WriteLine("Id: " + item.Value.UnitID + ", Team: " + item.Value.Team + ", Pos: " + item.Value.Position + ", Target:" + item.Value.Target + ", HP: " + item.Value.HP);
                }
            }
        }
    }
}
