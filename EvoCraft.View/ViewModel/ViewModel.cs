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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Threading;

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
                                    case Actions.BuildBarracs: worker.OrderABuild(new Barracks(worker.PlayerId), pointOnMap); break;
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
                        if (Engine.SelectedMapObject.GetType() == typeof(Worker) && Engine.ThePlayer.Resources.HasEnoughToReduceBy(new Barracks(0).Costs))
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

        public void UpdateResourcesOnView()
        {
            int[] resources = Engine.AmountOfResources;
            this.Panel.Gold = resources[0];
            this.Panel.Wood = resources[1];
            this.Panel.Food = resources[2];
        }

        public void KeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                RenderHelper.Instance.MoveUp();
            }
            else if (e.Key == Key.S)
            {
                RenderHelper.Instance.MoveDown();
            }
            else if (e.Key == Key.A)
            {
                RenderHelper.Instance.MoveLeft();
            }
            else if (e.Key == Key.D)
            {
                RenderHelper.Instance.MoveRight();
            }
        }

        public void StartTimer()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timerCycle;
            timer.Start();

            Engine.EngineTimer = new BackgroundWorker();
            Engine.EngineTimer.DoWork += UpdateBackEnd;
            Engine.EngineTimer.RunWorkerCompleted += Render;
        }

        /// <summary>
        /// The fired method by the DispatcherTimer. It will only run the updating background thread if it is done with the previous update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void timerCycle(object sender, EventArgs e)
        {
            if (!Engine.EngineTimer.IsBusy)
            {
                Engine.EngineTimer.RunWorkerAsync();
            }
        }

        /// <summary>
        /// Updates the back-end's state. Every object's update will be called in the process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateBackEnd(object sender, DoWorkEventArgs e)
        {
            if (Engine.State == GameState.Running || Engine.State == GameState.PostVictory)
            {
                Engine.Update();
            }
        }

        /// <summary>
        /// Renders the screen with the updated state of the objects on the map. This controls the main display of the running game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Render(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Engine.State == GameState.Running || Engine.State == GameState.PostVictory)
            {
                for (int i = 0; i < this.Rows; i++)
                {
                    for (int j = 0; j < this.Columns; j++)
                    {
                        // Clearing
                        this.Tiles[j + i * this.Columns].StoredMapObjectImage = MapObjectImage.None;
                        this.Tiles[j + i * this.Columns].StoredBulletImage = MapObjectImage.None;
                        this.Tiles[j + i * this.Columns].Selection = SelectionImage.None;

                        // Setting Ground texture
                        this.Tiles[j + i * this.Columns].StoredFieldImage = GroundTextureToFieldImage.Convert(Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).Ground);

                        // Setting Visiblity
                        this.Tiles[j + i * this.Columns].Visibility = Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).Visibility;

                        // Setting target
                        if (Engine.SelectedMapObject != null)
                        {
                            if (Engine.SelectedMapObject is Unit)
                            {
                                Unit unit = (Unit)Engine.SelectedMapObject;
                                if (unit.MoveTarget != null && (unit.MoveTarget.x - RenderHelper.Instance.LeftTopCorner.Row) == i && (unit.MoveTarget.y - RenderHelper.Instance.LeftTopCorner.Column) == j)
                                {
                                    this.Tiles[j + i * this.Columns].Selection = SelectionImage.MoveTarget;
                                }
                            }
                            if (Engine.SelectedMapObject is TrainerBuilding)
                            {
                                TrainerBuilding b = (TrainerBuilding)Engine.SelectedMapObject;
                                if (b.SpawnTarget != null && (b.SpawnTarget.x - RenderHelper.Instance.LeftTopCorner.Row) == i && (b.SpawnTarget.y - RenderHelper.Instance.LeftTopCorner.Column) == j)
                                {
                                    this.Tiles[j + i * this.Columns].Selection = SelectionImage.SpawnFlag;
                                }
                            }
                        }

                        foreach (MapObject mapObj in Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).MapObjects)
                        {

                            // Set the mapobject image

                            if (mapObj.GetType() == typeof(Bullet))
                            {
                                this.Tiles[j + i * this.Columns].StoredBulletImage =
                                        MapObjectConverterFromBackEnd.Convert(mapObj);
                            }
                            else
                            {
                                if (mapObj is Animal)
                                {
                                    Animal animal = (Animal)mapObj;
                                    if (animal.Dead || this.Tiles[j + i * this.Columns].Visibility == VisibilityType.Active)
                                    {
                                        this.Tiles[j + i * this.Columns].StoredMapObjectImage =
                                            MapObjectConverterFromBackEnd.Convert(mapObj);
                                    }
                                }
                                else
                                {
                                    this.Tiles[j + i * this.Columns].StoredMapObjectImage =
                                        MapObjectConverterFromBackEnd.Convert(mapObj);
                                }
                            }
                            // Setting selection

                            if (Engine.SelectedMapObject != null)
                            {
                                if (mapObj.Id == Engine.SelectedMapObject.Id && this.Tiles[j + i * this.Columns].Visibility == VisibilityType.Active)
                                {
                                    this.Tiles[j + i * this.Columns].Selection = SelectionImage.Selected;
                                }
                            }
                        }

                        // Setting building allowances
                        if (BuildMode)
                        {
                            if (Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).canBlockTypeBePlaced(BlockType.BlockOtherBlock))
                            {
                                this.Tiles[j + i * this.Columns].AllowBuild = AllowBuildImage.Allow;
                            }
                            else
                            {
                                this.Tiles[j + i * this.Columns].AllowBuild = AllowBuildImage.Block;
                            }
                        }
                        else
                        {
                            this.Tiles[j + i * this.Columns].AllowBuild = AllowBuildImage.None;
                        }
                    }
                }
                RenderPanel();
            }
            else
            {
                this.Panel.CurrentGameState = Engine.State;
                switch (this.Panel.CurrentGameState)
                {
                    case GameState.Victory:
                        global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = false;
                        Sounds.PlayVictorySong();
                        break;
                    case GameState.Defeat:
                        global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = false;
                        Sounds.PlayDefeatSong();
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Renders the panel.
        /// </summary>
        public void RenderPanel()
        {
            if (Engine.SelectedMapObject == null)
            {
                this.Panel.SelectedMapObjectImage = MapObjectImage.None;
                this.Panel.SelectedMapObjectLabel = "";
                this.Panel.SelectedMapObjectInfo = "";
                this.Panel.SelectedMapObjectHealth = "";
                if (Engine.SelectedMapObject == null)
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(this.Panel.Actions);
                }
            }
            else
            {
                this.Panel.SelectedMapObjectHealth = "";
                this.Panel.SelectedMapObjectInfo = "";
                this.Panel.SelectedMapObjectImage = MapObjectConverterFromBackEnd.Convert(Engine.SelectedMapObject);
                this.Panel.SelectedMapObjectLabel = Engine.SelectedMapObject.Label;
                if (Engine.SelectedMapObject is Unit)
                {
                    ActionConverterFromBackEnd.RefreshActionList(this.Panel.Actions, (Unit)Engine.SelectedMapObject);

                    Unit unit = (Unit)Engine.SelectedMapObject;
                    this.Panel.SelectedMapObjectHealth = "Health: " + unit.ActualHealthPoints + "/" + unit.MaximalHealthPoints;

                    if (Engine.SelectedMapObject.GetType() == typeof(Worker))
                    {
                        Worker worker = (Worker)Engine.SelectedMapObject;
                        string info = "";
                        if (worker.Amount > 0)
                        {
                            switch (worker.CarriedResourceType)
                            {
                                case ResourceType.Food: info = "Carrying " + worker.Amount + " food"; break;
                                case ResourceType.Wood: info = "Carrying " + worker.Amount + " wood"; break;
                                case ResourceType.Gold: info = "Carrying " + worker.Amount + " gold"; break;
                            }
                        }
                        this.Panel.SelectedMapObjectInfo = info;
                    }
                    if (Engine.SelectedMapObject.GetType() == typeof(Soldier) || Engine.SelectedMapObject.GetType() == typeof(Hero))
                    {
                        Unit u = (Unit)Engine.SelectedMapObject;
                        string info;
                        if (u.AlertMode)
                        {
                            info = "Auto Attack ON";
                        }
                        else
                        {
                            info = "Idle";
                        }
                        this.Panel.SelectedMapObjectInfo = info;
                    }
                    if (Engine.SelectedMapObject is Doctor)
                    {
                        Unit u = (Unit)Engine.SelectedMapObject;
                        string info;
                        if (u.AlertMode)
                        {
                            info = "Auto Heal ON";
                        }
                        else
                        {
                            info = "Idle";
                        }
                        this.Panel.SelectedMapObjectInfo = info;
                    }
                }
                else if (Engine.SelectedMapObject is Building)
                {
                    Building building = (Building)Engine.SelectedMapObject;
                    this.Panel.SelectedMapObjectHealth = "Health: " + building.ActualHealthPoints + "/" + building.MaximalHealthPoints;
                    ActionConverterFromBackEnd.RefreshActionList(this.Panel.Actions, (Building)Engine.SelectedMapObject);
                    if (Engine.SelectedMapObject is TrainerBuilding)
                    {
                        TrainerBuilding tb = (TrainerBuilding)Engine.SelectedMapObject;
                        if (tb.GetNumberOfUnitsInTheQueue() > 0)
                        {
                            this.Panel.SelectedMapObjectInfo = "Training " + tb.GetNumberOfUnitsInTheQueue() + " unit(s).";
                        }
                        else
                        {
                            this.Panel.SelectedMapObjectInfo = "No training";
                        }
                    }
                }
                else if (Engine.SelectedMapObject is Resource)
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(this.Panel.Actions);

                    Resource resource = (Resource)Engine.SelectedMapObject;
                    string info = "";
                    switch (resource.Type)
                    {
                        case ResourceType.Food: info = "Contains: " + resource.Capacity + " food"; break;
                        case ResourceType.Wood: info = "Contains: " + resource.Capacity + " wood"; break;
                        case ResourceType.Gold: info = "Contains: " + resource.Capacity + " gold"; break;
                    }
                    this.Panel.SelectedMapObjectInfo = info;
                    if (Engine.SelectedMapObject is Animal)
                    {
                        Animal animal = (Animal)Engine.SelectedMapObject;
                        this.Panel.SelectedMapObjectHealth = "Health: " + animal.ActualHealthPoints + "/" + animal.MaximalHealthPoints;
                    }
                }
                else
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(this.Panel.Actions);
                    this.Panel.SelectedMapObjectInfo = "";
                }
            }
            this.UpdateResourcesOnView();
        }
    }
}
