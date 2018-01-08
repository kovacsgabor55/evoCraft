using EvoCraft.Common;
using EvoCraft.Common.MapObjects.PlayerControlled;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using System.Collections.Generic;

namespace EvoCraft.Core.MapObjects.PlayerControlled.Buildings
{
    public class BarracksExtension : Barracks
    {
        public BarracksExtension(int PlayerId) : this(PlayerId, true) { }
        public BarracksExtension(int PlayerId, bool UnderConstruction)
            : base(PlayerId, UnderConstruction)
        { }

        public override void FinishBuilding()
        {
            this.ActualHealthPoints = this.MaximalHealthPoints;
            this.PossibleActions = new List<Actions> { Actions.TrainSoldier, Actions.TrainPozsiHero, Actions.TrainGunMan, Actions.Cancel };
        }
    }
}
