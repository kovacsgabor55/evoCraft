using EvoCraft.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace View
{
    /// <summary>
    /// Ez az osztály tartalmazza azokat a bind-olt propertyket amik a játékban a panelen jelennek meg.
    /// </summary>
    public class Panel : INotifyPropertyChanged
    {
        public Panel()
        {
            myActions = new List<ActionOnPanel>();
            for (int i=0; i< 8; i++)
            {
                myActions.Add(new ActionOnPanel(EvoCraft.Core.Actions.None));
            }
            myFood = 100;
            myGold = 200;
            myWood = 50;
        }

        /// <summary>
        /// Ha van benne MapObject akkor az ahhoz tartozó kép
        /// </summary>
        public MapObjectImage SelectedMapObjectImage
        {
            get
            {
                return mySelectedMapObjectImage;
            }
            set
            {
                if (value != mySelectedMapObjectImage)
                {
                    mySelectedMapObjectImage = value;
                    NotifyPropertyChanged("SelectedMapObjectImage");
                }
            }
        }

        public string SelectedMapObjectLabel
        {
            get
            {
                return mySelectedMapObjectLabel;
            }
            set
            {
                if (!value.Equals(mySelectedMapObjectLabel))
                {
                    mySelectedMapObjectLabel = value;
                    NotifyPropertyChanged("SelectedMapObjectLabel");
                }
            }
        }

        public string SelectedMapObjectHealth
        {
            get
            {
                return mySelectedMapObjectHealth;
            }
            set
            {
                if (!value.Equals(mySelectedMapObjectHealth))
                {
                    mySelectedMapObjectHealth = value;
                    NotifyPropertyChanged("SelectedMapObjectHealth");
                }
            }
        }

        public string SelectedMapObjectInfo
        {
            get
            {
                return mySelectedMapObjectInfo;
            }
            set
            {
                if (!value.Equals(mySelectedMapObjectInfo))
                {
                    mySelectedMapObjectInfo = value;
                    NotifyPropertyChanged("SelectedMapObjectInfo");
                }
            }
        }

        public int Wood
        {
            get
            {
                return myWood;
            }
            set
            {
                if (value != myWood)
                {
                    myWood = value;
                    NotifyPropertyChanged("Wood");
                }
            }
        }

        public int Gold
        {
            get
            {
                return myGold;
            }
            set
            {
                if (value != myGold)
                {
                    myGold = value;
                    NotifyPropertyChanged("Gold");
                }
            }
        }

        public int Food
        {
            get
            {
                return myFood;
            }
            set
            {
                if (value != myFood)
                {
                    myFood = value;
                    NotifyPropertyChanged("Food");
                }
            }
        }

        public List<ActionOnPanel> Actions
        {
            get
            {
                return myActions;
            }
            set
            {
                myActions = value;
            }
        }

        public GameState CurrentGameState
        {
            get
            {
                return myCurrentGameState;
            }
            set
            {
                if (value != myCurrentGameState)
                {
                    myCurrentGameState = value;
                    NotifyPropertyChanged("CurrentGameState");
                }
            }
        }

        public Command<Tile> ActionClickCommand { get; private set; }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private MapObjectImage mySelectedMapObjectImage;
        private string mySelectedMapObjectLabel;
        private string mySelectedMapObjectInfo;
        private string mySelectedMapObjectHealth;
        private List<ActionOnPanel> myActions;
        private GameState myCurrentGameState;
        private int myWood;
        private int myGold;
        private int myFood;
    }
}
