using EvoCraft.Common;
using EvoCraft.Common.MapObjects;
using EvoCraft.Common.MapObjects.PlayerControlled.Buildings;
using EvoCraft.Common.MapObjects.PlayerControlled.Units;
using EvoCraft.Common.MapObjects.Resources;
using EvoCraft.Common.MapObjects.Resources.Animals;
using EvoCraft.Core;
using System;
using System.ComponentModel;
using System.IO;
using System.Media;
using System.Windows.Media;

namespace View
{
    public class Sounds
    {
        static MediaPlayer soundPlayer = new MediaPlayer();
        static SoundPlayer menuSoundPlayer = new SoundPlayer();
        static string path = Environment.CurrentDirectory;
        static Random rnd = new Random();

        //The balance between the left and right speaker volumes.
        //The ratio of volume across the left and right speakers in a range between -1
        //and 1. The default is 0.
        public static int balance = 0;

        //The media's volume represented on a linear scale between 0 and 1. The default is 0.5.
        public static int volume = 1;
       
        static Sounds()
        {
            path = new DirectoryInfo(path).FullName.ToString() + "\\Sounds\\";
        }

        public static void PlaySelectionSound(MapObject mo)
        {
            if (mo != null)
            {
                if (mo is Worker)
                {
                    int tmp = rnd.Next(0, 5);

                    switch (tmp)
                    {
                        case 0: Play(path + "Soldier.wav"); break;
                        case 1: Play(path + "TheEndIsNear.wav"); break;
                        case 2: Play(path + "ShibaInuBark.wav"); break;
                        case 3: Play(path + "Engineering.wav"); break;
                        case 4: Play(path + "AtLeastIHaveJob.wav"); break;
                    }
                }
                else if (mo is Soldier)
                {
                    int tmp = rnd.Next(0,5);

                    switch (tmp)
                    {
                        case 0: Play(path + "Soldier.wav"); break;
                        case 1: Play(path + "TheEndIsNear.wav"); break;
                        case 2: Play(path + "ShibaInuBark.wav"); break;
                        case 3: Play(path + "ForMotherRussia.wav"); break;
                        case 4: Play(path + "AtLeastIHaveJob.wav"); break;
                    }
                }
                else if (mo is Doctor)
                {
                    Play(path + "Medic.wav");
                }

                else if (mo is Hero)
                {
                    Play(path + "SpaceUnicorn.wav");
                }

                else if (mo is MainHall)
                {
                    Play(path + "MainHall.wav");
                }
                else if (mo is Barracks)
                {
                    Play(path + "ShortReveille.wav");
                }
                else if (mo is Mine)
                {
                    Play(path + "Mine.wav");
                }
                else if (mo is Sloth)
                {
                    Animal animal = (Animal)mo;
                    if (animal.Dead)
                    {
                        //Play(path + "DeadSloth.wav");
                    }
                    else
                    {
                        Play(path + "Sloth.wav");
                    }
                }
                else if (mo is Chupacabra)
                {
                    Animal animal = (Animal)mo;
                    if (animal.Dead)
                    {
                        //Play(path + "DeadChupy.wav");
                    }
                    else
                    {
                        Play(path + "Chupy.wav");
                    }
                }
                else if (mo is Llama)
                {
                    Animal animal = (Animal)mo;
                    if (animal.Dead)
                    {
                        //Play(path + "DeadLlama.wav");
                    }
                    else
                    {
                        Play(path + "Llama.wav");
                    }
                }
                else if (mo is Boss)
                {
                    Animal animal = (Animal)mo;
                    if (animal.Dead)
                    {
                        //Play(path + "DeadGrumpyCat.wav");
                    }
                    else
                    {
                        Play(path + "GrumpyCat.wav");
                    }
                }
            }
        }

        public static void PlayOrderGivenSound(MapObject mo)
        {
            if (mo != null)
            {
                if (mo is Worker)
                {
                    int tmp = rnd.Next(0, 6);

                    switch (tmp)
                    {
                        case 0: Play(path + "LetsDoIt.wav"); break;
                        case 1: Play(path + "ForMotherRussia.wav"); break;
                        case 2: Play(path + "YesCommander.wav"); break;
                        case 3: Play(path + "YesSir.wav"); break;
                        case 4: Play(path + "Movin.wav"); break;
                        case 5: Play(path + "ChangingPosition.wav"); break;
                    }
                }
                else if (mo is Soldier)
                {
                    int tmp = rnd.Next(0, 6);

                    switch (tmp)
                    {
                        case 0: Play(path + "LetsDoIt.wav"); break;
                        case 1: Play(path + "TheEndIsNear.wav"); break;
                        case 2: Play(path + "ForMotherRussia.wav"); break;
                        case 3: Play(path + "YesCommander.wav"); break;
                        case 4: Play(path + "YesSir.wav"); break;
                        case 5: Play(path + "ChangingPosition.wav"); break;
                    }
                }
                else if (mo is MainHall)
                {
                    int tmp = rnd.Next(0, 3);

                    switch (tmp)
                    {
                        case 0: Play(path + "NewRallyPoint.wav"); break;
                        case 1: Play(path + "LocationConfirmed.wav"); break;
                        case 2: Play(path + "ChangingPosition.wav"); break;
                    }
                }
                else if (mo is Barracks)
                {
                    int tmp = rnd.Next(0, 3);

                    switch (tmp)
                    {
                        case 0: Play(path + "NewRallyPoint.wav"); break;
                        case 1: Play(path + "LocationConfirmed.wav"); break;
                        case 2: Play(path + "ChangingPosition.wav"); break;
                    }
                }
            }
        }

        public static void PlayVictorySong()
        {
            Play(path + "Victory.wav");
        }

        public static void PlayDefeatSong()
        {
            Play(path + "Defeat.wav");
        }

        public static void PlaySoundComingFromBackEnd(SoundState soundState)
        {
            switch (soundState)
            {
                case SoundState.Battle: Play(path + "Attacking.wav"); break;
                case SoundState.Heal: Play(path + "Medic.wav"); break;
                case SoundState.TrainingFinish: Play(path + "UnitReady.wav"); break;
                case SoundState.UnitDeath: Play(path + "UnitLost.wav"); break;
                case SoundState.Building: Play(path + "BuildingInProgress.wav"); break;
            }
        }

        public static void Startup()
        {
            Play(path + "Startup.wav");
        }

        public static void ShutDown()
        {
            Play(path + "Shutdown.wav");
        }

        public static void StartMenuMusic()
        {
            if (!global::View.Properties.Settings.Default.MenuSoundPlayerActive)
            {
                menuSoundPlayer.SoundLocation = path + "EpicSongForMenu.wav";
                menuSoundPlayer.PlayLooping();
                global::View.Properties.Settings.Default.MenuSoundPlayerActive = true;
            }
        }

        public static void StopMenuMusic()
        {
            menuSoundPlayer.Stop();
            global::View.Properties.Settings.Default.MenuSoundPlayerActive = false;
        }

        public static void InGameMusic()
        {
            Play(path + "EpicBattleMusic.wav");
        }

        static void Play(string audioPath)
        {
            soundPlayer.Open(new System.Uri(audioPath));
            soundPlayer.Balance = balance;
            soundPlayer.Volume = volume;
            soundPlayer.Play();
        }
    }
}
