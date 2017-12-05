﻿using EvoCraft.Common;
using EvoCraft.Core;
using System.Collections.Generic;
using System.ComponentModel;

namespace View
{
    public class ActionOnPanel : INotifyPropertyChanged
    {
        public ActionOnPanel(Actions type)
        {
            myType = type;
        }

        public Actions Type
        {
            get
            {
                return myType;
            }
            set
            {
                if (value != myType)
                {
                    myType = value;
                    NotifyPropertyChanged("Type");
                }
            }
        }

        public string Label
        {
            get
            {
                return myLabel;
            }
            set
            {
                if (!value.Equals(myLabel))
                {
                    myLabel = value;
                    NotifyPropertyChanged("Label");
                }
            }
        }


        // Ezekhez ne nyúlj hozzá
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public Actions myType;
        public string myLabel;
    }
}
