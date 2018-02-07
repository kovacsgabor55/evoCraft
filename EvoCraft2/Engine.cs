using System;
using EvoCraft2.Common;
using System.Collections.Generic;

namespace EvoCraft2.Core
{
    public class Engine
    {
        public static bool isEngineRunning = false;
        public static List<Command> CommandList = new List<Command>();
        public static List<Unit> Map = new List<Unit>();
        public static Coordinate MapSize;
        private static List<Unit> DeadUnits; 
        
        public static event EventHandler<List<Unit>> OnUpdateFinished;

        public static void CreateMap()
        {
            MapSize = new Coordinate(20, 20);
            Unit unit = new Unit(0, new Coordinate(1, 1), 100, 10);
            Unit unit2 = new Unit(1, new Coordinate(19, 19), 100, 10);

            Map.Add(unit);
            Map.Add(unit2);
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

                commands.Clear();

                foreach (var item in Map)
                {
                    UnitUpdate(item);
                }

                //Remove "dead" units
                if (DeadUnits != null && DeadUnits.Count > 0)
                {
                    foreach (var item in Map)
                    {
                        foreach (var item2 in DeadUnits)
                        {
                            if (item.Target == item2.Position)
                            {
                                item.Target = null;
                            }
                        }
                    }
                    DeadUnits.Clear();
                }

                Map.RemoveAll(item => item.HP <= 0);
                

                //TODO
                //Send Map
                OnUpdateFinished?.Invoke(null, Map);

            }
        }

        public static void AddCommand(Command command)
        {
            if (command != null)
            {
                CommandList.Add(command);               
            }
        }

        private static void UnitUpdate(Unit unit)
        {
            Directions direction = Directions.NoMove;
            Coordinate checkCoordinate = null;

            #region SetDirection

            if (unit.Target != null)
            {
                if (unit.Position != unit.Target)
                {
                    int deltaX = unit.Position.X - unit.Target.X;
                    int deltaY = unit.Position.Y - unit.Target.Y;

                    if (deltaX > 0) //Left
                    {
                        if (deltaY > 0) //UpLeft
                        {
                            direction = Directions.UpLeft;
                            checkCoordinate = new Coordinate(unit.Position.X - 1, unit.Position.Y - 1);
                        }
                        else if (deltaY < 0) //DownLeft
                        {
                            direction = Directions.DownLeft;
                            checkCoordinate = new Coordinate(unit.Position.X - 1, unit.Position.Y + 1);
                        }
                        else //Left
                        {
                            direction = Directions.Left;
                            checkCoordinate = new Coordinate(unit.Position.X - 1, unit.Position.Y);
                        }
                    }
                    else if (deltaX < 0) //Right
                    {
                        if (deltaY > 0) //UpRight
                        {
                            direction = Directions.UpRight;
                            checkCoordinate = new Coordinate(unit.Position.X + 1, unit.Position.Y - 1);
                        }
                        else if (deltaY < 0) //DownRight
                        {
                            direction = Directions.DownRight;
                            checkCoordinate = new Coordinate(unit.Position.X + 1, unit.Position.Y + 1);
                        }
                        else //Right
                        {
                            direction = Directions.Right;
                            checkCoordinate = new Coordinate(unit.Position.X + 1, unit.Position.Y);
                        }
                    }
                    else //deltaX == 0
                    {
                        if (deltaY > 0) //UP
                        {   
                            direction = Directions.Up;
                            checkCoordinate = new Coordinate(unit.Position.X, unit.Position.Y - 1);
                        }
                        else if (deltaY < 0) //Down
                        {
                            direction = Directions.Down;
                            checkCoordinate = new Coordinate(unit.Position.X, unit.Position.Y + 1);
                        }
                        else //No Movement
                        {
                            direction = Directions.NoMove;
                            checkCoordinate = new Coordinate(unit.Position.X, unit.Position.Y);
                        }
                    }
                }
            }

            #endregion

            #region CheckCoordinate

            bool matchFound = false;
            Unit matchUnit = null;

            foreach (var item in Map)
            {
                if (checkCoordinate != null)
                {
                    if (item.Position.X == checkCoordinate.X && item.Position.Y == checkCoordinate.Y)
                    {
                        matchFound = true;
                        matchUnit = item;
                    }
                }
            }
            #endregion

            #region DoAction

            if (matchFound)
            {
                Attack(unit, matchUnit);
            }
            else
            {
                Move(unit, direction);
            }

            #endregion
        }

        private static void Attack(Unit Attacker, Unit Defender)
        {
            //CounterAttack
            AddCommand(new MoveCommand(Defender, Attacker));

            //TakeDamage
            Defender.HP -= Attacker.Damage;

            if (Defender.HP <= 0)
            {
                DeadUnits.Add(Defender);
            }
        }

        private static void Move(Unit unit, Directions direction)
        {
            switch (direction)
            {
                case Directions.Up:
                    unit.Position.Y -= 1;
                    break;
                case Directions.UpRight:
                    unit.Position.X += 1;
                    unit.Position.Y -= 1;
                    break;
                case Directions.Right:
                    unit.Position.X += 1;
                    break;
                case Directions.DownRight:
                    unit.Position.X += 1;
                    unit.Position.Y += 1;
                    break;
                case Directions.Down:
                    unit.Position.Y += 1;
                    break;
                case Directions.DownLeft:
                    unit.Position.X -= 1;
                    unit.Position.Y += 1;
                    break;
                case Directions.Left:
                    unit.Position.X -= 1;
                    break;
                case Directions.UpLeft:
                    unit.Position.X -= 1;
                    unit.Position.Y -= 1;
                    break;
                default:
                    break;
            }
        }
    }
}
