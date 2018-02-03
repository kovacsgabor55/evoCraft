using EvoCraft2.Common;
using System.Collections.Generic;

namespace EvoCraft2.Core
{
    public class Engine
    {
        public static bool isEngineRunning = false;
        public static List<Command> CommandList = new List<Command>();
        public static Dictionary<int, Unit> Map = new Dictionary<int, Unit>();
        public static Coordinate MapSize;

        public delegate void SendMap(Dictionary<int, Unit> map);
        public static event SendMap OnUpdateFinished;

        public static void CreateMap()
        {
            MapSize = new Coordinate(20, 20);
            Unit unit = new Unit(new Coordinate(1, 1), 100, 0);
            Unit unit2 = new Unit(new Coordinate(19, 19), 100, 1);

            Map.Add(unit.UnitID, unit);
            Map.Add(unit2.UnitID, unit2);
        }

        public static void StartEngine()
        {
            CreateMap();
            if (!isEngineRunning)
            {
                isEngineRunning = true;
                Update();
            }
        }

        public static void StopEngine()
        {
            isEngineRunning = false;
        }

        public static void Update()
        {
            while (isEngineRunning)
            {
                //GetCommands
                List<Command> commands = CommandList; 

                //CalcMovement/Attack
                foreach (var item in commands)
                {
                    item.Execute();
                }

                //TODO
                //Send Map
                OnUpdateFinished.Invoke(Map);
            }
        }

        public static void AddCommand(Command command)
        {
            if (command != null)
            {
                CommandList.Add(command);               
            }
        }
    }
}
