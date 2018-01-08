using EvoCraft.Common.Map;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.Resources;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Core;
using EvoCraft.Core.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Core.MapObjects.PlayerControlled.Units;
using System.Collections.Generic;

namespace View
{
    /// <summary>
    /// Az összekötő kapocs a Model és a View Között. A valóságos váltotatásokat a RunningGame végzi.
    /// Itt találhatóak a kattintások lekezelő függvényei.
    /// </summary>
    public class ViewModel
    {
        int _rows;
        int _columns;
        List<Tile> _tiles = new List<Tile>();
        public Command<Tile> TileClickCommand { get; set; }
        public Command<Tile> TileRightClickCommand { get; set; }
        public Command<ActionOnPanel> ActionClickCommand { get; set; }
        Panel _panel = new Panel();

        public static bool BuildMode = false;
        public static Actions SelectedAction = Actions.None;

        public ViewModel(int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            System.Random rand = new System.Random();
            for (int r = 0; r < rows; r++)
            {
                for (int c = 0; c < columns; c++)
                { 
                    _tiles.Add(new Tile(r, c));

                }
            }

            TileClickCommand = new Command<Tile>(OnTileActionClick);
            TileRightClickCommand = new Command<Tile>(OnTileSelectionClick);
            ActionClickCommand = new Command<ActionOnPanel>(OnActionClick);
        }

        /// <summary>
        /// Jobb Egér gomb lekezelő függvény
        /// Ezzel lehet feladatot adni az egységeknek.
        /// </summary>
        /// <param name="tile"></param>
        public void OnTileActionClick(Tile tile)
        {
            Point pointOnMap = new Point(tile.Row + RenderHelper.Instance.LeftTopCorner.Row, tile.Col + RenderHelper.Instance.LeftTopCorner.Column);
            Cell selectedCell = Engine.Map.GetCellAt(pointOnMap); // Innen lehet tudni, hova klikkeltek

            if (Engine.SelectedMapObject != null)
            {
                if (Engine.SelectedMapObject is Unit)
                {
                    Unit unit = (Unit)Engine.SelectedMapObject;
                    unit.MoveTarget = pointOnMap;
                    if (Engine.SelectedMapObject.GetType() == typeof(Worker))
                    {
                        Worker worker = (Worker)Engine.SelectedMapObject;
                        if (BuildMode)
                        {
                            if (tile.AllowBuild == AllowBuildImage.Allow)
                            {
                                switch (SelectedAction)
                                {
                                    case Actions.BuildMainHall: worker.OrderABuild(new MainHall(worker.PlayerId), pointOnMap); break;
                                    case Actions.BuildWall: worker.OrderABuild(new Wall(worker.PlayerId), pointOnMap); break;
                                    case Actions.BuildBarracs: worker.OrderABuild(new BarracksExtension(worker.PlayerId), pointOnMap); break;
                                    case Actions.BuildTower: worker.OrderABuild(new Tower(worker.PlayerId), pointOnMap); break;
                                    case Actions.BuildMedicalTent: worker.OrderABuild(new MedicalTent(worker.PlayerId), pointOnMap); break;
                                    case Actions.BuildFarm: worker.OrderABuild(new FarmBuilding(worker.PlayerId), pointOnMap); break;
                                }
                            }
                            BuildMode = false;
                        }
                        else
                        {
                            bool AutomaticWorkTriggered = false;
                            foreach (MapObject mo in selectedCell.MapObjects)
                            {
                                if (mo is Resource)
                                {
                                    AutomaticWorkTriggered = true;
                                    worker.RememberedGatherTarget = pointOnMap;
                                    Resource res = (Resource)mo;
                                    switch (res.Type)
                                    {
                                        case ResourceType.Food: worker.NextOrder = Order.GoToFood; break;
                                        case ResourceType.Wood: worker.NextOrder = Order.GoToWood; break;
                                        case ResourceType.Gold: worker.NextOrder = Order.GoToGold; break;
                                    }
                                }
                                if (mo.GetType() == typeof(MainHall))
                                {
                                    MainHall mainh = (MainHall)mo;
                                    if (!mainh.IsUnderConstruction)
                                    {
                                        AutomaticWorkTriggered = true;
                                        worker.RememberedReturnTarget = pointOnMap;
                                        if (worker.Amount == 0)
                                        {
                                            worker.NextOrder = Order.None;
                                        }
                                        else
                                        {
                                            worker.NextOrder = Order.ReturnResource;
                                        }
                                    }
                                }
                                if (mo is Building)
                                {
                                    Building b = (Building)mo;
                                    if (b.IsUnderConstruction)
                                    {
                                        worker.NextOrder = Order.Build;
                                        AutomaticWorkTriggered = true;
                                    }
                                }
                            }
                            if (!AutomaticWorkTriggered)
                            {
                                worker.NextOrder = Order.None;
                            }
                            
                        }
                    }
                    Sounds.PlayOrderGivenSound(Engine.SelectedMapObject);
                }
                if (Engine.SelectedMapObject is TrainerBuilding)
                {
                    TrainerBuilding tb = (TrainerBuilding)Engine.SelectedMapObject;
                    tb.SpawnTarget = pointOnMap;
                    Sounds.PlayOrderGivenSound(Engine.SelectedMapObject);
                }
                if (Engine.SelectedMapObject.GetType() == typeof(Tower))
                {
                    Tower tb = (Tower)Engine.SelectedMapObject;
                    tb.Target = pointOnMap;
                }

            }

        }

        /// <summary>
        ///  Bal Egér gomb lekezelő függvény,
        ///  Ezzel lehet kiválasztani az egységeket
        /// </summary>
        /// <param name="tile"></param>
        public void OnTileSelectionClick(Tile tile)
        {
            BuildMode = false;
            Point pointOnMap = new Point(tile.Row + RenderHelper.Instance.LeftTopCorner.Row, tile.Col + RenderHelper.Instance.LeftTopCorner.Column);
            Cell selectedCell = Engine.Map.GetCellAt(pointOnMap); // Innen lehet tudni, hova klikkeltek

            MapObject mo = null;
            if (selectedCell.Visibility != VisibilityType.Unexplored)
            {
                for (int i = 0; i < selectedCell.MapObjects.Count; i++)
                {
                    if (mo != null)
                    {
                        if (mo.ZIndex < selectedCell.MapObjects[i].ZIndex)
                        {
                            mo = selectedCell.MapObjects[i];
                        }
                    }
                    else
                    {
                        mo = selectedCell.MapObjects[i];
                    }
                }
            }
            if (selectedCell.Visibility == VisibilityType.Explored)
            {
                if (mo != null && mo is Animal)
                {
                    Animal animal = (Animal)mo;
                    if (!animal.Dead)
                    {
                        mo = null;
                    }
                }
            }
            
            Sounds.PlaySelectionSound(mo);
            Engine.SelectedMapObject = mo;
        }

        /// <summary>
        /// Click handler for all the panel actions
        /// </summary>
        /// <param name="actionOnPanel"></param>
        public void OnActionClick(ActionOnPanel actionOnPanel)
        {
            if (Engine.SelectedMapObject != null)
            {
                switch (actionOnPanel.Type)
                {
                    case Actions.TrainWorker:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.StartMakingUnit(new Worker(mh.PlayerId));
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.TrainSoldier:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.StartMakingUnit(new Soldier(mh.PlayerId));
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.TrainPozsiHero:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.StartMakingUnit(new Hero(mh.PlayerId));
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.TrainDoctor:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.StartMakingUnit(new Doctor(mh.PlayerId));
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.TrainGunMan:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.StartMakingUnit(new GunMan(mh.PlayerId));
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.Cancel:
                        if (Engine.SelectedMapObject is TrainerBuilding)
                        {
                            TrainerBuilding mh = (TrainerBuilding)Engine.SelectedMapObject;
                            mh.CancelTraining();
                            BuildMode = false;
                            SelectedAction = Actions.None;
                        }
                        break;
                    case Actions.BuildWall:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new Wall(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildWall;
                        }

                        break;
                    case Actions.BuildBarracs:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new BarracksExtension(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildBarracs;
                        }

                        break;
                    case Actions.BuildMainHall:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new MainHall(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildMainHall;
                        }

                        break;
                    case Actions.BuildMedicalTent:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new MedicalTent(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildMedicalTent;
                        }

                        break;
                    case Actions.BuildTower:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new Tower(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildTower;
                        }

                        break;
                    case Actions.BuildFarm:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new FarmBuilding(0).Costs))
                        {
                            BuildMode = true;
                            SelectedAction = Actions.BuildFarm;
                        }

                        break;
                    case Actions.Stop:
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker))
                        {
                            Worker worker = (Worker)Engine.SelectedMapObject;
                            worker.NextOrder = Order.None;
                            worker.MoveTarget = null;
                            BuildMode = false;
                        }

                        break;
                    case Actions.AutoAttack:
                    case Actions.AutoHeal:
                        if (Engine.SelectedMapObject is Unit)
                        {
                            Unit unit = (Unit)Engine.SelectedMapObject;
                            if (unit.AlertMode)
                            {
                                unit.AlertMode = false;
                            }
                            else
                            {
                                unit.AlertMode = true;
                            }
                            BuildMode = false;
                        }

                        break;
                }
            }       
        }

        public int Rows
        {
            get { return _rows; }
            set { _rows = value; }
        }

        public int Columns
        {
            get { return _columns; }
            set { _columns = value; }
        }

        public List<Tile> Tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }

        public Panel Panel
        {
            get { return _panel; }
            set { _panel = value; }
        } 
    }
}
