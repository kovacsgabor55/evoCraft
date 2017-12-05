using EvoCraft.Core;
using System.Collections.Generic;
using System;
using EvoCraft.Common;

namespace View
{
    /// <summary>
    /// Helps the conversion from the back end types to the front end rendering types
    /// </summary>
    public class ActionConverterFromBackEnd
    {
        /// <summary>
        /// Resresh the Panel's Action list based on a Unit's possible Actions.
        /// </summary>
        /// <param name="actionList"></param>
        /// <param name="unit"></param>
        public static void RefreshActionList(List<ActionOnPanel> actionList, PlayerControlled playerCtrl)
        {
            int i = 0;
            
            foreach (ActionOnPanel act in actionList)
            {
                if (playerCtrl.PossibleActions != null && playerCtrl.PossibleActions.Count != 0)
                {
                    if (i < playerCtrl.PossibleActions.Count)
                    {
                        act.Type = playerCtrl.PossibleActions[i];
                        act.Label = GetLabelBasedOnActionType(act.Type);
                        i++;
                    }
                    else
                    {
                        act.Type = Actions.None;
                        act.Label = "";
                    }
                }
                else
                {
                    act.Type = Actions.None;
                    act.Label = "";
                }
            }
        }

        /// <summary>
        /// Resresh the Panel's Action list back to nothing. This is best used for other selectable MapObjects, like Animals.
        /// </summary>
        public static void RefreshActionListToNone(List<ActionOnPanel> actionList)
        {
            foreach (ActionOnPanel act in actionList)
            {
                act.Type = Actions.None;
                act.Label = "";
            }
        }


        public static string GetLabelBasedOnActionType(Actions type)
        {
            switch (type)
            {
                case Actions.None: return "";
                case Actions.TrainWorker: return "Gold: " + Worker.GoldCost + "\nFood: " + Worker.FoodCost + "\nWood: " + Worker.WoodCost;
                case Actions.TrainSoldier: return "Gold: " + Soldier.GoldCost + "\nFood: " + Soldier.FoodCost + "\nWood: " + Soldier.WoodCost;
                case Actions.TrainPozsiHero: return "Gold: " + Hero.GoldCost + "\nFood: " + Hero.FoodCost + "\nWood: " + Hero.WoodCost;
                case Actions.TrainDoctor: return "Gold: " + Doctor.GoldCost + "\nFood: " + Doctor.FoodCost + "\nWood: " + Doctor.WoodCost;
                case Actions.TrainGunMan: return "Gold: " + GunMan.GoldCost + "\nFood: " + GunMan.FoodCost + "\nWood: " + GunMan.WoodCost;
                case Actions.BuildBarracs: return "Gold: " + Barracks.GoldCost + "\nFood: " + Barracks.FoodCost + "\nWood: " + Barracks.WoodCost;
                case Actions.BuildMainHall: return "Gold: " + MainHall.GoldCost + "\nFood: " + MainHall.FoodCost + "\nWood: " + MainHall.WoodCost;
                case Actions.BuildWall: return "Gold: " + Wall.GoldCost + "\nFood: " + Wall.FoodCost + "\nWood: " + Wall.WoodCost;
                case Actions.BuildTower: return "Gold: " + Tower.GoldCost + "\nFood: " + Tower.FoodCost + "\nWood: " + Tower.WoodCost;
                case Actions.BuildMedicalTent: return "Gold: " + MedicalTent.GoldCost + "\nFood: " + MedicalTent.FoodCost + "\nWood: " + MedicalTent.WoodCost;
                case Actions.BuildFarm: return "Gold: " + FarmBuilding.GoldCost + "\nFood: " + FarmBuilding.FoodCost + "\nWood: " + FarmBuilding.WoodCost;
                default: return "";
            }
        }

    }
}
