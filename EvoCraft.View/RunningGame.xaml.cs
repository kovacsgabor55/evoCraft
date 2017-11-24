using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Threading;
using EvoCraft.Core;
using System.IO;
using System.Media;
using System.Windows.Input;
using EvoCraft.Common;

namespace View
{
    /// <summary>
    /// Interaction logic for RunningGame.xaml
    /// </summary>
    public partial class RunningGame : Page
    {
        ViewModel viewModel;
        BackgroundWorker threadForBackEnd;
        SoundPlayer soundPlayer = new SoundPlayer();
        //private ChatBackend.ChatBackend backend;

        public RunningGame()
        {
            InitializeComponent();

            if (!global::View.Properties.Settings.Default.RunningGameSoundPlayerActive)
            {
                string path = System.IO.Directory.GetCurrentDirectory();
                path = new DirectoryInfo(path).FullName.ToString();
                soundPlayer.SoundLocation = path + "\\Sounds\\EpicBattleMusic.wav";
                soundPlayer.PlayLooping();
                global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = true;
            }
            
            //// Loading a Map
            //Map playedMap = MapLoader.LoadFromFileWithEasyFileName("first.txt");
            //// Giving the map to the engine
            //Engine.Map = playedMap;
            
            viewModel = new ViewModel(RenderHelper.Instance.Size.Height, RenderHelper.Instance.Size.Width);

            this.DataContext = viewModel;
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(100);
            timer.Tick += timerCycle;
            timer.Start();

            threadForBackEnd = new BackgroundWorker();
            threadForBackEnd.DoWork += UpdateBackEnd;
            threadForBackEnd.RunWorkerCompleted += Render;

            //backend = new ChatBackend.ChatBackend(this.DisplayMessage);
        }

        /// <summary>
        /// Renders the screen with the updated state of the objects on the map. This controls the main display of the running game.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Render(object sender, RunWorkerCompletedEventArgs e)
        {
            if (Engine.State == GameState.Running || Engine.State == GameState.PostVictory)
            {
                for (int i = 0; i < viewModel.Rows; i++)
                {
                    for (int j = 0; j < viewModel.Columns; j++)
                    {

                        // Clearing
                        viewModel.Tiles[j + i * viewModel.Columns].StoredMapObjectImage = MapObjectImage.None;
                        viewModel.Tiles[j + i * viewModel.Columns].StoredBulletImage = MapObjectImage.None; 
                        viewModel.Tiles[j + i * viewModel.Columns].Selection = SelectionImage.None;

                        // Setting Ground texture
                        viewModel.Tiles[j + i * viewModel.Columns].StoredFieldImage = GroundTextureToFieldImage.Convert(Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).Ground);

                        // Setting Visiblity
                        viewModel.Tiles[j + i * viewModel.Columns].Visibility = Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).Visibility;

                        // Setting target
                        if (Engine.SelectedMapObject != null)
                        {
                            if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(Unit)))
                            {
                                Unit unit = (Unit)Engine.SelectedMapObject;
                                if (unit.MoveTarget != null && (unit.MoveTarget.x - RenderHelper.Instance.LeftTopCorner.Row) == i && (unit.MoveTarget.y - RenderHelper.Instance.LeftTopCorner.Column) == j)
                                {
                                    viewModel.Tiles[j + i * viewModel.Columns].Selection = SelectionImage.MoveTarget;
                                }
                            }
                            if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(TrainerBuilding)))
                            {
                                TrainerBuilding b = (TrainerBuilding)Engine.SelectedMapObject;
                                if (b.SpawnTarget != null && (b.SpawnTarget.x - RenderHelper.Instance.LeftTopCorner.Row) == i && (b.SpawnTarget.y - RenderHelper.Instance.LeftTopCorner.Column) == j)
                                {
                                    viewModel.Tiles[j + i * viewModel.Columns].Selection = SelectionImage.SpawnFlag;
                                }
                            }
                        }

                        foreach (MapObject mapObj in Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).MapObjects)
                        {

                            // Set the mapobject image

                            if (mapObj.GetType() == typeof(Bullet))
                            {
                                viewModel.Tiles[j + i * viewModel.Columns].StoredBulletImage =
                                        MapObjectConverterFromBackEnd.Convert(mapObj);
                            } else
                            {
                                if (mapObj.GetType().IsSubclassOf(typeof(Animal)))
                                {
                                    Animal animal = (Animal)mapObj;
                                    if (animal.Dead || viewModel.Tiles[j + i * viewModel.Columns].Visibility == VisibilityType.Active)
                                    {
                                        viewModel.Tiles[j + i * viewModel.Columns].StoredMapObjectImage =
                                            MapObjectConverterFromBackEnd.Convert(mapObj);
                                    }
                                }
                                else
                                {
                                    viewModel.Tiles[j + i * viewModel.Columns].StoredMapObjectImage =
                                        MapObjectConverterFromBackEnd.Convert(mapObj);
                                }
                            }
                            // Setting selection

                            if (Engine.SelectedMapObject != null)
                            {
                                if (mapObj.Id == Engine.SelectedMapObject.Id && viewModel.Tiles[j + i * viewModel.Columns].Visibility == VisibilityType.Active)
                                {
                                    viewModel.Tiles[j + i * viewModel.Columns].Selection = SelectionImage.Selected;
                                }
                            }
                        }

                        // Setting building allowances
                        if (ViewModel.BuildMode)
                        {
                            if (Engine.Map.GetCellAt(i + RenderHelper.Instance.LeftTopCorner.Row, j + RenderHelper.Instance.LeftTopCorner.Column).canBlockTypeBePlaced(BlockType.BlockOtherBlock))
                            {
                                viewModel.Tiles[j + i * viewModel.Columns].AllowBuild = AllowBuildImage.Allow;
                            }
                            else
                            {
                                viewModel.Tiles[j + i * viewModel.Columns].AllowBuild = AllowBuildImage.Block;
                            }
                        }
                        else
                        {
                            viewModel.Tiles[j + i * viewModel.Columns].AllowBuild = AllowBuildImage.None;
                        }
                    }
                }
                RenderPanel();
            }
            else
            {
                viewModel.Panel.CurrentGameState = Engine.State;
                switch (viewModel.Panel.CurrentGameState)
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

        private void EndGame()
        {
            /*EndGameWindow modalWindow = new EndGameWindow(Engine.State);
            modalWindow.ShowDialog();

            if (modalWindow.response == GameState.Defeat)
            {
                MainMenu page = new MainMenu();
                this.NavigationService.Navigate(page);
            }*/
        }


        /// <summary>
        /// Updates the back-end's state. Every object's update will be called in the process.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBackEnd(object sender, DoWorkEventArgs e)
        {
            if (Engine.State == GameState.Running || Engine.State == GameState.PostVictory)
            {
                Engine.Update();
            }
        }
        
        /// <summary>
        /// The fired method by the DispatcherTimer. It will only run the updating background thread if it is done with the previous update.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerCycle(object sender, EventArgs e)
        {
            if (!threadForBackEnd.IsBusy)
            {
                threadForBackEnd.RunWorkerAsync();
            }
        }

        private void Tile_MouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Console.WriteLine(sender.GetType());
        }


        private void Page_onClose()
        {
            soundPlayer.Stop();
            global::View.Properties.Settings.Default.RunningGameSoundPlayerActive = false;
        }
        

        //// Stuffs for chat
        //public void DisplayMessage(ChatBackend.CompositeType composite)
        //{
        //    if (File.Exists("options.xml"))
        //    {
        //        XmlSerializer xs = new XmlSerializer(typeof(Information));
        //        FileStream read = new FileStream("options.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
        //        Information info = (Information)xs.Deserialize(read);

        //        string username = info.UserName;

        //        string message = composite.Message == null ? "" : composite.Message;
        //        textBoxChatPane.Text += (username + ": " + message + Environment.NewLine);
                
        //    }
        //}

        //private void textBoxEntryField_KeyDown(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Return || e.Key == Key.Enter)
        //    {
        //        backend.SendMessage(textBoxEntryField.Text);
        //        textBoxEntryField.Clear();
        //        e.Handled = true;
        //    }
        //    else 
        //    {
        //        try
        //        {
        //            textBoxEntryField.Text += (char)e.Key;
        //            e.Handled = true;
        //        }
        //        catch (Exception) { }          
                
        //    }
        //}

        private void textBoxEntryField_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Renders the panel.
        /// </summary>
        public void RenderPanel()
        {
            if (Engine.SelectedMapObject == null)
            {
                viewModel.Panel.SelectedMapObjectImage = MapObjectImage.None;
                viewModel.Panel.SelectedMapObjectLabel = "";
                viewModel.Panel.SelectedMapObjectInfo = "";
                viewModel.Panel.SelectedMapObjectHealth = "";
                if (Engine.SelectedMapObject == null)
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(viewModel.Panel.Actions);
                }
            }
            else
            {
                viewModel.Panel.SelectedMapObjectHealth = "";
                viewModel.Panel.SelectedMapObjectInfo = "";
                viewModel.Panel.SelectedMapObjectImage = MapObjectConverterFromBackEnd.Convert(Engine.SelectedMapObject);
                viewModel.Panel.SelectedMapObjectLabel = Engine.SelectedMapObject.Label;
                if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(Unit)))
                {
                    ActionConverterFromBackEnd.RefreshActionList(viewModel.Panel.Actions, (Unit)Engine.SelectedMapObject);

                    Unit unit = (Unit)Engine.SelectedMapObject;
                    viewModel.Panel.SelectedMapObjectHealth = "Health: " + unit.ActualHealthPoints + "/" + unit.MaximalHealthPoints;

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
                        viewModel.Panel.SelectedMapObjectInfo = info;
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
                        viewModel.Panel.SelectedMapObjectInfo = info;
                    }
                    if (Engine.SelectedMapObject.GetType() == typeof(Doctor))
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
                        viewModel.Panel.SelectedMapObjectInfo = info;
                    }
                }
                else if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(Building)))
                {
                    Building building = (Building)Engine.SelectedMapObject;
                    viewModel.Panel.SelectedMapObjectHealth = "Health: " + building.ActualHealthPoints + "/" + building.MaximalHealthPoints;
                    ActionConverterFromBackEnd.RefreshActionList(viewModel.Panel.Actions, (Building)Engine.SelectedMapObject);
                    if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(TrainerBuilding)))
                    {
                        TrainerBuilding tb = (TrainerBuilding)Engine.SelectedMapObject;
                        if (tb.GetNumberOfUnitsInTheQueue() > 0)
                        {
                            viewModel.Panel.SelectedMapObjectInfo = "Training " + tb.GetNumberOfUnitsInTheQueue() + " unit(s).";
                        }
                        else
                        {
                            viewModel.Panel.SelectedMapObjectInfo = "No training";
                        }
                    }
                }
                else if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(Resource)))
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(viewModel.Panel.Actions);

                    Resource resource = (Resource)Engine.SelectedMapObject;
                    string info = "";
                    switch (resource.Type)
                    {
                        case ResourceType.Food: info = "Contains: " + resource.Capacity + " food"; break;
                        case ResourceType.Wood: info = "Contains: " + resource.Capacity + " wood"; break;
                        case ResourceType.Gold: info = "Contains: " + resource.Capacity + " gold"; break;
                    }
                    viewModel.Panel.SelectedMapObjectInfo = info;
                    if (Engine.SelectedMapObject.GetType().IsSubclassOf(typeof(Animal)))
                    {
                        Animal animal = (Animal)Engine.SelectedMapObject;
                        viewModel.Panel.SelectedMapObjectHealth = "Health: " + animal.ActualHealthPoints + "/" + animal.MaximalHealthPoints;
                    }
                }
                else
                {
                    ActionConverterFromBackEnd.RefreshActionListToNone(viewModel.Panel.Actions);
                    viewModel.Panel.SelectedMapObjectInfo = "";
                }
            }
            // Updating the resources
            viewModel.Panel.Gold = Engine.ThePlayer.Resources.Gold;
            viewModel.Panel.Wood = Engine.ThePlayer.Resources.Wood;
            viewModel.Panel.Food = Engine.ThePlayer.Resources.Food;
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.W)
            {
                RenderHelper.Instance.MoveUp();
            }
            if (e.Key == Key.S)
            {
                RenderHelper.Instance.MoveDown();
            }
            if (e.Key == Key.A)
            {
                RenderHelper.Instance.MoveLeft();
            }
            if (e.Key == Key.D)
            {
                RenderHelper.Instance.MoveRight();
            }
        }

        private void theGame_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            theGame.Focus();
        }

        private void MainMenuButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Page_onClose();
            MainMenu page = new MainMenu();
            this.NavigationService.Navigate(page);
        }
    }
}
