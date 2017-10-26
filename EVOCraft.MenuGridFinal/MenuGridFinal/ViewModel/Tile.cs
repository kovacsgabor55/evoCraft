using EvoCraft.Core;
using System.ComponentModel;

namespace MenuGridFinal
{
    /// <summary>
    /// Ez az osztály a kirajzolás egy cellája.
    /// </summary>
    public class Tile : INotifyPropertyChanged
    {
        /// <summary>
        /// Azt tárolja hanyadik sorban van az adott tile
        /// </summary>
        public int Row
        {
            get
            {
                return row;
            }
        }
        /// <summary>
        /// Azt tárolja hanyadik oszlopban van az adott tile
        /// </summary>
        public int Col
        {
            get
            {
                return col;
            }
        }
        /// <summary>
        /// Ha van benne MapObject akkor az ahhoz tartozó kép
        /// </summary>
        public MapObjectImage StoredMapObjectImage
        {
            get
            {
                return myStoredMapObjectImage;
            }
            set
            {
                if (value != myStoredMapObjectImage)
                {
                    myStoredMapObjectImage = value;
                    NotifyPropertyChanged("StoredMapObjectImage");
                }
            }
        }
        /// <summary>
        /// Ha van benne MapObject akkor az ahhoz tartozó kép
        /// </summary>
        public MapObjectImage StoredBulletImage
        {
            get
            {
                return myStoredFylingImage;
            }
            set
            {
                if (value != myStoredFylingImage)
                {
                    myStoredFylingImage = value;
                    NotifyPropertyChanged("StoredBulletImage");
                }
            }
        }

        /// <summary>
        /// Ha van benne MapObject akkor az ahhoz tartozó kép
        /// </summary>
        public FieldImage StoredFieldImage
        {
            get
            {
                return myStoredFieldImage;
            }
            set
            {
                if (value != myStoredFieldImage)
                {
                    myStoredFieldImage = value;
                    NotifyPropertyChanged("StoredFieldImage");
                }
            }
        }



        /// <summary>
        /// Ha ki van választva, vagy ha a mozgási célpontja az, akkor ahhoz a kép.
        /// </summary>
        public SelectionImage Selection
        {
            get
            {
                return mySelection;
            }
            set
            {
                if (value != mySelection)
                {
                    mySelection = value;
                    NotifyPropertyChanged("Selection");
                }
            }
        }

        /// <summary>
        /// Ahhoz tartozó kép, hogy lehet-e oda építeni. Csak akkor jelenik meg, ha BuildMode-ban van a játék. 
        /// </summary>
        public AllowBuildImage AllowBuild
        {
            get
            {
                return myAllowBuild;
            }
            set
            {
                if (value != myAllowBuild)
                {
                    myAllowBuild = value;
                    NotifyPropertyChanged("AllowBuild");
                }
            }
        }

        /// <summary>
        /// A láthatóságot szabályzó kép
        /// </summary>
        public VisibilityType Visibility
        {
            get
            {
                return myVisibility;
            }
            set
            {
                if (value != myVisibility)
                {
                    myVisibility = value;
                    NotifyPropertyChanged("Visibility");
                }
            }
        }

        public Tile(int row, int col)
        {
            this.row = row;
            this.col = col;
            myStoredMapObjectImage = MapObjectImage.None;
            myAllowBuild = AllowBuildImage.None;
            mySelection = SelectionImage.None;
            myStoredFieldImage = FieldImage.Grass1;
            myStoredFylingImage = MapObjectImage.None;
        }

        // Ezekhez ne nyúlj hozzá
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        
        private MapObjectImage myStoredMapObjectImage;
        private MapObjectImage myStoredFylingImage;
        private FieldImage myStoredFieldImage;
        private SelectionImage mySelection;
        private AllowBuildImage myAllowBuild;
        private VisibilityType myVisibility;
        private int row;
        private int col;
    }
}
