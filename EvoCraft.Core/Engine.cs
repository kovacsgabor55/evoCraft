using System.Collections.Generic;
using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.Resources.Animals;
using System;

namespace EvoCraft.Core
{
    /// <summary>
    /// Runs the whole game in the data.
    /// Update needs to be called every loop.
    /// </summary>
    public class Engine
    {
        private static Map myMap;
        private static MapObject selectedMapObject = null;
        private static Player myThePlayer = null;
        private static GameState myState;

        public static Map Map
        {
            get
            {
                return myMap;
            }
            set
            {
                myMap = value;
                myThePlayer = new Player(myMap.InitialFood, myMap.InitialGold, myMap.InitialWood);
                selectedMapObject = null;
                State = GameState.Running;
            }
        }

        public static GameState State
        {
            get
            {
                return myState;
            }
            set
            {
                myState = value;
            }
        }

        
        public static MapObject SelectedMapObject
        {
            get
            {
                return selectedMapObject;
            }
            set
            {
                selectedMapObject = value;
            }
        }
        
        public static Player ThePlayer
        {
            get { return myThePlayer; }
            set { myThePlayer = value; }
        }
        
        

        private Engine() { }

        /// <summary>
        /// Updates the whole scene.
        /// This method should be called every loop.
        /// </summary>
        public static void Update()
        {
            bool thereArePlayerUnits = false;
            bool thereAreEnemies = false;
            ResetActiveVisiblitiesToExplored();

            List<MapObject> updatingMapObjectList = new List<MapObject>();
            foreach (Cell cell in Map.Cells)
            {
                updatingMapObjectList.AddRange(cell.MapObjects);   
            }
            foreach (MapObject mapObj in updatingMapObjectList)
            {
                bool found;
                Point pos = GetMapObjectPosition(mapObj, out found);

                UnitUpdate(mapObj, pos);
                
                if (found)
                {
                    if (mapObj.GetType().IsSubclassOf(typeof(PlayerControlled)))
                    {
                        PlayerControlled unit = (PlayerControlled)mapObj;
                        SetVisibilitiesToActiveInRange(pos, unit.SightRange);
                        thereArePlayerUnits = true;
                    }
                    if (mapObj.GetType().IsSubclassOf(typeof(AggressiveAnimal)))
                    {
                        AggressiveAnimal ani = (AggressiveAnimal)mapObj;
                        if (!ani.Dead)
                        {
                            thereAreEnemies = true;
                        }
                    }
                }

            }
            if (!thereAreEnemies && State != GameState.PostVictory)
            {
                State = GameState.Victory;
            }
            if (!thereArePlayerUnits)
            {
                State = GameState.Defeat;
            }
        }

        private static void ResetActiveVisiblitiesToExplored()
        {
            for (int i=0; i< Map.Height; i++)
            {
                for (int j=0; j< Map.Width; j++)
                {
                    if (Map.GetCellAt(i,j).Visibility == VisibilityType.Active)
                    {
                        Map.GetCellAt(i, j).Visibility = VisibilityType.Explored;
                    }
                }
            }
        }

        private static void SetVisibilitiesToActiveInRange(Point point, int range)
        {
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (point.DistanceFrom(p) < range && !(point.DistanceFrom(p) == range-1 && point.IsOnTheSameAxisParallelLine(p)))
                    {
                        Map.GetCellAt(i, j).Visibility = VisibilityType.Active;
                    }
                }
            }
        }

        /// <summary>
        /// Puts the base of a building.
        /// </summary>
        /// <param name="building"></param>
        /// <param name="place"></param>
        /// <returns></returns>
        public static bool StartBuilding(Building building, Point place)
        {
            bool couldBuild = true;
            if (Map.GetCellAt(place).canMapObjectBePlaced(building))
            {
                Map.GetCellAt(place).MapObjects.Add(building);
            }
            else
            {
                couldBuild = false;
            }

            return couldBuild;
        }

        /// <summary>
        /// Moves an object to the given direction if it can. If it can't it does nothing. Checks for collisions.
        /// </summary>
        public static void MoveMapObject(MapObject mapObj, Direction direction)
        {
            if (direction != Direction.None)
            {
                
                bool found;
                Point pos = GetMapObjectPosition(mapObj, out found);

                if (found)
                {
                    switch (direction)
                    {
                        case Direction.Left:
                            if (pos.y - 1 >= 0 && Map.GetCellAt(pos.x, pos.y - 1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x, pos.y - 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.Right:
                            if (pos.y + 1 < Map.Width && Map.GetCellAt(pos.x, pos.y + 1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x, pos.y + 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.Up:
                            if (pos.x - 1 >= 0 && Map.GetCellAt(pos.x - 1, pos.y).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x - 1, pos.y).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.Down:
                            if (pos.x + 1 < Map.Height && Map.GetCellAt(pos.x + 1, pos.y).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x + 1, pos.y).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.LeftUp:
                            if (pos.x - 1 >= 0 && pos.y - 1 >= 0 && Map.GetCellAt(pos.x - 1, pos.y - 1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x - 1, pos.y - 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.LeftDown:
                            if (pos.x + 1 < Map.Height && pos.y - 1 >= 0 && Map.GetCellAt(pos.x + 1, pos.y-1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x + 1, pos.y - 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.RightUp:
                            if (pos.x - 1 >= 0 && pos.y + 1 < Map.Width && Map.GetCellAt(pos.x - 1, pos.y + 1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x - 1, pos.y + 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                        case Direction.RightDown:
                            if (pos.x + 1 < Map.Height && pos.y + 1 < Map.Width && Map.GetCellAt(pos.x + 1, pos.y + 1).canMapObjectBePlaced(mapObj))
                            {
                                Map.GetCellAt(pos.x + 1, pos.y + 1).MapObjects.Add(mapObj);
                                Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                            }
                            break;
                    }
                }
            }
            
        }

        

        /// <summary>
        /// Moves an object to the given direction if it can. If it can't it does nothing. Checks for collisions.
        /// </summary>
        public static void MoveMapObject(MapObject mapObj, Direction direction, Point pos)
        {
            if (direction != Direction.None)
            {
                switch (direction)
                {
                    case Direction.Left:
                        if (pos.y - 1 >= 0 && Map.GetCellAt(pos.x, pos.y - 1).canMapObjectBePlaced(mapObj))
                        {
                            Map.GetCellAt(pos.x, pos.y - 1).MapObjects.Add(mapObj);
                            Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                        }
                        break;
                    case Direction.Right:
                        if (pos.y + 1 < Map.Width && Map.GetCellAt(pos.x, pos.y + 1).canMapObjectBePlaced(mapObj))
                        {
                            Map.GetCellAt(pos.x, pos.y + 1).MapObjects.Add(mapObj);
                            Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                        }
                        break;
                    case Direction.Up:
                        if (pos.x - 1 >= 0 && Map.GetCellAt(pos.x - 1, pos.y).canMapObjectBePlaced(mapObj))
                        {
                            Map.GetCellAt(pos.x - 1, pos.y).MapObjects.Add(mapObj);
                            Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                        }
                        break;
                    case Direction.Down:
                        if (pos.x + 1 < Map.Height && Map.GetCellAt(pos.x + 1, pos.y).canMapObjectBePlaced(mapObj))
                        {
                            Map.GetCellAt(pos.x + 1, pos.y).MapObjects.Add(mapObj);
                            Map.GetCellAt(pos.x, pos.y).MapObjects.Remove(mapObj);
                        }
                        break;
                }
            }
        }


        /// <summary>
        /// Gets the position of a MapObject.
        /// </summary>
        public static Point GetMapObjectPosition(MapObject mapObj, out bool found)
        {
            
            int x = 0;
            int y = 0;
            found = false;

            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    if (Map.GetCellAt(i, j).MapObjects.Contains(mapObj))
                    {
                        x = i;
                        y = j;
                        found = true;
                        break;
                    }
                }
                if (found)
                {
                    break;
                }
            }

             return new Point(x,y);
        }
        
        /// <summary>
        /// Used by the Agressive Animals.
        /// To find the closest target to attack.
        /// </summary>
        public static Point GetClosestUnitOrBuildingInRange(Point mapObjPos, int range, out bool foundNearbyUnit)
        {
            Point nearbyUnitPos = null;
            foundNearbyUnit = false;
            int bestRange = range;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (mapObjPos.DistanceFrom(p) < bestRange)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo.GetType().IsSubclassOf(typeof(PlayerControlled)))
                            {
                                bestRange = mapObjPos.DistanceFrom(p);
                                nearbyUnitPos = p;
                                foundNearbyUnit = true;
                                break;
                            }
                        }
                    }
                }
            }
            return nearbyUnitPos;
        }

        public static Point SearchClosestMainHall(Point pos)
        {
            int bestDistance = 10000;
            Point thePlace = null;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (pos.DistanceFrom(p) < bestDistance)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo.GetType() == typeof(MainHall))
                            {
                                bestDistance = pos.DistanceFrom(p);
                                thePlace = p;
                                break;
                            }
                        }
                    }
                }
            }
            return thePlace;
        }
        
        public static Point SearchClosestResourceInRange(Point pos, ResourceType carriedResourceType, int sightRange)
        {
            int bestRange = sightRange;
            Point nearbyResourcePos = null;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (pos.DistanceFrom(p) < bestRange)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo.GetType().IsSubclassOf(typeof(Resource)))
                            {
                                Resource res = (Resource)mo;
                                if (res.Type == carriedResourceType)
                                {
                                    bestRange = pos.DistanceFrom(p);
                                    nearbyResourcePos = p;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return nearbyResourcePos;
        }

        public static Point SearchClosestPeacefulAnimalInRange(Point pos, int sightRange, out bool isDead)
        {
            int bestRange = sightRange;
            Point nearbyResourcePos = null;
            isDead = false;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (pos.DistanceFrom(p) < bestRange)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo.GetType().IsSubclassOf(typeof(Animal)) && !mo.GetType().IsSubclassOf(typeof(AggressiveAnimal)))
                            {
                                Animal animal = (Animal)mo;
                                bestRange = pos.DistanceFrom(p);
                                nearbyResourcePos = p;
                                if (animal.Dead)
                                {
                                    isDead = true;
                                }
                                break;
                            }
                        }
                    }
                }
            }
            return nearbyResourcePos;
        }

        /// <summary>
        /// Used by the Soldier to find a nearyby Chupacabra.
        /// </summary>
        /// <param name="mapObj"></param>
        /// <param name="range"></param>
        /// <returns>The position of the chupacabra</returns>
        public static Point GetClosestAggressiveAnimalInRange(Point mapObjPos, int range)
        {
            Point nearbyEnemyPos = null;
            int bestRange = range;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (mapObjPos.DistanceFrom(p) < bestRange)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo.GetType().IsSubclassOf(typeof(AggressiveAnimal)))
                            {
                                AggressiveAnimal chu = (AggressiveAnimal)mo;
                                if (!chu.Dead)
                                {
                                    bestRange = mapObjPos.DistanceFrom(p);
                                    nearbyEnemyPos = p;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return nearbyEnemyPos;
        }

        /// <summary>
        /// Used by the Doctor to find a nearyby needy unit.
        /// </summary>
        /// <param name="mapObj"></param>
        /// <param name="range"></param>
        /// <returns>The position of the chupacabra</returns>
        public static Point GetClosestInjuredUnitInRange(Point mapObjPos, int range)
        {
            Point nearbyInjuredFriendPos = null;
            int bestRange = range;
            for (int i = 0; i < Map.Height; i++)
            {
                for (int j = 0; j < Map.Width; j++)
                {
                    Point p = new Point(i, j);
                    if (mapObjPos.DistanceFrom(p) < bestRange)
                    {
                        foreach (MapObject mo in Map.GetCellAt(i, j).MapObjects)
                        {
                            if (mo is Unit)
                            {
                                Unit unit = (Unit)mo;
                                if (unit.ActualHealthPoints < unit.MaximalHealthPoints)
                                {
                                    bestRange = mapObjPos.DistanceFrom(p);
                                    nearbyInjuredFriendPos = p;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return nearbyInjuredFriendPos;
        }

        /// <summary>
        /// Checks if the map object is on a given position. It is important for the move to a target.
        /// </summary>
        /// <param name="mapObj"></param>
        /// <param name="position"></param>
        public static Direction GetDirectionForPathToTargetPosition(Point pos, Point targetPosition)
        {
            Direction direction = Direction.None;
            AStarSearch search = new AStarSearch(Map, pos, targetPosition);
            direction = search.FindPathAndGiveDirection();
            return direction;
        }

        /// <summary>
        /// Checks if the map object is on a given position. It is important for the move to a target.
        /// </summary>
        /// <param name="mapObj"></param>
        /// <param name="position"></param>
        public static Direction GetDirectionForPathToTargetPosition(Point pos, Point targetPosition, BlockType blockT)
        {
            Direction direction = Direction.None;
            AStarSearch search = new AStarSearch(Map, pos, targetPosition, blockT);
            direction = search.FindPathAndGiveDirection();
            return direction;
        }

        /// <summary>
        /// Removes the mapobject from the map entierly.
        /// </summary>
        /// <param name="mapObj"></param>
        public static void DestroyMapObject(MapObject mapObj)
        {
            if (mapObj != null)
            {
                if (mapObj == SelectedMapObject)
                {
                    SelectedMapObject = null;
                }

                bool found;
                Point pos = GetMapObjectPosition(mapObj, out found);

                if (found)
                {
                    Map.GetCellAt(pos).MapObjects.Remove(mapObj);
                }
            }
        }

        /// <summary>
        /// Removes the mapobject from the map entierly.
        /// </summary>
        /// <param name="mapObj"></param>
        public static void DestroyMapObject(MapObject mapObj, Point pos)
        {
            if (mapObj != null)
            {
                if (mapObj == SelectedMapObject)
                {
                    SelectedMapObject = null;
                }
                Map.GetCellAt(pos).MapObjects.Remove(mapObj);
            }

        }

        /// <summary>
        /// Puts the MapObject where the Building is.
        /// </summary>
        public static void SpawnUnitFromBuilding(Building building, MapObject mo)
        {
            bool found;
            Point pos = GetMapObjectPosition(building, out found);

            if (found)
            {
                Map.GetCellAt(pos.x, pos.y).MapObjects.Add(mo);
            }
        }

        private static void UnitUpdate(MapObject mapObj, Point pos)
        {
            mapObj.Update(pos);
        }
    }
}
