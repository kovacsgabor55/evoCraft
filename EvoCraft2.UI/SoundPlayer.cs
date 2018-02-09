using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Media;
using System.Windows.Media;


namespace UserControlUnits
{
    public class SoundPlayer
    {
        public enum SoundState
        {
            None,
            Battle,
            WoodCut,
            Mining,
            Building,
            AnimalDeath,
            UnitDeath,
            BuildingFinish,
            TrainingFinish,
            Heal,

        }
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

        static SoundPlayer()
        {
            path = new DirectoryInfo(path).FullName.ToString() + "\\Sounds\\";
        }

        public static void PlaySelectionSound(string snd)
        {
            int tmp = rnd.Next(0, 5);

            Play(path + snd);


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



        /*public static void StopMenuMusic()
        {
            menuSoundPlayer.Stop();
        }*/

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
